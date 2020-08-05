using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.DTOs
{
    public class Use_DichVuSearchDTOs
    {
        public int TotalRows { get; set; }
        public string CANHO_ID { get; set; }
        public string DICHVU_ID { get; set; }
        public DateTime? NG_START { get; set; }
        public DateTime? NG_END { get; set; }
        public float GT_DAU { get; set; }
        public float GT_CUOI { get; set; }
        public float DON_GIA { get; set; }
        public float TONG { get; set; }
        public string RECORD_STATUS { get; set; }
    }
}
