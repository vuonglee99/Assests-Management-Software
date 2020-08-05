using System;
using System.Collections.Generic;
using System.Text;

namespace Group11.XE.Test.BAODUONGDTO
{
    public class BAODUONG_DTO
    {
        public string BD_ID { get; set; }
        public string BD_CODE { get; set; }
        public string BD_GARAGE { get; set; }
        public string BD_ADDRESS { get; set; }
        public decimal BD_PRICE { get; set; }
        public DateTime? BD_FROM_DT { get; set; }
        public DateTime? BD_TO_DT { get; set; }
        public string XE_ID { get; set; }
        public int BD_LAST_DISTANCE { get; set; }
        public string BD_NOTES { get; set; }        
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }   

        public BAODUONG_DTO() { }

        public BAODUONG_DTO(String Code, String Garage, String Address, decimal Price, DateTime Bd_from_dt, DateTime Bd_to_dt, String Xe_id, int Bd_last_distance, String Bd_notes)
        {

            this.BD_CODE = Code;
            this.BD_GARAGE = Garage;
            this.BD_ADDRESS = Address;
            this.BD_PRICE = Price;
            this.BD_FROM_DT = Bd_from_dt;
            this.BD_TO_DT = Bd_to_dt;
            this.XE_ID = Xe_id;
            this.BD_LAST_DISTANCE = Bd_last_distance;
            this.BD_NOTES = Bd_notes;            
        }
    }
}
