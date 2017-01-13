using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.EntityModel {
    /// <summary>
    /// 知识库
    /// </summary>
    [Table("KnowledgeBase")]
    public class KnowledgeBase : CreationAuditedEntity, ISoftDelete {
        [Required,MaxLength(100)]
        /// <summary>
        ///知识库标题
        /// </summary>
        public string Title { get; set; }
        [ MaxLength(500)]

        public string Description { get; set; }
        /// <summary>
        /// 知识库 知识类型
        /// </summary>
        public KnowledgeType KnowledgeType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public KnowledgeState KnowledgeState { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        [ForeignKey("KnowledgeId")]
        public virtual ICollection<KnowledgeComment> KnowledgeComment { get; set; }

    }
    [Table("KnowledgeComment")]
    public class KnowledgeComment : CreationAuditedEntity, ISoftDelete, IPassivable {
        public int KnowledgeId { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public string Comment { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 附件id
        /// </summary>
        public Guid? ProfileId { get; set; }

        public bool IsActive { get; set; }
    }
}
