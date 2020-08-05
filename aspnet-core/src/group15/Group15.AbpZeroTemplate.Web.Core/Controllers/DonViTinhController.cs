using GWebsite.AbpZeroTemplate.Application.DonViTinhs;
using GWebsite.AbpZeroTemplate.Application.Services;
using GWebsite.AbpZeroTemplate.Application.Services.DonViTinhs;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DonViTinhController : AbpController
    {
        private IDonViTinhService dvtService;

        public DonViTinhController(IDonViTinhService dvtService)
        {
            this.dvtService = dvtService;
        }

        [HttpPost]
        public ServiceResult MaDVT()
        {
            return this.dvtService.DVT_MaDVT();
        }

        [HttpGet]
        public List<DonViTinh_DTO> All()
        {
            return this.dvtService.DVT_GetAll();
        }

        [HttpPost]
        public ServiceResult Create([FromBody]DVTCreate_DTO createInput)
        {
            ServiceResult createResult = this.dvtService.DVT_Create(createInput);
            return createResult;
        }

        [HttpPut]
        public ServiceResult Update([FromBody]DVTUpdate_DTO updateInput)
        {
            ServiceResult result = this.dvtService.DVT_Update(updateInput);
            return result;
        }

    }
}
