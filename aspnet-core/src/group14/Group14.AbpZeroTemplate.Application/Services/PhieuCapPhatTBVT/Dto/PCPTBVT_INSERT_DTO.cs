using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group14.AbpZeroTemplate.Web.Core.Services.PhieuCapPhatTBVT.Dto
{
    public class PCPTBVT_INSERT_DTO
    {
		public string PCPTBVT_MA_PCP { get; set; }
		public string PCPTBVT_DEP_ID { get; set; }
		public string PCPTBVT_GHI_CHU { get; set; }
		public string MAKER_ID { get; set; }
		public DateTime? CREATE_DT { get; set; }
		public string BRANCH_CREATE { get; set; }
	}
}
