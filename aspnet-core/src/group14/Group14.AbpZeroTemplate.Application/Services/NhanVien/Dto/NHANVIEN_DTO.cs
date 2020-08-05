using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group14.AbpZeroTemplate.Web.Core.Services.NhanVien.Dto
{
    public class NHANVIEN_DTO
    {
        public int? NV_ID { get; set; }
        public string NV_MA_NV { get; set; }
        public string NV_TEN { get; set; }
        public string NV_PHONG_BAN { get; set; }
        public string NV_TEN_PHONG_BAN { get; set; }
        public string NV_CHUC_VU { get; set; }
        public string NV_SDT { get; set; }
        public int? NV_TRANG_THAI { get; set; }
        public string NV_CMND { get; set; }
        public DateTime? NV_NGAY_CAP_CMND { get; set; }
        public string NV_NOI_CAP_CMND { get; set; }
        public string NV_MA_SO_THUE { get; set; }
        public string NV_EMAIL { get; set; }
        public string NV_DIA_CHI { get; set; }
        public string NV_MO_TA { get; set; }
		public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string BRANCH_CREATE { get; set; }

    }
}
