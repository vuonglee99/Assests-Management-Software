﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group14.AbpZeroTemplate.Web.Core.Services.PhieuCapPhatTBVT.Dto
{
    public class PCPTBVT_SEARCH_DTO
    {
        public string PCPTBVT_MA_PCP { get; set; }
        public string PCPTBVT_DEP_ID { get; set; }
        public string AUTH_STATUS { get; set; }
        public DateTime? CREATE_DT_START { get; set; }
        public DateTime? CREATE_DT_END { get; set; }
    }
}
