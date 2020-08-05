using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GWebsite.AbpZeroTemplate.Application.DonViTinhs;
using GSoft.AbpZeroTemplate.Helpers;
using Abp.Authorization;
using Group15.AbpZeroTemplate.Application;

namespace GWebsite.AbpZeroTemplate.Application.Services.DonViTinhs
{
    public interface IDonViTinhService : IApplicationService
    {
        ServiceResult DVT_MaDVT();
        ServiceResult DVT_Create(DVTCreate_DTO input);
        ServiceResult DVT_Update(DVTUpdate_DTO input);
        List<DonViTinh_DTO> DVT_GetAll();
    }

    [AbpAuthorize(Group15PermissionsConst.Pages_Administration_DVT)]
    public class DonViTinhService : BaseService, IDonViTinhService
    {
        private static int MAX_CODE_LENGTH = 12;
        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_DVT_Add)]
        public ServiceResult DVT_Create(DVTCreate_DTO input)
        {
            var result = this.procedureHelper.GetData<ServiceResult>("DVT_Create", input);
            return result[0];
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_DVT_GetAll)]
        public List<DonViTinh_DTO> DVT_GetAll()
        {
            return this.procedureHelper.GetData<DonViTinh_DTO>("DVT_GetAll", new { });
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_DVT_CreateCode)]
        public ServiceResult DVT_MaDVT()
        {
            var dvtCount = this.procedureHelper.GetData<int>("DVT_GetDVTCount", new { })[0];
            var newCode = "DVT" + (dvtCount + 1).ToString("D" + MAX_CODE_LENGTH.ToString());
            return ServiceResult.CreateSuccess(newCode);
        }

        [AbpAuthorize(Group15PermissionsConst.Pages_Administration_DVT_Update)]
        public ServiceResult DVT_Update(DVTUpdate_DTO input)
        {
            return this.procedureHelper.GetData<ServiceResult>("DVT_Update", input)[0];
        }
    }
}
