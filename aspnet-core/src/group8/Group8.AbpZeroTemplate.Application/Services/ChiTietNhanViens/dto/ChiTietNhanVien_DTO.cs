using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.AbpZeroTemplate.Web.Core.Services.ChiTietNhanViens.dto
{
    public class ChiTietNhanVien_DTO
    {
        public string CTNV_MANV { get; set; }
        public string CTNV_TEN_NV { get; set; }
        public string CTNV_CHUC_VU { get; set; }
        public string CTNV_SDT { get; set; }
        public string CTNV_PHONG_BAN { get; set; }
        public string CTNV_CMND { get; set; }
        public string CTNV_DIA_CHI { get; set; }
        public DateTime? CTNV_NGAY_CAP_CMND { get; set; }
        public string CTNV_NOI_CAP_CMND { get; set; }
        public string CTNV_MA_SO_THUE { get; set; }
        public string CTNV_TRANG_THAI_HOAT_DONG { get; set; }
        public string CTNV_EMAIL { get; set; }
        public string CTNV_MO_TA { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }

}
