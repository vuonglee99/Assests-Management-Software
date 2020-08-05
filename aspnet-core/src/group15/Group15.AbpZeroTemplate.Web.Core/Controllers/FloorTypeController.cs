using GWebsite.AbpZeroTemplate.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using Group15.AbpZeroTemplate.Application.Services.CM_FloorTypes;

namespace GWebsite.AbpZeroTemplate.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FloorTypeController : AbpController
    {
        private IFloorTypeService floorTypeService;

        public FloorTypeController(IFloorTypeService floorTypeService)
        {
            this.floorTypeService = floorTypeService;
        }

        [HttpPost]
        public FloorType_DTO GetById([FromQuery]string floorTypeId)
        {
            return this.floorTypeService.FloorType_GetById(floorTypeId);
        }

        [HttpPost]
        public ServiceResult Create([FromBody]FloorTypeCreate_DTO createInput)
        {
            return this.floorTypeService.FloorType_Create(createInput);
        }

        [HttpPost]
        public List<FloorType_DTO> Search([FromQuery]FloorTypeSearch_DTO searchInput)
        {
            return this.floorTypeService.FloorType_Search(searchInput);
        }

        [HttpPost]
        public ServiceResult Update([FromBody] FloorTypeUpdate_DTO updateInput)
        {
            return this.floorTypeService.FloorType_Update(updateInput);
        }

        [HttpPost]
        public ServiceResult Delete([FromQuery] string updateInput)
        {
            return this.floorTypeService.FloorType_Delete(updateInput);
        }


        [HttpPost]
        public List<FloorType_DTO> GetAll()
        {
            return this.floorTypeService.FloorType_GetAll();
        }

        [HttpPost]
        public ServiceResult ApproveAdd([FromQuery] string floorTypeId)
        {
            return this.floorTypeService.FloorType_Approve_Add(floorTypeId);
        }

        [HttpPost]
        public ServiceResult CancelApproveAdd([FromQuery] string floorTypeId)
        {
            return this.floorTypeService.FloorType_Approve_Add_Cancel(floorTypeId);
        }

        [HttpPost]
        public ServiceResult ApproveUpdate([FromQuery] string floorTypeId)
        {
            return this.floorTypeService.FloorType_Approve_Update(floorTypeId);
        }

        [HttpPost]
        public ServiceResult CancelApproveUpdate([FromQuery] string floorTypeId)
        {
            return this.floorTypeService.FloorType_Approve_Update_Cancel(floorTypeId);
        }

        [HttpPost]
        public FloorType_DTO GetApproveOfId([FromQuery] string floorTypeId)
        {
            return this.floorTypeService.FloorType_GetApproveById(floorTypeId);
        }

        [HttpPost]
        public ServiceResult ApproveDelete([FromQuery]string floorTypeId)
        {
            return this.floorTypeService.FloorType_Approve_Delete(floorTypeId);
        }

        [HttpPost]
        public ServiceResult CancelApproveDelete([FromQuery]string floorTypeId)
        {
            return this.floorTypeService.FloorType_Approve_Delete_Cancel(floorTypeId);
        }
    }
}
