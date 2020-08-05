using Abp.AspNetCore.Mvc.Controllers;
using Abp.Dependency;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_PHONGBANs;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_PHONGBANs.Dto;
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
    public class PhongBanController : AbpController
    {
        private readonly IPhongBanAppService phongBanAppService;

        public PhongBanController(IPhongBanAppService phongBanAppService)
        {
            this.phongBanAppService = phongBanAppService;
        }

        [HttpPost]
        public List<dynamic> DEPARTMENT_GET_BRANCH_ID_NAME_BYID(string branch_id)
        {
            return phongBanAppService.DEPARTMENT_GET_BRANCH_ID_NAME_BYID(branch_id);
        }


        [HttpPost]
        public AppUser DEPARTMENT_GET_USER_BRANCH_BY_USERID()
        {
            return phongBanAppService.DEPARTMENT_GET_USER_BRANCH_BY_USERID();
        }

        [HttpPost]
        public List<CM_PHONGBAN_DTO> DEPARTMENT_Search(string index,[FromBody]CM_PHONGBAN_DTO filterInput)
        {
            return phongBanAppService.DEPARTMENT_Search(index,filterInput);
        }

        [HttpPost]
        public IDictionary<string, object> DEPARTMENT_Delete(string list_dep_id)
        {
            return phongBanAppService.DEPARTMENT_Delete(list_dep_id);
        }

    }
}
