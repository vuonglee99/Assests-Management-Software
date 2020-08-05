using QRScan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRScan.Services
{
    public class DataUpdater
    {
        public static void AddNewRecordToKiemKe(ChiTietBanKiemKe chiTietBanKiemKe)
        {
            if (string.IsNullOrEmpty(chiTietBanKiemKe.CTBKK_MA_TB))
            {
                //add vào record mới
                AppValue.kiemKe.RECORDS.Add(chiTietBanKiemKe);
                return;
            }
            //xóa đi record nếu đã tồn tại
            foreach (ChiTietBanKiemKe item in AppValue.kiemKe.RECORDS)
                if (item.CTBKK_MA_TB == chiTietBanKiemKe.CTBKK_MA_TB)
                {
                    AppValue.kiemKe.RECORDS.Remove(item);
                    break;
                }

            //add vào record mới
            AppValue.kiemKe.RECORDS.Add(chiTietBanKiemKe);
        }

        public static void UpdateKiemKeInLocalData()
        {
            foreach(KiemKe kiemke in LocalData.KiemKes)
            {
                BanKiemKe banKiemKe1 = kiemke.BKK;
                BanKiemKe banKiemKe2 = AppValue.kiemKe.BKK;
                if (banKiemKe1.KK_CODE == banKiemKe2.KK_CODE && banKiemKe1.KK_MADONVI == banKiemKe2.KK_MADONVI
                    && DataProvider.CheckSameDate(banKiemKe1.KK_NGAYTAO, banKiemKe2.KK_NGAYTAO))
                {
                    kiemke.RECORDS = AppValue.kiemKe.RECORDS;
                    break;
                }
            }

        }

        public static void UpdateDataToFile()
        {
            FileService.WriteFileData(LocalData.KiemKes);
        }

        public static void UpdateRecordInKiemKeInLocalData(KiemKe kiemke)
        {
            foreach(KiemKe item in LocalData.KiemKes)
            {
                BanKiemKe banKiemKe1 = item.BKK;
                BanKiemKe banKiemKe2 = kiemke.BKK;
                if (banKiemKe1.KK_CODE == banKiemKe2.KK_CODE && banKiemKe1.KK_MADONVI == banKiemKe2.KK_MADONVI
                    && DataProvider.CheckSameDate(banKiemKe1.KK_NGAYTAO, banKiemKe2.KK_NGAYTAO))
                {
                    item.RECORDS = kiemke.RECORDS;
                    break;
                }
            }
        }

        public static void RemoveEmptyKiemKe()
        {
            if (LocalData.KiemKes.Count == 0) return;
            List<KiemKe> emptys = new List<KiemKe>();
            foreach (KiemKe item in LocalData.KiemKes)
                if (item.RECORDS.Count == 0)
                    emptys.Add(item);

            foreach (KiemKe emptyItem in emptys)
                LocalData.KiemKes.Remove(emptyItem);
        }

        public static void RemoveRecordInKiemKe(KiemKe kiemke, ChiTietBanKiemKe deletedRecord)
        {
            foreach(KiemKe item in LocalData.KiemKes)
                if (item.BKK.KK_CODE == kiemke.BKK.KK_CODE && item.BKK.KK_MADONVI == kiemke.BKK.KK_MADONVI
                    && DataProvider.CheckSameDate(item.BKK.KK_NGAYTAO, kiemke.BKK.KK_NGAYTAO))
                {
                    foreach(ChiTietBanKiemKe record in item.RECORDS)
                        if (record.CTBKK_THOI_GIAN == deletedRecord.CTBKK_THOI_GIAN)
                        {
                            item.RECORDS.Remove(record);
                            break;
                        }
                    break;
                }
        }

        public static KiemKe RemoveRecordInKiemKe(KiemKe kiemke, string maThietBi)
        {
            foreach(ChiTietBanKiemKe record in kiemke.RECORDS)
                if (record.CTBKK_MA_TB == maThietBi)
                {
                    kiemke.RECORDS.Remove(record);
                    break;
                }
            return kiemke;
        }

        public static void RemoveKiemKeByBKK(BanKiemKe deletedBKK)
        {
            foreach (KiemKe kiemke in LocalData.KiemKes)
            {
                if (kiemke.BKK.KK_CODE == deletedBKK.KK_CODE && kiemke.BKK.KK_MADONVI == deletedBKK.KK_MADONVI
                    && DataProvider.CheckSameDate(kiemke.BKK.KK_NGAYTAO, deletedBKK.KK_NGAYTAO))
                {
                    LocalData.KiemKes.Remove(kiemke);
                    return;
                }
            }
        }

    }
}
