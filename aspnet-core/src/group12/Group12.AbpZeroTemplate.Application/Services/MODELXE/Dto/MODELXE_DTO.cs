using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group12.AbpZeroTemplate.Web.Core.Services.MODELXE.Dto
{
    public class MODELXE_DTO
    {
        public string MODEL_ID { get; set; }
        public string MODEL_CODE { get; set; }
        public string MODEL_NAME { get; set; }
        public string MODEL_TYPE { get; set; }
        public string MODEL_HSX { get; set; }
        public string MODEL_DMNL { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }
}
