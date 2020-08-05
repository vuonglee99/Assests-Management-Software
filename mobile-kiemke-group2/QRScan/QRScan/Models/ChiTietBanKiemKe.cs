using System;
using System.Collections.Generic;
using System.Text;

namespace QRScan.Models
{
     public class ChiTietBanKiemKe
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
