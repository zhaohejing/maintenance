using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using MyCompanyName.AbpZeroTemplate;
using MyCompanyName.AbpZeroTemplate.AppService.Konwledge;
using MyCompanyName.AbpZeroTemplate.AppService.Konwledge.Dtos;
using MyCompanyName.AbpZeroTemplate.AppService.Konwledge.Inputs;
using MyCompanyName.AbpZeroTemplate.Authorization;
using MyCompanyName.AbpZeroTemplate.EntityModel;

namespace Maintenance.AppService.Konwledge {
    public class KnowledgeAppService : AbpZeroTemplateAppServiceBase, IKnowledgeAppService {
        private readonly IRepository<KnowledgeBase> _knowledgeRepository;
        private readonly IRepository<KnowledgeComment> _commentRepository;
        public KnowledgeAppService(IRepository<KnowledgeBase> knowledgeRepository, IRepository<KnowledgeComment> commentRepository) {
            _knowledgeRepository = knowledgeRepository;
            _commentRepository = commentRepository;
        }

        public async Task<PagedResultOutput<KnowledgeDto>> GetDevicesFaultInfoList(GetKnowledgeInput input) {
            var roles = await UserManager.GetRolesAsync((long)AbpSession.UserId);
            var flag = roles.Any(c => c.Contains("Admin"));

            var e = (from c in _knowledgeRepository.GetAll()
                    join d in _commentRepository.GetAll().WhereIf(!flag, c => c.IsActive)
on c.Id equals d.KnowledgeId into dd
                    from ee in dd.DefaultIfEmpty()
                    select new { c, dd }).OrderByDescending(f=>f.c.CreationTime).ToList();

            //var query = _knowledgeRepository.GetAll().Include(c => c.KnowledgeComment).WhereIf(!input.Filter.IsNullOrWhiteSpace(),
            //    c => c.Title.Contains(input.Filter) || c.Description.Contains(input.Filter))
            //    ;
         
            var temp = new List<KnowledgeDto>();
            foreach (var item in e) {
                var mo = 
                new KnowledgeDto() {
                    Id=item.c.Id,
                    Description = item.c.Description,
                    KnowledgeState = item.c.KnowledgeState,
                    KnowledgeType = item.c.KnowledgeType,
                    Title = item.c.Title
                };
                var eee = new List<CommentDto>();
                foreach (var tt in item.dd) {
                    if (tt.KnowledgeId==mo.Id) {
                        eee.Add(new CommentDto() {
                            Id=tt.Id,
                            Comment = tt.Comment,
                            IsActive = tt.IsActive,
                            ProfileId = tt.ProfileId,
                            UserName = tt.User.UserName
                        });
                    }
                }
                mo.KnowledgeComment = eee;
                if (!temp.Any(c=>c.Id==mo.Id)) {
                    temp.Add(mo);
                }
            }
            var faultCount = temp.Count;
          
            return new PagedResultOutput<KnowledgeDto>(faultCount, temp);
        }
        [AbpAuthorize(AppPermissions.Pages_KnowledgeBase_Maintenance_Create)]

        public async Task InsertKnowledge(KnowledgeDto model) {
            var dto = model.MapTo<KnowledgeBase>();
            dto.KnowledgeType = KnowledgeType.ComputerKnowledge;
            dto.KnowledgeState = KnowledgeState.NotSolved;
            await _knowledgeRepository.InsertAsync(dto);
        }
        [AbpAuthorize(AppPermissions.Pages_KnowledgeBase_Maintenance_CreateComment)]

        public async Task InsertComment(CommentInput model) {
            var dto = new KnowledgeComment() {
                Comment = model.Comment,
                IsActive = false,
                KnowledgeId = model.KnowId,
                UserId = (long)AbpSession.UserId,
                ProfileId = model.ProfileId
            };
            await _commentRepository.InsertAsync(dto);
        }
        [AbpAuthorize(AppPermissions.Pages_KnowledgeBase_Maintenance_Delete)]

        public async Task DeleteKnowAsync(EntityRequestInput input) {
            await _knowledgeRepository.DeleteAsync(input.Id);
        }

        public async Task SovleKnowAsync(EntityRequestInput input) {
            var dto = await _knowledgeRepository.FirstOrDefaultAsync(c => c.Id == input.Id);
            if (dto == null) {
                throw new UserFriendlyException("当前知识不存在");
            }
            dto.KnowledgeState = KnowledgeState.HadSolved;
        }

        [AbpAuthorize(AppPermissions.Pages_KnowledgeBase_Maintenance_DeleteComment)]

        public async Task DeleteCommentAsync(EntityRequestInput input) {
            await _commentRepository.DeleteAsync(input.Id);
        }
        public async Task PassCommentAsync(EntityRequestInput input) {
            var dto = await _commentRepository.FirstOrDefaultAsync(c => c.Id == input.Id);
            if (dto == null) {
                throw new UserFriendlyException("当前评论不存在");
            }
            dto.IsActive = true;
        }

    }
}
