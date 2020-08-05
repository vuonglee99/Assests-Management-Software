using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group9.AbpZeroTemplate.Application
{
    public class Group9AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group9AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group9AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group9 = pages.CreateChildPermission("Pages.Group9", L("Group9"));


            var demoModels = pages.CreateChildPermission(Group9PermissionsConst.Pages_Administration_Xe, L("Xe"));
            demoModels.CreateChildPermission(Group9PermissionsConst.Pages_Administration_Xe_Add, L("Create"));
            demoModels.CreateChildPermission(Group9PermissionsConst.Pages_Administration_Xe_Update, L("Edit"));
            demoModels.CreateChildPermission(Group9PermissionsConst.Pages_Administration_Xe_Delete, L("Delete"));
            demoModels.CreateChildPermission(Group9PermissionsConst.Pages_Administration_Xe_Approve, L("Approve"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
