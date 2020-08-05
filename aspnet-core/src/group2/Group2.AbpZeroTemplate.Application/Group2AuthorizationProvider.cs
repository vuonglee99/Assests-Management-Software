using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group2.AbpZeroTemplate.Application
{
    public class Group2AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group2AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group2AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group2 = pages.CreateChildPermission("Pages.Group2", L("Group2"));


            var demoModels = pages.CreateChildPermission(Group2PermissionsConst.Pages_Administration_LoaiXe, L("LoaiXe"));
            demoModels.CreateChildPermission(Group2PermissionsConst.Pages_Administration_LoaiXe_Add, L("Create"));
            demoModels.CreateChildPermission(Group2PermissionsConst.Pages_Administration_LoaiXe_Update, L("Edit"));
            demoModels.CreateChildPermission(Group2PermissionsConst.Pages_Administration_LoaiXe_Delete, L("Delete"));
            demoModels.CreateChildPermission(Group2PermissionsConst.Pages_Administration_LoaiXe_View, L("View"));
            demoModels.CreateChildPermission(Group2PermissionsConst.Pages_Administration_ChiTietKiemKe, L("View"));
            demoModels.CreateChildPermission(Group2PermissionsConst.Pages_Administration_TaoQRThietBi, L("TaoQR"));

            demoModels.CreateChildPermission(Group2PermissionsConst.Pages_Administration_LoaiXe_Approve, L("Approve"));
            demoModels.CreateChildPermission(Group2PermissionsConst.Pages_Administration_LoaiXe_UnApprove, L("UnApprove"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
