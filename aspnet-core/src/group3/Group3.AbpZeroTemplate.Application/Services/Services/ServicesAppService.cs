using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Group3.AbpZeroTemplate.Application;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group3.AbpZeroTemplate.Application.DTOs;
using Group3.AbpZeroTemplate.Web.Core.DTOs;

namespace Group3.AbpZeroTemplate.Web.Core.Services.Services
{
    public interface IServicesAppService : IApplicationService
    {
        PagedResultDto<ServiceSearchDTO> SearchServices(ServiceSearchRequest filter);
    }

    public class ServicesAppService : Group3AppServiceBase, IServicesAppService
    {        
        public PagedResultDto<ServiceSearchDTO> SearchServices(ServiceSearchRequest filter)
        {
            var services = procedureHelper.GetData<ServiceSearchDTO>(
                "Services_Search", new
                {
                    SERVICE_CODE = filter.SERVICE_CODE,
                    SERVICE_NAME = filter.SERVICE_NAME,
                    RECORD_STATUS = 1,
                    Search = filter.SEARCH,
                    Sorting = filter.Sorting,
                    PageNo = filter.SkipCount,
                    PageSize = filter.MaxResultCount,
                });
            int TotalRows = 0;
            if (services.Count != 0) 
                TotalRows = services[0].TotalRows;

            return new PagedResultDto<ServiceSearchDTO>(
                TotalRows, services);
        }
    }
}