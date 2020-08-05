using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization;
using Group8.AbpZeroTemplate.Web.Core.Services.ChiTietNhanViens;
using Group8.AbpZeroTemplate.Web.Core.Services.ChiTietNhanViens.dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CTNVController : AbpController
    {
        private readonly IChiTietNhanVienService ChiTietNhanVienService;

        public CTNVController(IChiTietNhanVienService ChiTietNhanVienService)
        {
            this.ChiTietNhanVienService = ChiTietNhanVienService;
        }

        [HttpPost]
        public List<dynamic> CTNV_GetAll()
        {
            return ChiTietNhanVienService.CTNV_GetAll();
        }

        [HttpPost]
        public ChiTietNhanVien_DTO CTNV_ById(string manv)
        {
            return ChiTietNhanVienService.CTNV_ById(manv);
        }

        [HttpPost]
        public List<ChiTietNhanVien_DTO> CTNV_Search(ChiTietNhanVien_DTO filter_input)
        {
            return ChiTietNhanVienService.CTNV_Search(filter_input);
        }

        [HttpPost]
        public  List<dynamic> CTNV_Insert([FromBody]ChiTietNhanVien_DTO filter_input)
        {
            return ChiTietNhanVienService.CTNV_Insert(filter_input);
        }

        [HttpPost]
        public List<dynamic> CTNV_Update(ChiTietNhanVien_DTO input)
        {
            return ChiTietNhanVienService.CTNV_Update(input);
        }

        [HttpPost]
        public dynamic CTNV_Delete(string manv)
        {
            return ChiTietNhanVienService.CTNV_Delete(manv);
        }
    }
}
