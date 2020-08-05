using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group14.AbpZeroTemplate.Web.Core.Services.NhaCungUng;
using Group14.AbpZeroTemplate.Web.Core.Services.NhaCungUng.Dto;
using GSoft.AbpZeroTemplate.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Group14.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NhaCungUngController : AbpController
    {
        private readonly INhaCungUngService _service;

        public NhaCungUngController(INhaCungUngService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<NHACUNGUNG_DTO> GetAllNhaCungUng()
        {
            return _service.NhaCungUng_GetAll();
        }

        [HttpPost]
        public PagedResultDto<NHACUNGUNG_DTO> SearchNhaCungUng(
            [FromBody] NHACUNGUNG_DTO inputFilter,
            string orderBy, bool desc = false,
            int? skip = null, int? take = null)
        {
            return _service.NhaCungUng_Filter(inputFilter, orderBy, desc, skip, take);
        }

        [HttpDelete]
        public List<dynamic> DeleteNhaCungUng(string maNhaCungUng)
        {
            return _service.NhaCungUng_Delete(maNhaCungUng);
        }

        [HttpPost]
        public FileDto ExportNhaCungUng([FromBody] NHACUNGUNG_DTO input)
        {
            return _service.NhaCungUng_ExportToExcel(input);
        }
    }
}
