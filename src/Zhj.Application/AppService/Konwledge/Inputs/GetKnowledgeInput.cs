using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.AppService.Konwledge.Inputs {
 
    public class GetKnowledgeInput : PagedInputDto {
        public string Filter { get; set; }

    }
    public class CommentInput : IInputDto {
        public int KnowId { get; set; }
        public string Comment { get; set; }
        public Guid ProfileId { get; set; }
    }
}
