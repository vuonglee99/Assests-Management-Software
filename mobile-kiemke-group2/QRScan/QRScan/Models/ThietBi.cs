using System;
using System.Collections.Generic;
using System.Text;

namespace QRScan.Models
{
    public class ThietBi
    {
        public string TB_ID { get; set; }
        public string TB_TEN { get; set; }
        public string TB_TT_HOAT_DONG { get; set; }
        public string TB_DV_QL { get; set; }
        public string TB_CHU_KY_BAO_DUONG { get; set; }
        public DateTime TB_NGAY_MUA { get; set; }
        public DateTime TB_NGAY_BH { get; set; }
        public DateTime TB_NGAY_HET_BH { get; set; }
        public string TB_MO_TA { get; set; }
        public string TB_NSX { get; set; }
        public int TB_Nam_SX { get; set; }
        public string TB_SERIAL { get; set; }
        public string TB_TEN_DV { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime APPROVE_DT { get; set; }
    }
}
