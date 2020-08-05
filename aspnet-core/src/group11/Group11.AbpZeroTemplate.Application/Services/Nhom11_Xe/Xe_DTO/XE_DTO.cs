using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_Xe.Xe_DTO
{
    public class XE_DTO
    {
        public string XE_ID { get; set; }
        public string XE_CODE { get; set; }
        public string XE_NAME { get; set; }
        public string XE_COLOR { get; set; }
        public int XE_SEATS { get; set; }
        public string XE_MODEL { get; set; }
        public string XE_LICENSE_PLATE { get; set; }
        public int XE_PRICE { get; set; }
        public int XE_CONSUMPTION { get; set; }
        public string XE_NOTES { get; set; }
        public int XE_MAX_SPEED { get; set; }
        public string XE_MANUFACTURER { get; set; }
        public int XE_MANUFACTURE_YEAR { get; set; }
        public string XE_STATUS { get; set; }
        public DateTime CREATED_DT { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime APPROVE_DT { get; set; }
        public int XE_TOTAL_KM { get; set; }
        public string XE_NEED_MAINTENANCE { get; set; }
        public int XE_LAST_TOTAL_KM { get; set; }
        public decimal XE_LAST_DISTANCE { get; set; }
    }
}
