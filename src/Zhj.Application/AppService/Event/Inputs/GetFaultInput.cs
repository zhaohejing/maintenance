using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.AppService.Event.Inputs {
   
    public class GetFaultInput : PagedInputDto {
        public string Filter { get; set; }
      
    }
    public class UpdateFaultInput : IInputDto {
        public int? Id { get; set; }
        public FaultType FaultType { get; set; }
    }
}
