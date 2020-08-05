using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Aspose.Cells;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_Xe.Xe_DTO;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_Xe
{
    public interface IXeService : IApplicationService
    {
        PagedResultDto<XE_DTO> XE_Search(XE_DTO filterInput);
        List<XE_DTO> XE_Insert(XE_DTO filterInput);
        List<XE_DTO> XE_Update(XE_DTO filterInput);
        List<XE_DTO> XE_FindByCode(string xeCode);
        List<XE_DTO> XE_Delete(string xeId);
        List<XE_DTO> XE_ExportExcel(List<XE_DTO> listInput);
        PagedResultDto<XE_DTO> XE_ByNtxID(string Id);
        XE_DTO XE_ById(string Id);


    }
    public class XeService : BaseService, IXeService
    {
        public List<XE_DTO> XE_Delete(string xeId)
        {
            return procedureHelper.GetData<XE_DTO>("XE_DELETE", new XE_DTO
            {
                XE_ID = xeId
            });
        }

        public List<XE_DTO> XE_ExportExcel(List<XE_DTO> listInput)
        {

            if (listInput == null)
            {
                return null;
            }

            //Create the workbook and the worksheet
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Danh sách xe";

            //Set a title and format its style
            var cell = sheet.Cells["A1"];
            cell.PutValue("DANH SÁCH XE");

            Style style = cell.GetStyle();
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Font.Size = 20;
            style.Font.IsBold = true;
            style.ForegroundColor = Color.AliceBlue;
            style.Pattern = BackgroundType.Solid;
            cell.SetStyle(style);

            List<string> listProperty = new List<string> { "Mã xe", "Tên xe","Màu xe","Số chỗ ngồi", "Tên mẫu", "Chứng chỉ", "Giá","Tiêu thụ","Ghi chú"
                ,"Tốc độ tối đa", "Nhà sản xuất","Năm sản xuất","Trạng thái","Thời gian tạo","Tổng quãng đường đi" };
            int countProperty = 0;
            for (char c = 'A'; c <= 'O'; c++)
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
                , new string[] { "XE_CODE", "XE_NAME", "XE_COLOR", "XE_SEATS", "XE_MODEL", "XE_LICENSE_PLATE", "XE_PRICE", "XE_CONSUMPTION","XE_NOTES"
                ,"XE_MAX_SPEED","XE_MANUFACTURER","XE_MANUFACTURER_YEAR","XE_STATUS","CREATED_DT","XE_TOTAL_KM"}
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

        public List<XE_DTO> XE_FindByCode(string xeCode)
        {
            return procedureHelper.GetData<XE_DTO>("XE_GETBYCODE", new XE_DTO
            {
                XE_CODE = xeCode
            });
        }

        public List<XE_DTO> XE_Insert(XE_DTO filterInput)
        {
            return procedureHelper.GetData<XE_DTO>("XE_INSERT", filterInput);
        }

        public PagedResultDto<XE_DTO> XE_Search(XE_DTO filterInput)
        {
            var list = procedureHelper.GetData<XE_DTO>("XE_SEARCH", new XE_DTO
            {
                XE_CODE = filterInput.XE_CODE,
                XE_NAME = filterInput.XE_NAME,
                XE_COLOR = filterInput.XE_COLOR,
            });

            var totalCount = list.Count();
            return new PagedResultDto<XE_DTO> { TotalCount = totalCount, Items = list };
        }

        public List<XE_DTO> XE_Update(XE_DTO filterInput)
        {
            return procedureHelper.GetData<XE_DTO>("XE_UPDATE", filterInput);
        }
        public PagedResultDto<XE_DTO> XE_ByNtxID(string Id)
        {

            var list = procedureHelper.GetData<XE_DTO>("XE_ByNTX_ID", new { NTX_ID = Id }).ToList();
            var totalCount = list.Count();


            return new PagedResultDto<XE_DTO>(
               totalCount,
               list
            );
        }

        public XE_DTO XE_ById(string Id)
        {
            return procedureHelper.GetData<XE_DTO>("XE_GETBYID", new { XE_ID = Id }).ToList().FirstOrDefault();
        }

    }
}
