﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Test.TTYCSC_DTO
{
    class CM_TTYCSC_DTO
    {
        public string TTYCSC_ID { get; set; }
        public string TTYCSC_CODE { get; set; }
        public string TTYCSC_NAME { get; set; }
        public string TTYCSC_DESC { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }

        public CM_TTYCSC_DTO() { }
        public CM_TTYCSC_DTO(string code, string name, string desc)
        {
            this.TTYCSC_CODE = code;
            this.TTYCSC_NAME = name;
            this.TTYCSC_DESC = desc;
        }
    }
}
