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
    /// 设备信息错误信息
    /// </summary>
    [Table("DeviceFaultInfo")]
   public class DeviceFaultInfo: CreationAuditedEntity, ISoftDelete {
        public DeviceFaultInfo() {
            FaultType = FaultType.NotSolved;
        }
    
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        public FaultType FaultType { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        [Required,MaxLength(50)]
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 设备资产号
        /// </summary>
        [Required,MaxLength(50)]

        public string DeviceAssetNo { get; set; }
        /// <summary>
        /// 设备别名
        /// </summary>
        public string DeviceAlias { get; set; }
        /// <summary>
        /// 设备厂商
        /// </summary>
        public string DeviceManufacture { get; set; }
        /// <summary>
        /// 设备负责人
        /// </summary>
        public string DeviceHead { get; set; }
     
        /// <summary>
        /// 设备标识
        /// </summary>
        public string DeviceSIgnkey { get; set; }
        /// <summary>
        /// 设备位置
        /// </summary>
        public string DeviceLocation { get; set; }
        /// <summary>
        /// 最迟修复时间
        /// </summary>
        public DateTime? LastRepairTime { get; set; }
        /// <summary>
        /// 修复人id
        /// </summary>
        public long? EnginerId { get; set; }
        public virtual User Enginer { get; set; }
        /// <summary>
        /// 设备描述
        /// </summary>
        [MaxLength(500)]

        public string DeviceDescription { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        [MaxLength(500)]
        public string FaultDescription { get; set; }
        [ForeignKey("DeviceId")]

        public virtual ICollection<DeviceAttach> AttachList { get; set; }

    }
    [Table("DeviceAttach")]
    public class DeviceAttach: CreationAuditedEntity, ISoftDelete {
        [Required]
        public int DeviceId { get; set; }
        [Required]

        public Guid ProfileId { get; set; }
        public AttachType FileType { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class TempModel {
        /// <summary>
        /// 设备名称
        /// </summary>
    
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 设备资产号
        /// </summary>
    

        public string DeviceAssetNo { get; set; }
        /// <summary>
        /// 设备别名
        /// </summary>
        public int DeviceAlias { get; set; }
        /// <summary>
        /// 设备厂商
        /// </summary>
        public string DeviceManufacture { get; set; }
        /// <summary>
        /// 设备负责人
        /// </summary>
        public string DeviceHead { get; set; }
        /// <summary>
        /// 设备标识
        /// </summary>
        public string DeviceSIgnkey { get; set; }
        /// <summary>
        /// 设备位置
        /// </summary>
        public string DeviceLocation { get; set; }
        public DateTime RepairTime { get; set; }
        /// <summary>
        /// 设备描述
        /// </summary>

        public string DeviceDescription { get; set; }
    
    }

    public class EnginerInfo {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int FullSolve { get; set; }
        public int Solving { get; set; }
        public int HadSolve { get; set; }
    }
}
