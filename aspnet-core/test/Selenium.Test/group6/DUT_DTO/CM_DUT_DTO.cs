using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Test.DUT_DTO
{
    class CM_DUT_DTO
    {
        public string DUT_ID { get; set; }
        public string DUT_CODE { get; set; }
        public string DUT_NAME { get; set; }
        public string DUT_DESC { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }

        public CM_DUT_DTO() { }
        public CM_DUT_DTO (string name, string desc)
        {
           
            this.DUT_NAME = name;
            this.DUT_DESC = desc;
        }
    }
}
