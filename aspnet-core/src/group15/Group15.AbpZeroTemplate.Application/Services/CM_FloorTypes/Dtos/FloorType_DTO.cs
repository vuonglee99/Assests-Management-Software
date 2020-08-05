using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Application.Services.CM_FloorTypes
{
    public class FloorType_DTO
    {
        public string FloorType_ID { get; set; }
        public string FloorType_CODE { get; set; }
        public string FloorType_NAME { get; set; }
        public string FloorType_DESC { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string DELETE_REQUESTED { get; set; }
    }
}
