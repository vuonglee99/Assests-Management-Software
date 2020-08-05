using System;

namespace Selenium.Test.group4.DTO
{
    public class ApartmentDTO
    {
        public string APARTMENT_ID { get; set; }
        public string APARTMENT_CODE { get; set; }
        public string APARTMENT_NAME { get; set; }
        public string APARTMENT_TYPE_ID { get; set; }
        public string BUILDING_ID { get; set; }
        public string Floor_ID { get; set; }
        public string APARTMENT_STATUS { get; set; }
        public long? APARTMENT_PRICE { get; set; }
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
