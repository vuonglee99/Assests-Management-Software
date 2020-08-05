using Abp.Runtime.Validation;
using GSoft.AbpZeroTemplate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Services.Branches
{
    public class Group03BranchFilter : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Search { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchStatus { get; set; }

        public void Normalize()
        {
            if (0 == SkipCount)
                SkipCount = 1;
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "branchCode ASC";
            }
        }
    }
}
