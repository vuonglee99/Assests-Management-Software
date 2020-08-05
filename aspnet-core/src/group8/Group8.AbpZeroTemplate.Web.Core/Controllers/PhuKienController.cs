using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization;
using Group8.AbpZeroTemplate.Web.Core.Services.PhuKien;
using Group8.AbpZeroTemplate.Web.Core.Services.PhuKien.dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PhuKienController : AbpController
    {
        private readonly IPhuKienService PhuKienService;

        public PhuKienController(IPhuKienService phuKienService)
        {
            PhuKienService = phuKienService;
        }

        [HttpPost]
        public PhuKien_DTO GetById(string tbvt_ma)
        {
            return PhuKienService.PhuKien_ById(tbvt_ma);
        }

        [HttpPost]
        public List<PhuKien_DTO> GetAll()
        {
            return PhuKienService.PhuKien_GetAll();
        }

        [HttpPost]
        public List<PhuKien_DTO> Search([FromBody]PhuKien_DTO filter_input)
        {
            return PhuKienService.PhuKien_Search(filter_input);
        }

        [HttpPost]
        public List<dynamic> Insert([FromBody]PhuKien_DTO input)
        {
            return PhuKienService.PhuKien_Insert(input);
        }

        [HttpPost]
        public List<dynamic> Update([FromBody]PhuKien_DTO input)
        {
            return PhuKienService.PhuKien_Update(input);
        }

        [HttpPost]
        public List<dynamic> Delete(string tbvt_ma)
        {
            return PhuKienService.PhuKien_Delete(tbvt_ma);
        }
    }
}
