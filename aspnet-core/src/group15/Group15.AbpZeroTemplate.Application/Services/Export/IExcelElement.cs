using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Web.Core.Services.Export
{
    public interface IExcelElement
    {
        /// <summary>
        /// Render element vào excel (hàm này dùng để custom vị trí hiển thị element lên Excel)
        /// </summary>
        /// <param name="excel"></param>
        void SerializeToExcel(Excel excel);
    }
}
