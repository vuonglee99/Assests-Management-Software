using System;
using System.Collections.Generic;
using System.Text;

namespace Group11.XE.Test.PTXDTO
{
    public class PTX_DTO
    {
        public string PTX_ID { get; set; }
        public string PTX_CODE { get; set; }
        public DateTime? PTX_RENT_DT { get; set; }
        public DateTime? PTX_EXP_DT { get; set; }
        public DateTime? PTX_RETURN_DT { get; set; }
        public decimal PTX_PRICE { get; set; }
        public string PTX_NOTE { get; set; }
        public string XE_ID { get; set; }
        public string NTX_ID { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string BRANCH_CREATE { get; set; }

        public PTX_DTO() { }

        public PTX_DTO(String Code, DateTime Rent_dt, DateTime Exp_dt, DateTime Return_dt, decimal Price, String Note, string Xe_Id, string Ntx_Id)
        {

            this.PTX_CODE = Code;
            this.PTX_RENT_DT = Rent_dt;
            this.PTX_EXP_DT = Exp_dt;
            this.PTX_RETURN_DT = Return_dt;
            this.PTX_PRICE = Price;
            this.PTX_NOTE = Note;
            this.XE_ID = Xe_Id;
            this.NTX_ID = Ntx_Id;            
        }
    }
}
