using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Web.Core.Services.LOAI_XEs.Dto
{
    public class LOAI_XE_DTO
    {
        public string LX_ID { get; set; }
        public string LX_TEN { get; set; }
        public string LX_MO_TA { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }

        //Bo sung
        public string LX_CODE { get; set; }
        public string LX_HANGXE { get; set; }
        public int LX_NAMSX { get; set; }
        public string LX_LOAINGUYENLIEU { get; set; }
        public float LX_DINHMUCNL { get; set; }
    }
}
