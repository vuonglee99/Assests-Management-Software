using System;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group3.AbpZeroTemplate.Web.Core.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Group3.AbpZeroTemplate.Web.Core.DTOs;
using System.Collections.Generic;


namespace Group3.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServicesController : AbpController
    {
        private readonly IServicesAppService _servicesAppService = null;
        public ServicesController(IServicesAppService servicesAppService)
        {
            _servicesAppService = servicesAppService;
        }

        [HttpGet]
        public PagedResultDto<ServiceSearchDTO> ServicesSearch(ServiceSearchRequest filter)
        {
            return _servicesAppService.SearchServices(filter);
        }
    }
}
