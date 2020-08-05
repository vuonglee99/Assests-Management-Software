using Abp.AspNetCore.Mvc.Controllers;
using Abp.Dependency;
using Group12.AbpZeroTemplate.Web.Core.Services.KIEMKE;
using Group12.AbpZeroTemplate.Web.Core.Services.KIEMKE.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group12.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class KiemKeController : AbpController
    {
        private readonly IGroup12KiemKeService kiemKeService;

        public KiemKeController(IGroup12KiemKeService kiemKeService)
        {
            this.kiemKeService = kiemKeService;
        }

        [HttpPost]
        public List<dynamic> KiemKe_Insert([FromBody] KIEMKE_DTO input)
        {
            return kiemKeService.KiemKe_Insert(input);
        }

        [HttpPost]
        public List<KIEMKE_DTO> KiemKe_GetAll([FromBody]KIEMKE_DTO filterInput)
        {
            return kiemKeService.KiemKe_GetAll(filterInput);
        }

       

        [HttpPost]
        public List<dynamic> KiemKe_Update([FromBody]KIEMKE_DTO input)
        {
            return kiemKeService.KiemKe_Update(input);
        }

        [HttpPost]
        public List<dynamic> KiemKe_Delete(string kiemKeId)
        {
            return kiemKeService.KiemKe_Delete(kiemKeId);
        }

        [HttpPost]
        public List<dynamic> KiemKe_Close(string id,string status)
        {
            return kiemKeService.KiemKe_Close(id,status);
        }

    }
}
