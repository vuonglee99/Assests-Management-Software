﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Group10.AbpZeroTemplate.Web.Core.Services.CM_NSX.Dto
{
    public class CM_NSX_DTO
    {
        public string NSX_ID { get; set; }
        public string NSX_CODE { get; set; }
        public string NSX_NAME { get; set; }
        public string NSX_FROM { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string BRANCH_CREATE { get; set; }
    }
}
