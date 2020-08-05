using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Test.DVT_DTO
{
    class CM_DVT_DTO
    {
        public string DVT_ID { get; set; }
        public string DVT_CODE { get; set; }
        public string DVT_NAME { get; set; }
        public string DVT_DESC { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }

        public CM_DVT_DTO() { }
        public CM_DVT_DTO (string code,string name, string desc)
        {
            this.DVT_CODE = code;
            this.DVT_NAME = name;
            this.DVT_DESC = desc;
        }
    }
}
