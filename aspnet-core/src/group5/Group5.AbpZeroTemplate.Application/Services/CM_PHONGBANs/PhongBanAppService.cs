using Abp.Application.Services;
using Abp.Authorization;
using Group5.AbpZeroTemplate.Application;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_PHONGBANs.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using GSoft.AbpZeroTemplate.Helpers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5.AbpZeroTemplate.Web.Core.Services.CM_PHONGBANs
{
    public interface IPhongBanAppService : IApplicationService
    {
        List<dynamic> DEPARTMENT_GET_BRANCH_ID_NAME_BYID(string branch_id);
        AppUser DEPARTMENT_GET_USER_BRANCH_BY_USERID();
        List<CM_PHONGBAN_DTO> DEPARTMENT_Search(string index,CM_PHONGBAN_DTO filterInput);
        //CM_PHONGBAN_DTO Nhom05_ById_PhongBan(string depId);
        dynamic DEPARTMENT_Delete(string listDepId);
    }


    public class PhongBanAppService : BaseService, IPhongBanAppService
    {

        public AppUser DEPARTMENT_GET_USER_BRANCH_BY_USERID()
        {
            return procedureHelper.GetData<AppUser>("DEPARTMENT_GET_USER_BRANCH_BY_USERID", new
            {
                USER_ID = AbpSession.UserId
            }).FirstOrDefault();
        }

        public List<dynamic> DEPARTMENT_GET_BRANCH_ID_NAME_BYID(string branch_id)
        {
            return procedureHelper.GetData<dynamic>("DEPARTMENT_GET_BRANCH_ID_NAME_BYID", new
            {

                BRANCH_ID = branch_id

            });
        }

      

        public List<CM_PHONGBAN_DTO> DEPARTMENT_Search(string index,CM_PHONGBAN_DTO filterInput)
        {
            return procedureHelper.GetData<CM_PHONGBAN_DTO>("DEPARTMENT_Search", new {
                INDEX = index,
                CURRENT_BRANCH = AbpSession.UserId.ToString(),
                DEP_CODE = filterInput.DEP_CODE,
                DEP_NAME = filterInput.DEP_NAME,
                BRANCH_ID = filterInput.BRANCH_ID,
                RECORD_STATUS = filterInput.RECORD_STATUS
            });
        }

 

        [AbpAuthorize(Group5PermissionsConst.Pages_Administration_PhongBan_Delete)]
        public dynamic DEPARTMENT_Delete(string listDepId)
        {
            return procedureHelper.GetData<dynamic>("DEPARTMENT_Delete", new
            {
                MAKER_ID= AbpSession.UserId.ToString(),
                DEP_ID = listDepId
            }).FirstOrDefault();
        }
    }
}
