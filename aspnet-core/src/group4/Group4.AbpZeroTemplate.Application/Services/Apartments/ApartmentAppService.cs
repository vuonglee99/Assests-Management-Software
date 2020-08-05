using Abp.Application.Services;
using Abp.Authorization;
using Aspose.Cells;
using Group4.AbpZeroTemplate.Application;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Group4.AbpZeroTemplate.Web.Core.Services.Apartments
{
    public interface IApartmentAppService : IApplicationService
    {
        List<ApSearchDTO> Apartment_Search(string apartmentCode, string apartmentName, string apTypeID, string buildingID, string floor_id, string status, string auth_status, int max_tenant, string approve);
        ApartmentDTO Apartment_ByID(string apartmentID);
        List<dynamic> Apartment_Insert(ApartmentDTO dto);
        List<dynamic> Apartment_Update(ApartmentDTO dto);
        List<dynamic> Apartment_Delete(string apartmentID);
        List<dynamic> Apartment_Approve(string apartmentID, string checkerID);
        List<CustomApartmentDTO> Apartment_ExportExcel(List<CustomApartmentDTO> listInput);
        List<dynamic> Apartment_GetAuthStatusName(string apartmentID);
        List<dynamic> Apartment_Deny(string apartmentID, string checkerID);
        List<dynamic> Apartment_FindNewID(string apartmentID);


    }

    [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment)]
    public class ApartmentAppService : BaseService, IApartmentAppService
    {
        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment_View)]
        public List<ApSearchDTO> Apartment_Search(string apartmentCode, string apartmentName, string apTypeID, string buildingID, string floor_id, string status, string auth_status, int max_tenant, string approve)
        {
            return procedureHelper.GetData<ApSearchDTO>("Apartment_Search", new ApartmentDTO
            {
                APARTMENT_CODE = apartmentCode,
                APARTMENT_NAME = apartmentName,
                APARTMENT_TYPE_ID = apTypeID,
                BUILDING_ID = buildingID,
                Floor_ID = floor_id,
                APARTMENT_STATUS = status,
                AUTH_STATUS = auth_status,
                APARTMENT_MAX_TENANT = max_tenant,
                APPROVE_PERMISSION = approve
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment_View)]
        public ApartmentDTO Apartment_ByID(string apartmentID)
        {
            return procedureHelper.GetData<ApartmentDTO>("Apartment_ByID", new
            {
                APARTMENT_ID = apartmentID,
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment_Add)]
        public List<dynamic> Apartment_Insert(ApartmentDTO dto)
        {
            return procedureHelper.GetData<dynamic>("Apartment_Insert", dto);
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment_Update)]
        public List<dynamic> Apartment_Update(ApartmentDTO dto)
        {
            return procedureHelper.GetData<dynamic>("Apartment_Update", dto);
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment_Delete)]
        public List<dynamic> Apartment_Delete(string apartmentID)
        {
            return procedureHelper.GetData<dynamic>("Apartment_Delete", new
            {
                APARTMENT_ID = apartmentID,
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment_Approve)]
        public List<dynamic> Apartment_Approve(string apartmentID, string checkerID)
        {
            return procedureHelper.GetData<dynamic>("Apartment_Approve", new
            {
                APARTMENT_ID = apartmentID,
                CHECKER_ID = checkerID,
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment_View)]
        public List<CustomApartmentDTO> Apartment_ExportExcel(List<CustomApartmentDTO> listInput)
        {
            if (listInput == null)
            {
                return null;
            }

            //Create the workbook and the worksheet
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Danh sách căn hộ";

            //Set a title and format its style
            var cell = sheet.Cells["A1"];
            cell.PutValue("DANH SÁCH CĂN HỘ");

            Style style = cell.GetStyle();
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Font.Size = 20;
            style.Font.IsBold = true;
            style.ForegroundColor = Color.AliceBlue;
            style.Pattern = BackgroundType.Solid;
            cell.SetStyle(style);

            List<string> listProperty = new List<string> { "Mã căn hộ", "Tên căn hộ", "Loại căn hộ", "Tòa nhà", "Tầng", "Số người ở", "Giá", "Trạng thái căn hộ", "Trạng thái duyệt" };
            int countProperty = 0;
            for (char c = 'A'; c <= 'Z' && countProperty < listProperty.Count; c++)
            {
                var headerCell = sheet.Cells[c + "3"];
                headerCell.PutValue(listProperty[countProperty]);
                countProperty++;
            }

            Row header = sheet.Cells.Rows[2];
            StyleFlag headerFlag = new StyleFlag() { All = true };
            style = header.Style;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.ForegroundColor = Color.LightGreen;
            style.BackgroundColor = Color.LightGreen;
            style.Font.Size = 15;
            style.Font.IsBold = true;
            header.ApplyStyle(style, headerFlag);

            sheet.Cells.ImportCustomObjects((System.Collections.ICollection)listInput
                , new string[] { "APARTMENT_CODE", "APARTMENT_NAME", "APARTMENT_TYPE_NAME", "BUILDING_NAME", "Floor_NAME", "NUMBER_OF_PEOPLE", "APARTMENT_PRICE", "APARTMENT_STATUS", "AUTH_STATUS" }
                , false, 3, 0, listInput.Count
                , false
                , "dd/mm/yyyy"
                , false);


            //Set the columns to fit the size of their content
            sheet.AutoFitColumns();

            //Save the Excel workbook
            var fileName1 = sheet.Name + ".xlsx";
            workbook.Save(fileName1, SaveFormat.Xlsx);


            return listInput;
        }

        public List<dynamic> Apartment_GetAuthStatusName(string apartmentID)
        {
            return procedureHelper.GetData<dynamic>("AuthStatusNameByApID", new
            {
                APARTMENT_ID = apartmentID,
            });
        }

             [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Apartment_Approve)]
        public List<dynamic> Apartment_Deny(string apartmentID, string checkerID)
        {
            return procedureHelper.GetData<dynamic>("Apartment_Deny", new
            {
                APARTMENT_ID = apartmentID,
                CHECKER_ID = checkerID,
            });
        }

         public List<dynamic> Apartment_FindNewID (string apartmentID)
        {
            return procedureHelper.GetData<dynamic>("GetWaitingUpdateApartment", new
            {
                APARTMENT_ID = apartmentID,
            });
        }
    }
}
