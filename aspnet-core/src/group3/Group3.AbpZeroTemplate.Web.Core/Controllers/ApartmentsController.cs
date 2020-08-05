using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group3.AbpZeroTemplate.Web.Core.Apartments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ApartmentsController : AbpController
    {
        private readonly IApartmentsAppService _service = null;
        public ApartmentsController(IApartmentsAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public IDictionary<string, object> Approve([FromBody] ApartmentApproveRequest body)
        {
            return _service.Approve(body);
        }

        [HttpPost]
        public IDictionary<string, object> Create([FromBody] ApartmentCreateRequest body)
        {
            return _service.Create(body);
        }

        [HttpDelete]
        public IDictionary<string, object> Delete([FromQuery] string APARTMENT_ID)
        {
            return _service.Delete(APARTMENT_ID);
        }

        [HttpGet]
        public ApartmentTableDTO GetByID([FromQuery] string APARTMENT_ID)
        {
            return _service.GetByID(APARTMENT_ID);
        }

        [HttpGet]
        public PagedResultDto<ApartmentPagingSearchDTO> PagingSearch(
            [FromQuery] ApartmentPagingSearchRequest query)
        {
            return _service.PagingSearch(query);
        }

        [HttpPut]
        public IDictionary<string, object> Update([FromBody] ApartmentUpdateRequest body)
        {
            return _service.Update(body);
        }
    }
}
