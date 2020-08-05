using System;
using System.Collections.Generic;

namespace Group6.AbpZeroTemplate.Web.Core.Services.DashboardYCSC.Dto
{
    public class YCSCStatisticsOutput_DTO
    {
        public string Status_YCSC { set; get; }
        public int Total_Status { set; get; }
        public List<YCSCStatistic> YCSCStatistics { get; set; }

        public YCSCStatisticsOutput_DTO(List<YCSCStatistic> ycscStatistics)
        {
            YCSCStatistics = ycscStatistics;
        }
        public YCSCStatisticsOutput_DTO(List<YCSCStatistic> ycscStatistics, string status, int total) {
            YCSCStatistics = ycscStatistics;
            Status_YCSC = status;
            Total_Status = total;
        }
    }
}
