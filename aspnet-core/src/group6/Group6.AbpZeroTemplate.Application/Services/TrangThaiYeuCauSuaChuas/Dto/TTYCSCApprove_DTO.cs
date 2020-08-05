using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6.AbpZeroTemplate.Web.Core.Services.TrangThaiYeuCauSuaChuas.Dto
{
    public class TTYCSCApprove_DTO
    {
        public string TTYCSC_ID { get; set; }
        public string TTYCSC_CODE { get; set; }
        public string TTYCSC_NAME { get; set; }
        public string TTYCSC_DESC { get; set; }
        public string CHECKER_ID { get; set; }
        public string TYPE_APPROVE { get; set; }
        public int isAPPROVE { get; set; }
    }
}
