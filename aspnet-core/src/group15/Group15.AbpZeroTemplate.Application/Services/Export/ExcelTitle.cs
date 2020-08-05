using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Web.Core.Services.Export
{
    public class ExcelTitle : IExcelTitle
    {
        protected string label;
        protected string value;
        protected short height;

        public ExcelTitle(string label, string value)
        {
            this.label = label;
            this.value = value;
            this.height = 20;
        }

        public void SerializeToExcel(Excel excel)
        {
            excel.CurrentRow.HeightInPoints = this.height;
            var labelCell = excel.CurrentRow.CreateCell(0);
            var valueCell = excel.CurrentRow.CreateCell(1);

            labelCell.CellStyle = this.GetLabelStyle();
            valueCell.CellStyle = this.GetValueStyle();

            labelCell.SetCellValue(label);
            valueCell.SetCellValue(value);
        }

        public void SetHeight(short value)
        {
            this.height = value;
        }

        protected ICellStyle GetLabelStyle()
        {
            var style = Excel.Workbook.CreateCellStyle();
            style.VerticalAlignment = VerticalAlignment.Center;
            var font = Excel.Workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            font.FontHeightInPoints = Excel.FONTSIZE_MEDIUM;
            style.SetFont(font);

            return style;
        }

        protected ICellStyle GetValueStyle()
        {
            var style = Excel.Workbook.CreateCellStyle();
            style.VerticalAlignment = VerticalAlignment.Center;
            var font = Excel.Workbook.CreateFont();
            font.FontHeightInPoints = Excel.FONTSIZE_MEDIUM;
            style.SetFont(font);

            return style;
        }
    }
}
