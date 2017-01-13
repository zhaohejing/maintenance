using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.AppService.Event.Dtos;
using MyCompanyName.AbpZeroTemplate.AppService.Event.Inputs;
using MyCompanyName.AbpZeroTemplate.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.AppService.Event {
   public  interface IEventAppService:IApplicationService {
        Task<PagedResultOutput<FaultInfoDto>> GetDevicesFaultInfoList(GetFaultInput input);
        Task InsertFaultInfo(FaultInfoDto dto);
        Task UpdateFaultInfo(UpdateFaultInput input);
        Task<PagedResultOutput<FaultInfoDto>> GetDevicesRepairList(GetFaultInput input);
        PagedResultOutput<EnginerInfo> GetEnginerRepairList(GetFaultInput input);
        SolveDto GetSolveInfo();
    }
}
