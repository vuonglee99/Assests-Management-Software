using System;

namespace Group6.AbpZeroTemplate.Web.Core.Services.DoUuTiens.Dto
{
    public class DUT_DTO
    {
        public string DUT_ID { get; set; }
        public string DUT_CODE { get; set; }
        public string DUT_NAME { get; set; }
        public string DUT_DESC { get; set; }
        public int    DUT_LEVEL { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }
}