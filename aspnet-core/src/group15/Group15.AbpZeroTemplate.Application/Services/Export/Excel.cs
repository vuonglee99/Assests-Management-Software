using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Group15.AbpZeroTemplate.Web.Core.Services.Export
{

    public class Excel
    {
        public static short FONTSIZE_LARGE = 20;
        public static short FONTSIZE_MEDIUM = 15;
        public static short FONTSIZE_NORMAL = 11;

        public static IWorkbook workbook;
        protected ISheet sheet;
        protected ICell currentCell;
        protected int currentRowIndex;
        protected IFont font;

        public IRow CurrentRow
        {
            get => this.sheet.GetRow(this.currentRowIndex);
        }

        public ISheet Sheet
        {
            get => this.sheet;
        }

        public static IWorkbook Workbook
        {
            get => Excel.workbook;
        }

        public void SetFontName(string fontName)
        {
            this.font = Excel.workbook.CreateFont();
            this.font.FontName = fontName;
        }

        public Excel(string sheetName = "Sheet1")
        {
            Excel.workbook = new XSSFWorkbook();
            this.sheet = Excel.Workbook.CreateSheet(sheetName);
            this.currentRowIndex = -1;
            this.CreateNextRow();
        }

        public void Serialize(IExcelElement element)
        {
            element.SerializeToExcel(this);
        }

        public Excel SelectRow(int index)
        {
            this.sheet.CreateRow(index);
            this.currentRowIndex = index;
            return this;
        }

        public Excel CreateNextRow()
        {
            this.sheet.CreateRow(++this.currentRowIndex);
            return this;
        }

        public Excel MergeCell(int fromColumn, int toColumn)
        {
            var currentRowIndex = this.CurrentRow.RowNum;
            this.sheet.AddMergedRegion(new CellRangeAddress(currentRowIndex, currentRowIndex, fromColumn, toColumn));
            return this;
        }

        public Excel Tabular(int fromColumn, int toColumn)
        {
            var cellRange = new CellRangeAddress(currentRowIndex, currentRowIndex, fromColumn, toColumn);
            for (int i = fromColumn; i <= toColumn; i++)
            {
                var cell = this.CurrentRow.GetCell(i);
                if (cell == null)
                {
                    cell = this.CurrentRow.CreateCell(i);
                }
                this.TabularCell(cell);
            }
            return this;
        }

        public Excel TabularCell(ICell cell)
        {
            cell.CellStyle.BorderLeft = BorderStyle.Thin;
            cell.CellStyle.BorderTop = BorderStyle.Thin;
            cell.CellStyle.BorderRight = BorderStyle.Thin;
            cell.CellStyle.BorderBottom = BorderStyle.Thin;

            return this;
        }

        public bool SaveFile(string filePath, out string errorMsg)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    Excel.Workbook.Write(stream);
                    errorMsg = null;
                    return true;
                }
            } catch (Exception err)
            {
                errorMsg = err.Message;
                return false;
            }
        }

        public Stream Write()
        {
            var memoryStream = new NPOIStream();
            memoryStream.AllowClose = false;
            Excel.Workbook.Write(memoryStream);
            memoryStream.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.AllowClose = true;
            return memoryStream;
        }


        public void Close()
        {
            Excel.Workbook.Close();
        }

        public static IExcelTable CreateTable(DataTable table, string tableTitle = "")
        {
            return new ExcelTable(table, tableTitle);
        }

        public static IExcelTitle CreateTitle(string label, string value)
        {
            return new ExcelTitle(label, value);
        }

        public static T CreateTemplate<T>(params object[] parameters) where T : class, IExcelTemplate
        {
            return Activator.CreateInstance(typeof(T), parameters) as T;
        }

        public static IBasicTemplate CreateBasicTemplate()
        {
            return new BasicTemplate();
        }
    }
}
