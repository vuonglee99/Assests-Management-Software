using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group5.AbpZeroTemplate.Application
{
    public class Group5AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group5AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group5AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var group5 = pages.CreateChildPermission("Pages.Group5", L("Group5"));

            //deparment
            var PhongBan = pages.CreateChildPermission(Group5PermissionsConst.Pages_Administration_PhongBan, L("PhongBan"));
            PhongBan.CreateChildPermission(Group5PermissionsConst.Pages_Administration_PhongBan_Delete, L("Delete"));

            //equiment
            var Equipment = pages.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Equipment, L("Equipment"));
            Equipment.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Equipment_Add, L("Create"));
            Equipment.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Equipment_View, L("View"));
            Equipment.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Equipment_Insert, L("Insert"));
            Equipment.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Equipment_GetCode, L("GetCode"));
            Equipment.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Equipment_Approve, L("Approve"));

            //device
            var Device = pages.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Dev, L("Device"));
            Device.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Dev_Add, L("Create"));
            Device.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Dev_Update, L("Edit"));
            Device.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Dev_Delete, L("Delete"));
            Device.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Dev_View, L("View"));
            Device.CreateChildPermission(Group5PermissionsConst.Pages_Administration_Dev_Approve, L("Approve"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
