using Abp.Application.Services;
using Abp.Authorization;
using Aspose.Cells;
using Group6.AbpZeroTemplate.Application;
using Group6.AbpZeroTemplate.Web.Core.Services.TrangThaiYeuCauSuaChuas.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group6.AbpZeroTemplate.Web.Core.Services.TrangThaiYeuCauSuaChuas
{
    public interface ITTYCSCAppService : IApplicationService
    {
        dynamic TTYCSC_Delete(string TTYCSCId);

        TTYCSC_DTO TTYCSC_ById(string TTYCSCId);

        PageResult TTYCSC_SearchFilter(int pageNumber, int pageSize, string TTYCSCName, string TTYCSCCode, string TTYCSCDesc, string APPROVEStatus);

        dynamic TTYCSC_Create(TTYCSC_DTO input);

        string TTYCSC_GetValueGenCode();

        dynamic TTYCSC_Update(TTYCSC_DTO input);

        dynamic TTYCSC_Approve(TTYCSCApprove_DTO input);

        dynamic TTYCSC_ExportExcel(int pageNumber, int pageSize, string TTYCSCName, string TTYCSCCode, string TTYCSCDesc, string APPROVEStatus);
    }

    //[AbpAuthorize(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua)]
    public class TTYCSCAppService : BaseService, ITTYCSCAppService
    {
        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_ById)]
        public TTYCSC_DTO TTYCSC_ById(string TTYCSCId)
        {
            string maker_id = AbpSession.UserId.ToString();
            return procedureHelper.GetData<TTYCSC_DTO>("TTYCSC_ById", new
            {
                TTYCSC_ID = TTYCSCId,
                MAKER_ID = maker_id
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_Delete)]
        public dynamic TTYCSC_Delete(string TTYCSCId)
        {
            return procedureHelper.GetData<dynamic>("TTYCSC_Delete", new
            {
                TTYCSC_ID = TTYCSCId
            }).FirstOrDefault();
        }

        //[AbpAuthorize(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_SearchFilter)]
        public PageResult TTYCSC_SearchFilter(int pageNumber, int pageSize, string TTYCSCName, string TTYCSCCode, string TTYCSCDesc, string APPROVEStatus)
        {
            var result = procedureHelper.GetData<dynamic>("TTYCSC_SearchFilter", new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TTYCSC_NAME = TTYCSCName,
                TTYCSC_CODE = TTYCSCCode,
                TTYCSC_DESC = TTYCSCDesc,
                APPROVE_STATUS = APPROVEStatus
            });
            var total_record = procedureHelper.GetData<int>("TTYCSC_GetCount", new
            {
                TTYCSC_NAME = TTYCSCName,
                TTYCSC_CODE = TTYCSCCode,
                TTYCSC_DESC = TTYCSCDesc,
                APPROVE_STATUS = APPROVEStatus
            }).FirstOrDefault();

            return PageResult.CreateSuccess(result, total_record);
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_Create)]
        public dynamic TTYCSC_Create(TTYCSC_DTO input)
        {
            input.MAKER_ID = AbpSession.UserId.ToString();
            input.RECORD_STATUS = "1";
            return procedureHelper.GetData<dynamic>("TTYCSC_Create", input).FirstOrDefault();
        }

        public string TTYCSC_GetValueGenCode()
        {
            return procedureHelper.GetData<string>("TTYCSC_GetValueGenCode", new { }).FirstOrDefault();
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_Update)]
        public dynamic TTYCSC_Update(TTYCSC_DTO input)
        {
            return procedureHelper.GetData<dynamic>("TTYCSC_Update", input).FirstOrDefault();
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_Approve)]
        public dynamic TTYCSC_Approve(TTYCSCApprove_DTO input)
        {
            input.CHECKER_ID = AbpSession.UserId.ToString();
            return procedureHelper.GetData<dynamic>("TTYCSC_Approve", input).FirstOrDefault();
        }

        public dynamic TTYCSC_ExportExcel(int pageNumber, int pageSize, string TTYCSCName, string TTYCSCCode, string TTYCSCDesc, string APPROVEStatus)
        {
            try
            {
                var list = procedureHelper.GetData<dynamic>("TTYCSC_SearchFilter", new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TTYCSC_NAME = TTYCSCName,
                    TTYCSC_CODE = TTYCSCCode,
                    TTYCSC_DESC = TTYCSCDesc,
                    APPROVE_STATUS = APPROVEStatus
                });

                if (list == null)
                {
                    return null;
                }

                list.ForEach(l =>
                {
                    l.AUTH_STATUS = l.AUTH_STATUS == "A" ? "Đã duyệt" : (l.AUTH_STATUS == "U" ? "Từ chối" : "Chưa duyệt");
                });


                Workbook book = new Workbook(); // Create A Workbook
                Worksheet sheet = book.Worksheets[0]; // Create a worksheet
                sheet.Name = "Danh sách căn hộ";

                var cell = sheet.Cells["A1"];
                cell.PutValue("DANH SÁCH TRẠNG THÁI YÊU CẦU SỬA CHỮA");

                // Create Styles
                Style style = cell.GetStyle();
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Size = 20;
                style.Font.IsBold = true;
                style.ForegroundColor = Color.AliceBlue;
                style.Pattern = BackgroundType.Solid;
                cell.SetStyle(style);

                List<string> listProperty = new List<string> { "Mã trạng thái", "Tên trạng thái", "Chi tiết trạng thái", "Trạng thái duyệt" };
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

                sheet.Cells.ImportCustomObjects((System.Collections.ICollection)list
                    , new string[] { "TTYCSC_CODE", "TTYCSC_NAME", "TTYCSC_DECS", "AUTH_STATUS" }
                    , false, 3, 0, list.Count
                    , false
                    , "dd/mm/yyyy"
                    , false);

                //Set the columns to fit the size of their content
                sheet.AutoFitColumns();

                //Save the Excel workbook
                var fileName1 = sheet.Name + ".xlsx";
                book.Save(fileName1, SaveFormat.Xlsx);

                return list;

            }
            catch
            {
                return null;
            }
        }
    }
}
