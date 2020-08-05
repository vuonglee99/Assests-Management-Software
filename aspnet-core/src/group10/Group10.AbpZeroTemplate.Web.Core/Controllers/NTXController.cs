using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Dapper;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_NTX;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_NTX.Dto;
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
    public class NTXController : AbpController
    {
        private readonly INTXAppService NTXAppService;
        public NTXController(INTXAppService NTXAppService)
        {
            this.NTXAppService = NTXAppService;


        }

        [HttpPost]
        public IDictionary<string, object> NguoiThueXe_Insert([FromBody] NguoiThueXe_DTO input)
        {

            return NTXAppService.NguoiThueXe_Insert(input);
        }

        [HttpPut]
        public IDictionary<string, object> NguoiThueXe_Update([FromBody] NguoiThueXe_DTO input)
        {
            return NTXAppService.NguoiThueXe_Update(input);
        }

        [HttpDelete("{id}")]
        public IDictionary<string, object> NguoiThueXe_Delete(string id)
        {
            return NTXAppService.NguoiThueXe_Delete(id);
        }

        [HttpGet("{id}")]
        public NguoiThueXe_DTO NguoiThueXe_ById(string id)
        {
            return NTXAppService.NguoiThueXe_ById(id);
        }

        [HttpPost]
        public PagedResultDto<NguoiThueXe_DTO> NguoiThueXe_Search([FromBody] NguoiThueXe_DTO filterInput)
        {
            return NTXAppService.NguoiThueXe_Search(filterInput);
        }

        [HttpGet("{id}")]
        public PagedResultDto<NguoiThueXe_DTO> NguoiThueXe_ByXeID(string id)
        {
            return NTXAppService.NguoiThueXe_ByXeID(id);
        }

        [HttpGet("{id}")]
        public NguoiThueXe_DTO NguoiThueXe_ByXeId_HienTai(string id)
        {
            return NTXAppService.NguoiThueXe_ByXeId_HienTai(id);
        }
    }
}
