using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Group3.AbpZeroTemplate.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Apartments
{
    public interface IApartmentsAppService : IApplicationService
    {
        dynamic Approve(ApartmentApproveRequest body);
        dynamic Create(ApartmentCreateRequest body);
        dynamic Delete(string id);
        ApartmentTableDTO GetByID(string id);
        PagedResultDto<ApartmentPagingSearchDTO> PagingSearch(
            ApartmentPagingSearchRequest query);
        dynamic Update(ApartmentUpdateRequest body);
    }
    public class ApartmentsAppService : Group3AppServiceBase, IApartmentsAppService
    {
        [AbpAuthorize(Group3PermissionsConst.Apartments_Approve)]
        public dynamic Approve(ApartmentApproveRequest body)
        {
            return procedureHelper.GetData<dynamic>("Apartment_Approve", body)
               .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Apartments_Create)]
        public dynamic Create(ApartmentCreateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Apartment_Create", body)
                .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Apartments_Delete)]
        public dynamic Delete(string id)
        {
            return procedureHelper.GetData<dynamic>(
                "Apartment_Delete", new { APARTMENT_ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Apartments)]
        public ApartmentTableDTO GetByID(string id)
        {
            return procedureHelper.GetData<ApartmentTableDTO>(
                "Apartment_GetByID", new { APARTMENT_ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Apartments)]
        public PagedResultDto<ApartmentPagingSearchDTO> PagingSearch(ApartmentPagingSearchRequest query)
        {
            var items = procedureHelper.GetData<ApartmentPagingSearchDTO>(
                "Apartment_Paging_Search", query);
            int totalRows = items.Count == 0 ? 0 : (int)items[0].TotalRows;
            return new PagedResultDto<ApartmentPagingSearchDTO>(totalRows, items);
        }

        [AbpAuthorize(Group3PermissionsConst.Apartments_Update)]
        public dynamic Update(ApartmentUpdateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Apartment_Update", body)
                .FirstOrDefault();
        }
    }
}
