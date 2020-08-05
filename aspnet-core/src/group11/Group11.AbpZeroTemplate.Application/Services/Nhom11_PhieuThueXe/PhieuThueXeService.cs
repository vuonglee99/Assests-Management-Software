using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Aspose.Cells;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_PhieuThueXe.PhieuThueXe_DTO
{
    public interface IPhieuThueXeService : IApplicationService
    {
        List<PTX_DTO> DeletCustomer(string phieuThue_Code);
        List<PTX_DTO> Insert(PTX_DTO phieuThue);
        List<PTX_DTO> Update(PTX_DTO phieuThue);
        List<PTX_DTO> GetByCode(string phieuThue_Code);
        PagedResultDto<PTX_DTO> PTX_ExportExcel();

    }
    public class PhieuThueXeService : BaseService, IPhieuThueXeService
    {
        public List<PTX_DTO> DeletCustomer(string phieuThue_Code)
        {
            return procedureHelper.GetData<PTX_DTO>("PTX_Delete", new PTX_DTO
            {
                PTX_CODE = phieuThue_Code,
            });
        }

        public List<PTX_DTO> GetByCode(string phieuThue_Code)
        {
            return procedureHelper.GetData<PTX_DTO>("PTX_GetByCode", new PTX_DTO
            {
                PTX_CODE = phieuThue_Code,
            });
        }

        public List<PTX_DTO> Insert(PTX_DTO phieuThue)
        {
            return procedureHelper.GetData<PTX_DTO>("PTX_Insert", phieuThue);
        }

        public List<PTX_DTO> Update(PTX_DTO phieuThue)
        {
            return procedureHelper.GetData<PTX_DTO>("PTX_Update", phieuThue);
        }

        public PagedResultDto<PTX_DTO> PTX_ExportExcel()
        {
            List<PTX_DTO> listPTX = procedureHelper.GetData<PTX_DTO>("PTX_SEARCH", new PTX_DTO());
            int count = listPTX.Count();

            if (listPTX == null)
            {
                return null;
            }

            //Create the workbook and the worksheet
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Danh sách phiếu thuê";

            //Set a title and format its style
            var cell = sheet.Cells["A1"];
            cell.PutValue("DANH SÁCH PHIẾU THUÊ");

            Style style = cell.GetStyle();
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Font.Size = 20;
            style.Font.IsBold = true;
            style.ForegroundColor = Color.AliceBlue;
            style.Pattern = BackgroundType.Solid;
            cell.SetStyle(style);

            List<string> listProperty = new List<string> { "Mã phiếu thuê","Ngày thuê","Ngày hết hạn","Ngày trả","Tổng tiền","Ghi chú","Mã xe","Mã người thuê","Ngày tạo" };
            int countProperty = 0;
            for (char c = 'A'; c <= 'I'; c++)
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

            sheet.Cells.ImportCustomObjects((System.Collections.ICollection)listPTX
                , new string[] { "PTX_CODE","PTX_RENT_DT","PTX_EXP_DT","PTX_RETURN_DT","PTX_PRICE","PTX_NOTE","XE_ID","NTX_ID","CREATED_DT"}
                , false, 3, 0, listPTX.Count
                , false
                , "dd/mm/yyyy"
                , false);


            //Set the columns to fit the size of their content
            sheet.AutoFitColumns();

            //Save the Excel workbook
            var fileName1 = sheet.Name + ".xlsx";
            workbook.Save(fileName1, SaveFormat.Xlsx);


            return new PagedResultDto<PTX_DTO> { TotalCount = count, Items = listPTX };
        }
    }
}
