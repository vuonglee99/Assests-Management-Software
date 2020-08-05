using Abp.AspNetCore.Mvc.Controllers;
using Group4.AbpZeroTemplate.Web.Core.Services.Apartments;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace Group4.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ApartmentController : AbpController
    {
        private readonly IApartmentAppService apartmentAppService;

        public ApartmentController(IApartmentAppService apartmentAppService)
        {
            this.apartmentAppService = apartmentAppService;
        }

        [HttpGet]
        public List<ApSearchDTO> ApartmentSearch(string apartmentCode, string apartmentName, string apTypeID, string buildingID, string floor_id, string status, string auth_status, int max_tenant, string approve)
        {
            return apartmentAppService.Apartment_Search(
                apartmentCode,
                apartmentName,
                apTypeID,
                buildingID,
                floor_id,
                status,
                auth_status,
                max_tenant,
                approve);
        }

        [HttpPost]
        public ApartmentDTO ApartmentByID(string apartmentID)
        {
            return apartmentAppService.Apartment_ByID(apartmentID);
        }

        [HttpPost]
        public List<dynamic> ApartmentInsert([FromBody] ApartmentDTO dto)
        {
            return apartmentAppService.Apartment_Insert(dto);
        }

        [HttpPost]
        public List<dynamic> ApartmentUpdate([FromBody] ApartmentDTO dto)
        {
            return apartmentAppService.Apartment_Update(dto);
        }

        [HttpDelete]
        public List<dynamic> ApartmentDelete(string apartmentID)
        {
            return apartmentAppService.Apartment_Delete(apartmentID);
        }

        [HttpPost]
        public List<dynamic> ApartmentApprove(string apartmentID, string checkerID)
        {
            return apartmentAppService.Apartment_Approve(apartmentID, checkerID);
        }

        [HttpPost]
        public List<dynamic> ApartmentDeny(string apartmentID, string checkerID)
        {
            return apartmentAppService.Apartment_Deny(apartmentID, checkerID);
        }


        [HttpPost]
        public List<CustomApartmentDTO> Apartment_ExportExcel([FromBody] List<CustomApartmentDTO> listInput)
        {
            try
            {
                return apartmentAppService.Apartment_ExportExcel(listInput);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        [HttpGet]
        public List<dynamic> Apartment_GetAuthStatusName(string apartmentID)
        {
            return apartmentAppService.Apartment_GetAuthStatusName(apartmentID);
        }

         [HttpGet]
        public List<dynamic> Apartment_FindNewID(string apartmentID)
        {
            return apartmentAppService.Apartment_FindNewID(apartmentID);
        }
    }
}
