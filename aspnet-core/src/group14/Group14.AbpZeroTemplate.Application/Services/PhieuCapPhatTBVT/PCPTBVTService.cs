using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AspNetZeroCore.Net;
using Abp.Authorization;
using Abp.Runtime.Session;
using Aspose.Cells;
using Group14.AbpZeroTemplate.Application;
using Group14.AbpZeroTemplate.Web.Core.Services.NhanVien.Dto;
using Group14.AbpZeroTemplate.Web.Core.Services.PhieuCapPhatTBVT.Dto;
using GSoft.AbpZeroTemplate;
using GSoft.AbpZeroTemplate.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Group14.AbpZeroTemplate.Web.Core.Services.PhieuCapPhatTBVT
{
    public interface IPCPTBVTService : IApplicationService
    {

        List<PCPTBVT_DTO> PCPTBVT_GetAll();

        PagedResultDto<PCPTBVT_DTO> PCPTBVT_Filter(
            PCPTBVT_DTO inputFilter,
            string orderBy,
            bool desc,
            int? skip,
            int? take);

        PagedResultDto<PCPTBVT_DTO> PCPTBVT_Search(
            PCPTBVT_SEARCH_DTO inputFilter,
            string orderBy = null,
            bool desc = false,
            int? skip = null,
            int? take = null);

        List<dynamic> PCPTBVT_Delete(string maPhieu);
        List<dynamic> PCPTBVT_Insert(PCPTBVT_INSERT_DTO input);
        List<dynamic> PCPTBVT_Update(PCPTBVT_UPDATE_DTO input);
        List<dynamic> PCPTBVT_Approve(PCPTBVT_APPROVE_DTO input);
        List<dynamic> PCPTBVT_Deny(PCPTBVT_APPROVE_DTO input);
        PCPTBVT_DTO PCPTBVT_GetByID(string id);
        FileDto PCPTBVT_ExportToExcel(PCPTBVT_DTO input);
    }

    public class PCPTBVTService : BaseService, IPCPTBVTService
    {

        private readonly IAppFolders _appFolders;
        private readonly IAbpSession _abpSession;
        public PCPTBVTService(IAppFolders appFolders, IAbpSession abpSession)
        {
            _abpSession = abpSession;
            _appFolders = appFolders;
        }


        private IEnumerable<PCPTBVT_DTO> SortAndLimit(
            IQueryable<PCPTBVT_DTO> list,
            string orderBy = null,
            bool desc = false,
            int? skip = null,
            int? take = null)
        {
            if (orderBy != null)
            {
                if (!desc)
                    list = list.OrderBy(orderBy);
                else
                    list = list.OrderBy(orderBy + " descending");
            }

            if (skip.HasValue)
            {
                list = list.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                list = list.Take(take.Value);
            }
            return list;
        }


        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Search)]
        public PagedResultDto<PCPTBVT_DTO> PCPTBVT_Filter(
            PCPTBVT_DTO inputFilter,
            string orderBy = null,
            bool desc = false,
            int? skip = null,
            int? take = null)
        {
            IEnumerable<PCPTBVT_DTO> list = procedureHelper.GetData<PCPTBVT_DTO>("PhieuCapPhatTBVTYT_Filter", inputFilter);

            var totalCount = list.Count();
            list = SortAndLimit(list.AsQueryable(), orderBy, desc, skip, take);
            // result
            return new PagedResultDto<PCPTBVT_DTO>(
                totalCount,
                list.ToList());
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Search)]
        public PagedResultDto<PCPTBVT_DTO> PCPTBVT_Search(
            PCPTBVT_SEARCH_DTO inputFilter,
            string orderBy = null,
            bool desc = false,
            int? skip = null,
            int? take = null)
        {
            if (!inputFilter.CREATE_DT_START.HasValue)
            {
                inputFilter.CREATE_DT_START = DateTime.Parse("1800-01-01");
            }
            if (!inputFilter.CREATE_DT_END.HasValue)
            {
                inputFilter.CREATE_DT_END = DateTime.Parse("9999-12-31");
            }
            IEnumerable<PCPTBVT_DTO> list = procedureHelper.GetData<PCPTBVT_DTO>("PhieuCapPhatTBVTYT_Search", inputFilter);

            var totalCount = list.Count();
            list = SortAndLimit(list.AsQueryable(), orderBy, desc, skip, take);
            // result
            return new PagedResultDto<PCPTBVT_DTO>(
                totalCount,
                list.ToList());
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Search)]
        public List<PCPTBVT_DTO> PCPTBVT_GetAll()
        {
            return procedureHelper.GetData<PCPTBVT_DTO>("PhieuCapPhatTBVTYT_GetAll", new { });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Insert)]
        public List<dynamic> PCPTBVT_Insert(PCPTBVT_INSERT_DTO input)
        {
            input.MAKER_ID = _abpSession.GetUserId().ToString();
            return procedureHelper.GetData<dynamic>("PhieuCapPhatTBVTYT_Insert", input);
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Delete)]
        public List<dynamic> PCPTBVT_Delete(string maPhieu)
        {
            return procedureHelper.GetData<dynamic>("PhieuCapPhatTBVTYT_Delete", new { PCPTBVT_MA_PCP = maPhieu });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Update)]
        public List<dynamic> PCPTBVT_Update(PCPTBVT_UPDATE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("PhieuCapPhatTBVTYT_Update", input);
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Approve)]
        public List<dynamic> PCPTBVT_Approve(PCPTBVT_APPROVE_DTO input)
        {
            input.CHECKER_ID = _abpSession.GetUserId().ToString();
            return procedureHelper.GetData<dynamic>("PhieuCapPhatTBVTYT_Approve", input);
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Approve)]
        public List<dynamic> PCPTBVT_Deny(PCPTBVT_APPROVE_DTO input)
        {
            input.CHECKER_ID = _abpSession.GetUserId().ToString();
            return procedureHelper.GetData<dynamic>("PhieuCapPhatTBVTYT_Deny", input);
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_ById)]
        public PCPTBVT_DTO PCPTBVT_GetByID(string maPhieu)
        {
            return procedureHelper.GetData<PCPTBVT_DTO>("PhieuCapPhatTBVTYT_ById", new { PCPTBVT_MA_PCP = maPhieu }).FirstOrDefault();
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_PCPTBVT_Search)]
        public FileDto PCPTBVT_ExportToExcel(PCPTBVT_DTO input)
        {

            var workbook = new Workbook();
            var sheet0 = workbook.Worksheets[0];
            var data = procedureHelper.GetData<PCPTBVT_EXPORT_DTO>("PhieuCapPhatTBVTYT_Filter", input);

            sheet0.Cells.ImportCustomObjects(
                data, 0, 0,
                new ImportTableOptions()
                {
                    DateFormat = "dd/MM/yyyy",
                    IsFieldNameShown = true,
                });

            sheet0.Cells.ImportArray(
                new string[]
                {
                    "Mã PCP",
                    "Phòng ban",
                    "Ngày tạo",
                    "Ghi chú"
                },
                0, 0, false);

            var range = sheet0.Cells.MaxDisplayRange;

            var cellStyle = workbook.CreateStyle();
            cellStyle.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
            cellStyle.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);
            cellStyle.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
            cellStyle.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);

            for (int r = range.FirstRow; r < range.RowCount; r++)
            {
                for (int c = range.FirstColumn; c < range.ColumnCount; c++)
                {
                    Cell cell = sheet0.Cells[r, c];
                    cell.SetStyle(cellStyle, new StyleFlag() { TopBorder = true, BottomBorder = true, LeftBorder = true, RightBorder = true });
                }
            }

            sheet0.AutoFitColumns();

            var fileName = $"PCPTBVT_{DateTime.Now.ToString("MMddyyyyHHmmss")}.xlsx";
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            workbook.Save(Path.Combine(filePath), SaveFormat.Xlsx);
            return file;
        }
    }
}
