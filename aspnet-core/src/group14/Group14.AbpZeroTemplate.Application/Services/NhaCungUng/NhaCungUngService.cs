using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AspNetZeroCore.Net;
using Abp.Authorization;
using Aspose.Cells;
using Group14.AbpZeroTemplate.Application;
using Group14.AbpZeroTemplate.Web.Core.Services.NhaCungUng.Dto;
using GSoft.AbpZeroTemplate;
using GSoft.AbpZeroTemplate.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Group14.AbpZeroTemplate.Web.Core.Services.NhaCungUng
{

    public interface INhaCungUngService : IApplicationService
    {

        List<NHACUNGUNG_DTO> NhaCungUng_GetAll();

        PagedResultDto<NHACUNGUNG_DTO> NhaCungUng_Filter(
            NHACUNGUNG_DTO inputFilter,
            string orderBy,
            bool desc,
            int? skip,
            int? take);
        List<dynamic> NhaCungUng_Delete(string maNCU);
        FileDto NhaCungUng_ExportToExcel(NHACUNGUNG_DTO input);
    }

    public class NhaCungUngService : BaseService, INhaCungUngService
    {

        private readonly IAppFolders _appFolders;
        public NhaCungUngService(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        private IEnumerable<NHACUNGUNG_DTO> SortAndLimit(
            IQueryable<NHACUNGUNG_DTO> list,
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


        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhaCungUng_Search)]
        public PagedResultDto<NHACUNGUNG_DTO> NhaCungUng_Filter(
            NHACUNGUNG_DTO inputFilter,
            string orderBy = null,
            bool desc = false,
            int? skip = null,
            int? take = null)
        {
            IEnumerable<NHACUNGUNG_DTO> list = procedureHelper.GetData<NHACUNGUNG_DTO>("NhaCungUng_Filter", inputFilter);

            var totalCount = list.Count();
            list = SortAndLimit(list.AsQueryable(), orderBy, desc, skip, take);
            // result
            return new PagedResultDto<NHACUNGUNG_DTO>(
                totalCount,
                list.ToList());
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhaCungUng_Search)]
        public List<NHACUNGUNG_DTO> NhaCungUng_GetAll()
        {
            return procedureHelper.GetData<NHACUNGUNG_DTO>("NhaCungUng_GetAll", new { });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhaCungUng_Delete)]
        public List<dynamic> NhaCungUng_Delete(string maNCU)
        {
            return procedureHelper.GetData<dynamic>("NhaCungUng_Delete", new { NCU_MA_NCU = maNCU });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhaCungUng_Search)]
        public FileDto NhaCungUng_ExportToExcel(NHACUNGUNG_DTO input)
        {

            var workbook = new Workbook();
            var sheet0 = workbook.Worksheets[0];
            var data = procedureHelper.GetData<NHACUNGUNG_EXPORT_DTO>("NhaCungUng_Filter", input);

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
                    "Mã NCU",
                    "Tên nhà cung ứng",
                    "Địa chỉ",
                    "SĐT",
                    "Mã số thuế",
                    "Fax",
                    "Tên người liên hệ",
                    "Email người liên hệ",
                    "SĐT người liên hệ",
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

            var fileName = $"NhaCungUng_{DateTime.Now.ToString("MMddyyyyHHmmss")}.xlsx";
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            workbook.Save(Path.Combine(filePath), SaveFormat.Xlsx);
            return file;
        }
    }
}
