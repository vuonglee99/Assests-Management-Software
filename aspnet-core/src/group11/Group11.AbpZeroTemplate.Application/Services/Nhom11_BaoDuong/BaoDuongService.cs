using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_CTBD.Dto;
using Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_BaoDuong.BaoDuong_DTO;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Aspose.Cells;
using System.Drawing;
using GSoft.AbpZeroTemplate.Dto;

namespace Group11.AbpZeroTemplate.Web.Core.Services.Nhom11_BaoDuong
{
    public interface IBaoDuongService : IApplicationService
    {
        PagedResultDto<BD_DTO> BD_Search(BD_DTO filterInput);
        List<BD_DTO> BD_Insert(BD_DTO filterInput);
        List<BD_DTO> BD_Update(BD_DTO filterInput);
        List<BD_DTO> BD_Delete(string bdId);
        List<BD_DTO> BD_GetByCode(string bdCode);
        List<BD_DTO> BD_GetByID(string bdId);
        List<BD_DTO> ExportData(List<BD_DTO> listInput);
        List<BD_DTO> BD_Approve_Insert(string bdId);
        List<BD_DTO> BD_Approve_Delete(string bdId);
        List<BD_DTO> BD_Approve_Update(string bdCode);
        List<BD_DTO> BD_Deny_Insert(string bdId);
        List<BD_DTO> BD_Deny_Delete(string bdId);
        List<BD_DTO> BD_Deny_Update(string bdCode);
        bool BD_IsUpdating(string bdCode);
        PagedResultDto<BD_DTO> BD_GetAll(BD_DTO filterInput);
    }
    public class BaoDuongService : BaseService, IBaoDuongService
    {
        public PagedResultDto<BD_DTO> BD_Search(BD_DTO filterInput)
        {
            List<BD_DTO> listBaoDuong = procedureHelper.GetData<BD_DTO>("BD_SEARCH", filterInput);

            int count = listBaoDuong.Count();

            return new PagedResultDto<BD_DTO> { TotalCount = count, Items = listBaoDuong };
        }

        public List<BD_DTO> BD_Delete(string bdId)
        {
            List<BD_DTO> listBD = procedureHelper.GetData<BD_DTO>("BD_DELETE", new BD_DTO { BD_ID = bdId });

            return listBD;
        }

        public List<BD_DTO> BD_GetByCode(string bdCode)
        {
            List<BD_DTO> listBD = procedureHelper.GetData<BD_DTO>("BD_GETBYCode", new BD_DTO { BD_CODE = bdCode });
            List<ChiTietBaoDuong_DTO> listCTBD = procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBD_GetByBD_ID", new ChiTietBaoDuong_DTO
            {
                BD_ID = "BD000000000000000004",
            });

            return listBD;
        }

        public List<BD_DTO> BD_Insert(BD_DTO filterInput)
        {
            return procedureHelper.GetData<BD_DTO>("BD_INSERT", filterInput);
        }

        public List<BD_DTO> BD_Update(BD_DTO filterInput)
        {
            return procedureHelper.GetData<BD_DTO>("BD_UPDATE", filterInput);
        }

        public List<BD_DTO> BD_GetByID(string bdId)
        {
            List<BD_DTO> listBD = procedureHelper.GetData<BD_DTO>("BD_GETBYID", new BD_DTO { BD_ID = bdId });
            List<ChiTietBaoDuong_DTO> listCTBD = procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBD_GetByBD_ID", new ChiTietBaoDuong_DTO
            {
                BD_ID = bdId,
            });

            return listBD;
        }

        public List<BD_DTO> ExportData(List<BD_DTO> listInput)
        {
            if (listInput == null)
            {
                return null;
            }

            //Create the workbook and the worksheet
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Danh Sách Phiếu Bảo Dưỡng";

            //Set a title and format its style
            var cell = sheet.Cells["A1"];
            cell.PutValue("BẢO DƯỠNG XE");

            Style style = cell.GetStyle();
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Font.Size = 20;
            style.Font.IsBold = true;
            style.ForegroundColor = Color.AliceBlue;
            style.Pattern = BackgroundType.Solid;
            cell.SetStyle(style);

            List<string> listProperty = new List<string> { "Mã Bảo Dưỡng", "Garage", "Địa chỉ", "Tổng tiền", "Mã xe", "Ngày bắt đầu", "Ngày kết thúc", "Ngày tạo" };
            int countProperty = 0;
            for (char c = 'A'; c <= 'H'; c++)
            {
                var headerCell = sheet.Cells[c + "2"];
                headerCell.PutValue(listProperty[countProperty]);
                countProperty++;
            }

            Row header = sheet.Cells.Rows[1];
            StyleFlag headerFlag = new StyleFlag() { All = true };
            style = header.Style;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.ForegroundColor = Color.LightGreen;
            style.BackgroundColor = Color.LightGreen;
            style.Font.Size = 15;
            style.Font.IsBold = true;
            header.ApplyStyle(style, headerFlag);

            sheet.Cells.ImportCustomObjects((System.Collections.ICollection)listInput
                , new string[] { "BD_CODE", "BD_GARAGE", "BD_ADDRESS", "BD_PRICE", "XE_ID", "BD_FROM_DT", "BD_TO_DT", "CREATED_DT" }
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

        public List<BD_DTO> BD_Approve_Insert(string bdId)
        {
            return procedureHelper.GetData<BD_DTO>("BD_APPROVE_INSERT", new BD_DTO { BD_ID = bdId });
        }

        public List<BD_DTO> BD_Approve_Delete(string bdId)
        {
            return procedureHelper.GetData<BD_DTO>("BD_APPROVE_DELETE", new BD_DTO { BD_ID = bdId });
        }

        public List<BD_DTO> BD_Approve_Update(string bdCode)
        {
            return procedureHelper.GetData<BD_DTO>("BD_APPROVE_UPDATE", new BD_DTO { BD_CODE = bdCode });
        }

        public List<BD_DTO> BD_Deny_Insert(string bdId)
        {
            return procedureHelper.GetData<BD_DTO>("BD_DENY_INSERT", new BD_DTO { BD_ID = bdId });
        }

        public List<BD_DTO> BD_Deny_Delete(string bdId)
        {
            return procedureHelper.GetData<BD_DTO>("BD_DENY_DELETE", new BD_DTO { BD_ID = bdId });
        }

        public List<BD_DTO> BD_Deny_Update(string bdCode)
        {
            return procedureHelper.GetData<BD_DTO>("BD_DENY_UPDATE", new BD_DTO { BD_CODE = bdCode });
        }
        public bool BD_IsUpdating(string bdCode)
        {
            var list = procedureHelper.GetData<BD_DTO>("BD_IsUpdating", new BD_DTO { BD_CODE = bdCode });
            if (list.Count() > 1)
                return true;
            return false;
        }
        public PagedResultDto<BD_DTO> BD_GetAll(BD_DTO filterInput)
        {
            List<BD_DTO> listBaoDuong = procedureHelper.GetData<BD_DTO>("BD_GETALL", filterInput);

            int count = listBaoDuong.Count();

            return new PagedResultDto<BD_DTO> { TotalCount = count, Items = listBaoDuong };
        }

    }
}
