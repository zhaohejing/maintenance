using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.AppService.Konwledge.Dtos;
using MyCompanyName.AbpZeroTemplate.AppService.Konwledge.Inputs;

namespace Maintenance.AppService.Konwledge {
    public interface IKnowledgeAppService : IApplicationService {
        Task<PagedResultOutput<KnowledgeDto>> GetDevicesFaultInfoList(GetKnowledgeInput input);
        Task InsertKnowledge(KnowledgeDto model);
        Task InsertComment(CommentInput model);
        Task DeleteKnowAsync(EntityRequestInput input);
        Task SovleKnowAsync(EntityRequestInput input);


        Task DeleteCommentAsync(EntityRequestInput input);
        Task PassCommentAsync(EntityRequestInput input);

    }
}
