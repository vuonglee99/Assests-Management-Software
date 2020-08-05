using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.AbpZeroTemplate.Web.Core.Services.ChiTietThietBiVatTu.DTO
{
    public class ThietBiVatTu_Dto
    {
        public int TBVT_ID { get; set; }
        public string TBVT_MA_TBVT { get; set; }
        public string TBVT_TEN { get; set; }
        public string TBVT_SERIAL { get; set; }
        public string TBVT_LOAI { get; set; }
        public DateTime? TBVT_NGAY_MUA { get; set; }
        public string TBVT_DVT { get; set; }
        public string TBVT_NHAP_THEO_LO { get; set; }
        public int TBVT_SL_THEO_LO { get; set; }
        public string TBVT_HANG_SX { get; set; }
        public string TBVT_NAM_SX { get; set; }
        public DateTime? TBVT_NGAY_TINH_BAO_HANH { get; set; }
        public DateTime? TBVT_NGAY_KET_THUC_BAO_HANH { get; set; }
        public string TBVT_NHA_CUNG_CAP { get; set; }
        public string TBVT_TINH_TRANG_THIET_BI { get; set; }
        public string TBVT_GHI_CHU_TINH_TRANG { get; set; }
        public string TBVT_CAN_BAO_DUONG { get; set; }
        public string TBVT_CHU_KY_BAO_DUONG { get; set; }
        public string TBVT_NOI_DUNG_BAO_DUONG { get; set; }
        public string TBVT_TI_LE_HAO_MON { get; set; }
        public string TBVT_TINH_TRANG_TBTBVT { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string BRANCH_CREATE { get; set; }
    }

}
