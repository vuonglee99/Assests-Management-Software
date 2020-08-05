using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4.AbpZeroTemplate.Web.Core.Services.ApartmentsResidents
{
    public class ApartmentResidentDTO
    {
        public string APARTMENT_RESIDENT_ID { get; set; }
        public string APARTMENT_ID { get; set; }
        public string RESIDENT_ID { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }


    }
}
