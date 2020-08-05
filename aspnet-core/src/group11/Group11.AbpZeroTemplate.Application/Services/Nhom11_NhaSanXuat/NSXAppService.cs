using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Aspose.Cells;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_NhaSanXuat.NhaSanXuat_DTO;
using GSoft.AbpZeroTemplate.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_NhaSanXuat
{
    public interface INSXAppService : IApplicationService
    {
        PagedResultDto<NSX_DTO> NSX_Search(NSX_DTO filterInput);
        List<NSX_DTO> NSX_Delete(string nsxID);
        List<NSX_DTO> NSX_ExportExcel(List<NSX_DTO> listInput);
    }
    public class NSXAppService : BaseService, INSXAppService
    {
        public List<NSX_DTO> NSX_Delete(string nsxID)
        {

            return procedureHelper.GetData<NSX_DTO>("NhaSanXuat_Delete", new
            {
                NSX_ID = nsxID,
            });
        }

        public List<NSX_DTO> NSX_ExportExcel(List<NSX_DTO> listInput)
        {
            if (listInput == null)
            {
                return null;
            }

            //Create the workbook and the worksheet
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Danh sách nhà sản xuất";

            //Set a title and format its style
            var cell = sheet.Cells["A1"];
            cell.PutValue("DANH SÁCH NHÀ SẢN XUẤT");

            Style style = cell.GetStyle();
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Font.Size = 20;
            style.Font.IsBold = true;
            style.ForegroundColor = Color.AliceBlue;
            style.Pattern = BackgroundType.Solid;
            cell.SetStyle(style);

            List<string> listProperty = new List<string> { "Mã nhà sản xuất", "Tên nhà sản xuất", "Địa chỉ","Ghi chú" };
            int countProperty = 0;
            for (char c = 'A'; c <= 'D'; c++)
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
                , new string[] { "NSX_CODE", "NSX_NAME","NSX_FROM", "NOTES"}
                , false, 3, 0, listInput.Count
                , false
                , "dd/mm/yyyy"
                , true);


            //Set the columns to fit the size of their content
            sheet.AutoFitColumns();

            //Save the Excel workbook
            var fileName1 = sheet.Name + ".xlsx";
            workbook.Save(fileName1, SaveFormat.Xlsx);


            return listInput;
        }

        public PagedResultDto<NSX_DTO> NSX_Search(NSX_DTO filterInput)
        {
            if (filterInput.RECORD_STATUS == null) {
                filterInput.RECORD_STATUS = "1";
            }

            var list = procedureHelper.GetData<NSX_DTO>("NhaSanXuat_Search", filterInput);
            
            var totalCount = list.Count();


            return new PagedResultDto<NSX_DTO>(
               totalCount,
               list
            );
        }
    }
}
