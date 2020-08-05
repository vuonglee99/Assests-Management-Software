using Abp.Runtime.Validation;
using Group3.AbpZeroTemplate.Application.DTOs;
using GSoft.AbpZeroTemplate.Dto;
using System;

namespace Group3.AbpZeroTemplate.Web.Core.Services.Buildings
{
    public class BuildingSearchRequest : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Search { get; set; }
        public string BUILDING_CODE { get; set; }
        public string BUILDING_NAME { get; set; }
        public string AUTH_STATUS { get; set; }
        public int? RECORD_STATUS { get; set; }
        public void Normalize()
        { 
            if (string.IsNullOrEmpty(Sorting))
                Sorting = "BUILDING_CODE ASC";
        }
    }

    public class BuildingCreateRequest
    {
        public string BUILDING_CODE { get; set; }
        public string BUILDING_NAME { get; set; }
        public string ADDRESS { get; set; }
        public int FLOOR_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string MANAGER_ID { get; set; }
        public bool RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public bool AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }

    public class BuildingUpdateRequest : BuildingTableDTO { }
}