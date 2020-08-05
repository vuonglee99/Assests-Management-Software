using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_BaoDuong;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_BaoDuong.BaoDuong_DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Group11.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BaoDuongController : AbpController
    {
        private readonly IBaoDuongService _baoDuongService;

        public BaoDuongController(IBaoDuongService baoDuongService)
        {
            this._baoDuongService = baoDuongService;
        }
        [HttpPost]
        public List<BD_DTO> BD_GetById(string bdId)
        {
            return _baoDuongService.BD_GetByID(bdId);
        }

        [HttpPost]
        public List<BD_DTO> BD_GetByCode(string bdCode)
        {
            return _baoDuongService.BD_GetByCode(bdCode);
        }

        [HttpPost]
        public List<BD_DTO> BD_Insert([FromBody] BD_DTO model)
        {
            return _baoDuongService.BD_Insert(model);
        }

        [HttpPost]
        public List<BD_DTO> BD_Update([FromBody] BD_DTO model)
        {
            return _baoDuongService.BD_Update(model);
        }

        [HttpPost]
        public List<BD_DTO> BD_Delete(string bdId)
        {
            return _baoDuongService.BD_Delete(bdId);
        }

        [HttpPost]
        public PagedResultDto<BD_DTO> BD_Search([FromBody] BD_DTO filterInput)
        {
            return _baoDuongService.BD_Search(filterInput);
        }

        [HttpPost]
        public List<BD_DTO> BD_ExportData([FromBody] List<BD_DTO> listInput)
        {
            return _baoDuongService.ExportData(listInput);
        }

        [HttpPost]
        public List<BD_DTO> BD_Approve_Insert(string bdId)
        {
            return _baoDuongService.BD_Approve_Insert(bdId);
        }
        [HttpPost]
        public List<BD_DTO> BD_Approve_Delete(string bdId)
        {
            return _baoDuongService.BD_Approve_Delete(bdId);
        }
        [HttpPost]
        public List<BD_DTO> BD_Approve_Update(string bdCode)
        {
            return _baoDuongService.BD_Approve_Update(bdCode);
        }
        [HttpPost]
        public List<BD_DTO> BD_Deny_Insert(string bdId)
        {
            return _baoDuongService.BD_Deny_Insert(bdId);
        }
        [HttpPost]
        public List<BD_DTO> BD_Deny_Delete(string bdId)
        {
            return _baoDuongService.BD_Deny_Delete(bdId);
        }
        [HttpPost]
        public List<BD_DTO> BD_Deny_Update(string bdCode)
        {
            return _baoDuongService.BD_Deny_Update(bdCode);
        }
        [HttpPost]
        public bool isUpdating(string bdCode)
        {
            return _baoDuongService.BD_IsUpdating(bdCode);
        }
        [HttpPost]
        public PagedResultDto<BD_DTO> GetAll([FromBody] BD_DTO filterInput)
        {
            return _baoDuongService.BD_GetAll(filterInput);
        }
    }
}
