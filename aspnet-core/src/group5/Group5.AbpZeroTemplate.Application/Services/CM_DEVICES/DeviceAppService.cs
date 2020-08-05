using Abp.Application.Services;
using Abp.Authorization;
using Abp.Extensions;
using Group5.AbpZeroTemplate.Application;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_DEVICES.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using GSoft.AbpZeroTemplate.Helpers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5.AbpZeroTemplate.Web.Core.Services.CM_DEVICES
{
    public interface IDeviceAppService : IApplicationService
    {
        List<CM_DEV_DTO> DEVICE_Search(string index,CM_DEV_DTO filterInput);
        List<dynamic> DEVICE_Get_All_Branch();
        List<dynamic> DEVICE_Insert(CM_DEV_DTO input);
        List<dynamic> DEVICE_Update(CM_DEV_DTO input);
        dynamic DEVICE_Delete(string listDepId);
        List<string> DEVICE_Get_All_Name();
        List<CM_DEV_DTO> G2_DEVICE_GETALL();
        dynamic DEVICE_Approve(int check,string devID);
        string Get_Current_ID();
    }


    public class DeviceAppService : BaseService, IDeviceAppService
    {
        public string Get_Current_ID()
        {
            return AbpSession.UserId.ToString();
        }
        public List<CM_DEV_DTO> DEVICE_Search(string index, CM_DEV_DTO filterInput)
        {
            return procedureHelper.GetData<CM_DEV_DTO>("DEVICE_Search", new {
                INDEX = index,
                CURRENT_BRANCH    = AbpSession.UserId.ToString(),
                DEVICE_CODE       = filterInput.DEVICE_CODE,
                DEVICE_NAME       = filterInput.DEVICE_NAME,
                BRANCH_ID         = filterInput.BRANCH_ID,
                ACTIVE_STATUS     = filterInput.ACTIVE_STATUS
            });
        }

      

        public List<dynamic> DEVICE_Get_All_Branch()
        {
            return procedureHelper.GetData<dynamic>("DEVICE_Get_All_Branch", new
            {
            });
        }

        [AbpAuthorize(Group5PermissionsConst.Pages_Administration_Dev_Add)]
        public List<dynamic> DEVICE_Insert(CM_DEV_DTO input)
        {
            input.MAKER_ID = AbpSession.UserId.ToString();
            return procedureHelper.GetData<dynamic>("DEVICE_Insert", input);
        }

        [AbpAuthorize(Group5PermissionsConst.Pages_Administration_Dev_Delete)]
        public dynamic DEVICE_Delete(string listDepId)
        {
            return procedureHelper.GetData<dynamic>("DEVICE_Delete", new
            {
                DEVICE_ID = listDepId,
                MAKER_ID = AbpSession.UserId.ToString()
        }).FirstOrDefault();
        }

        [AbpAuthorize(Group5PermissionsConst.Pages_Administration_Dev_Update)]
        public List<dynamic> DEVICE_Update(CM_DEV_DTO input)
        {
            input.MAKER_ID = AbpSession.UserId.ToString();  
            return procedureHelper.GetData<dynamic>("DEVICE_Update", input);
        }

        public List<string> DEVICE_Get_All_Name()
        {
            return procedureHelper.GetData<string>("DEVICE_Get_All_Name", new{ });
        }

        public List<CM_DEV_DTO> G2_DEVICE_GETALL()
        {
            return procedureHelper.GetData<CM_DEV_DTO>("G2_DEVICE_GETALL", new { });
        }
        
        [AbpAuthorize(Group5PermissionsConst.Pages_Administration_Dev_Approve)]
        public dynamic DEVICE_Approve(int check, string devID)
        {
            return procedureHelper.GetData<dynamic>("Device_Duyet", new
            {
                CHECK = check,
                DEVICE_ID = devID,
                APPROVE_ID = AbpSession.UserId.ToString()
            }).FirstOrDefault();
        }
    }
}



