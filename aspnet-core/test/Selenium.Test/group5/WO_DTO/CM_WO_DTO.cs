using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Test.group5.WO_DTO
{
    class CM_WO_DTO
    {
        public string WO_ID { get; set; }
        public string WO_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public string DESCRIPTIONS { get; set; }
        public string PRIORITY_ORDER { get; set; }
        public string RECORD_STATUS { get; set; }
        public string KIND_FIX { get; set; }
        public string FIXER { get; set; }
        public DateTime? DATE_IN { get; set; }
        public string DESCRIPTIONS_ERROR { get; set; }
        public DateTime? DATE_OUT { get; set; }
    }
}
