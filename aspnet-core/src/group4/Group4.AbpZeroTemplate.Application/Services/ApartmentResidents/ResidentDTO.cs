using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4.AbpZeroTemplate.Web.Core.Services.ApartmentsResidents
{
    public class ResidentDTO
    {
        public string RESIDENT_ID { get; set; }
        public string RESIDENT_CODE { get; set; }
        public string RESIDENT_NAME { get; set; }
        public DateTime? RESIDENT_BIRTH { get; set; }
        public string RESIDENT_IDCARD { get; set; }
        public string CUR_APARTMENT { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }
}
