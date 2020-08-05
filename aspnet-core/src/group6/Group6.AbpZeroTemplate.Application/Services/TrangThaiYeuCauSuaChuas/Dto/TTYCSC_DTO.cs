using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6.AbpZeroTemplate.Web.Core.Services.TrangThaiYeuCauSuaChuas.Dto
{
    public class TTYCSC_DTO
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
        public string TYPE_APPROVE { get; set; }
    }
}
