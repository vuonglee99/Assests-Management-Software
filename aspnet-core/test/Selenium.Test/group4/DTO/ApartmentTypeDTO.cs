using System;

namespace Selenium.Test.group4.DTO
{
    public class ApartmentTypeDTO
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

        public ApartmentTypeDTO() { }

        public ApartmentTypeDTO(string code, string name, string description)
        {
            this.APARTMENT_TYPE_CODE = code;
            this.APARTMENT_TYPE_NAME = name;
            this.APARTMENT_TYPE_DESCRIPTION = description;
        }
    }
}
