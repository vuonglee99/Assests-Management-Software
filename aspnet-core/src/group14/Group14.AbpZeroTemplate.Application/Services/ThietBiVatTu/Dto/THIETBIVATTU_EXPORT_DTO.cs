using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Group14.AbpZeroTemplate.Web.Core.Services.ThietBiVatTu.Dto
{
    public class THIETBIVATTU_EXPORT_DTO
    {
        public string TBVT_MA_TBVT { get; set; }
        public string TBVT_TEN { get; set; }
        public string TBVT_SERIAL { get; set; }
        public string TBVT_LOAI { get; set; }
        public DateTime? TBVT_NGAY_MUA { get; set; }
        public string TBVT_TEN_DVT { get; set; }
        public string TBVT_NHAP_THEO_LO { get; set; }
        public string TBVT_SL_THEO_LO { get; set; }
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
    }
}