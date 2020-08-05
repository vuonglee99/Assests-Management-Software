using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5.AbpZeroTemplate.Web.Core.Services.CM_PHONGBANs.Dto
{
    public class CM_PHONGBAN_DTO
    {
        public string DEP_ID { get; set; }
        public string DEP_CODE { get; set; }
        public string DEP_NAME { get; set; }
        public string DAO_CODE { get; set; }
        public string DAO_NAME { get; set; }
        public string BRANCH_ID { get; set; }
        public string GROUP_ID { get; set; }
        public string TEL { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }

        public string BRANCH_NAME { get; }
    }

}
