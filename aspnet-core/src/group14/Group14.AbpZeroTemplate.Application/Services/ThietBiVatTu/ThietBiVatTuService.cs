using System.Linq.Dynamic.Core;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Group14.AbpZeroTemplate.Web.Core.Services.ThietBiVatTu.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Linq;
using GSoft.AbpZeroTemplate.Dto;
using Aspose.Cells;
using System.Drawing;
using System;
using Abp.AspNetZeroCore.Net;
using System.IO;
using GSoft.AbpZeroTemplate;

namespace Group14.AbpZeroTemplate.Application.Services.ThietBiVatTu
{
    public interface IThietBiVatTuService : IApplicationService
    {
        List<THIETBIVATTU_DTO> ThietBiVatTu_GetAll();
        PagedResultDto<THIETBIVATTU_DTO> ThietBiVatTu_Filter(
           THIETBIVATTU_DTO inputFilter,
           string orderBy,
           bool desc,
           int? skip,
           int? take);
        List<dynamic> ThietBiVatTu_Delete(string maTBVT);
        List<dynamic> ThietBiVatTu_AddToPCPTBVT(string maTBVT, string pcpID);
        List<dynamic> ThietBiVatTu_RemoveFromPCPTBVT(string maTBVT, string pcpID);
        FileDto ThietBiVatTu_ExportToExcel(THIETBIVATTU_DTO input);
    }
    public class ThietBiVatTuService : BaseService, IThietBiVatTuService
    {
        private readonly IAppFolders _appFolders;
        public ThietBiVatTuService(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        private IEnumerable<THIETBIVATTU_DTO> SortAndLimit(
           IQueryable<THIETBIVATTU_DTO> list,
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


        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_Search)]
        public PagedResultDto<THIETBIVATTU_DTO> ThietBiVatTu_Filter(
            THIETBIVATTU_DTO inputFilter,
            string orderBy = null,
            bool desc = false,
            int? skip = null,
            int? take = null)
        {
            IEnumerable<THIETBIVATTU_DTO> list = procedureHelper.GetData<THIETBIVATTU_DTO>("ThietBiVatTu_Filter", inputFilter);

            var totalCount = list.Count();
            list = SortAndLimit(list.AsQueryable(), orderBy, desc, skip, take);
            // result
            return new PagedResultDto<THIETBIVATTU_DTO>(
                totalCount,
                list.ToList());
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_Search)]
        public List<THIETBIVATTU_DTO> ThietBiVatTu_GetAll()
        {
            return procedureHelper.GetData<THIETBIVATTU_DTO>("ThietBiVatTu_GetAll", new { });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_Delete)]
        public List<dynamic> ThietBiVatTu_Delete(string maTBVT)
        {
            return procedureHelper.GetData<dynamic>("ThietBiVatTu_Delete", new { TBVT_MA_TBVT = maTBVT });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_AddToPCPTBVT)]
        public List<dynamic> ThietBiVatTu_AddToPCPTBVT(string maTBVT, string pcpID)
        {
            return procedureHelper.GetData<dynamic>("ThietBiVatTu_AddToPCPTBVT", new { TBVT_MA_TBVT = maTBVT, TBVT_PCPTBVT_MA_PCP = pcpID });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_RemoveFromPCPTBVT)]
        public List<dynamic> ThietBiVatTu_RemoveFromPCPTBVT(string maTBVT, string pcpID)
        {
            return procedureHelper.GetData<dynamic>("ThietBiVatTu_RemoveFromPCPTBVT", new { TBVT_MA_TBVT = maTBVT, TBVT_PCPTBVT_MA_PCP = pcpID });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_Search)]
        public FileDto ThietBiVatTu_ExportToExcel(THIETBIVATTU_DTO input)
        {
            var workbook = new Workbook();
            var sheet0 = workbook.Worksheets[0];
            var data = procedureHelper.GetData<THIETBIVATTU_EXPORT_DTO>("ThietBiVatTu_Filter", input);

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
                                "Mã TBVT",
                                "Tên",
                                "Serial",
                                "Loại",
                                "Ngày mua",
                                "Đơn vị tính",
                                "Nhập theo lô",
                                "Số lượng theo lô",
                                "Hãng SX",
                                "Năm SX",
                                "Ngày tính bảo hành",
                                "Ngày kết thức bảo hành",
                                "Nhà cung cấp",
                                "Tình trạng",
                                "Ghi chú tình trạng",
                                "Cần bảo dưỡng",
                                "Chu kì bảo dưỡng",
                                "Nội dung bảo dưỡng",
                                "Tỉ lệ hao mòn",
                                "Tình trạng TBTBVT",
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

            var fileName = $"ThietBiVatTu_{DateTime.Now.ToString("MMddyyyyHHmmss")}.xlsx";
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            workbook.Save(Path.Combine(filePath), SaveFormat.Xlsx);
            return file;
        }
    }
}