using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Group6.AbpZeroTemplate.Web.Core.Services.DoUuTiens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group6.AbpZeroTemplate.Web.Core.Services.DoUuTiens.Dto;
using Group6.AbpZeroTemplate.Web.Core.Services;

namespace Group6.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DoUuTienController : AbpController
    {
        private readonly IDUTAppService dUTAppService;

        public DoUuTienController(IDUTAppService dUTAppService)
        {
            this.dUTAppService = dUTAppService;
        }

        [HttpGet]
        public DUT_DTO DUT_ById(string dutId)
        {
            return dUTAppService.DUT_ById(dutId);
        }

        [HttpPut]
        public dynamic DUT_Delete(string dutId)
        {
            return dUTAppService.DUT_Delete(dutId);
        }

        [HttpGet]
        public PageResult DUT_SearchFilter(int pageNumber, int pageSize, string dutName, string dutCode, string dutDesc, string record_status, string sort)
        {
            return dUTAppService.DUT_SearchFilter(pageNumber, pageSize, dutName, dutCode, dutDesc, record_status, sort);
        }

        [HttpPost]
        public dynamic DUT_Create(DUTCreate_DTO input)
        {
            return dUTAppService.DUT_Create(input);
        }

        [HttpGet]
        public string DUT_GetValueGenCode()
        {
            return dUTAppService.DUT_GetValueGenCode();
        }

        [HttpPut]
        public dynamic DUT_Update(DUT_DTO input)
        {
            return dUTAppService.DUT_Update(input);
        }

        [HttpGet]
        public dynamic DUT_GetLevel()
        {
            return dUTAppService.DUT_GetLevel();
        }
    }
}
