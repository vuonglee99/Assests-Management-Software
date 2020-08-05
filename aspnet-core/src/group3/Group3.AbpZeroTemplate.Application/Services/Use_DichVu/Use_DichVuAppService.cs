using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Group3.AbpZeroTemplate.Application;
using Group3.AbpZeroTemplate.Web.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Services.Use_DichVu
{
    public interface IUse_DichVuAppService : IApplicationService
    {
        dynamic InsertUse_DichVu(Use_DichVuInsertRequest filter);
        PagedResultDto<Use_DichVuSearchDTOs> SearchUse_DichVu(Use_DichVuSearchRequest filter);

    }
    public class Use_DichVuAppService: Group3AppServiceBase,IUse_DichVuAppService
    {
        public dynamic InsertUse_DichVu(Use_DichVuInsertRequest filter)
        {
            return procedureHelper.GetData<dynamic>(
                "Use_DichVu_Insert",new {
                CANHO_ID=filter.CANHO_ID,
	            DICHVU_ID=filter.DICHVU_ID,
	            NG_START=filter.NG_START,
	            NG_END=filter.NG_END,
	            GT_DAU=filter.GT_DAU,
	            GT_CUOI=filter.GT_CUOI,
	            DON_GIA=filter.DON_GIA
            }).FirstOrDefault();
        }

        public PagedResultDto<Use_DichVuSearchDTOs> SearchUse_DichVu(Use_DichVuSearchRequest filter)
        {
            var useDichVu = procedureHelper.GetData<Use_DichVuSearchDTOs>(
                "Use_DichVu_Search", new
                {
                    CANHO_ID=filter.CANHO_ID,
                    DICHVU_ID=filter.DICHVU_ID,
                    NG_START=filter.NG_START,
                    NG_END= filter.NG_END,
                    RECORD_STATUS=filter.RECORD_STATUS,
                    Sorting = filter.Sorting,
                    PageNo = filter.SkipCount,
                    PageSize = filter.MaxResultCount
                });
            int TotalRows = 0;
            if (useDichVu.Count != 0)
                TotalRows = useDichVu[0].TotalRows;

            return new PagedResultDto<Use_DichVuSearchDTOs>(
                TotalRows, useDichVu);
        }
    }
}
