using Abp.AspNetCore.Mvc.Controllers;
using Group4.AbpZeroTemplate.Web.Core.Services.ApartmentTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Group4.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ApartmentTypeController : AbpController
    {
        private readonly IApartmentTypeAppService apartmentTypeAppService;

        public ApartmentTypeController(IApartmentTypeAppService ApartmentTypeAppService)
        {
            this.apartmentTypeAppService = ApartmentTypeAppService;
        }

        [HttpPost]
        public List<ApartmentTypeDTO> ApartmentTypeSearch(string apTypeCode, string apTypeName, int userID)
        {
            return apartmentTypeAppService.ApartmentType_Search(
                apTypeCode,
                apTypeName,
                userID
                );
        }

        [HttpPost]
        public List<ApartmentTypeDTO> ApartmentTypeGetAll()
        {
            return apartmentTypeAppService.ApartmentType_GetAll();
        }

        [HttpPost]
        public ApartmentTypeDTO ApartmentTypeByID(string apartmentTypeID)
        {
            return apartmentTypeAppService.ApartmentType_ByID(apartmentTypeID);
        }

        [HttpPost]
        public List<dynamic> ApartmentTypeInsert([FromBody] ApartmentTypeDTO dto)
        {
            return apartmentTypeAppService.ApartmentType_Insert(dto);
        }

        [HttpPost]
        public List<dynamic> ApartmentTypeUpdate([FromBody] ApartmentTypeDTO dto)
        {
            return apartmentTypeAppService.ApartmentType_Update(dto);
        }

        [HttpDelete]
        public List<dynamic> ApartmentTypeDelete(string ApartmentTypeID)
        {
            return apartmentTypeAppService.ApartmentType_Delete(ApartmentTypeID);
        }

        [HttpPost]
        public List<dynamic> ApartmentTypeApprove(string ApartmentTypeID, string checkerID)
        {
            return apartmentTypeAppService.ApartmentType_Approve(ApartmentTypeID, checkerID);
        }

        [HttpPost]
        public List<ApartmentTypeDTO> ApartmentType_ExportExcel([FromBody] List<ApartmentTypeDTO> listInput)
        {
            try
            {
                return apartmentTypeAppService.ApartmentType_ExportExcel(listInput);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        [HttpGet]
        public List<dynamic> ApartmentType_GetAuthStatusName(string apartmentTypeID)
        {
            return apartmentTypeAppService.ApartmentType_GetAuthStatusName(apartmentTypeID);
        }
    }
}
