using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using MyCompanyName.AbpZeroTemplate.EntityModel;

namespace MyCompanyName.AbpZeroTemplate.Storage
{
    [Table("AppBinaryObjects")]
    public class BinaryObject : Entity<Guid>
    {
        [Required]
        public string Url { get; set; }
        /// <summary>
        /// 资源id
        /// </summary>
        public AttachType BinaryType { get; set; }

        public BinaryObject()
        {
            Id = Guid.NewGuid();
        }

        public BinaryObject(string url)
            : this()
        {
            Url = url;
        }
        public BinaryObject(string url,AttachType type)
         : this() {
            Url = url; BinaryType = type;
        }
        public BinaryObject(Guid id, string url,AttachType type) {
            Id = id;
            Url = url;
            BinaryType = type;
        }
    }
}
