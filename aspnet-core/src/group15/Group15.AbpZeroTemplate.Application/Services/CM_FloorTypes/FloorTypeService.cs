using Abp.Application.Services;
using Abp.Authorization;
using GSoft.AbpZeroTemplate.Helpers;
using GWebsite.AbpZeroTemplate.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Application.Services.CM_FloorTypes
{
    public interface IFloorTypeService : IApplicationService
    {
        List<FloorType_DTO> FloorType_GetAll();
        FloorType_DTO FloorType_GetById(string floorTypeId);
        ServiceResult FloorType_Create(FloorTypeCreate_DTO dto);
        List<FloorType_DTO> FloorType_Search(FloorTypeSearch_DTO dto);
        ServiceResult FloorType_Update(FloorTypeUpdate_DTO dto);
        ServiceResult FloorType_Delete(string floorTypeId);
        ServiceResult FloorType_Approve_Add(string floorTypeId);
        ServiceResult FloorType_Approve_Add_Cancel(string floorTypeId);
        ServiceResult FloorType_Approve_Update(string floorTypeId);
        ServiceResult FloorType_Approve_Update_Cancel(string floorTypeId);
        FloorType_DTO FloorType_GetApproveById(string floorTypeId);
        ServiceResult FloorType_Approve_Delete(string floorTypeId);
        ServiceResult FloorType_Approve_Delete_Cancel(string floorTypeId);
    }

    [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType)]
    public class FloorTypeService : BaseService, IFloorTypeService 
    {

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_Create)]
        public ServiceResult FloorType_Create(FloorTypeCreate_DTO dto)
        {
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Create", dto)[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_Search)]
        public List<FloorType_DTO> FloorType_Search(FloorTypeSearch_DTO dto)
        {
            return this.procedureHelper.GetData<FloorType_DTO>("FloorType_Search", dto);
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_Update)]
        public ServiceResult FloorType_Update(FloorTypeUpdate_DTO dto)
        {
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Update", dto)[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_Delete)]
        public ServiceResult FloorType_Delete(string floorTypeId)
        {
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Delete", new { FloorType_ID = floorTypeId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_GetAll)]
        public List<FloorType_DTO> FloorType_GetAll()
        {
            return this.procedureHelper.GetData<FloorType_DTO>("FloorType_GetAll", new { });
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_GetById)]
        public FloorType_DTO FloorType_GetById(string floorTypeId)
        {
            var result = this.procedureHelper.GetData<FloorType_DTO>("FloorType_GetById", new { FloorType_ID = floorTypeId });
            if (result.Count == 0)
            {
                return null;
            }
            return result[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_ApproveAdd)]
        public ServiceResult FloorType_Approve_Add(string floorTypeId)
        {
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Approve_Add", new { FloorType_ID = floorTypeId })[0];
        }


        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_CancelApproveAdd)]
        public ServiceResult FloorType_Approve_Add_Cancel(string floorTypeId)
        {
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Approve_Add_Cancel", new { FloorType_ID = floorTypeId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_ApproveUpdate)]
        public ServiceResult FloorType_Approve_Update(string floorTypeId)
        {
            var newFloorType = this.FloorType_GetById(floorTypeId);
            if (newFloorType == null)
            {
                return ServiceResult.CreateError("Không tìm thấy loại tầng");
            }
            newFloorType.CREATE_DT = newFloorType.APPROVE_DT = null;
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Approve_Update", newFloorType)[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_CancelApproveUpdate)]
        public ServiceResult FloorType_Approve_Update_Cancel(string floorTypeId)
        {
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Approve_Update_Cancel", new { FloorType_ID = floorTypeId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_GetApproveOfId)]
        public FloorType_DTO FloorType_GetApproveById(string floorTypeId)
        {
            var result = this.procedureHelper.GetData<FloorType_DTO>("FloorType_GetApproveUpdateById", new { FloorType_ID = floorTypeId });
            if (result == null || result.Count == 0)
            {
                return null;
            }
            return result[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_ApproveDelete)]
        public ServiceResult FloorType_Approve_Delete(string floorTypeId)
        {
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Approve_Delete", new { FloorType_ID = floorTypeId })[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_FloorType_CancelApproveDelete)]
        public ServiceResult FloorType_Approve_Delete_Cancel(string floorTypeId)
        {
            return this.procedureHelper.GetData<ServiceResult>("FloorType_Approve_Delete_Cancel", new { FloorType_ID = floorTypeId })[0];
        }
    }
}
