using System;

namespace Group6.AbpZeroTemplate.Web.Core.Services.DashboardYCSC.Dto
{
    public class YCSCStatistic
    {
        public string Label { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }

        public YCSCStatistic()
        {
        }

        public YCSCStatistic(DateTime date)
        {
            Date = date;
        }

        public YCSCStatistic(DateTime date, int amount)
        {
            Date = date;
            Amount = amount;
        }
    }
}
