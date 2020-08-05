using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.AbpZeroTemplate.Web.Core.Services.PhuKien.dto
{
    public class PhuKien_DTO
    {
        public int? PHU_KIEN_ID { get; set; }
        public int? PHU_KIEN_TBVT_ID { get; set; }
        public string PHU_KIEN_MA_PK { get; set; }
        public string PHU_KIEN_TEN { get; set; }
        public string PHU_KIEN_DVT { get; set; }
        public int? PHU_KIEN_SO_LUONG { get; set; }
        public string PHU_KIEN_GHI_CHU { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string BRANCH_CREATE { get; set; }
    }

}
