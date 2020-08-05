using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;
using System.Net.PeerToPeer.Collaboration;

namespace Group11.AbpZeroTemplate.Application
{
    public class Group11AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group11AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group11AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group11 = pages.CreateChildPermission("Pages.Group11", L("Group11"));


            var demoModels = pages.CreateChildPermission(Group11PermissionsConst.Pages_Administration_Xe, L("Xe-GROUP11"));
            demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_Xe_Add, L("Create"));
            demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_Xe_Update, L("Edit"));
            demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_Xe_Delete, L("Delete"));
            demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_Xe_Approve, L("Approve"));
            //demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_NSX, L("NSX"));
           // demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_NSX_Delete, L("Delete"));
            //demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_PTX_Insert, L("Insert"));
            //demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_PTX_Update, L("Update"));
           // demoModels.CreateChildPermission(Group11PermissionsConst.Pages_Administration_PTX_Delete, L("Delete"));

            var NSX = pages.CreateChildPermission(Group11PermissionsConst.Pages_Administration_NSX, L("NSX-GROUP11"));
            NSX.CreateChildPermission(Group11PermissionsConst.Pages_Administration_NSX_Delete, L("Delete"));

            var PTX  = pages.CreateChildPermission(Group11PermissionsConst.Pages_Administration_PTX, L("PTX-GROUP11"));
            PTX.CreateChildPermission(Group11PermissionsConst.Pages_Administration_PTX_Insert, L("Insert"));
            PTX.CreateChildPermission(Group11PermissionsConst.Pages_Administration_PTX_Update, L("Update"));
            PTX.CreateChildPermission(Group11PermissionsConst.Pages_Administration_PTX_Delete, L("Delete"));

            var BD = pages.CreateChildPermission(Group11PermissionsConst.Pages_Administration_BD, L("BD"));
            BD.CreateChildPermission(Group11PermissionsConst.Pages_Administration_BD_Insert, L("Insert"));
            BD.CreateChildPermission(Group11PermissionsConst.Pages_Administration_BD_Search, L("Search"));
            BD.CreateChildPermission(Group11PermissionsConst.Pages_Administration_BD_Update, L("Update"));
            BD.CreateChildPermission(Group11PermissionsConst.Pages_Administration_BD_GetByCode, L("GetByCode"));
            BD.CreateChildPermission(Group11PermissionsConst.Pages_Administration_BD_Approve, L("Approve"));
            BD.CreateChildPermission(Group11PermissionsConst.Pages_Administration_BD_Delete, L("Delete"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
