using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group14.AbpZeroTemplate.Web.Core.Services.NhanVien.Dto
{
    public class NHANVIEN_FILTER
    {
        public string MaNV { get; set; }
        public string TenNhanVien { get; set; }
        public string PhongBan { get; set; }
        public int? TrangThai { get; set; }
        public string OrderBy { get; set; }
        public bool? Desc { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
