using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group11.XE.Test.XEDTO
{
    public class XE_DTO
    {
        public string XE_ID { get; set; }
        public string XE_CODE { get; set; }
        public string XE_NAME { get; set; }
        public string XE_COLOR { get; set; }
        public int XE_SEATS { get; set; }
        public string XE_MODEL { get; set; }
        public string XE_LICENSE_PLATE { get; set; }
        public int XE_PRICE { get; set; }
        public int XE_CONSUMPTION { get; set; }
        public string XE_NOTES { get; set; }
        public int XE_MAX_SPEED { get; set; }
        public string XE_MANUFACTURER { get; set; }
        public int XE_MANUFACTURE_YEAR { get; set; }
        public string XE_STATUS { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string BRANCH_CREATE { get; set; }

        public XE_DTO() { }

        public XE_DTO(String Code, String Name, String Color,int Seats, String Model, String License,int Price, int Consumption,String Notes, int Mspeed, String Manufacturer, int Manufacture_year,String Status)
        {

            this.XE_CODE = Code;
            this.XE_NAME = Name;
            this.XE_COLOR = Color;
            this.XE_SEATS = Seats;
            this.XE_MODEL = Model;
            this.XE_LICENSE_PLATE = License;
            this.XE_PRICE = Price;
            this.XE_CONSUMPTION = Consumption;
            this.XE_NOTES = Notes;
            this.XE_MAX_SPEED = Mspeed;
            this.XE_MANUFACTURER= Manufacturer;
            this.XE_MANUFACTURE_YEAR = Manufacture_year;
            this.XE_STATUS = Status;
        }
    }
}
