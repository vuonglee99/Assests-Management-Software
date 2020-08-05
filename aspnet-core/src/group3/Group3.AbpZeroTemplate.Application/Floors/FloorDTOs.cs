using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Floors
{
    public class FloorTableDTO
    {
        public string FLOOR_ID { get; set; }
        public string FLOOR_CODE { get; set; }
        public string FLOOR_NAME { get; set; }
        public string FLOORTYPE_ID { get; set; }
        public string BUILDING_ID { get; set; }
        public string FLOOR_STATUS { get; set; }
        public string DESCRIPTION { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }

    public class FloorPagingSearchDTO : FloorTableDTO
    {
        public int? TotalRows { get; set; }
        public string FLOORTYPE_NAME { get; set; }
    }
}
