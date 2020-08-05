using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4.AbpZeroTemplate.Web.Core.Services.Apartments
{
    public class CustomApartmentDTO
    {
        public string APARTMENT_ID { get; set; }
        public string APARTMENT_CODE { get; set; }
        public string APARTMENT_NAME { get; set; }
        public string APARTMENT_TYPE_NAME { get; set; }
        public string BUILDING_NAME { get; set; }
        public string Floor_NAME { get; set; }
        public string APARTMENT_STATUS { get; set; }
        public long? APARTMENT_PRICE { get; set; }
        public string NUMBER_OF_PEOPLE { get; set; }
        public int? APARTMENT_ROOMS { get; set; }
        public int? APARTMENT_AREA { get; set; }
        public int? APARTMENT_MAX_TENANT { get; set; }
        public string APARTMENT_DESCRIPTION { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }
}
