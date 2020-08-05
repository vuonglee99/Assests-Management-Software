using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Group7.AbpZeroTemplate.Web.Core.Services.NHA_CUNG_UNG;
using Group7.AbpZeroTemplate.Web.Core.Services.NHA_CUNG_UNG.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Group7.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NhaCungUngController : AbpController
    {
        private readonly INhaCungUngAppService nhaCungUngAppService;

        public NhaCungUngController(INhaCungUngAppService nhaCungUngAppService)
        {
            this.nhaCungUngAppService = nhaCungUngAppService;
        }

        [HttpPost]
        public NHA_CUNG_UNG_DTO NhaCungUngById(string NCU_Ma_NCU)
        {
            return nhaCungUngAppService.NhaCungUngById(NCU_Ma_NCU);
        }

        [HttpPost]
        public IDictionary<string, object> NhaCungUngInsert([FromBody]NHA_CUNG_UNG_DTO input)
        {
            return nhaCungUngAppService.NhaCungUngInsert(input);
        }
        [HttpPost]
        public List<dynamic> NhaCungUngUpdate([FromBody]NHA_CUNG_UNG_DTO input)
        {
            return nhaCungUngAppService.NhaCungUngUpdate(input);
        }
        [HttpPost]
        public List<dynamic> NhaCungUngDelete([FromQuery]string NCU_Ma_NCU)
        {
            return nhaCungUngAppService.NhaCungUngDelete(NCU_Ma_NCU);
        }
        [HttpGet]
        public List<NHA_CUNG_UNG_DTO> NhaCungUngGetAll()
        {
            return nhaCungUngAppService.NhaCungUngGetAll();
        }
    }
}
