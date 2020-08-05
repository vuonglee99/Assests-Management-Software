using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Group3.AbpZeroTemplate.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Buildings
{
    public interface IBuildingsAppService : IApplicationService
    {
        dynamic Approve(BuildingApproveRequest body);
        dynamic Create(BuildingCreateRequest body);
        dynamic Delete(string id);
        BuildingTableDTO GetByID(string id);
        PagedResultDto<BuildingPagingSearchDTO> PagingSearch(
            BuildingPagingSearchRequest query);
        dynamic Update(BuildingUpdateRequest body);
    }
    public class BuildingsAppService : Group3AppServiceBase, IBuildingsAppService
    {
        [AbpAuthorize(Group3PermissionsConst.Buildings_Approve)]
        public dynamic Approve(BuildingApproveRequest body)
        {
            return procedureHelper.GetData<dynamic>("Building_Approve", body)
                .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Buildings_Create)]
        public dynamic Create(BuildingCreateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Building_Create", body)
                .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Buildings_Delete)]
        public dynamic Delete(string id)
        {
            return procedureHelper.GetData<dynamic>(
                "Building_Delete", new { BUILDING_ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Buildings)]
        public BuildingTableDTO GetByID(string id)
        {
            return procedureHelper.GetData<BuildingTableDTO>(
                "Building_GetByID", new { BUILDING_ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Buildings)]
        public PagedResultDto<BuildingPagingSearchDTO> PagingSearch(
            BuildingPagingSearchRequest query)
        {
            var items = procedureHelper.GetData<BuildingPagingSearchDTO>(
                "Building_Paging_Search", query);
            int totalRows = items.Count == 0 ? 0 : (int)items[0].TotalRows;
            return new PagedResultDto<BuildingPagingSearchDTO>(totalRows, items);
        }

        [AbpAuthorize(Group3PermissionsConst.Buildings_Update)]
        public dynamic Update(BuildingUpdateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Building_Update", body)
                .FirstOrDefault();
        }
    }
}
