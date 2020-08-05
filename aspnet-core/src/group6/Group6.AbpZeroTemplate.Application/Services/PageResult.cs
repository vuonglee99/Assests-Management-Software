using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6.AbpZeroTemplate.Web.Core.Services
{
    public class PageResult
    {
        public const string ERROR_CODE = "-1";
        public const string SUCCESS_CODE = "0";
        public string CODE { get; set; }
        public string ERROR { get; set; }
        public dynamic VALUE { get; set; }
        public int TOTAL_RECORD { get; set; }

        public static PageResult CreateSuccess(dynamic value, int total_record)
        {
            return new PageResult()
            {
                CODE = SUCCESS_CODE,
                TOTAL_RECORD = total_record,
                VALUE = value,
            };
        }

        public static PageResult CreateError(string error)
        {
            return new PageResult()
            {
                CODE = ERROR_CODE,
                ERROR = error
            };
        }
    }
}
