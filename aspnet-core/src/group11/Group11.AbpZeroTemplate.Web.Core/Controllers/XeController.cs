using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_Xe;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_Xe.Xe_DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Group11.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class XeController : AbpController
    {
        private readonly IXeService xeService;

        public XeController(IXeService xeService)
        {
            this.xeService = xeService;
        }
        
        [HttpPost]
        public PagedResultDto<XE_DTO> XE_Search([FromBody] XE_DTO filterInput)
        {
            return xeService.XE_Search(filterInput);
        }
        [HttpPost]
        public List<XE_DTO> XE_ExportExcel([FromBody] List<XE_DTO> listInput)
        {
            return xeService.XE_ExportExcel(listInput);
        }
        [HttpPost]
        public List<XE_DTO> XE_ByCode(string xeCode)
        {
            return xeService.XE_FindByCode(xeCode);
        }
        [HttpPost]
        public List<XE_DTO> XE_Insert([FromBody] XE_DTO input)
        {
            return xeService.XE_Insert(input);
        }

        [HttpPost]
        public List<XE_DTO> XE_Update([FromBody]XE_DTO input)
        {
            return xeService.XE_Update(input);
        }
        [HttpPost]
        public List<XE_DTO> XE_Delete(string xeId)
        {
            return xeService.XE_Delete(xeId);
        }
        [HttpGet("{id}")]
        public PagedResultDto<XE_DTO> XE_ByNtxID(string id)
        {
            return xeService.XE_ByNtxID(id);
        }
        [HttpGet("{id}")]
        public XE_DTO XE_ByID(string id)
        {
            return xeService.XE_ById(id);
        }

    }
}
