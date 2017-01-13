using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.AppService.Konwledge.Dtos {
  
    [AutoMap(typeof(KnowledgeBase))]
    public class KnowledgeDto : EntityDto {
        /// <summary>
        ///知识库标题
        /// </summary>
        public string Title { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// 知识库 知识类型
        /// </summary>
        public KnowledgeType KnowledgeType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public KnowledgeState KnowledgeState { get; set; }
        public virtual ICollection<CommentDto> KnowledgeComment { get; set; }
    }
    [AutoMap(typeof(KnowledgeComment))]

    public class CommentDto : EntityDto {
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        /// <summary>
        /// 附件id
        /// </summary>
        public Guid? ProfileId { get; set; }
    }

}
