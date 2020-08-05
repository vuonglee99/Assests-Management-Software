﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group10.AbpZeroTemplate.Web.Core.Services.CM_CTBD.Dto
{
    public class ChiTietBaoDuong_DTO
    {
        public string CTBD_ID { get; set; }
        public string CTBD_CODE { get; set; }
        public string CTBD_NAME { get; set; }
        public int? CTBD_QUANTITY { get; set; }
        public decimal? CTBD_UNIT_PRICE { get; set; }
        public decimal? CTBD_TOTAL_PRICE { get; set; }
        public string BD_ID { get; set; }
        public DateTime CREATED_DT { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public string APPROVE_STATUS { get; set; }
        public string REASON { get; set; }
        public DateTime APPROVE_DT { get; set; }
    }

}