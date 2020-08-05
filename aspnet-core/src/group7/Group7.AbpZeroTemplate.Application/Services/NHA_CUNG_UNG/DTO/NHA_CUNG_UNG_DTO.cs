using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group7.AbpZeroTemplate.Web.Core.Services.NHA_CUNG_UNG.DTO
{
    public class NHA_CUNG_UNG_DTO
    {
        public string NCU_ID { get; set; }
        public string NCU_MA_NCU { get; set; }
        public string NCU_TEN { get; set; }
        public string NCU_DIA_CHI { get; set; }
        public string NCU_SDT { get; set; }
        public string NCU_MA_SO_THUE { get; set; }
        public string NCU_FAX { get; set; }
        public string NCU_TEN_NGUOI_LIEN_HE { get; set; }
        public string NCU_EMAIL_NGUOI_LIEN_HE { get; set; }
        public string NCU_SDT_NGUOI_LIEN_HE { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string BRANCH_CREATE { get; set; }
    }
}
