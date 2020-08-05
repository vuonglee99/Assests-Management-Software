using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group0.AbpZeroTemplate.Web.Core.Services.CM_XEs.Dto
{
    public class CM_XE_DTO
    {
        public string XE_ID { get; set; }
        public string XE_CODE { get; set; }
        public string XE_NAME { get; set; }
        public string XE_COLOR { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }
}
