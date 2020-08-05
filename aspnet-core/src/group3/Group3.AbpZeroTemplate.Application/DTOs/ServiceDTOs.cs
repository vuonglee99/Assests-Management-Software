using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.DTOs
{
    public class ServiceSearchDTO
    {
        public int TotalRows { get; set; }
        public string SERVICE_ID { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string SERVICE_UNIT { get; set; }
        public string RECORD_STATUS { get; set; }
    }
}
