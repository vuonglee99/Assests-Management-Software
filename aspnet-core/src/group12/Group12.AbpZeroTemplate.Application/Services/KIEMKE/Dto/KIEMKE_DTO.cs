using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group12.AbpZeroTemplate.Web.Core.Services.KIEMKE.Dto
{
    public class KIEMKE_DTO
    {
        public string KK_ID { get; set; }
        public string KK_CODE { get; set; }
        public string KK_NGAYTAO { get; set; }
        public string KK_NGUOITAO { get; set; }
        public string KK_MADONVI { get; set; }
        public string KK_TENDONVI { get; set; }
        public string KK_NGAYCHOT { get; set; }
        public int? KK_TONGTB_DUOCKIEMKE { get; set; }
        public int? KK_TONGTB_DUSOVOISAOKE { get; set; }
        public int? KK_TONGTB_THIEUSOVOISAOKE { get; set; }
        public int? KK_TONGTB_THUASOVOISAOKE { get; set; }
        public string KK_TRANGTHAI { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public string CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public string APPROVE_DT { get; set; }

    }
}
