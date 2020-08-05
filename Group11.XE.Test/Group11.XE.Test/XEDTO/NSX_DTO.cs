using System;
using System.Collections.Generic;
using System.Text;

namespace Group11.XE.Test.NSXDTO
{
    public class NSX_DTO
    {
        public string NSX_ID { get; set; }
        public string NSX_CODE { get; set; }
        public string NSX_NAME { get; set; }
        public string NSX_ORIGIN { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string BRANCH_CREATE { get; set; }

        public NSX_DTO() { }

        public NSX_DTO(String code, String name, String origin)
        {

            this.NSX_CODE = code;
            this.NSX_NAME = name;
            this.NSX_ORIGIN = origin;

        }
    }
}
