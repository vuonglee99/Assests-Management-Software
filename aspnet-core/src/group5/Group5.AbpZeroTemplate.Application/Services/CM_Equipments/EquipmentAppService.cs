using Abp.Application.Services;
using Abp.Authorization;
using Group5.AbpZeroTemplate.Application;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_Equipments.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using GSoft.AbpZeroTemplate.Helpers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5.AbpZeroTemplate.Web.Core.Services.CM_Equipments
{

    public interface IEquipmentAppService : IApplicationService
    {
        List<CM_EQUIP_DTO> WORK_ORDER_Search(CM_EQUIP_DTO filterInput);
        List<dynamic> CM_Equipment_Insert(CM_EQUIP_DTO input);
        string CM_Equipment_GetCode_WO(string WO_NAME);

    }


    public class EquipmentAppService : BaseService, IEquipmentAppService
    {
        public List<CM_EQUIP_DTO> WORK_ORDER_Search(CM_EQUIP_DTO filterInput)
        {
            return procedureHelper.GetData<CM_EQUIP_DTO>("WORK_ORDER_Search", filterInput);
        }

        [AbpAuthorize(Group5PermissionsConst.Pages_Administration_Equipment_GetCode)]
        public string CM_Equipment_GetCode_WO(string WO_NAME)
        {
            return procedureHelper.GetData<dynamic>("WORK_ORDER_GetCode_WorkOder", new
            {
                WO_NAME = WO_NAME
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group5PermissionsConst.Pages_Administration_Equipment_Insert)]
        public List<dynamic> CM_Equipment_Insert(CM_EQUIP_DTO input)
        {
            input.MAKER_ID = AbpSession.UserId.ToString();
            return procedureHelper.GetData<dynamic>("WORK_ORDER_Insert_WorkOrder", input);
        }


    }
}
