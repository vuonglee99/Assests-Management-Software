using Abp.Application.Services;
using Abp.Authorization;
using Group15.AbpZeroTemplate.Application;
using Group15.AbpZeroTemplate.Web.Core.Services.Apartments.DTOs;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Web.Core.Services.Apartments
{
    public interface IApartmentService: IApplicationService
    {
        List<Apartment_DTO> GetAll();
    }

    [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Apartment)]
    public class ApartmentService : BaseService, IApartmentService
    {
        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Apartment_GetAll)]
        public List<Apartment_DTO> GetAll()
        {
            return this.procedureHelper.GetData<Apartment_DTO>("APARTMENT_GetAll_", new { });
        }
    }
}
