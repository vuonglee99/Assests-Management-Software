using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Group3.AbpZeroTemplate.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Floors
{
    public interface IFloorsAppService : IApplicationService
    {
        dynamic Approve(FloorApproveRequest body);
        dynamic Create(FloorCreateRequest body);
        dynamic Delete(string id);
        FloorTableDTO GetByID(string id);
        PagedResultDto<FloorPagingSearchDTO> PagingSearch(
            FloorPagingSearchRequest query);
        dynamic Update(FloorUpdateRequest body);
    }
    public class FloorsAppService : Group3AppServiceBase, IFloorsAppService
    {
        [AbpAuthorize(Group3PermissionsConst.Floors_Approve)]
        public dynamic Approve(FloorApproveRequest body)
        {
            return procedureHelper.GetData<dynamic>("Floor_Approve", body)
                .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Floors_Create)]
        public dynamic Create(FloorCreateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Floor_Create", body)
                .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Floors_Delete)]
        public dynamic Delete(string id)
        {
            return procedureHelper.GetData<dynamic>(
                "Floor_Delete", new { FLOOR_ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Floors)]
        public FloorTableDTO GetByID(string id)
        {
            return procedureHelper.GetData<FloorTableDTO>(
                "Floor_GetByID", new { FLOOR_ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.Floors)]
        public PagedResultDto<FloorPagingSearchDTO> PagingSearch(
            FloorPagingSearchRequest query)
        {
            var items = procedureHelper.GetData<FloorPagingSearchDTO>(
                "Floor_Paging_Search", query);
            int totalRows = items.Count == 0 ? 0 : (int)items[0].TotalRows;
            return new PagedResultDto<FloorPagingSearchDTO>(totalRows, items);
        }

        [AbpAuthorize(Group3PermissionsConst.Floors_Update)]
        public dynamic Update(FloorUpdateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Floor_Update", body)
                .FirstOrDefault();
        }
    }
}
