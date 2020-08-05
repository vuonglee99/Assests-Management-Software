using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4.AbpZeroTemplate.Web.Core.Services.CM_BRANCHs.Dto
{
    public class CM_BRANCH_DTO
    {
        public string BRANCH_ID { get; set; }
        public string FATHER_ID { get; set; }
        public string IS_POTENTIAL { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string DAO_CODE { get; set; }
        public string DAO_NAME { get; set; }
        public string REGION_ID { get; set; }
        public string BRANCH_TYPE { get; set; }
        public string BRANCH_EMAIL { get; set; }
        public string BRANCH_FAX { get; set; }
        public string BRANCH_STATUS { get; set; }
        public string ADDR { get; set; }
        public string PROVICE { get; set; }
        public string TEL { get; set; }
        public string TAX_NO { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }

}
