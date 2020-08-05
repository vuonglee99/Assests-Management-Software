using Abp.Application.Services;
using Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs;
using Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs.Dto;
using Group2.AbpZeroTemplate.Web.Core.Services.KIEM_KEs.Dto;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Web.Core.Services.KIEM_KEs
{
    public interface IKiemKeAppService : IApplicationService
    {
        KIEM_KE_DTO KiemKeById(string kiemkeId);
        dynamic BKK_CHECK(string dv_QL,string bkk_ID,DateTime ngay_TAO);
        dynamic KiemKeUpdate(KIEM_KE_DTO input);
        dynamic KiemKeInsert(KIEM_KE_DTO input);
        //dynamic KiemKeOfflineProcess(string DONVI_ID, string BKK_CODE, DateTime NGAY_TAO, string listtb);
        List<string> KiemKeOfflineCheckBKK(string DONVI_ID, string BKK_CODE, DateTime NGAY_TAO);

        dynamic GetKiemKeDateAndCode(string branch_ID);
        List<string> GetBranchIdFromUserId(int id);
        dynamic KiemKeOfflineProcess(CUSTOM_DTO input);
    }
    public class KiemKeAppService : BaseService, IKiemKeAppService
    {
        public dynamic BKK_CHECK(string dv_QL, string bkk_ID, DateTime ngay_TAO)
        {
            return procedureHelper.GetData<dynamic>("KiemKe_CHECK", new
            {
                DV_QL = dv_QL,
                BKK_ID = bkk_ID,
                NGAY_TAO = ngay_TAO
            });
        }

        public List<string> GetBranchIdFromUserId(int id)
        {
            return procedureHelper.GetData<string>("GetBranchIdFromUserId", new { id = id });
        }

        public dynamic GetKiemKeDateAndCode(string branch_ID)
        {
            return procedureHelper.GetData<dynamic>("getkiemke", new { branch_id = branch_ID });
        }

        public KIEM_KE_DTO KiemKeById(string kiemkeId)
        {
            return procedureHelper.GetData<KIEM_KE_DTO>("KiemKe_GetById", new { BKK_ID = kiemkeId }).FirstOrDefault();
        }

        public dynamic KiemKeInsert(KIEM_KE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("KiemKe_Insert", input);
        }

        public List<string> KiemKeOfflineCheckBKK(string DONVI_ID, string BKK_CODE, DateTime NGAY_TAO)
        {
            return procedureHelper.GetData<string>("KiemKeofflineCheckBKK", new
            {
                DONVI_ID = DONVI_ID,
                BKK_CODE = BKK_CODE,
                NGAY_TAO = NGAY_TAO,
            });
        }

        public dynamic KiemKeOfflineProcess(CUSTOM_DTO input)
        {
            var res = this.KiemKeOfflineCheckBKK(input.DONVI_ID, input.BKK_CODE, input.NGAY_TAO);
            if (res[0] == "Bản kiểm kê không tồn tại hoặc đóng")
                return "Bản kiểm kê ko tồn tại";
            //get all device

            List<THIET_BI_DTO> lstdevice = new ThietBiAppService().GetAllThietBi();

            List<CHI_TIET_BAN_KIEM_KE_DTO> lstCTKK = new List<CHI_TIET_BAN_KIEM_KE_DTO>();
            lstCTKK = JsonConvert.DeserializeObject<List<CHI_TIET_BAN_KIEM_KE_DTO>>(input.LISTTB);
            Console.WriteLine("so ctbkk la: " + lstCTKK.Count);
            string tempstr = string.Empty;

            foreach (var elem in lstCTKK)
            {
                if (String.IsNullOrEmpty(elem.CTBKK_MA_TB) && !string.IsNullOrEmpty(elem.CTBKK_TEN_TB))//tay
                {
                    CHI_TIET_BAN_KIEM_KE_DTO ctkk = new CHI_TIET_BAN_KIEM_KE_DTO
                    {
                        CTBKK_BKK_ID = input.BKK_CODE,
                        CTBKK_TT_SAU = elem.CTBKK_TT_SAU,
                        CTBKK_THOI_GIAN = DateTime.Now,
                        CTBKK_TEN_TB = elem.CTBKK_TEN_TB
                    };
                    procedureHelper.GetData<dynamic>("ChiTietKiemKe_Insert_Ten", ctkk);
                }
                else
                {
                    var check = from j in lstdevice
                                where j.TB_ID.ToUpper() == elem.CTBKK_MA_TB.ToUpper()
                                select j;
                    if (check.Count() == 0)
                        tempstr += elem.CTBKK_MA_TB + "#";
                    else
                        new ChiTietBanKiemKeAppService(null).CTBKK_XacNhanKK(input.DONVI_ID, input.BKK_CODE, input.NGAY_TAO, elem.CTBKK_MA_TB, elem.CTBKK_TT_SAU);
                }
            }
            return "danh sách mã tb ko tồn tại trong db " + "|" + tempstr;
            //return "bkk hợp lệ";
        }

        /*public dynamic KiemKeOfflineProcess(string DONVI_ID, string BKK_CODE, DateTime NGAY_TAO, string listtb)
        {
            var res = this.KiemKeOfflineCheckBKK(DONVI_ID, BKK_CODE, NGAY_TAO);
            if (res[0] == "Bản kiểm kê không tồn tại hoặc đóng")
                return "Bản kiểm kê ko tồn tại";
            //get all device
            List<THIET_BI_DTO> lstdevice = new ThietBiAppService().GetAllThietBi();
            List<CHI_TIET_BAN_KIEM_KE_DTO> lstCTKK = new List<CHI_TIET_BAN_KIEM_KE_DTO>();
            lstCTKK = JsonConvert.DeserializeObject<List<CHI_TIET_BAN_KIEM_KE_DTO>>(listtb);

            string tempstr = string.Empty;
            foreach (var elem in lstCTKK)
            {
                if (String.IsNullOrEmpty(elem.CTBKK_MA_TB) && !string.IsNullOrEmpty(elem.CTBKK_TEN_TB))//tay
                {
                    CHI_TIET_BAN_KIEM_KE_DTO ctkk = new CHI_TIET_BAN_KIEM_KE_DTO
                    {
                        CTBKK_BKK_ID = BKK_CODE,
                        CTBKK_TT_SAU = elem.CTBKK_TT_SAU,
                        CTBKK_THOI_GIAN =(DateTime) elem.CTBKK_THOI_GIAN,
                        CTBKK_TEN_TB = elem.CTBKK_TEN_TB
                    };
                    procedureHelper.GetData<dynamic>("ChiTietKiemKe_Insert_Ten", ctkk);
                }
                else
                {
                    var check = from j in lstdevice
                                where j.TB_ID == elem.CTBKK_MA_TB
                                select j;
                    if (check.Count() == 0)
                        tempstr += elem.CTBKK_MA_TB + "#";
                    else
                        //new ChiTietBanKiemKeAppService().CTBKK_XacNhanKK(DONVI_ID, BKK_CODE, NGAY_TAO, elem.CTBKK_MA_TB, elem.CTBKK_TT_SAU);
                        new ChiTietBanKiemKeAppService().CTBKK_XacNhanKK_Offline_Ngay(DONVI_ID, BKK_CODE, NGAY_TAO, elem.CTBKK_MA_TB, elem.CTBKK_TT_SAU,(DateTime)elem.CTBKK_THOI_GIAN);
                }
            }
            return "danh sách mã tb ko tồn tại trong db " + "|" + tempstr;
			//return "bkk hợp lệ";
        }*/

        /*public dynamic KiemKeOfflineProcess(string DONVI_ID, string BKK_CODE, DateTime NGAY_TAO, string listtb)
        {
            var res = this.KiemKeOfflineCheckBKK(DONVI_ID, BKK_CODE, NGAY_TAO);
            if (res[0] == "Bản kiểm kê không tồn tại hoặc đóng")
                return "Bản kiểm kê ko tồn tại";
            //get all device

            List<THIET_BI_DTO> lstdevice = new ThietBiAppService().GetAllThietBi();

            List<CHI_TIET_BAN_KIEM_KE_DTO> lstCTKK = new List<CHI_TIET_BAN_KIEM_KE_DTO>();
            lstCTKK = JsonConvert.DeserializeObject<List<CHI_TIET_BAN_KIEM_KE_DTO>>(listtb);

            string tempstr = string.Empty;

            foreach (var elem in lstCTKK)
            {
                if (String.IsNullOrEmpty(elem.CTBKK_MA_TB) && !string.IsNullOrEmpty(elem.CTBKK_TEN_TB))//tay
                {
                    CHI_TIET_BAN_KIEM_KE_DTO ctkk = new CHI_TIET_BAN_KIEM_KE_DTO
                    {
                        CTBKK_BKK_ID = BKK_CODE,
                        CTBKK_TT_SAU = elem.CTBKK_TT_SAU,
                        CTBKK_THOI_GIAN = DateTime.Now,
                        CTBKK_TEN_TB = elem.CTBKK_TEN_TB
                    };
                    procedureHelper.GetData<dynamic>("ChiTietKiemKe_Insert_Ten", ctkk);
                }
                else
                {
                    var check = from j in lstdevice
                                where j.TB_ID == elem.CTBKK_MA_TB
                                select j;
                    if (check.Count() == 0)
                        tempstr += elem.CTBKK_MA_TB + "#";
                    else
                        new ChiTietBanKiemKeAppService().CTBKK_XacNhanKK(DONVI_ID, BKK_CODE, NGAY_TAO, elem.CTBKK_MA_TB, elem.CTBKK_TT_SAU);

                }
            }
            return "danh sách mã tb ko tồn tại trong db " + "|" + tempstr;
            //return "bkk hợp lệ";
        }*/


        public dynamic KiemKeUpdate(KIEM_KE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("KiemKe_Update", input);
        }
    }
}
