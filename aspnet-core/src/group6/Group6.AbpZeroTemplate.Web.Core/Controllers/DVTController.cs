using Abp.AspNetCore.Mvc.Controllers;
using Abp.Dependency;
using Group6.AbpZeroTemplate.Web.Core.Services;
using Group6.AbpZeroTemplate.Web.Core.Services.DonViTinhs;
using Group6.AbpZeroTemplate.Web.Core.Services.DonViTinhs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DVTController : AbpController
    {
        private readonly IDVTAppService dVTAppService;

        public DVTController(IDVTAppService dVTAppService)
        {
            this.dVTAppService = dVTAppService;
        }

        [HttpPost]
        public dynamic CM_DVT_Delete(string dvtId)
        {
            return dVTAppService.DVT_Delete(dvtId);
        }

        [HttpPost]
        public List<DVT_DTO> CM_DVT_Search(string dvtCode, string dvtName, string record_status = "1")
        {
            return dVTAppService.DVT_Search(dvtCode, dvtName, record_status);
        }

        [HttpPost]
        public DVT_DTO CM_DVT_ById(string dvtId)
        {
            return dVTAppService.DVT_ById(dvtId);
        }

        [HttpGet]
        public PageResult CM_DVT_searchFilter(int pageNumber, int pageSize, string dvtCode, string dvtName, string record_status = "1")
        {
            return dVTAppService.DVT_searchFilter(pageNumber, pageSize, dvtCode, dvtName, record_status);
        }
    }

}
