using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_NhaSanXuat.NhaSanXuat_DTO
{
    public class NSX_DTO
    {
        public string NSX_ID { get; set; }
        public string NSX_CODE { get; set; }
        public string NSX_NAME { get; set; }
        public string NSX_FROM { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }
}
