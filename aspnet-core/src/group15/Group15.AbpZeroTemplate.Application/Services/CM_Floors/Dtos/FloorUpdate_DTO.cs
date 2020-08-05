using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Application.Services.CM_Floors
{
    public class FloorUpdate_DTO
    {
        public string Floor_ID { get; set; }
        public string Floor_CODE { get; set; }
        public string Floor_NAME { get; set; }
        public string FloorType_ID { get; set; }
        public string Building_ID { get; set; }
        public string Floor_STATUS { get; set; }
        public string Floor_NOTE { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public string APPROVE_DT { get; set; }
    }
}
