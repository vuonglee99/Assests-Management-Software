using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Web.Core.Services.Export
{
    public interface IBasicTemplate : IExcelTemplate
    {
        IExcelTitle CompanyName { get; set; }
        IExcelTitle ExportDate { get; set; }
        IExcelTable Table { get; set; }

        void SetTableData(DataTable table, string tableTitle = "");
    }

    public class BasicTemplate : IBasicTemplate
    {
        public const string COMPANY_NAME = "SE ASSET MANAGEMENT SOFTWARE";
        public IExcelTitle CompanyName { get; set; }
        public IExcelTitle ExportDate { get; set; }
        public IExcelTable Table { get; set; }


        public BasicTemplate()
        {
            this.CompanyName = Excel.CreateTitle("CÔNG TY", COMPANY_NAME);
            this.CompanyName.SetHeight(28);
            this.ExportDate = Excel.CreateTitle("NGÀY XUẤT", DateTime.Now.ToString("dd/MM/yyyy"));
            this.ExportDate.SetHeight(28);
        }

        public void SetTableData(DataTable table, string tableTitle = "")
        {
            this.Table = Excel.CreateTable(table, tableTitle);
            this.Table.SetColumnWidths(new short[] { 17, 17, 16, 15, 17, 25 });
        }

        public virtual void SerializeToExcel(Excel excel)
        {
            excel.SelectRow(0).Serialize(this.CompanyName);
            excel.MergeCell(1, 5);
            excel.SelectRow(1).Serialize(this.ExportDate);
            excel.MergeCell(1, 5);
            excel.SelectRow(3).Serialize(this.Table);
        }
    }
}
