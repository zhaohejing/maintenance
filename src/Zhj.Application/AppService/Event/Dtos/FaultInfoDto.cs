using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyCompanyName.AbpZeroTemplate.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.AppService.Event.Dtos {
    [AutoMap(typeof(DeviceFaultInfo))]
    public class FaultInfoDto : EntityDto {
        public DateTime? LastRepairTime { get; set; }
        public FaultType FaultType { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }
        public long? EnginerId { get; set; }
        /// <summary>
        /// 设备资产号
        /// </summary>
        public int Count { get; set; }

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
        /// 设备描述
        /// </summary>

        public string DeviceDescription { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        public string FaultDescription { get; set; }

        public IList<Guid> ProfileList { get; set; }
    }


    public class SolveDto {
        public List<FaultInfoDto> last { get; set; }
        public List<FaultInfoDto> mast { get; set; }
    }
}
