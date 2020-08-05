using Abp.Application.Services;
using Abp.Authorization;
using GSoft.AbpZeroTemplate.Helpers;
using GWebsite.AbpZeroTemplate.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Application.Services.CM_Floors
{
    public interface IFloorService : IApplicationService
    {
        ServiceResult Create(FloorCreate_DTO input);
        List<Floor_DTO> GetAll();
        List<Floor_DTO> GetByBuildingID(string buildingId);
        Floor_DTO GetById(string floorId);
        ServiceResult Delete(string floorId);
        ServiceResult Update(FloorUpdate_DTO updateInput);
        ServiceResult Floor_Approve_Add(string floorId);
        ServiceResult Floor_Approve_Add_Cancel(string floorId);
        ServiceResult Floor_Approve_Update(string floorId);
        ServiceResult Floor_Approve_Update_Cancel(string floorId);
        Floor_DTO Floor_GetApproveById(string floorId);
        ServiceResult Floor_Approve_Delete(string floorId);
        ServiceResult Floor_Approve_Delete_Cancel(string floorId);
    }

    [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor)]
    public class FloorService : BaseService, IFloorService
    {
        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_Create)]
        public ServiceResult Create(FloorCreate_DTO input)
        {
            return this.procedureHelper.GetData<ServiceResult>("Floor_Create", input)[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_Delete)]
        public ServiceResult Delete(string floorId)
        {
            return this.procedureHelper.GetData<ServiceResult>("Floor_Delete", new { Floor_ID = floorId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_GetAll)]
        public List<Floor_DTO> GetAll()
        {
            return this.procedureHelper.GetData<Floor_DTO>("Floor_GetAll", new { });
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_GetByBuildingId)]
        public List<Floor_DTO> GetByBuildingID(string buildingId)
        {
            return this.procedureHelper.GetData<Floor_DTO>("Floor_GetByBuildingID", new { Building_ID = buildingId });
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_GetById)]
        public Floor_DTO GetById(string floorId)
        {
            var result = this.procedureHelper.GetData<Floor_DTO>("Floor_GetById", new { Floor_ID = floorId });
            if (result.Count == 0)
            {
                return null;
            }
            return result[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_Update)]
        public ServiceResult Update(FloorUpdate_DTO updateInput)
        {
            return this.procedureHelper.GetData<ServiceResult>("Floor_Update", updateInput)[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_ApproveAdd)]
        public ServiceResult Floor_Approve_Add(string floorId)
        {
            return this.procedureHelper.GetData<ServiceResult>("Floor_Approve_Add", new { Floor_ID = floorId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_CancelApproveAdd)]
        public ServiceResult Floor_Approve_Add_Cancel(string floorId)
        {
            return this.procedureHelper.GetData<ServiceResult>("Floor_Approve_Add_Cancel", new { Floor_ID = floorId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_ApproveUpdate)]
        public ServiceResult Floor_Approve_Update(string floorId)
        {
            var newFloor = this.GetById(floorId);
            if (newFloor == null)
            {
                return ServiceResult.CreateError("Không tìm thấy id của tầng");
            }
            newFloor.CREATE_DT = newFloor.APPROVE_DT = null;
            return this.procedureHelper.GetData<ServiceResult>("Floor_Approve_Update", newFloor)[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_CancelApproveUpdate)]
        public ServiceResult Floor_Approve_Update_Cancel(string floorId)
        {
            return this.procedureHelper.GetData<ServiceResult>("Floor_Approve_Update_Cancel", new { Floor_ID = floorId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_GetApproveOfId)]
        public Floor_DTO Floor_GetApproveById(string floorId)
        {
            var result = this.procedureHelper.GetData<Floor_DTO>("Floor_GetApproveUpdateById", new { Floor_ID = floorId });
            if (result == null || result.Count == 0)
            {
                return null;
            }
            return result[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_ApproveDelete)]
        public ServiceResult Floor_Approve_Delete(string floorId)
        {
            return this.procedureHelper.GetData<ServiceResult>("Floor_Approve_Delete", new { Floor_ID = floorId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_Floor_CancelApproveDelete)]
        public ServiceResult Floor_Approve_Delete_Cancel(string floorId)
        {
            return this.procedureHelper.GetData<ServiceResult>("Floor_Approve_Delete_Cancel", new { Floor_ID = floorId })[0];
        }
    }
}
