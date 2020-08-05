using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Residents
{
    public class ResidentCreateRequest : IShouldNormalize
    {
        public string RESIDENT_CODE { get; set; }
        public string RESIDENT_NAME { get; set; }
        public string RESIDENT_PHONE { get; set; }
        public DateTime? RESIDENT_BIRTH { get; set; }
        public string RESIDENT_IDCARD { get; set; }
        public string CUR_APARTMENT { get; set; }
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

    public class ResidentUpdateRequest : ResidentCreateRequest
    {
        public string RESIDENT_ID { get; set; }
    }

    public class ResidentPagingSearchRequest : PagedAndSortedResultRequestDto
    {
        public string BUILDING_ID { get; set; }
        public string FLOOR_ID { get; set; }
        public string APARTMENT_ID { get; set; }
        public string RESIDENT_CODE { get; set; }
        public string RESIDENT_NAME { get; set; }
        public string RESIDENT_IDCARD { get; set; }
        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
    }

    public class ResidentApproveRequest
    {
        public string RESIDENT_ID { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
    }
}
