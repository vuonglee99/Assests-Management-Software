using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group13.AbpZeroTemplate.Application
{
    public class Group13AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group13AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group13AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group13 = pages.CreateChildPermission("Pages.Group13", L("Group13"));


            var demoModels = pages.CreateChildPermission(Group13PermissionsConst.Pages_Administration_Xe, L("Xe"));
            demoModels.CreateChildPermission(Group13PermissionsConst.Pages_Administration_Xe_Add, L("Create"));
            demoModels.CreateChildPermission(Group13PermissionsConst.Pages_Administration_Xe_Update, L("Edit"));
            demoModels.CreateChildPermission(Group13PermissionsConst.Pages_Administration_Xe_Delete, L("Delete"));
            demoModels.CreateChildPermission(Group13PermissionsConst.Pages_Administration_Xe_Approve, L("Approve"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
