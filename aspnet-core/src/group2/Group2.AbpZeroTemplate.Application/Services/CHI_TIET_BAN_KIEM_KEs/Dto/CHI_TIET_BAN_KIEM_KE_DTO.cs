using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs.Dto
{
    public class CHI_TIET_BAN_KIEM_KE_DTO
    {
        public string CTBKK_ID { get; set; }
        public string CTBKK_BKK_ID { get; set; }
	    public string CTBKK_MA_TB { get; set; }
	    public string CTBKK_TT { get; set; }
	    public string CTBKK_CHECK { get; set; }
	    public string CTBKK_TT_SAU { get; set; }

        public string CTBKK_TEN_TB { get; set; }
        public string CTBKK_DV_QL { get; set; }

	    public DateTime? CTBKK_THOI_GIAN { get; set; }
	    public string RECORD_STATUS { get; set; }
	    public string MAKER_ID { get; set; }
	    public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
	    public string CHECKER_ID { get; set; }
	    public DateTime? APPROVE_DT { get; set; }
    }
}
