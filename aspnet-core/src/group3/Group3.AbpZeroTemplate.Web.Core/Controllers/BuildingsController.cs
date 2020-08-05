using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group3.AbpZeroTemplate.Web.Core.Buildings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BuildingsController : AbpController
    {
        private readonly IBuildingsAppService _service = null;
        public BuildingsController(IBuildingsAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public IDictionary<string, object> Approve([FromBody] BuildingApproveRequest body)
        {
            return _service.Approve(body);
        }

        [HttpPost]
        public IDictionary<string, object> Create([FromBody] BuildingCreateRequest body)
        {
            return _service.Create(body);
        }

        [HttpDelete]
        public IDictionary<string, object> Delete([FromQuery] string BUILDING_ID)
        {
            return _service.Delete(BUILDING_ID);
        }

        [HttpGet]
        public BuildingTableDTO GetByID([FromQuery] string BUILDING_ID)
        {
            return _service.GetByID(BUILDING_ID);
        }

        [HttpGet]
        public PagedResultDto<BuildingPagingSearchDTO> PagingSearch(
            [FromQuery] BuildingPagingSearchRequest query)
        {
            return _service.PagingSearch(query);
        }

        [HttpPut]
        public IDictionary<string, object> Update([FromBody] BuildingUpdateRequest body)
        {
            return _service.Update(body);
        }
    }
}
