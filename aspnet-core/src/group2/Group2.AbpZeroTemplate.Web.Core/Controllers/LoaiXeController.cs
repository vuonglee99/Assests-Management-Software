using Abp.AspNetCore.Mvc.Controllers;
using Group2.AbpZeroTemplate.Web.Core.Services.LOAI_XEs;
using Group2.AbpZeroTemplate.Web.Core.Services.LOAI_XEs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoaiXeController : AbpController
    {
        private readonly ILoaiXeAppService loaixeAppService;

        public LoaiXeController(ILoaiXeAppService loaixeAppService)
        {
            this.loaixeAppService = loaixeAppService;
        }

        /*
        [HttpPost]
        public List<LOAI_XE_DTO> LoaiXeSearch([FromBody]LOAI_XE_DTO filterInput)
        {
            return loaixeAppService.LoaiXeSearch(filterInput);
        }*/
        [HttpPost]
        public LOAI_XE_DTO LoaiXeById(string loaixeId)
        {
            return loaixeAppService.LoaiXeById(loaixeId);
        }
        [HttpPost]
        public IDictionary<string, object> LoaiXeInsert([FromBody]LOAI_XE_DTO input)
        {
            return loaixeAppService.LoaiXeInsert(input);
        }
        [HttpPost]
        public List<dynamic> LoaiXeUpdate([FromBody]LOAI_XE_DTO input)
        {
            return loaixeAppService.LoaiXeUpdate(input);
        }
        [HttpPost]
        public List<dynamic> LoaiXeDelete([FromQuery]string loaixeId)
        {
            return loaixeAppService.LoaiXeDelete(loaixeId);
        }

        [HttpPost]
        public List<dynamic> LoaiXe_Approve([FromQuery]string loaixeCode)
        {
            return loaixeAppService.LoaiXe_Approve(loaixeCode);
        }

        [HttpPost]
        public List<dynamic> LoaiXe_UnApprove([FromQuery]string loaixeCode)
        {
            return loaixeAppService.LoaiXe_UnApprove(loaixeCode);
        }
    }
}
