using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group3.AbpZeroTemplate.Web.Core.Residents;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ResidentsController : AbpController
    {
        private readonly IResidentsAppService _service = null;
        public ResidentsController(IResidentsAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public IDictionary<string, object> Approve([FromBody] ResidentApproveRequest body)
        {
            return _service.Approve(body);
        }

        [HttpPost]
        public IDictionary<string, object> Create([FromBody] ResidentCreateRequest body)
        {
            return _service.Create(body);
        }

        [HttpDelete]
        public IDictionary<string, object> Delete([FromQuery] string BUILDING_ID)
        {
            return _service.Delete(BUILDING_ID);
        }

        [HttpGet]
        public ResidentTableDTO GetByID([FromQuery] string BUILDING_ID)
        {
            return _service.GetByID(BUILDING_ID);
        }

        [HttpGet]
        public PagedResultDto<ResidentPagingSearchDTO> PagingSearch(
            [FromQuery] ResidentPagingSearchRequest query)
        {
            return _service.PagingSearch(query);
        }

        [HttpPut]
        public IDictionary<string, object> Update([FromBody] ResidentUpdateRequest body)
        {
            return _service.Update(body);
        }
    }
}
