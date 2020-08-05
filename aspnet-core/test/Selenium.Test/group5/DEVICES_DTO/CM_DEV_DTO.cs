using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Test.group5.DEVICES_DTO
{
    public class CM_DEV_DTO
    {
        public string DEVICE_ID { get; set; }
        public string DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public string BRANCH_ID { get; set; }
        public string MAINTENANCE_CYCLE { get; set; }
        public string RECORD_STATUS { get; set; }
        public string ACTIVE_STATUS { get; set; }
        public DateTime? DATE_BUY { get; set; }
        public DateTime? DATE_WARRANTY_BEGIN { get; set; }
        public DateTime? DATE_WARRANTY_END { get; set; }
        public string DESCRIPTIONS { get; set; }
        public string PRODUCER_ID { get; set; }
        public int? YEAR_PRODUCTION { get; set; }
        public string SERIAL { get; set; }
        public string MAKER_ID { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public DateTime? APPROVE_DT { get; set; }

    }
}
