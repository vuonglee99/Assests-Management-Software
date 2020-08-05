using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_PhieuThueXe.PhieuThueXe_DTO
{
    public class PTX_DTO
    {
        public string PTX_ID { get; set; }
        public string PTX_CODE { get; set; }
        public DateTime PTX_RENT_DT { get; set; }
        public DateTime PTX_EXP_DT { get; set; }
        public DateTime PTX_RETURN_DT { get; set; }
        public decimal PTX_PRICE { get; set; }
        public string PTX_NOTE { get; set; }
        public string XE_ID { get; set; }
        public string NTX_ID { get; set; }
        public DateTime CREATED_DT { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime APPROVE_DT { get; set; }
    }
}
