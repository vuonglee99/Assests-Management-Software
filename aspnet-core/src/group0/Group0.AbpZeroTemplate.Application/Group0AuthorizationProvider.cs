using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group0.AbpZeroTemplate.Application
{
    public class Group0AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group0AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group0AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var group0 = pages.CreateChildPermission("Pages.Group0", L("Group0"));


            var demoModels = pages.CreateChildPermission(Group0PermissionsConst.Pages_Administration_Xe, L("Xe"));
            demoModels.CreateChildPermission(Group0PermissionsConst.Pages_Administration_Xe_Add, L("Create"));
            demoModels.CreateChildPermission(Group0PermissionsConst.Pages_Administration_Xe_Update, L("Edit"));
            demoModels.CreateChildPermission(Group0PermissionsConst.Pages_Administration_Xe_Delete, L("Delete"));
            demoModels.CreateChildPermission(Group0PermissionsConst.Pages_Administration_Xe_Approve, L("Approve"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
