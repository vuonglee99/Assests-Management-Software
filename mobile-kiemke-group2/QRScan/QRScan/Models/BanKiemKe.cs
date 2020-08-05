using System;
using System.Collections.Generic;
using System.Text;

namespace QRScan.Models
{
    public class BanKiemKe
    {
        public string KK_ID { get; set; }
        public string KK_CODE { get; set; }
        public DateTime KK_NGAYTAO { get; set; }
        public string KK_NGUOITAO { get; set; }
        public string KK_MADONVI { get; set; }
        public string KK_TENDONVI { get; set; }
        public DateTime KK_NGAYCHOT { get; set; }
        public int KK_TONGTB_DUOCKIEMKE { get; set; }
    }
}
