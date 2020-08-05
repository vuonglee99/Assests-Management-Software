using Abp.Application.Services;
using Abp.Authorization;
using Group6.AbpZeroTemplate.Application;
using Group6.AbpZeroTemplate.Web.Core.Services.DoUuTiens.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Group6.AbpZeroTemplate.Web.Core.Services.DoUuTiens
{
    public interface IDUTAppService : IApplicationService
    {
        dynamic DUT_Delete(string dutId);

        DUT_DTO DUT_ById(string dutId);

        PageResult DUT_SearchFilter(int PageNumber, int PageSize, string dutName, string dutCode, string dutDesc, string record_status, string sort);

        dynamic DUT_Create(DUTCreate_DTO input);

        string DUT_GetValueGenCode();

        dynamic DUT_Update(DUT_DTO input);

        dynamic DUT_GetLevel();
    }

    //[AbpAuthorize(Group6PermissionsConst.Pages_Administration_DVT)]
    public class DUTAppService : BaseService, IDUTAppService
    {
        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_DoUuTien_ById)]
        public DUT_DTO DUT_ById(string dutId)
        {
            return procedureHelper.GetData<DUT_DTO>("DUT_ById", new
            {
                DUT_ID = dutId
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_DoUuTien_Delete)]
        public dynamic DUT_Delete(string dutId)
        {
            return procedureHelper.GetData<dynamic>("DUT_Delete", new
            {
                DUT_ID = dutId
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_DoUuTien_SearchFilter)]
        public PageResult DUT_SearchFilter(int pageNumber, int pageSize, string dutName, string dutCode, string dutDesc, string record_status, string sort)
        {
            var result = procedureHelper.GetData<dynamic>("DUT_SearchFilter", new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                DUT_NAME = dutName,
                DUT_CODE = dutCode,
                DUT_DESC= dutDesc,
                RECORD_STATUS = record_status,
                SORT = sort
            });
            var total_record = procedureHelper.GetData<int>("DUT_GetCount", new
            {
                DUT_NAME = dutName,
                DUT_CODE = dutCode,
                DUT_DESC = dutDesc,
                RECORD_STATUS = record_status
            }).FirstOrDefault();

            return PageResult.CreateSuccess(result, total_record);
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_DoUuTien_Create)]
        public dynamic DUT_Create(DUTCreate_DTO input)
        {
            input.RECORD_STATUS = input.RECORD_STATUS ?? "1";
            return procedureHelper.GetData<dynamic>("DUT_Create", input).FirstOrDefault();
        }

        public string DUT_GetValueGenCode()
        {
            return procedureHelper.GetData<string>("DUT_GetValueGenCode", new { }).FirstOrDefault();
        }

        [AbpAuthorize(Group6PermissionsConst.Pages_Administration_DoUuTien_Update)]
        public dynamic DUT_Update(DUT_DTO input)
        {
            return procedureHelper.GetData<dynamic>("DUT_Update", input);
        }

        public dynamic DUT_GetLevel()
        {
            return procedureHelper.GetData<dynamic>("DUT_GetLevel", new { });
        }
    }
}
