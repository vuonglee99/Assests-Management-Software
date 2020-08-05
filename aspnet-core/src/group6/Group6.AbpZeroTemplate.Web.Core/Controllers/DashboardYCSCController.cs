using System;
using System.Collections.Generic;
using Abp.AspNetCore.Mvc.Controllers;
using Group6.AbpZeroTemplate.Web.Core.Services.DashboardYCSC;
using Group6.AbpZeroTemplate.Web.Core.Services.DashboardYCSC.Dto;
using Group6.AbpZeroTemplate.Web.Core.Services.TrangThaiYeuCauSuaChuas;
using Microsoft.AspNetCore.Mvc;

namespace Group6.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DashboardYCSCController : AbpController
    {
        private readonly IYCSCStatisticsService YCSCStatisticsService;

        public DashboardYCSCController(IYCSCStatisticsService YCSCStatisticsService)
        {
            this.YCSCStatisticsService = YCSCStatisticsService;
        }

        [HttpGet]
        public List<YCSCStatisticsOutput_DTO> GetYCSCStatistics(YCSCStatisticsInput_DTO input)
        {
            return YCSCStatisticsService.GetYCSCStatistics(input);
        }
    }
}
