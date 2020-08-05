using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Contracts
{
    public class ContractCreateRequest : IShouldNormalize
    {

        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }

        public void Normalize()
        {
            if (CREATE_DT == new DateTime()) CREATE_DT = null;
            if (APPROVE_DT == new DateTime()) APPROVE_DT = null;
        }
    }

    public class ContractUpdateRequest : ContractCreateRequest
    {

    }

    public class ContractPagingSearchRequest : PagedAndSortedResultRequestDto
    {

        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
    }

    public class ContractApproveRequest
    {

        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
    }
}
