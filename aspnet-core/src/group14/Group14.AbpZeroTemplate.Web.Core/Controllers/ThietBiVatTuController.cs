using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group14.AbpZeroTemplate.Application.Services.ThietBiVatTu;
using Group14.AbpZeroTemplate.Web.Core.Services.ThietBiVatTu.Dto;
using GSoft.AbpZeroTemplate.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Group14.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThietBiVatTuController : AbpController
    {
        private readonly IThietBiVatTuService _service;

        public ThietBiVatTuController(IThietBiVatTuService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<THIETBIVATTU_DTO> GetAllThietBiVatTu()
        {
            return _service.ThietBiVatTu_GetAll();
        }

        [HttpPost]
        public PagedResultDto<THIETBIVATTU_DTO> SearchThietBiVatTu(
            [FromBody] THIETBIVATTU_DTO inputFilter,
            string orderBy, bool desc = false,
            int? skip = null, int? take = null)
        {
            return _service.ThietBiVatTu_Filter(inputFilter, orderBy, desc, skip, take);
        }

        [HttpDelete]
        public List<dynamic> DeleteThietBiVatTu(string maThietBiVatTu)
        {
            return _service.ThietBiVatTu_Delete(maThietBiVatTu);
        }

        [HttpPost]
        public List<dynamic> RemoveTBVTFromPCPTBVT(string maTBVT, string maPCP)
        {
            return _service.ThietBiVatTu_RemoveFromPCPTBVT(maTBVT, maPCP);
        }

        [HttpPost]
        public List<dynamic> AddTBVTToPCPTBVT(string maTBVT, string maPCP)
        {
            return _service.ThietBiVatTu_AddToPCPTBVT(maTBVT, maPCP);
        }

        [HttpPost]
        public FileDto ExportThietBiVatTu([FromBody] THIETBIVATTU_DTO input)
        {
            return _service.ThietBiVatTu_ExportToExcel(input);
        }
    }
}