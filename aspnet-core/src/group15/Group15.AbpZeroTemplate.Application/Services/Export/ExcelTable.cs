using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Web.Core.Services.Export
{
    public class ExcelTable : IExcelTable
    {
        protected string[] headers;
        protected DataTable table;
        protected string tableTitle;
        protected int[] columnWidths;

        public bool HasTitle
        {
            get => !String.IsNullOrEmpty(this.tableTitle);
        }

        public ExcelTable(DataTable table, string tableTitle = "")
        {
            this.headers = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
            this.table = table;
            this.tableTitle = tableTitle;
            this.columnWidths = new int[table.Columns.Count];
            for (int i = 0; i < this.columnWidths.Length; i++)
            {
                this.columnWidths[i] = -1;
            }
        }

        public void SetColumnWidth(int columnIndex, short value)
        {
            this.columnWidths[columnIndex] = value;
        }

        public void SetColumnWidths(short[] values)
        {
            values.CopyTo(this.columnWidths, 0);
        }

        public void SerializeToExcel(Excel excel)
        {
            this.AddTitle(excel);

            excel.CreateNextRow();
            excel.CurrentRow.HeightInPoints = 30;
            // Set headers
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = excel.CurrentRow.CreateCell(i);
                cell.CellStyle = this.GetCellTableHeaderStyle();
                cell.SetCellValue(headers[i]);
            }

            // Fill data to excel table
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                excel.CreateNextRow();
                excel.CurrentRow.HeightInPoints = 35;
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    var cell = excel.CurrentRow.CreateCell(j);
                    cell.CellStyle = this.GetCellTableStyle();
                    cell.SetCellValue(row[j].ToString());
                }
            }

            this.SizingColumnWidths(excel);
        }

        protected void SizingColumnWidths(Excel excel)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var width = this.columnWidths[i];
                if (width == -1)
                {
                    excel.Sheet.AutoSizeColumn(i);
                }
                else
                {
                    excel.Sheet.SetColumnWidth(i, width << 8);
                }
            }
        }

        protected void AddTitle(Excel excel)
        {
            if (!this.HasTitle)
            {
                return;
            }
            var cell = excel.MergeCell(0, table.Columns.Count - 1).CurrentRow.CreateCell(0);
            cell.CellStyle = this.GetTitleStyle();
            cell.SetCellValue(this.tableTitle);
        }

        protected ICellStyle GetCellTableStyle()
        {
            var borderedCellStyle = Excel.workbook.CreateCellStyle();
            this.BorderAll(borderedCellStyle);
            borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;
            borderedCellStyle.WrapText = true;

            return borderedCellStyle;
        }

        protected ICellStyle GetCellTableHeaderStyle()
        {
            var cellStyle = this.GetCellTableStyle();
            cellStyle.SetFont(this.GetTableHeaderFont());

            return cellStyle;
        }


        protected IFont GetTableHeaderFont()
        {
            var font = Excel.workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            return font;
        }

        protected ICellStyle GetTitleStyle()
        {
            var cellStyle = Excel.workbook.CreateCellStyle();
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            var font = Excel.workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            font.FontHeightInPoints = Excel.FONTSIZE_MEDIUM;
            cellStyle.SetFont(font);

            return cellStyle;
        }

        protected void BorderAll(ICellStyle cell)
        {
            cell.BorderLeft = BorderStyle.Thin;
            cell.BorderTop = BorderStyle.Thin;
            cell.BorderRight = BorderStyle.Thin;
            cell.BorderBottom = BorderStyle.Thin;
        }
    }
}
