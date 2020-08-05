using Abp.AspNetCore.Mvc.Controllers;
using Group7.AbpZeroTemplate.Web.Core.Services.LOAI_XEs;
//using Group7.AbpZeroTemplate.Web.Core.Services.LOAI_XEs.Dto;
using Group2.AbpZeroTemplate.Web.Core.Services.LOAI_XEs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group7.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoaiXeController : AbpController
    {
        private readonly ILoaiXeAppService loaiXeAppService;

        public LoaiXeController(ILoaiXeAppService xeAppService)
        {
            this.loaiXeAppService = xeAppService;
        }

        [HttpPost]
        public List<LOAI_XE_DTO> LoaiXeSearch([FromBody]LOAI_XE_DTO filterInput)
        {
            return loaiXeAppService.LOAI_XE_Search(filterInput);
        }

        [HttpGet]
        public List<LOAI_XE_DTO> LoaiXeSearchAlternative([FromQuery]string toSearch, [FromQuery]string searchType)
        {
            return loaiXeAppService.SearchWithOption(toSearch, searchType);
        }
        /*
        [HttpPost]
        public LOAI_XE_DTO LoaiXeById(string loaixeId)
        {
            return loaiXeAppService.LOAI_XE_ByID(loaixeId);
        }

        [HttpPost]
        public IDictionary<string, object> LoaiXeInsert([FromBody]LOAI_XE_DTO input)
        {
            return loaiXeAppService.LOAI_XE_Insert(input);
        }
        [HttpPost]
        public List<dynamic> LoaiXeUpdate([FromBody]LOAI_XE_DTO input)
        {
            return loaiXeAppService.LOAI_XE_Update(input);
        }

        [HttpPost]
        public List<dynamic> LoaiXeDelete([FromQuery]string loaixeId)
        {
            return loaiXeAppService.LOAI_XE_Delete(loaixeId);
        }
        */
        [HttpGet]
        public List<LOAI_XE_DTO> LoaiXeGetAll()
        {
            return loaiXeAppService.GetAll();
        }
    }
}
