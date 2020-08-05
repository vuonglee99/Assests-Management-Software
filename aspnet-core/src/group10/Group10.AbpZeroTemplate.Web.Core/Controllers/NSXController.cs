using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Dapper;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_NSX;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_NSX.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group10.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NSXController : AbpController
    {
        private readonly INSXAppService NSXAppService;
        public NSXController(INSXAppService NSXAppService)
        {
            this.NSXAppService = NSXAppService;
           
           
        }
        [HttpGet("{colname}")]
        public string CM_NSX_GetSizeCol(string colname)
        {

            return NSXAppService.CM_NSX_GetSizeCol(colname);
        }
        [HttpPost]
        public IDictionary<string, object> CM_NSX_Insert([FromBody]CM_NSX_DTO input)
        {
            
            return NSXAppService.CM_NSX_Insert(input);
        }
        [HttpPut]
        public IDictionary<string, object> CM_NSX_Update([FromBody]CM_NSX_DTO input)
        {
            return NSXAppService.CM_NSX_Update(input);
        }
        [HttpDelete("{id}")]
        public IDictionary<string, object> CM_NSX_Delete(string id)
        {
            return NSXAppService.CM_NSX_Delete(id);
        }
       
       
        [HttpGet("{id}")]
        public CM_NSX_DTO CM_NSX_ById(string id)
        {
            return NSXAppService.CM_NSX_ById(id);
        }
        [HttpPost]
        public PagedResultDto<CM_NSX_DTO> CM_NSX_Search([FromBody]CM_NSX_DTO filterInput)
        {
            return NSXAppService.CM_NSX_Search(filterInput);
        }
    }
}
