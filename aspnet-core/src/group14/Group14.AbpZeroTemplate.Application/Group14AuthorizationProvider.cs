using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group14.AbpZeroTemplate.Application
{
    public class Group14AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group14AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group14AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group14 = pages.CreateChildPermission("Pages.Group14", L("Group14"));

            var nhanvien = pages.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhanVien, L("NhanVien"));
            nhanvien.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhanVien_Delete, L("NhanVien_Delete"));
            nhanvien.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhanVien_Search, L("NhanVien_Search"));
            nhanvien.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhanVien_ById, L("NhanVien_ById"));
            nhanvien.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhanVien_Insert, L("NhanVien_Insert"));
            nhanvien.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhanVien_Update, L("NhanVien_Update"));

            var nhaCungUng = pages.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhaCungUng, L("NhaCungUng"));
            nhaCungUng.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhaCungUng_Delete, L("NhaCungUng_Delete"));
            nhaCungUng.CreateChildPermission(Group14PermissionsConst.Pages_Administration_NhaCungUng_Search, L("NhaCungUng_Search"));


            var thietBiVatTu = pages.CreateChildPermission(Group14PermissionsConst.Pages_Administration_ThietBiVatTu, L("ThietBiVatTu"));
            thietBiVatTu.CreateChildPermission(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_Delete, L("ThietBiVatTu_Delete"));
            thietBiVatTu.CreateChildPermission(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_Search, L("ThietBiVatTu_Search"));
            thietBiVatTu.CreateChildPermission(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_AddToPCPTBVT, L("ThietBiVatTu_AddToPCPTBVT"));
            thietBiVatTu.CreateChildPermission(Group14PermissionsConst.Pages_Administration_ThietBiVatTu_RemoveFromPCPTBVT, L("ThietBiVatTu_RemoveFromPCPTBVT"));

            var pcptbvt = pages.CreateChildPermission(Group14PermissionsConst.Pages_Administration_PCPTBVT, L("PCPTBVT"));
            pcptbvt.CreateChildPermission(Group14PermissionsConst.Pages_Administration_PCPTBVT_Delete, L("PCPTBVT_Delete"));
            pcptbvt.CreateChildPermission(Group14PermissionsConst.Pages_Administration_PCPTBVT_Search, L("PCPTBVT_Search"));
            pcptbvt.CreateChildPermission(Group14PermissionsConst.Pages_Administration_PCPTBVT_Approve, L("PCPTBVT_Approve"));
            pcptbvt.CreateChildPermission(Group14PermissionsConst.Pages_Administration_PCPTBVT_Insert, L("PCPTBVT_Insert"));
            pcptbvt.CreateChildPermission(Group14PermissionsConst.Pages_Administration_PCPTBVT_Update, L("PCPTBVT_Update"));
            pcptbvt.CreateChildPermission(Group14PermissionsConst.Pages_Administration_PCPTBVT_ById, L("PCPTBVT_ById"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
