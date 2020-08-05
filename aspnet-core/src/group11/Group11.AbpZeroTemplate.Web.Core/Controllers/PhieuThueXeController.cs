using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Extensions;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_PhieuThueXe.PhieuThueXe_DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group11.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PhieuThueXeController : AbpController
    {
        private readonly IPhieuThueXeService _phieuThueXeService;

        public PhieuThueXeController(IPhieuThueXeService phieuThueXeService)
        {
            this._phieuThueXeService = phieuThueXeService;
        }

        [HttpPost]
        public List<PTX_DTO> PTX_Delete(string phieuThue_Code)
        {
            return _phieuThueXeService.DeletCustomer(phieuThue_Code);
        }

        [HttpPost]
        public PagedResultDto<PTX_DTO> PTX_ExportExcel()
        {
            return _phieuThueXeService.PTX_ExportExcel();
        }

        [HttpPost]
        public List<PTX_DTO> PTX_Insert([FromBody]PTX_DTO phieuThue)
        {
            return _phieuThueXeService.Insert(phieuThue);
        }

        [HttpPost]
        public List<PTX_DTO> PTX_Update([FromBody]PTX_DTO phieuThue)
        {
            return _phieuThueXeService.Update(phieuThue);
        }

        [HttpPost]
        public List<PTX_DTO> PTX_GetByCode(string phieuThue_Code)
        {
            return _phieuThueXeService.GetByCode(phieuThue_Code);
        }
    }
}
