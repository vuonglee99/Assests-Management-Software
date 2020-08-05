using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4.AbpZeroTemplate.Web.Core.Services.ApartmentTypes
{
    public class ApTypeSearchDTO
    {
        public string APARTMENT_TYPE_ID { get; set; }
        public string APARTMENT_TYPE_CODE { get; set; }
        public string APARTMENT_TYPE_NAME { get; set; }
        public string APARTMENT_TYPE_DESCRIPTION { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public int? USER_ID { get; set; }
        
    }
}
