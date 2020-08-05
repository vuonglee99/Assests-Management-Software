using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Services
{
    public class ServiceResult
    {
        public const string ERROR_CODE = "-1";
        public const string SUCCESS_CODE ="0";
        public string CODE { get; set; }
        public string ERROR { get; set; }
        public dynamic VALUE { get; set; }

        public static ServiceResult CreateSuccess(dynamic value)
        {
            return new ServiceResult()
            {
                CODE = SUCCESS_CODE,
                VALUE = value
            };
        }

        public static ServiceResult CreateError(string error)
        {
            return new ServiceResult()
            {
                CODE = ERROR_CODE,
                ERROR = error
            };
        }
    }
}
