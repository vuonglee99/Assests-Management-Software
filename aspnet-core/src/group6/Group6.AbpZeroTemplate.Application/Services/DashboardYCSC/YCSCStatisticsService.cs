using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Abp.Application.Services;
using Group6.AbpZeroTemplate.Web.Core.Services.DashboardYCSC.Dto;
using Group6.AbpZeroTemplate.Web.Core.Services.TrangThaiYeuCauSuaChuas.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using GSoft.AbpZeroTemplate.MultiTenancy.HostDashboard.Dto;

namespace Group6.AbpZeroTemplate.Web.Core.Services.DashboardYCSC
{
    public interface IYCSCStatisticsService : IApplicationService
    {
        List<YCSCStatisticsOutput_DTO> GetYCSCStatistics(YCSCStatisticsInput_DTO input);
    }

    //[AbpAuthorize(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua)]
    public class YCSCStatisticsService : BaseService, IYCSCStatisticsService
    {
        public List<YCSCStatisticsOutput_DTO> GetYCSCStatistics(YCSCStatisticsInput_DTO input)
        {
            return GetYCSCStatisticsData(input.StartDate, input.EndDate, input.IncomeStatisticsDateInterval);
        }

        public List<YCSCStatisticsOutput_DTO> GetYCSCStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval)
        {
            List<YCSCStatisticsOutput_DTO> ycscStastisticsOut;

            switch (dateInterval)
            {
                case ChartDateInterval.Daily:
                    ycscStastisticsOut = GetDailyYCSCStatisticsData(startDate, endDate);
                    break;
                case ChartDateInterval.Weekly:
                    ycscStastisticsOut = GetWeeklyYCSCStatisticsData(startDate, endDate);
                    break;
                case ChartDateInterval.Monthly:
                    ycscStastisticsOut = GetMonthlyYCSCStatisticsData(startDate, endDate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dateInterval), dateInterval, null);
            }
            Console.WriteLine(ycscStastisticsOut.Count);
            ycscStastisticsOut.ForEach(a =>
            {
                a.YCSCStatistics.ForEach(i =>
                {
                    i.Label = i.Date.ToString("MM.dd.yyyy");
                });
            });

            return ycscStastisticsOut;
        }


        private List<YCSCStatisticsOutput_DTO> GetDailyYCSCStatisticsData(DateTime startdate, DateTime enddate)
        {
            List<TTYCSC_DTO> tt = procedureHelper.GetData<TTYCSC_DTO>("TTYCSC_SearchFilter", new
            {
                pageNumber = 1,
                pageSize = 10,
                APPROVE_STATUS = '_'
            });

            var dailyRecords = new List<YCSCStatisticsOutput_DTO>();
            tt.ForEach(x =>
            {
                var a = procedureHelper.GetData<YCSCStatistic>("YCSC_GetDailyStatisticsData", new
                {
                    startDate = startdate,
                    endDate = enddate,
                    status = x.TTYCSC_CODE
                }).ToList();

                if(a.Count>0)
                {
                    int _total = 0;

                    a.ForEach(am => _total += am.Amount);

                    YCSCStatisticsOutput_DTO y = new YCSCStatisticsOutput_DTO(a, x.TTYCSC_CODE, _total);
                    dailyRecords.Add(y);
                }

            });
            //FillGapsInDailyYCSCStatistics(dailyRecords, startdate, enddate);

            Console.WriteLine(dailyRecords.Count);

            dailyRecords.ForEach(a =>
            {
                var currentDay = startdate;
                while (currentDay <= enddate)
                {
                    if (a.YCSCStatistics.All(d => d.Date != currentDay.Date))
                    {
                        a.YCSCStatistics.Add(new YCSCStatistic(currentDay));
                    }

                    currentDay = currentDay.AddDays(1);
                }

                //var m = a.YCSCStatistics.OrderBy(i => i.Date);
                //a.YCSCStatistics.Clear();
                //a.YCSCStatistics.AddRange(m);
                a.YCSCStatistics.Sort((c, d) => c.Date.CompareTo(d.Date));
            });
            return dailyRecords;
        }

        private static void FillGapsInDailyYCSCStatistics(List<YCSCStatisticsOutput_DTO> dailyRecords, DateTime startDate, DateTime endDate)
        {
            dailyRecords.ForEach(a =>
            {
                var currentDay = startDate;
                var ycscStastistics = new List<YCSCStatistic>();
                while (currentDay <= endDate)
                {
                    if (a.YCSCStatistics.All(d => d.Date != currentDay.Date))
                    {
                        a.YCSCStatistics.Add(new YCSCStatistic(currentDay));
                    }

                    currentDay = currentDay.AddDays(1);
                }
                a.YCSCStatistics.Sort((c, d) => c.Date.CompareTo(d.Date));
            });

        }

        private List<YCSCStatisticsOutput_DTO> GetWeeklyYCSCStatisticsData(DateTime startDate, DateTime endDate)
        {
            var dailyRecords = GetDailyYCSCStatisticsData(startDate, endDate);

            
            dailyRecords.ForEach(a =>
            {
                var firstDayOfWeek = DateTimeFormatInfo.CurrentInfo == null
                ? DayOfWeek.Sunday
                : DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
                
                var ycscStastistics = new List<YCSCStatistic>();
                int weeklyAmount = 0;
                var weekStart = a.YCSCStatistics.First().Date;
                var isFirstWeek = weekStart.DayOfWeek == firstDayOfWeek;

                a.YCSCStatistics.ForEach(dailyRecord =>
                {
                    if (dailyRecord.Date.DayOfWeek == firstDayOfWeek)
                    {
                        if (!isFirstWeek)
                        {
                            ycscStastistics.Add(new YCSCStatistic(weekStart, weeklyAmount));
                        }

                        isFirstWeek = false;
                        weekStart = dailyRecord.Date;
                        weeklyAmount = 0;
                    }

                    weeklyAmount += dailyRecord.Amount;
                });

                ycscStastistics.Add(new YCSCStatistic(weekStart, weeklyAmount));
                a.YCSCStatistics.Clear();
                a.YCSCStatistics.AddRange(ycscStastistics.OrderBy(i=>i.Date));
            });
            ;
            return dailyRecords;
        }

        private List<YCSCStatisticsOutput_DTO> GetMonthlyYCSCStatisticsData(DateTime startDate, DateTime endDate)
        {
            var dailyRecords = GetDailyYCSCStatisticsData(startDate, endDate);

            dailyRecords.ForEach(a =>
            {
                var query = a.YCSCStatistics.GroupBy(d => new
                {
                    d.Date.Year,
                    d.Date.Month
                })
                .Select(grouping => new YCSCStatistic
                {
                    Date = FindMonthlyDate(startDate, grouping.Key.Year, grouping.Key.Month),
                    Amount = grouping.DefaultIfEmpty().Sum(x => x.Amount)
                }).ToList();

                a.YCSCStatistics.Clear();
                a.YCSCStatistics.AddRange(query.OrderBy(i=>i.Date));
            });

            return dailyRecords;
        }

        private static DateTime FindMonthlyDate(DateTime startDate, int groupYear, int groupMonth)
        {
            if (groupYear == startDate.Year && groupMonth == startDate.Month)
            {
                return new DateTime(groupYear, groupMonth, startDate.Day);
            }

            return new DateTime(groupYear, groupMonth, 1);
        }
    }
}
