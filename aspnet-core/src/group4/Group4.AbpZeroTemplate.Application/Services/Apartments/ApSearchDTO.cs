using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4.AbpZeroTemplate.Web.Core.Services.Apartments
{
    public class ApSearchDTO
    {
        public string APARTMENT_ID { get; set; }
        public string APARTMENT_CODE { get; set; }
        public string APARTMENT_NAME { get; set; }
        public string APARTMENT_TYPE_NAME { get; set; }
        public string BUILDING_NAME { get; set; }
        public string Floor_NAME { get; set; }
        public int? RESIDENT_QUANTITY { get; set; }
        public int? APARTMENT_MAX_TENANT { get; set; }
        public long? APARTMENT_PRICE { get; set; }
        public string APARTMENT_STATUS { get; set; }
        public string AUTH_STATUS_NAME { get; set; }
    }
}
