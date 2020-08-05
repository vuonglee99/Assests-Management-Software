using Abp.AspNetCore.Mvc.Controllers;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThietBiController : AbpController
    {
        private readonly IThietBiAppService thietbiAppService;

        public ThietBiController(IThietBiAppService thietbiAppService)
        {
            this.thietbiAppService = thietbiAppService;
        }
        [HttpGet]
        public List<THIET_BI_DTO> GetAllThietBi()
        {
            return thietbiAppService.GetAllThietBi();
        }
        [HttpGet]
        public THIET_BI_DTO ThietBiById(string thietbiId)
        {
            return thietbiAppService.ThietBiById(thietbiId);
        }
        [HttpPost]
        public List<dynamic> ThietBiUpdate([FromBody]THIET_BI_DTO input)
        {
            return thietbiAppService.ThietBiUpdate(input);
        }
    }
}
