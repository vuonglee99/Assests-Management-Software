using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group14.AbpZeroTemplate.Web.Core.Services.PhieuCapPhatTBVT;
using Group14.AbpZeroTemplate.Web.Core.Services.PhieuCapPhatTBVT.Dto;
using GSoft.AbpZeroTemplate.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group14.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PhieuCapPhatTBVTController : AbpController
    {
        private readonly IPCPTBVTService _service;

        public PhieuCapPhatTBVTController(IPCPTBVTService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<PCPTBVT_DTO> GetAllPCPTBVT()
        {
            return _service.PCPTBVT_GetAll();
        }

        [HttpPost]
        public PagedResultDto<PCPTBVT_DTO> FilterPCPTBVT(
            [FromBody] PCPTBVT_DTO inputFilter,
            string orderBy, bool desc = false,
            int? skip = null, int? take = null)
        {
            return _service.PCPTBVT_Filter(inputFilter, orderBy, desc, skip, take);
        }

        [HttpPost]
        public PagedResultDto<PCPTBVT_DTO> SearchPCPTBVT(
            [FromBody] PCPTBVT_SEARCH_DTO inputFilter,
            string orderBy, bool desc = false,
            int? skip = null, int? take = null)
        {
            return _service.PCPTBVT_Search(inputFilter, orderBy, desc, skip, take);
        }

        [HttpPost]
        public List<dynamic> InsertPCPTBVT(
            [FromBody] PCPTBVT_INSERT_DTO input)
        {
            return _service.PCPTBVT_Insert(input);
        }

        [HttpPost]
        public List<dynamic> UpdatePCPTBVT(
            [FromBody] PCPTBVT_UPDATE_DTO input)
        {
            return _service.PCPTBVT_Update(input);
        }

        [HttpPost]
        public List<dynamic> ApprovePCPTBVT(
            [FromBody] PCPTBVT_APPROVE_DTO input)
        {
            return _service.PCPTBVT_Approve(input);
        }
        [HttpPost]
        public List<dynamic> DenyPCPTBVT(
            [FromBody] PCPTBVT_APPROVE_DTO input)
        {
            return _service.PCPTBVT_Deny(input);
        }

        [HttpGet]
        public PCPTBVT_DTO GetPCPTBVT(string maPCPTBVT)
        {
            return _service.PCPTBVT_GetByID(maPCPTBVT);
        }

        [HttpDelete]
        public List<dynamic> DeletePCPTBVT(string maPCPTBVT)
        {
            return _service.PCPTBVT_Delete(maPCPTBVT);
        }

        [HttpPost]
        public FileDto ExportPCPTBVT([FromBody] PCPTBVT_DTO input)
        {
            return _service.PCPTBVT_ExportToExcel(input);
        }
    }
}
