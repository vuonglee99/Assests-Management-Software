using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Dependency;
using Group14.AbpZeroTemplate.Web.Core.Services.NhanVien;
using Group14.AbpZeroTemplate.Web.Core.Services.NhanVien.Dto;
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
    public class NhanVienController : AbpController
    {
        private readonly INhanVienService nvService;

        public NhanVienController(INhanVienService nvService)
        {
            this.nvService = nvService;
        }

        [HttpGet]
        public List<NHANVIEN_DTO> GetAllNhanVien()
        {
            return nvService.NhanVien_GetAll();
        }

        [HttpGet]
        public List<string> GetAllDepartments()
        {
            return nvService.NhanVien_GetAllDepartments();
        }

        [HttpGet]
        public List<NHANVIEN_DEP_NAME_DTO> GetDepName()
        {
            return nvService.NhanVien_GetDepName();
        }

        [HttpPost]
        public FileDto ExportNhanVien([FromBody] NHANVIEN_DTO input)
        {
            return nvService.NhanVien_ExportToExcel(input);
        }

        [HttpPost]
        public PagedResultDto<NHANVIEN_DTO> SearchNhanVien([FromBody] NHANVIEN_FILTER inputFilter)
        {
            return nvService.NhanVien_Search(inputFilter);
        }

        [HttpDelete]
        public List<dynamic> DeleteNhanVien(string maNhanVien)
        {
            return nvService.NhanVien_Delete(maNhanVien);
        }

        [HttpPost]
        public List<NHANVIEN_DTO> NhanVien_ById(string manv)
        {
            return nvService.NhanVien_ById(manv);
        }

        [HttpPost]
        public List<dynamic> InsertNhanVien([FromBody]NHANVIEN_DTO filter_input)
        {
            return nvService.NhanVien_Insert(filter_input);
        }

        [HttpPost]
        public List<dynamic> UpdateNhanVien(NHANVIEN_DTO input)
        {
            return nvService.NhanVien_Update(input);
        }
    }
}
