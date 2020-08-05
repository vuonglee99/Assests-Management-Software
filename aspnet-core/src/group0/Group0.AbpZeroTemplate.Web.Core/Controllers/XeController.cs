using Abp.AspNetCore.Mvc.Controllers;
using Abp.Dependency;
using Group0.AbpZeroTemplate.Web.Core.Services.CM_XEs;
using Group0.AbpZeroTemplate.Web.Core.Services.CM_XEs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group0.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class XeController : AbpController
    {
        private readonly IXeAppService xeAppService;

        public XeController(IXeAppService xeAppService)
        {
            this.xeAppService = xeAppService;
        }


        [HttpPost]
        public List<CM_XE_DTO> CM_XE_Search([FromBody]CM_XE_DTO filterInput)
        {
            return xeAppService.CM_XE_Search(filterInput);
        }
        [HttpPost]
        public CM_XE_DTO CM_XE_ById(string xeId)
        {
            return xeAppService.CM_XE_ById(xeId);
        }
        [HttpPost]
        public List<dynamic> CM_XE_Insert([FromBody]CM_XE_DTO input)
        {
            return xeAppService.CM_XE_Insert(input);
        }
        [HttpPost]
        public List<dynamic> CM_XE_Update([FromBody]CM_XE_DTO input)
        {
            return xeAppService.CM_XE_Update(input);
        }
        [HttpPost]
        public List<dynamic> CM_XE_Delete(string xeId)
        {
            return xeAppService.CM_XE_Delete(xeId);
        }
        [HttpPost]
        public List<dynamic> CM_XE_Approve(string xeId, string checkerId)
        {
            return xeAppService.CM_XE_Approve(xeId, checkerId);
        }
    }
}
