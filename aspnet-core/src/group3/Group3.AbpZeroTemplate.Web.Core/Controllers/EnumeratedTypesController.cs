using Abp.AspNetCore.Mvc.Controllers;
using Group3.AbpZeroTemplate.Web.Core.Enumerated;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class EnumeratedTypesController : AbpController
    {
        private readonly IEnumeratedTypesAppService _service = null;
        public EnumeratedTypesController(IEnumeratedTypesAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public IDictionary<string, object> Create([FromBody] EnumeratedTypeCreateRequest body)
        {
            return _service.Create(body);
        }

        [HttpDelete]
        public IDictionary<string, object> Delete([FromQuery] int ID)
        {
            return _service.Delete(ID);
        }

        [HttpGet]
        public IEnumerable<EnumeratedTypeTableDTO> GetByType(string TYPE)
        {
            return _service.GetByType(TYPE);
        }
    }
}
