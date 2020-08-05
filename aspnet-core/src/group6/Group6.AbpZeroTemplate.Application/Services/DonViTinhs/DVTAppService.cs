using Abp.Application.Services;
using Abp.Authorization;
using Group6.AbpZeroTemplate.Application;
using Group6.AbpZeroTemplate.Web.Core.Services.DonViTinhs.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Group6.AbpZeroTemplate.Web.Core.Services.DonViTinhs
{
    public interface IDVTAppService : IApplicationService
    {
        List<DVT_DTO> DVT_Search(string dvtCode, string dvtName, string record_status = "1");

        dynamic DVT_Delete(string dvtId);

        DVT_DTO DVT_ById(string dvtId);

        PageResult DVT_searchFilter(int PageNumber, int PageSize, string dvtCode, string dvtName, string record_status = "1");

    }

    //[AbpAuthorize(Group6PermissionsConst.Pages_Administration_DVT)]
    public class DVTAppService : BaseService, IDVTAppService
    {
        public DVT_DTO DVT_ById(string dvtId)
        {
            return procedureHelper.GetData<DVT_DTO>("DVT_ById", new
            {
                DVT_ID = dvtId
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_DVT_Delete)]
        public dynamic DVT_Delete(string dvtId)
        {
            return procedureHelper.GetData<dynamic>("DVT_Delete", new
            {
                DVT_ID = dvtId
            }).FirstOrDefault();
        }

        public List<DVT_DTO> DVT_Search(string dvtCode, string dvtName, string record_status = "1")
        {
            return procedureHelper.GetData<DVT_DTO>("DVT_Search", new
            {
                DVT_CODE = dvtCode,
                DVT_NAME = dvtName,
                RECORD_STATUS = record_status
            });
        }

        public PageResult DVT_searchFilter(int pageNumber, int pageSize, string dvtCode, string dvtName, string record_status = "1")
        {
            var result = procedureHelper.GetData<dynamic>("DVT_searchFilter", new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                DVT_CODE = dvtCode,
                DVT_NAME = dvtName,
                RECORD_STATUS = record_status
            });
            var total_record = procedureHelper.GetData<int>("DVT_GetCountSearchFilter", new
            {
                DVT_CODE = dvtCode,
                DVT_NAME = dvtName,
            }).FirstOrDefault();

            return PageResult.CreateSuccess(result, total_record);
        }
    }
}
