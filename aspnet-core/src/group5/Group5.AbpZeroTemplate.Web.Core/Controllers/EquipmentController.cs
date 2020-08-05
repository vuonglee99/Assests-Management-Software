using Abp.AspNetCore.Mvc.Controllers;
using Abp.Dependency;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_Equipments;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_Equipments.Dto;
using GSoft.AbpZeroTemplate.Helpers.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class EquipmentController : AbpController
    {
        private readonly IEquipmentAppService equipmentAppService;

        public EquipmentController(IEquipmentAppService equipmentAppService)
        {
            this.equipmentAppService = equipmentAppService;
        }

        [HttpPost]
        public List<CM_EQUIP_DTO> WORK_ORDER_Search([FromBody] CM_EQUIP_DTO filterInput)
        {
            return equipmentAppService.WORK_ORDER_Search(filterInput);
        }
        [HttpPost]
        public List<dynamic> CM_Equipment_Insert([FromBody] CM_EQUIP_DTO input)
        {
            return equipmentAppService.CM_Equipment_Insert(input);
        }

        [HttpPost]
        public String CM_Equipment_GetCode_WO(string WO_NAME)
        {
            return equipmentAppService.CM_Equipment_GetCode_WO(WO_NAME);
        }

    }
}
