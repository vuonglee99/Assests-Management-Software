using System;

namespace Group3.AbpZeroTemplate.Application.DTOs
{
    public class BuildingTableDTO
    {
        public string BUILDING_ID { get; set; }
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
    }

    public class BuildingSearchDTO : BuildingTableDTO
    {
        public int TotalRows { get; set; }
    }
}