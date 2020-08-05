using Abp.AspNetCore.Mvc.Controllers;
using Group4.AbpZeroTemplate.Web.Core.Services.ApartmentsResidents;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Group4.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ApartmentResidentController : AbpController
    {
        private readonly IApartmentResidentAppService apartmentResidentAppService;

        public ApartmentResidentController(IApartmentResidentAppService apartmentAppService)
        {
            this.apartmentResidentAppService = apartmentAppService;
        }

        [HttpGet]
        public List<ResidentDTO> ApartmentResident_ResidentByApartment(string apartmentID)
        {
            return apartmentResidentAppService.ApartmentResident_ResidentByApartment(apartmentID);
        }

        [HttpGet]
        public List<ResidentDTO> ApartmentResident_FreeResidentByApartment(string apartmentID)
        {
            return apartmentResidentAppService.ApartmentResident_FreeResidentByApartment(apartmentID);
        }

        [HttpGet]
        public List<ResidentDTO> ApartmentResident_SearchFreeResident(string apartmentID, string filter)
        {
            return apartmentResidentAppService.ApartmentResident_SearchFreeResident(apartmentID, filter);
        }

        [HttpPost]
        public List<dynamic> ApartmentResident_Insert(string apartmentID, string residentID, string makerID)
        {
            return apartmentResidentAppService.ApartmentResident_Insert(apartmentID, residentID, makerID);
        }

        [HttpDelete]
        public List<dynamic> ApartmentResident_Delete(string apartmentID, string residentID)
        {
            return apartmentResidentAppService.ApartmentResident_Delete(apartmentID, residentID);
        }
    }
}
