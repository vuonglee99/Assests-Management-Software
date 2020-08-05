using System;
using System.Collections.Generic;
using System.Text;
using Selenium.Test.group10.DTO;

namespace Selenium.Test.group10.DTO
{
    public class BD_DTO
    {
        public string BD_ID { get; set; }
        public string BD_CODE { get; set; }
        public string BD_GARAGE { get; set; }
        public string BD_ADDRESS { get; set; }
        public int BD_PRICE { get; set; }
        public string XE_ID { get; set; }
        public DateTime BD_FROM_DT { get; set; }
        public DateTime BD_TO_DT { get; set; }
        public DateTime CREATED_DT { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime APPROVE_DT { get; set; }
        public int BD_LAST_DISTANCE { get; set; }
        public List<ChiTietBaoDuong_DTO> BD_DETAILS { get; set; }
    }
}
