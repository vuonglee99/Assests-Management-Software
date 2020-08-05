using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using System;

namespace Group3.AbpZeroTemplate.Web.Core.Services.Services
{
    public class ServiceSearchRequest : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SEARCH { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }

        public void Normalize()
        { 
            if (0 == SkipCount)
                SkipCount = 1;
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "BUILDING_CODE ASC";
            }
        }
    }
}