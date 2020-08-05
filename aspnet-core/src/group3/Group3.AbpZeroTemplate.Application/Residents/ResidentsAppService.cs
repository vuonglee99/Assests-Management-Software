using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Group3.AbpZeroTemplate.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Residents
{
    public interface IResidentsAppService : IApplicationService
    {
        dynamic Approve(ResidentApproveRequest body);
        dynamic Create(ResidentCreateRequest body);
        dynamic Delete(string id);
        ResidentTableDTO GetByID(string id);
        PagedResultDto<ResidentPagingSearchDTO> PagingSearch(
            ResidentPagingSearchRequest query);
        dynamic Update(ResidentUpdateRequest body);
    }
    public class ResidentsAppService : Group3AppServiceBase, IResidentsAppService
    {
        [AbpAuthorize(Group3PermissionsConst.Residents_Approve)]
        public dynamic Approve(ResidentApproveRequest body)
        {
            return procedureHelper.GetData<dynamic>("Resident_Approve", body)
                .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Residents_Create)]
        public dynamic Create(ResidentCreateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Resident_Create", body)
                .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Residents_Delete)]
        public dynamic Delete(string id)
        {
            return procedureHelper.GetData<dynamic>(
                "Resident_Delete", new { RESIDENT_ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Residents)]
        public ResidentTableDTO GetByID(string id)
        {
            return procedureHelper.GetData<ResidentTableDTO>(
                "Resident_GetByID", new { RESIDENT_ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Residents)]
        public PagedResultDto<ResidentPagingSearchDTO> PagingSearch(
            ResidentPagingSearchRequest query)
        {
            var items = procedureHelper.GetData<ResidentPagingSearchDTO>(
                "Resident_Paging_Search", query);
            int totalRows = items.Count == 0 ? 0 : (int)items[0].TotalRows;
            return new PagedResultDto<ResidentPagingSearchDTO>(totalRows, items);
        }

        [AbpAuthorize(Group3PermissionsConst.Residents_Update)]
        public dynamic Update(ResidentUpdateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Resident_Update", body)
                .FirstOrDefault();
        }
    }
}
