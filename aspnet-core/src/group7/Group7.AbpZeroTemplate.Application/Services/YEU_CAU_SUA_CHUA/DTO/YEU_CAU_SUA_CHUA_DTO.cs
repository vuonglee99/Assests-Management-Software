using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group7.AbpZeroTemplate.Web.Core.Services.YEU_CAU_SUA_CHUA.DTO
{
    public class YEU_CAU_SUA_CHUA_DTO
    {
        public string YCSC_ID { get; set; }
        public string YCSC_MA_YCSC { get; set; }    //Ma yeu cau
        public string YCSC_NGUOILAP { get; set; }   //Nguoi lap 
        public string YCSC_TBVT_ID { get; set; } //Ma thiet bi
        public string YCSC_DEP_ID { get; set; }    //Ma phong ban su dung
        public string YCSC_MOTA_SUCO { get; set; }  //Mo ta su co
        public string YCSC_MOTA_NGUYENNHAN { get; set; }    //Mo ta nguyen nhan
        public string YCSC_NHANVIEN_SUACHUA { get; set; }    //Nhan vien sua chua
        public string YCSC_YKIEN_LANHDAO { get; set; }  //Y kien lanh dao
        public DateTime? YCSC_NGAYYC { get; set; }  //Ngay yeu cau
        public DateTime? YCSC_NGAYKT { get; set; }  //Ngay kiem tra
        public DateTime? YCSC_NGAYHT { get; set; }  //Ngay ket thuc
        public string YCSC_KQTINHTRANG { get; set; }    //Ket qua tinh trang sau xu ly
        public int YCSC_TINHTRANG_DUYET { get; set; }   //Tinh trang duyet
        public string YCSC_NGUOIDUYET { get; set; }     //Nguoi duyet yeu cau
        public DateTime? CREATE_DT { get; set; }    //Ngay lap
        public string MAKER_ID { get; set; }
        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
    }
}
