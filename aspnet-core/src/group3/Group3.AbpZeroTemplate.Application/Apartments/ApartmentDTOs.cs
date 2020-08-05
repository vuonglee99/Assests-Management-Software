﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Apartments
{
    public class ApartmentTableDTO
    {
        public string APARTMENT_ID { get; set; }
        public string APARTMENT_CODE { get; set; }
        public string APARTMENT_NAME { get; set; }
        public string APARTMENTTYPE_ID { get; set; }
        public string FLOOR_ID { get; set; }
        public string APARTMENT_STATUS { get; set; }
        public string DESCRIPTION { get; set; }
        public long? APARTMENT_PRICE { get; set; }
        public int? APARTMENT_ROOMS { get; set; }
        public int? APARTMENT_AREA { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }

    public class ApartmentPagingSearchDTO : ApartmentTableDTO
    {
        public int? TotalRows { get; set; }
        public string APARTMENTTYPE_NAME { get; set; }
    }
}
