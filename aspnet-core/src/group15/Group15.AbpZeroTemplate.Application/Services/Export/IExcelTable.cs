using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Web.Core.Services.Export
{
    public interface IExcelTable : IExcelElement
    {
        void SetColumnWidth(int columnIndex, short value);

        void SetColumnWidths(short[] values);
    }
}
