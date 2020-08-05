using QRScan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRScan.Services
{
    public class DataProvider
    {
        public static KiemKe GetKiemKe(BanKiemKe banKiemKe)
        {
            if (LocalData.KiemKes == null) return null;
            foreach (KiemKe kiemKe in LocalData.KiemKes)
                if (kiemKe.BKK.KK_CODE == banKiemKe.KK_CODE && kiemKe.BKK.KK_MADONVI == banKiemKe.KK_MADONVI
                    && CheckSameDate(kiemKe.BKK.KK_NGAYTAO,banKiemKe.KK_NGAYTAO))
                    return kiemKe;

            return null;
        }

        public static bool CheckSameDate(DateTime date1,DateTime date2)
        {
            if (date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day) return true;
            return false;
        }

        public static List<BanKiemKe> GetListBKK()
        {
            List<BanKiemKe> banKiemKes = new List<BanKiemKe>();
            foreach(KiemKe kiemke in LocalData.KiemKes)
            {
                banKiemKes.Add(kiemke.BKK);
            }
            return banKiemKes;
        }

        public static KiemKe RemoveRecordFromKiemKe(KiemKe kiemke, ChiTietBanKiemKe record)
        {
            foreach(ChiTietBanKiemKe item in kiemke.RECORDS)
                if(item.CTBKK_THOI_GIAN==record.CTBKK_THOI_GIAN)
                {
                    kiemke.RECORDS.Remove(item);
                    break;
                }
            return kiemke;
        }

        public static List<ChiTietBanKiemKe> CloneRecordsFromKiemKe(KiemKe kiemke)
        {
            List<ChiTietBanKiemKe> result = new List<ChiTietBanKiemKe>();
            foreach(ChiTietBanKiemKe item in kiemke.RECORDS)
            {
                ChiTietBanKiemKe ct = new ChiTietBanKiemKe()
                {
                    CTBKK_MA_TB = item.CTBKK_MA_TB,
                    CTBKK_TEN_TB = item.CTBKK_TEN_TB,
                    CTBKK_TT_SAU = item.CTBKK_TT_SAU
                };
                result.Add(ct);
            }
            return result;
        }
    }
}
