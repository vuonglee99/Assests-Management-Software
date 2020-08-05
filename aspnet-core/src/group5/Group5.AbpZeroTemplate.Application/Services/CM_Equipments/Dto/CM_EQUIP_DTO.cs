using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5.AbpZeroTemplate.Web.Core.Services.CM_Equipments.Dto
{
    public class CM_EQUIP_DTO
    {
        public string WO_ID { get; }
        public string WO_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public string DESCRIPTIONS { get; set; }
        public string PRIORITY_ORDER { get; set; }
        public string RECORD_STATUS { get; set; }
        public string KIND_FIX { get; set; }
        public string FIXER { get; set; }
        public DateTime? DATE_IN { get; set; }
        public string DESCRIPTIONS_ERROR { get; set; }
        public DateTime? DATE_OUT { get; set; }
        public string MAKER_ID { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string AUTH_STATUS { get; set; }

    }
}
