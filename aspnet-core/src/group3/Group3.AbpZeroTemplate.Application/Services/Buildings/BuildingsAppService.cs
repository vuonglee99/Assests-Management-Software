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
using Group3.AbpZeroTemplate.Web.Core.Services.Buildings;

namespace Group3.AbpZeroTemplate.Web.Core.Services.Buildings
{
    public interface IBuildingsAppService : IApplicationService
    {
        PagedResultDto<BuildingSearchDTO> Search(BuildingSearchRequest filter);
        BuildingTableDTO GetByID(string id);
        dynamic Delete(string buildingId);
        dynamic Create(BuildingCreateRequest body);
        dynamic Update(BuildingUpdateRequest body);
    }

    public class BuildingsAppService : Group3AppServiceBase, IBuildingsAppService
    {
        public dynamic Delete(string id)
        {
            return procedureHelper.GetData<dynamic>(
                "Building_Delete", new { BUILDING_ID = id }
            ).FirstOrDefault();
        }

        public dynamic Update(BuildingUpdateRequest body)
        {
            return procedureHelper.GetData<dynamic>("Building_Update", body)
                .FirstOrDefault();
        }

        public PagedResultDto<BuildingSearchDTO> Search(BuildingSearchRequest filter)
        {
            var buildings = procedureHelper.GetData<BuildingSearchDTO>(
                "Building_Paging_Search", filter);
            int TotalRows = 0;
            if (buildings.Count != 0)
                TotalRows = buildings[0].TotalRows;

            return new PagedResultDto<BuildingSearchDTO>(TotalRows, buildings);
        }


        public dynamic Create(BuildingCreateRequest body)
        {
            if (body.CREATE_DT == new DateTime()) body.CREATE_DT = null;
            if (body.APPROVE_DT == new DateTime()) body.APPROVE_DT = null;
            return procedureHelper.GetData<dynamic>("Building_Create",
                new
                {
                    BUILDING_CODE = body.BUILDING_CODE,
                    BUILDING_NAME = body.BUILDING_NAME,
                    ADDRESS = body.ADDRESS,
                    FLOOR_NO = body.FLOOR_NO,
                    DESCRIPTION = body.DESCRIPTION,
                    MANAGER_ID = body.MANAGER_ID,
                    RECORD_STATUS = body.RECORD_STATUS ? '1' : '0',
                    MAKER_ID = body.MAKER_ID,
                    CREATE_DT = body.CREATE_DT,
                    AUTH_STATUS = body.AUTH_STATUS ? '1' : '0',
                    CHECKER_ID = body.CHECKER_ID,
                    APPROVE_DT = body.APPROVE_DT,
                }).FirstOrDefault();
        }

        public BuildingTableDTO GetByID(string id)
        {
            return procedureHelper.GetData<BuildingTableDTO>(
                "Building_GetByID", new { BUILDING_ID = id }
            ).FirstOrDefault();
        }
    }
}