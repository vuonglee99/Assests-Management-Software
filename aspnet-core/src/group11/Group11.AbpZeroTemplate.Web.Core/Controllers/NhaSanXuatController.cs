using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_NhaSanXuat;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_NhaSanXuat.NhaSanXuat_DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group11.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NhaSanXuatController : AbpController
    {
        private readonly NSXAppService _nsxAppService;

        public NhaSanXuatController(NSXAppService nsxAppService)
        {
            this._nsxAppService = nsxAppService;
        }

        [HttpPost]
        public PagedResultDto<NSX_DTO> NSX_Search([FromBody]NSX_DTO filterInput)
        {
            return _nsxAppService.NSX_Search(filterInput);
        }
        [HttpPost]
        public List<NSX_DTO> NSX_Delete(string nsxId)
        {
            return _nsxAppService.NSX_Delete(nsxId);
        }
        [HttpGet]
        public string gettest()
        {
            return "abc";
        }
        [HttpPost]
        public List<NSX_DTO> NSX_ExportExcel([FromBody] List<NSX_DTO> listInput)
        {
            return _nsxAppService.NSX_ExportExcel(listInput);
        }
    }
}
