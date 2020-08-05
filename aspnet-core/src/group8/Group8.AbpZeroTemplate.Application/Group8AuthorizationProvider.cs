using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group8.AbpZeroTemplate.Application
{
    public class Group8AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group8AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group8AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {

            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group8 = pages.CreateChildPermission("Pages.Group8", L("Group8"));


            var ChiTietNhanVien = pages.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien, L("ChiTietNhanVien"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_ById, L("ById"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_Insert, L("Insert"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_Update, L("Update"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_Delete, L("Delete"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_Search, L("Search"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_GetAll, L("GetAll"));

            var ThietBiVatTu = pages.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ThietBiVatTu, L("ThietBiVatTu"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_ById, L("ById"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_Insert, L("Insert"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_Update, L("Update"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_Delete, L("Delete"));
            ChiTietNhanVien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_GetAll, L("GetAll"));

            var PhuKien = pages.CreateChildPermission(Group8PermissionsConst.Pages_Administration_PhuKien, L("PhuKien"));
            PhuKien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_PhuKien_ById, L("ById"));
            PhuKien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_PhuKien_Insert, L("Insert"));
            PhuKien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_PhuKien_Update, L("Update"));
            PhuKien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_PhuKien_Delete, L("Delete"));
            PhuKien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_PhuKien_Search, L("Search"));
            PhuKien.CreateChildPermission(Group8PermissionsConst.Pages_Administration_PhuKien_GetAll, L("GetAll"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
