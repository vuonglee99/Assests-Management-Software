using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Dapper;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_PTX;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_PTX.Dto;
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
    public class PTXController : AbpController
    {
        private readonly IPTXAppService PTXAppService;
        public PTXController(IPTXAppService PTXAppService)
        {
            this.PTXAppService = PTXAppService;


        }

       
        [HttpDelete("{id}")]
        public IDictionary<string, object> PhieuThueXe_Delete(string id)
        {
            return PTXAppService.PhieuThueXe_Delete(id);
        }

      
        [HttpPost]
        public PagedResultDto<PhieuThueXe_DTO> PhieuThueXe_Search(string ntxcode, string ntxname, string xecode, string xename, string license)
        {
            return PTXAppService.PhieuThueXe_Search(ntxcode,ntxname,xecode,xename,license);
        }

    
    }
}
