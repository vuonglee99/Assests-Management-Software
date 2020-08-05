using Group15.AbpZeroTemplate.Web.Core.Services.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Web.Core.Services
{
    public class Floor_Template : BasicTemplate
    {
        public IExcelTitle BuildingId { get; set; }

        public Floor_Template() : base()
        {
        }

        public void SetBuildingId(string buildingId)
        {
            this.BuildingId = Excel.CreateTitle("ID TÒA", buildingId);
        }

        public override void SerializeToExcel(Excel excel)
        {
            excel.SelectRow(0).Serialize(this.CompanyName);
            excel.MergeCell(1, 5);
            excel.SelectRow(1).Serialize(this.ExportDate);
            excel.MergeCell(1, 5);
            excel.SelectRow(3).Serialize(this.BuildingId);
            excel.SelectRow(4).Serialize(this.Table);
        }
    }
}
