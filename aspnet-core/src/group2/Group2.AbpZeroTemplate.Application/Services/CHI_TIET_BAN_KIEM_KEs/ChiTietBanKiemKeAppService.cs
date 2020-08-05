using Abp.Application.Services;
using Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs.Dto;
using Group2.AbpZeroTemplate.Web.Core.Services.KIEM_KEs;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs.Dto;
using GSoft.AbpZeroTemplate;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs
{
    public interface IChiTietBanKiemKeAppService : IApplicationService
    {
        
        //get all chi tiết kiểm kê của một bản kiểm kê
        List<CHI_TIET_BAN_KIEM_KE_DTO> ChiTietKiemKe_Get(string bkk_ID);
        //xác nhận kiểm kê
        dynamic CTBKK_XacNhanKK(string dv_QL,string bkk_ID,DateTime ngay_TAO,string ma_TB,string tt_SAU);
        //insertn chi tiết kiểm kê
        dynamic ChiTietKiemKeInsert(CHI_TIET_BAN_KIEM_KE_DTO input);
        dynamic TaoChiTietKiemKe(string maKK, string maDV);
        List<CHI_TIET_BAN_KIEM_KE_DTO> ChiTietKiemKe_GetByIdKK_IdDV(string idKK,string idTB);
        dynamic CTBKK_XacNhanKK_Ten(string dv_QL, string bkk_ID, DateTime ngay_TAO, string ten_TB, string tt_SAU);
        DataSet DataFromStore(string storeName, object parameter);
        dynamic CTBKK_XacNhanKK_Offline_Ngay(string dv_QL, string bkk_ID, DateTime ngay_TAO, string ma_TB, string tt_SAU,DateTime kk_NGAY);
        String getDownloadPath();
    }
    public class ChiTietBanKiemKeAppService : BaseService, IChiTietBanKiemKeAppService
    {
        public readonly IAppFolders ctappFolders;
        public ChiTietBanKiemKeAppService(IAppFolders appFolders)
        {
            ctappFolders = appFolders;
        }

        public String getDownloadPath()
        {
            return ctappFolders.TempFileDownloadFolder;
        }

        private readonly ThietBiAppService thietbiAppService = new ThietBiAppService();
        public dynamic ChiTietKiemKeInsert(CHI_TIET_BAN_KIEM_KE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("ChiTietKiemKe_Insert", input).FirstOrDefault();
        }

        public List<CHI_TIET_BAN_KIEM_KE_DTO> ChiTietKiemKe_Get(string bkk_ID)
        {
            var listTBTonTai= procedureHelper.GetData<CHI_TIET_BAN_KIEM_KE_DTO>("ChiTietKiemKe_Get", new { BKK_ID = bkk_ID });
            var listTBLa= procedureHelper.GetData<CHI_TIET_BAN_KIEM_KE_DTO>("ChiTietKiemKeFE_GetThietBiLa", new { KK_ID = bkk_ID });
            return listTBTonTai.Concat(listTBLa).ToList();
        }

        public List<CHI_TIET_BAN_KIEM_KE_DTO> ChiTietKiemKe_GetByIdKK_IdDV(string idKK, string idTB)
        {
            return procedureHelper.GetData<CHI_TIET_BAN_KIEM_KE_DTO>("ChiTietKiemKe_GetByIdKK_IdDV", new { IDKK = idKK, IDTB = idTB });
        }

        public dynamic CTBKK_XacNhanKK(string dv_QL, string bkk_ID, DateTime ngay_TAO, string ma_TB, string tt_SAU)
        {
            return procedureHelper.GetData<dynamic>("ChiTietKiemKe_XacNhanKK", new
            {
                DV_QL=dv_QL,
                BKK_ID=bkk_ID,
                NGAY_TAO=ngay_TAO,
                MA_TB=ma_TB,
                TT_SAU=tt_SAU
            });
        }

        public dynamic CTBKK_XacNhanKK_Offline_Ngay(string dv_QL, string bkk_ID, DateTime ngay_TAO, string ma_TB, string tt_SAU, DateTime kk_NGAY)
        {
            return procedureHelper.GetData<dynamic>("ChiTietKiemKe_XacNhanKK_Offline_Ngay", new
            {
                DV_QL = dv_QL,
                BKK_ID = bkk_ID,
                NGAY_TAO = ngay_TAO,
                MA_TB = ma_TB,
                TT_SAU = tt_SAU,
                KK_NGAY = kk_NGAY
            });
        }

        public dynamic CTBKK_XacNhanKK_Ten(string dv_QL, string bkk_ID, DateTime ngay_TAO, string ten_TB, string tt_SAU)
        {
            var res = new KiemKeAppService().KiemKeOfflineCheckBKK(dv_QL, bkk_ID, ngay_TAO);
            if (res[0] == "Bản kiểm kê không tồn tại hoặc đóng")
                return new List<Object> { new { result = "Bản kiểm kê ko tồn tại" } };
            CHI_TIET_BAN_KIEM_KE_DTO ctkk = new CHI_TIET_BAN_KIEM_KE_DTO
            {
                CTBKK_BKK_ID = bkk_ID,
                CTBKK_TT_SAU = tt_SAU,
                CTBKK_THOI_GIAN = DateTime.Now,
                CTBKK_TEN_TB = ten_TB
            };
            return procedureHelper.GetData<dynamic>("ChiTietKiemKe_Insert_Ten", ctkk);
        }

        public DataSet DataFromStore(string storeName, object parameter)
        {
            return procedureHelper.DataFromStore(storeName, parameter);
        }

        public dynamic TaoChiTietKiemKe(string maKK, string maDV)
        {
            List<THIET_BI_DTO> lst = thietbiAppService.GetAllThietBi();

            List<THIET_BI_DTO> lstnew = new List<THIET_BI_DTO>();
            foreach (var tb in lst)
                if (tb.TB_DV_QL == maDV)
                    lstnew.Add(tb);
            foreach (var tb in lstnew)
            {
                CHI_TIET_BAN_KIEM_KE_DTO ctkk = new CHI_TIET_BAN_KIEM_KE_DTO
                {
                    CTBKK_BKK_ID = maKK,
                    CTBKK_MA_TB = tb.TB_ID
                };
                procedureHelper.GetData<dynamic>("ChiTietKiemKe_Insert", ctkk);
            };
            return 10;
        }
    }
}
