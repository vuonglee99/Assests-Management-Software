using Abp.Application.Services;
using Abp.Authorization;
using Aspose.Cells;
using Group4.AbpZeroTemplate.Application;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Group4.AbpZeroTemplate.Web.Core.Services.ApartmentTypes
{
    public interface IApartmentTypeAppService : IApplicationService
    {
        List<ApartmentTypeDTO> ApartmentType_Search(string apTypeCode, string apTypeName, int userID );
        ApartmentTypeDTO ApartmentType_ByID(string ApartmentTypeID);
        List<dynamic> ApartmentType_Insert(ApartmentTypeDTO dto);
        List<dynamic> ApartmentType_Update(ApartmentTypeDTO dto);
        List<dynamic> ApartmentType_Delete(string ApartmentTypeID);
        List<dynamic> ApartmentType_Approve(string ApartmentTypeID, string checkerID);
        List<ApartmentTypeDTO> ApartmentType_ExportExcel(List<ApartmentTypeDTO> listInput);
        List<dynamic> ApartmentType_GetAuthStatusName(string apartmentTypeID);
        List<ApartmentTypeDTO> ApartmentType_GetAll();
    }

    [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType)]
    public class ApartmentTypeAppService : BaseService, IApartmentTypeAppService
    {
        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType_View)]
        public List<ApartmentTypeDTO> ApartmentType_Search(string apTypeCode, string apTypeName, int userID)
        {
            return procedureHelper.GetData<ApartmentTypeDTO>("ApartmentType_Search", new
            {
                APARTMENT_TYPE_CODE = apTypeCode,
                APARTMENT_TYPE_NAME = apTypeName,
                USER_ID=userID
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType_View)]
        public List<ApartmentTypeDTO> ApartmentType_GetAll()
        {
            return procedureHelper.GetData<ApartmentTypeDTO>("ApartmentType_GetAll", new
            {
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType_View)]
        public ApartmentTypeDTO ApartmentType_ByID(string apTypeID)
        {
            return procedureHelper.GetData<ApartmentTypeDTO>("ApartmentType_ByID", new
            {
                APARTMENT_TYPE_ID = apTypeID,
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType_Add)]
        public List<dynamic> ApartmentType_Insert(ApartmentTypeDTO dto)
        {
            return procedureHelper.GetData<dynamic>("ApartmentType_Insert", dto);
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType_Update)]
        public List<dynamic> ApartmentType_Update(ApartmentTypeDTO dto)
        {
            return procedureHelper.GetData<dynamic>("ApartmentType_Update", dto);
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType_Delete)]
        public List<dynamic> ApartmentType_Delete(string apTypeID)
        {
            return procedureHelper.GetData<dynamic>("ApartmentType_Delete", new
            {
                APARTMENT_TYPE_ID = apTypeID,
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType_Approve)]
        public List<dynamic> ApartmentType_Approve(string apTypeID, string checkerID)
        {
            return procedureHelper.GetData<dynamic>("ApartmentType_Update", new
            {
                APARTMENT_TYPE_ID = apTypeID,
                CHECKER_ID = checkerID,
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_ApartmentType_View)]
        public List<ApartmentTypeDTO> ApartmentType_ExportExcel(List<ApartmentTypeDTO> listInput)
        {

            if (listInput == null)
            {
                return null;
            }

            //Create the workbook and the worksheet
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Danh sách loại căn hộ";

            //Set a title and format its style
            var cell = sheet.Cells["A1"];
            cell.PutValue("DANH SÁCH LOẠI CĂN HỘ");

            Style style = cell.GetStyle();
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Font.Size = 20;
            style.Font.IsBold = true;
            style.ForegroundColor = Color.AliceBlue;
            style.Pattern = BackgroundType.Solid;
            cell.SetStyle(style);

            List<string> listProperty = new List<string> { "Mã loại căn hộ", "Tên loại căn hộ" };
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
                , new string[] { "APARTMENT_TYPE_CODE", "APARTMENT_TYPE_NAME"}
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

        public List<dynamic> ApartmentType_GetAuthStatusName(string apartmentTypeID)
        {
            return procedureHelper.GetData<dynamic>("AuthStatusNameByApTypeID", new
            {
                APARTMENT_TYPE_ID = apartmentTypeID
            });
        }
    }
}
