using Abp.Application.Services;
using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group14.AbpZeroTemplate.Web.Core.Services.NhanVien.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System.Reflection;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Group14.AbpZeroTemplate.Application;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_PHONGBANs.Dto;
using System.Data;
using System.Web;
using System.IO;
using GSoft.AbpZeroTemplate.Dto;
using GSoft.AbpZeroTemplate;
using Abp.AspNetZeroCore.Net;
using Aspose.Cells;
using System.Drawing;

namespace Group14.AbpZeroTemplate.Web.Core.Services.NhanVien
{
    public interface INhanVienService : IApplicationService
    {
        List<NHANVIEN_DTO> NhanVien_GetAll();

        PagedResultDto<NHANVIEN_DTO> NhanVien_Filter(
            NHANVIEN_DTO inputFilter,
            string orderBy,
            bool desc,
            int? skip,
            int? take);

        List<NHANVIEN_DTO> NhanVien_Search(
            string maNhanVien = null,
            string tenNhanVien = null,
            string phongBan = null,
            int? trangThai = null);

        PagedResultDto<NHANVIEN_DTO> NhanVien_Search(NHANVIEN_FILTER inputFilter);

        List<dynamic> NhanVien_Delete(string maNhanVien);

        List<NHANVIEN_DTO> NhanVien_ById(string manv);
        List<dynamic> NhanVien_Insert(NHANVIEN_DTO input);
        List<dynamic> NhanVien_Update(NHANVIEN_DTO input);
        List<string> NhanVien_GetAllDepartments();
        List<NHANVIEN_DEP_NAME_DTO> NhanVien_GetDepName();
        FileDto NhanVien_ExportToExcel(NHANVIEN_DTO input);
    }

    public class NhanVienService : BaseService, INhanVienService
    {
        private readonly IAppFolders _appFolders;
        public NhanVienService(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        private List<NHANVIEN_DTO> SortAndLimit(
            IEnumerable<NHANVIEN_DTO> list,
            string orderBy = null,
            bool desc = false,
            int? skip = null,
            int? take = null)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                var property = TypeDescriptor.GetProperties(typeof(NHANVIEN_DTO)).Find(orderBy.ToUpper(), false);
                if (property != null)
                {
                    if (!desc)
                        list = list.OrderBy(x => property.GetValue(x)).ToList();
                    else
                        list = list.OrderByDescending(x => property.GetValue(x)).ToList();
                }
            }

            if (skip.HasValue)
            {
                list = list.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                list = list.Take(take.Value);
            }
            return list.ToList();
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Search)]
        public PagedResultDto<NHANVIEN_DTO> NhanVien_Filter(
            NHANVIEN_DTO inputFilter,
            string orderBy = null,
            bool desc = false,
            int? skip = null,
            int? take = null)
        {
            IEnumerable<NHANVIEN_DTO> list = procedureHelper.GetData<NHANVIEN_DTO>("NhanVien_Filter", inputFilter);

            var totalCount = list.Count();
            list = SortAndLimit(list, orderBy, desc, skip, take);
            // result
            return new PagedResultDto<NHANVIEN_DTO>(
                totalCount,
                list.ToList());
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Search)]
        public List<NHANVIEN_DTO> NhanVien_Search(
            string maNhanVien = null,
            string tenNhanVien = null,
            string phongBan = null,
            int? trangThai = null)
        {
            var filter = new NHANVIEN_DTO
            {
                NV_PHONG_BAN = phongBan,
                NV_TRANG_THAI = trangThai
            };

            IEnumerable<NHANVIEN_DTO> list = procedureHelper.GetData<NHANVIEN_DTO>("NhanVien_Filter", filter);

            if (maNhanVien != null)
                list = list.Where(x => x.NV_MA_NV.ToLower().Contains(maNhanVien.ToLower()));
            if (tenNhanVien != null)
                list = list.Where(x => x.NV_TEN.ToLower().Contains(tenNhanVien.ToLower()));

            return list.ToList();
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Search)]
        public List<NHANVIEN_DTO> NhanVien_GetAll()
        {
            return procedureHelper.GetData<NHANVIEN_DTO>("NhanVien_GetAll", new { });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Search)]
        public PagedResultDto<NHANVIEN_DTO> NhanVien_Search(NHANVIEN_FILTER inputFilter)
        {
            var filter = new NHANVIEN_DTO
            {
                NV_PHONG_BAN = inputFilter.PhongBan,
                NV_TRANG_THAI = inputFilter.TrangThai,
                NV_MA_NV = inputFilter.MaNV,
                NV_TEN = inputFilter.TenNhanVien,
            };

            IEnumerable<NHANVIEN_DTO> list = procedureHelper.GetData<NHANVIEN_DTO>("NhanVien_Filter", filter);

            list = list.Where(x => x.RECORD_STATUS == "1");

            var totalCount = list.Count();

            list = SortAndLimit(
                list,
                inputFilter.OrderBy,
                inputFilter.Desc ?? false,
                inputFilter.Skip,
                inputFilter.Take);

            return new PagedResultDto<NHANVIEN_DTO>(
                totalCount,
                list.ToList());
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Delete)]
        public List<dynamic> NhanVien_Delete(string maNhanVien)
        {
            return procedureHelper.GetData<dynamic>("NhanVien_Delete", new { NV_MA_NV = maNhanVien });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_ById)]
        public List<NHANVIEN_DTO> NhanVien_ById(string manv)
        {
            return procedureHelper.GetData<NHANVIEN_DTO>("NhanVien_ById",
                new NHANVIEN_DTO
                {
                    NV_MA_NV = manv
                });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Insert)]
        public List<dynamic> NhanVien_Insert(NHANVIEN_DTO input)
        {
            return procedureHelper.GetData<dynamic>("NhanVien_Insert", input);
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Update)]
        public List<dynamic> NhanVien_Update(NHANVIEN_DTO input)
        {
            return procedureHelper.GetData<dynamic>("NhanVien_Update", input);
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Search)]
        public List<string> NhanVien_GetAllDepartments()
        {
            return procedureHelper.GetData<string>("NhanVien_GetAllDepartments", new { });
        }

        public List<NHANVIEN_DEP_NAME_DTO> NhanVien_GetDepName()
        {
            return procedureHelper.GetData<NHANVIEN_DEP_NAME_DTO>("NhanVien_GetDepName", new { });
        }

        [AbpAuthorize(Group14PermissionsConst.Pages_Administration_NhanVien_Search)]
        public FileDto NhanVien_ExportToExcel(NHANVIEN_DTO input)
        {

            var workbook = new Workbook();
            var sheet0 = workbook.Worksheets[0];
            var data = procedureHelper.GetData<NHANVIEN_EXPORT_DTO>("NhanVien_Filter", input);

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
                                "Mã NV",
                                "Họ tên",
                                "Phòng ban",
                                "Chức vụ",
                                "SĐT",
                                "Trạng thái",
                                "CMND",
                                "Ngày cấp CMND",
                                "Nơi cấp CMND",
                                "Mã số thuế",
                                "Email",
                                "Địa chỉ",
                                "Mô tả",
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

            var fileName = $"NhanVien_{DateTime.Now.ToString("MMddyyyyHHmmss")}.xlsx";
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            workbook.Save(Path.Combine(filePath), SaveFormat.Xlsx);
            return file;
        }

    }
}
