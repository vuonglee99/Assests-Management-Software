using Abp.AspNetCore.Mvc.Controllers;
using Group6.AbpZeroTemplate.Web.Core.Services;
using Group6.AbpZeroTemplate.Web.Core.Services.TrangThaiYeuCauSuaChuas;
using Group6.AbpZeroTemplate.Web.Core.Services.TrangThaiYeuCauSuaChuas.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TrangThaiYeuCauSuaChuaController : AbpController
    {
        private readonly ITTYCSCAppService TTYCSCAppService;

        public TrangThaiYeuCauSuaChuaController(ITTYCSCAppService TTYCSCAppService)
        {
            this.TTYCSCAppService = TTYCSCAppService;
        }

        [HttpGet]
        public TTYCSC_DTO TTYCSC_ById(string TTYCSCId)
        {
            return TTYCSCAppService.TTYCSC_ById(TTYCSCId);
        }

        [HttpPut]
        public dynamic TTYCSC_Delete(string TTYCSCId)
        {
            return TTYCSCAppService.TTYCSC_Delete(TTYCSCId);
        }

        [HttpGet]
        public PageResult TTYCSC_SearchFilter(int pageNumber, int pageSize, string TTYCSCName, string TTYCSCCode, string TTYCSCDesc, string APPROVEStatus)
        {
            return TTYCSCAppService.TTYCSC_SearchFilter(pageNumber, pageSize, TTYCSCName, TTYCSCCode, TTYCSCDesc, APPROVEStatus);
        }

        [HttpPost]
        public dynamic TTYCSC_Create(TTYCSC_DTO input)
        {
            return TTYCSCAppService.TTYCSC_Create(input);
        }

        [HttpGet]
        public string TTYCSC_GetValueGenCode()
        {
            return TTYCSCAppService.TTYCSC_GetValueGenCode();
        }

        [HttpPut]
        public dynamic TTYCSC_Update(TTYCSC_DTO input)
        {
            return TTYCSCAppService.TTYCSC_Update(input);
        }

        [HttpPut]
        public dynamic TTYCSC_Approve(TTYCSCApprove_DTO input)
        {
            return TTYCSCAppService.TTYCSC_Approve(input);
        }

        [HttpGet]
        public dynamic TTYCSC_ExportExcel(int pageNumber, int pageSize, string TTYCSCName, string TTYCSCCode, string TTYCSCDesc, string APPROVEStatus)
        {
            return TTYCSCAppService.TTYCSC_ExportExcel(pageNumber, pageSize, TTYCSCName, TTYCSCCode, TTYCSCDesc, APPROVEStatus);
        }
    }
}
