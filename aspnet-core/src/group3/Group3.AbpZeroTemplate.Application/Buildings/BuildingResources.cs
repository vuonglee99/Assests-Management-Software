using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Buildings
{
    public class BuildingCreateRequest : IShouldNormalize
    {
        public string BUILDING_CODE { get; set; }
        public string BUILDING_NAME { get; set; }
        public string ADDRESS { get; set; }
        public int? FLOOR_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string MANAGER_ID { get; set; }
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

    public class BuildingUpdateRequest : BuildingCreateRequest
    {
        public string BUILDING_ID { get; set; }
    }

    public class BuildingPagingSearchRequest : PagedAndSortedResultRequestDto
    {
        public string BUILDING_CODE { get; set; }
        public string BUILDING_NAME { get; set; }
        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
    }

    public class BuildingApproveRequest
    {
        public string BUILDING_ID { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
    }
}
