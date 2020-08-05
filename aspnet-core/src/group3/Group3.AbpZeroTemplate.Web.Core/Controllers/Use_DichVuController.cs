using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group3.AbpZeroTemplate.Web.Core.DTOs;
using Group3.AbpZeroTemplate.Web.Core.Services.Use_DichVu;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Use_DichVuController:AbpController
    {
        private readonly IUse_DichVuAppService _use_DichVusAppService = null;
        public Use_DichVuController(IUse_DichVuAppService use_DichVuAppService)
        {
            _use_DichVusAppService = use_DichVuAppService;
        }

        [HttpPost]
        public IDictionary<string,object> Use_DichVuInsert([FromBody] Use_DichVuInsertRequest filter)
        {
            return _use_DichVusAppService.InsertUse_DichVu(filter);
        }

        [HttpGet]
        public PagedResultDto<Use_DichVuSearchDTOs> Use_DichVuSearch(Use_DichVuSearchRequest filter)
        {
            return _use_DichVusAppService.SearchUse_DichVu(filter);
        }
    }
}
