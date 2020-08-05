using Abp.Application.Services;
using Abp.Authorization;
using Group4.AbpZeroTemplate.Application;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Group4.AbpZeroTemplate.Web.Core.Services.ApartmentsResidents
{
    public interface IApartmentResidentAppService : IApplicationService
    {
        List<ResidentDTO> ApartmentResident_ResidentByApartment(string apartmentID);
        List<ResidentDTO> ApartmentResident_FreeResidentByApartment(string apartmentID);
        List<ResidentDTO> ApartmentResident_SearchFreeResident(string apartmentID, string filter);
        List<dynamic> ApartmentResident_Insert(string apartmentID, string residentID, string makerID);
        List<dynamic> ApartmentResident_Delete(string apartmentID, string residentID);
    }

    [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentResident)]
    public class ApartmentResidentAppService : BaseService, IApartmentResidentAppService
    {
        public List<ResidentDTO> ApartmentResident_ResidentByApartment(string apartmentID)
        {
            return procedureHelper.GetData<ResidentDTO>("APARTMENT_RESIDENT_ResidentByApartment", new
            {
                APARTMENT_ID = apartmentID,
            });
        }

        public List<ResidentDTO> ApartmentResident_FreeResidentByApartment(string apartmentID)
        {
            return procedureHelper.GetData<ResidentDTO>("APARTMENT_RESIDENT_FreeResidentByApartment", new
            {
                APARTMENT_ID = apartmentID,
            });
        }

        public List<ResidentDTO> ApartmentResident_SearchFreeResident(string apartmentID, string filter)
        {
            return procedureHelper.GetData<ResidentDTO>("APARTMENT_RESIDENT_SearchFreeResident", new
            {
                APARTMENT_ID = apartmentID,
                SearchString = filter,
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentResident_Add)]
        public List<dynamic> ApartmentResident_Insert(string apartmentID, string residentID, string makerID)
        {
            return procedureHelper.GetData<dynamic>("APARTMENT_RESIDENT_Insert", new
            {
                APARTMENT_ID = apartmentID,
                RESIDENT_ID = residentID,
                MAKER_ID = makerID,
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentResident_Delete)]
        public List<dynamic> ApartmentResident_Delete(string apartmentID, string residentID)
        {
            return procedureHelper.GetData<dynamic>("APARTMENT_RESIDENT_Delete", new
            {
                APARTMENT_ID = apartmentID,
                RESIDENT_ID = residentID
            });
        }
    }
}
