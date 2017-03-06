using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Timing;
using Abp.UI;
using MyCompanyName.AbpZeroTemplate.AppService.Event;
using MyCompanyName.AbpZeroTemplate.AppService.Event.Dtos;
using MyCompanyName.AbpZeroTemplate.AppService.Event.Inputs;
using MyCompanyName.AbpZeroTemplate.Authorization;
using MyCompanyName.AbpZeroTemplate.EntityModel;
using MyCompanyName.AbpZeroTemplate.Storage;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.ApplicationService.Event {
    [AbpAuthorize(AppPermissions.Pages_FaultRelease_EventRelease)]

    public class EventAppService : AbpZeroTemplateAppServiceBase, IEventAppService {
        private readonly IRepository<DeviceFaultInfo> _faultRepository;
        private readonly IRepository<DeviceAttach> _attachRepository;
        private readonly ISqlExecuter _sqlExecuter;
        public EventAppService(IRepository<DeviceFaultInfo> faultRepository,
            IRepository<DeviceAttach> attachRepository, ISqlExecuter sqlExecuter) {
            _faultRepository = faultRepository;
            _attachRepository = attachRepository;
            _sqlExecuter = sqlExecuter;
        }


        public async Task<PagedResultOutput<FaultInfoDto>> GetDevicesFaultInfoList(GetFaultInput input) {
            var query = _faultRepository.GetAll().WhereIf(input.Filter.IsNullOrWhiteSpace(),
                c => c.DeviceAlias.Contains(input.Filter) || c.DeviceDescription.Contains(input.Filter) || c.DeviceName.Contains(input.Filter));
            var faultCount = await query.CountAsync();
            var list = await query.OrderBy(c => c.DeviceName)
                .PageBy(input)
                .ToListAsync();

            var userListDtos = list.MapTo<List<FaultInfoDto>>();

            return new PagedResultOutput<FaultInfoDto>(faultCount, userListDtos);
        }

        public async Task UpdateFaultInfo(UpdateFaultInput input) {
            if (!input.Id.HasValue) {
                throw new UserFriendlyException(L("ThreeIsNoSelectedModel"));
            }
            var model = await _faultRepository.FirstOrDefaultAsync(c => c.Id == input.Id);
            model.FaultType = input.FaultType;
            model.EnginerId = AbpSession.UserId;
        }

        [AbpAuthorize(AppPermissions.Pages_FaultRelease_EventRelease_Release)]

        public async Task InsertFaultInfo(FaultInfoDto dto) {
            var guidlist = dto.ProfileList;
            var model = dto.MapTo<DeviceFaultInfo>();
            model.FaultType = FaultType.NotSolved;
            if (model.LastRepairTime.HasValue)
            {
             model.LastRepairTime    = Clock.Normalize((DateTime)model.LastRepairTime);
            }


            var id = await _faultRepository.InsertAndGetIdAsync(model);
            var list = guidlist.Select(c => new DeviceAttach() { DeviceId = id, ProfileId = c, FileType = AttachType.Image });
            foreach (var item in list) {
                await _attachRepository.InsertAsync(item);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        public async Task<PagedResultOutput<FaultInfoDto>> GetDevicesRepairList(GetFaultInput input) {
            var query = _faultRepository.GetAll().WhereIf(input.Filter.IsNullOrWhiteSpace(),
                c => c.DeviceAlias.Contains(input.Filter) || c.DeviceDescription.Contains(input.Filter)
                || c.DeviceName.Contains(input.Filter));

            var ddd = (from d in query group d by d.DeviceSIgnkey into g select new { g.Key, Count = g.Count() });

            var tt = from c in ddd
                     join d in query
        on c.Key equals d.DeviceSIgnkey into h from d in h.DefaultIfEmpty()
                     select new { Count= c.Count, d };

            var faultCount = await tt.CountAsync();
            var list = await tt.OrderBy(c => c.d.DeviceName)
                .PageBy(input)
                .ToListAsync();

            var userListDtos = list.Distinct(c=>c.d.DeviceSIgnkey).Select(c => new FaultInfoDto() {
                LastRepairTime = c.d.LastRepairTime,
                DeviceAlias = c.d.DeviceAlias,
                DeviceAssetNo = c.d.DeviceAssetNo,
                DeviceDescription = c.d.DeviceDescription,
                Count = c.Count,
                DeviceHead = c.d.DeviceHead,
                DeviceLocation = c.d.DeviceLocation,
                DeviceManufacture = c.d.DeviceManufacture,
                DeviceName = c.d.DeviceName,
                DeviceSIgnkey = c.d.DeviceSIgnkey,
                DeviceType = c.d.DeviceType,
                FaultDescription = c.d.FaultDescription,
                FaultType = c.d.FaultType,
                Id = c.d.Id
            }).ToList();//    list.MapTo<List<FaultInfoDto>>();

            return new PagedResultOutput<FaultInfoDto>(faultCount, userListDtos);
        }

        public PagedResultOutput<EnginerInfo> GetEnginerRepairList(GetFaultInput input) {
            var list = _sqlExecuter.GetEngierList();
            var count = list.Count;
            list = list.OrderByDescending(c => c.FullSolve).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultOutput<EnginerInfo>(count, list);
        }
        public  SolveDto GetSolveInfo() {
            DateTime start = DateTime.Today;
            DateTime End = DateTime.Today.AddDays(86399F / 86400);
            var query = _faultRepository.GetAll();
            var a = query.Where(c => c.LastRepairTime < start).OrderBy(c=>c.LastRepairTime).Take(5);
            var b = query.Where(c => c.LastRepairTime > start && c.LastRepairTime <= End).OrderBy(c => c.LastRepairTime).Take(5);
            SolveDto dto = new SolveDto();
            dto.last = a.MapTo<List<FaultInfoDto>>();
            dto.mast = b.MapTo<List<FaultInfoDto>>();
            return dto;
        }
    }

    public static class CommonFunction {
        /// <summary>
        /// 扩展Distinct方法
        /// </summary>
        /// <typeparam name=\"T\">源类型</typeparam>
        /// <typeparam name=\"V\">委托返回类型（根据V类型，排除重复项）</typeparam>
        /// <param name=\"source\">扩展源</param>
        /// <param name=\"keySelector\">委托（执行操作）</param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector) {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }
    }
    public class CommonEqualityComparer<T, V> : IEqualityComparer<T> {
        private Func<T, V> keySelector;
        private IEqualityComparer<V> comparer;

        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer) {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public CommonEqualityComparer(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default) { }

        public bool Equals(T x, T y) {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj) {
            return comparer.GetHashCode(keySelector(obj));
        }
    }
}
