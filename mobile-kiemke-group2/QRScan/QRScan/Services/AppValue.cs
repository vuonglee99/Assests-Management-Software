using QRScan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRScan.Services
{
    public class AppValue
    {
        public static int KKType { get; set; }//1: online, 2:offline
        public static List<BanKiemKe> banKiemKes { get; set; }//dành cho online
        public static BanKiemKe banKiemKe { get; set; }//Dành cho online
        public static KiemKe kiemKe { get; set; } //Dành cho offline

    }
}
