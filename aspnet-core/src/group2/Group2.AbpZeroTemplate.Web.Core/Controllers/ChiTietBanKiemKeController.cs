using Abp.AspNetCore.Mvc.Controllers;
using Abp.AspNetZeroCore.Net;
using Aspose.Cells;
using Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs;
using Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs.Dto;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs.Dto;
using GSoft.AbpZeroTemplate.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ChiTietBanKiemKeController : AbpController
    {
        private readonly IChiTietBanKiemKeAppService chitietbankiemkeAppService;
        
       

        public ChiTietBanKiemKeController(IChiTietBanKiemKeAppService ctbkkAppService)
        {
            this.chitietbankiemkeAppService = ctbkkAppService;
        }
        [HttpPost]
        public List<CHI_TIET_BAN_KIEM_KE_DTO> ChiTietKiemKe_Get(string bkk_ID)
        {
            return chitietbankiemkeAppService.ChiTietKiemKe_Get(bkk_ID);
        }
        [HttpPost]
        public dynamic CTBKK_XacNhanKK(string dv_QL, string bkk_ID, DateTime ngay_TAO, string ma_TB, string tt_SAU)
        {
            return chitietbankiemkeAppService.CTBKK_XacNhanKK(dv_QL,bkk_ID,ngay_TAO,ma_TB,tt_SAU);
        }
        [HttpPost]
        public dynamic TaoChiTietKiemKe(string maKK, string maDV)
        {
            return chitietbankiemkeAppService.TaoChiTietKiemKe(maKK, maDV);
        }
        [HttpPost]
        public IDictionary<string, object> ChiTietKiemKeInsert([FromBody]CHI_TIET_BAN_KIEM_KE_DTO input)
        {
            return chitietbankiemkeAppService.ChiTietKiemKeInsert(input);
        }
        [HttpPost]
        public List<CHI_TIET_BAN_KIEM_KE_DTO> ChiTietKiemKe_GetByIdKK_IdDV(string idKK, string idTB)
        {
            return chitietbankiemkeAppService.ChiTietKiemKe_GetByIdKK_IdDV(idKK, idTB);
        }
        [HttpPost]
        public dynamic CTBKK_XacNhanKK_Ten(string dv_QL, string bkk_ID, DateTime ngay_TAO, string ten_TB, string tt_SAU)
        {
            return chitietbankiemkeAppService.CTBKK_XacNhanKK_Ten(dv_QL, bkk_ID, ngay_TAO, ten_TB, tt_SAU);
        }
        [HttpPost]
        public FileDto ExportExcel(string KK_ID)
        {
            string curdir = Directory.GetCurrentDirectory();
            curdir = Directory.GetParent(curdir).FullName;
            string filePath = curdir + "/group2/template/group2_template.xlsx";

            DataSet data = chitietbankiemkeAppService.DataFromStore("ChiTietKiemKe_Get", new { BKK_ID = KK_ID });
            DataSet dtKiemKe = chitietbankiemkeAppService.DataFromStore("KiemKe_GetById", new { BKK_ID = KK_ID });

            Workbook designer = new Workbook(filePath);

            WorkbookDesigner designWord = new WorkbookDesigner(designer);

            designWord.SetDataSource(data);
            designWord.Process(false);
            designWord.Workbook.FileName = filePath;
            designWord.Workbook.FileFormat = FileFormatType.Xlsx;
            designWord.Workbook.Settings.CalcMode = CalcModeType.Automatic;
            designWord.Workbook.Settings.RecalculateBeforeSave = true;
            designWord.Workbook.Settings.ReCalculateOnOpen = true;
            designWord.Workbook.Settings.CheckCustomNumberFormat = true;



            designer.Worksheets[0].Cells["E2"].PutValue(dtKiemKe.Tables[0].Rows[0]["KK_CODE"].ToString());
            designer.Worksheets[0].Cells["G2"].PutValue(dtKiemKe.Tables[0].Rows[0]["KK_MADONVI"].ToString());
            designer.Worksheets[0].Cells["E3"].PutValue(dtKiemKe.Tables[0].Rows[0]["KK_NGUOITAO"].ToString());
            designer.Worksheets[0].Cells["G3"].PutValue(dtKiemKe.Tables[0].Rows[0]["KK_TENDONVI"].ToString());
            designer.Worksheets[0].Cells["E4"].PutValue(dtKiemKe.Tables[0].Rows[0]["KK_NGAYTAO"].ToString());
            designer.Worksheets[0].Cells["G4"].PutValue(dtKiemKe.Tables[0].Rows[0]["KK_NGAYCHOT"].ToString());


            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                int temp = i + 7;
                string cell = "I" + temp;
                string cell2 = "H" + temp;
                string cell3 = "C" + temp;
                string valueStatus = string.Empty;
                if (string.IsNullOrEmpty(data.Tables[0].Rows[i]["CTBKK_TT"].ToString())) valueStatus = "Thiếu";
                else
                    valueStatus = data.Tables[0].Rows[i]["CTBKK_TT"].ToString();

                designer.Worksheets[0].Cells[cell].PutValue(data.Tables[0].Rows[i]["CTBKK_THOI_GIAN"].ToString());
                designer.Worksheets[0].Cells[cell2].PutValue(valueStatus);
                designer.Worksheets[0].Cells[cell3].PutValue(i + 1 + "");
            }

            DataSet thietbila = chitietbankiemkeAppService.DataFromStore("ChiTietKiemKeGet_ThietBiLa", new { KK_ID = KK_ID });

            if (thietbila.Tables[0].Rows.Count>0)
            {
                
                int excelcolums = data.Tables[0].Rows.Count + 7;
                Console.WriteLine("có thiết bị lạ nhé!"+excelcolums);
                for (int i=0;i<thietbila.Tables[0].Rows.Count;i++)
                {
                    string cellSTT = "C" + (excelcolums+ i);
                    string celltenTB = "E" + (excelcolums + i);
                    string cellTT = "G" + (excelcolums + i);
                    string cellThieuThua = "H" + (excelcolums + i);
                    string cellThoiGian = "I" + (excelcolums + i);
                    designer.Worksheets[0].Cells[cellSTT].PutValue((data.Tables[0].Rows.Count + i + 1)+"");
                    designer.Worksheets[0].Cells[celltenTB].PutValue(thietbila.Tables[0].Rows[i]["CTBKK_TEN_TB"].ToString());
                    designer.Worksheets[0].Cells[cellTT].PutValue(thietbila.Tables[0].Rows[i]["CTBKK_TT_SAU"].ToString());
                    designer.Worksheets[0].Cells[cellThieuThua].PutValue(thietbila.Tables[0].Rows[i]["CTBKK_TT"].ToString());
                    designer.Worksheets[0].Cells[cellThoiGian].PutValue(thietbila.Tables[0].Rows[i]["CTBKK_THOI_GIAN"].ToString());
                }
                //designer.Worksheets[0].Cells["C12"].PutValue(12);
            }


            string fileName = "BKK_" + dtKiemKe.Tables[0].Rows[0]["KK_CODE"].ToString()+ ".xlsx";
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var excelPath = Path.Combine(this.chitietbankiemkeAppService.getDownloadPath(), file.FileToken);



            //designWord.Workbook.Save(str, Aspose.Cells.SaveFormat.Xlsx);
            designWord.Workbook.Save(Path.Combine(excelPath), SaveFormat.Xlsx);
            //Console.WriteLine("exx:"+excelPath);
            //Console.WriteLine("cbn:"+Path.Combine(excelPath));
            
            return file;
        }
    }
}
