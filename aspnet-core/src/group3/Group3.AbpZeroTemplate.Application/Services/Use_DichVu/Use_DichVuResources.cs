using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Services.Use_DichVu
{
    public class Use_DichVuInsertRequest:IShouldNormalize
    {
        public string CANHO_ID { get; set; }
        public string DICHVU_ID { get; set;}
        public DateTime? NG_START { get; set; }
        public DateTime? NG_END { get; set; }
        public float? GT_DAU { get; set; }
        public float? GT_CUOI { get; set; }
        public float? DON_GIA { get; set; }

        public void Normalize()
        {

        }
    }

    public class Use_DichVuSearchRequest: PagedAndSortedInputDto,IShouldNormalize
    {
        public string CANHO_ID { get; set; }
        public string DICHVU_ID { get; set; }
        public DateTime? NG_START { get; set; }
        public DateTime? NG_END { get; set; }
        public string RECORD_STATUS { get; set; }
        public void Normalize()
        {
            if (0 == SkipCount)
                SkipCount = 1;
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "ID ASC";
            }
            if (string.IsNullOrEmpty(CANHO_ID))
            {
                CANHO_ID = "";
            }
            if (string.IsNullOrEmpty(DICHVU_ID))
            {
                DICHVU_ID = "";
            }
            if (string.IsNullOrEmpty(RECORD_STATUS))
            {
                RECORD_STATUS = "1";
            }
            if (NG_START == null)
            {
                NG_START = new DateTime(1753,1,1,0,0,0);
            }
            if (NG_END == null)
            {
                NG_END = new DateTime(9999,12,31,23,59,59);
            }
        }
    }
}
