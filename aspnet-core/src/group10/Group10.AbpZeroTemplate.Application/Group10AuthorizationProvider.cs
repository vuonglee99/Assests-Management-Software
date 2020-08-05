using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group10.AbpZeroTemplate.Application
{
    public class Group10AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group10AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group10AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group10 = pages.CreateChildPermission("Pages.Group10", L("Group10"));


            var demoModels = Group10.CreateChildPermission(Group10PermissionsConst.Pages_Administration_Xe, L("Xe"));
            demoModels.CreateChildPermission(Group10PermissionsConst.Pages_Administration_Xe_Add, L("Create"));
            demoModels.CreateChildPermission(Group10PermissionsConst.Pages_Administration_Xe_Update, L("Edit"));
            demoModels.CreateChildPermission(Group10PermissionsConst.Pages_Administration_Xe_Delete, L("Delete"));
            demoModels.CreateChildPermission(Group10PermissionsConst.Pages_Administration_Xe_Approve, L("Approve"));

            var nsx = Group10.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NSX, L("NSX"));
            nsx.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NSX_Add, L("Create"));
            nsx.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NSX_Update, L("Edit"));
            nsx.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NSX_Delete, L("Delete"));
            nsx.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NSX_Approve, L("Approve"));
            nsx.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NSX_Detail, L("Detail"));

            var NTX = Group10.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NTX, L("NTX"));
            NTX.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NTX_Add, L("Create"));
            NTX.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NTX_Update, L("Edit"));
            NTX.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NTX_Delete, L("Delete"));
            NTX.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NTX_Detail, L("Detail"));
            NTX.CreateChildPermission(Group10PermissionsConst.Pages_Administration_NTX_Search, L("Search"));


            var PTX = Group10.CreateChildPermission(Group10PermissionsConst.Pages_Administration_PTX, L("PTX"));
            PTX.CreateChildPermission(Group10PermissionsConst.Pages_Administration_PTX_Delete, L("Delete"));
            PTX.CreateChildPermission(Group10PermissionsConst.Pages_Administration_PTX_Search, L("Search"));

            var CTBD = Group10.CreateChildPermission(Group10PermissionsConst.Pages_Administration_CTBD, L("CTBD"));
            CTBD.CreateChildPermission(Group10PermissionsConst.Pages_Administration_CTBD_Add, L("Create"));
            CTBD.CreateChildPermission(Group10PermissionsConst.Pages_Administration_CTBD_Update, L("Edit"));
            CTBD.CreateChildPermission(Group10PermissionsConst.Pages_Administration_CTBD_Delete, L("Delete"));
            CTBD.CreateChildPermission(Group10PermissionsConst.Pages_Administration_CTBD_Detail, L("Detail"));
            CTBD.CreateChildPermission(Group10PermissionsConst.Pages_Administration_CTBD_Approve, L("Approve"));
            CTBD.CreateChildPermission(Group10PermissionsConst.Pages_Administration_CTBD_Search, L("Search"));

            var BD = pages.CreateChildPermission(Group10PermissionsConst.Pages_Administration_BD, L("BD"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}