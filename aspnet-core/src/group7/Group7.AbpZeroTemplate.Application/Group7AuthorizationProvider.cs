using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group7.AbpZeroTemplate.Application
{
    public class Group7AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group7AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group7AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group7 = pages.CreateChildPermission("Pages.Group7", L("Group7"));


            var demoModels = pages.CreateChildPermission(Group7PermissionsConst.Pages_Administration_Xe, L("Xe"));
            demoModels.CreateChildPermission(Group7PermissionsConst.Pages_Administration_Xe_Add, L("Create"));
            demoModels.CreateChildPermission(Group7PermissionsConst.Pages_Administration_Xe_Update, L("Edit"));
            demoModels.CreateChildPermission(Group7PermissionsConst.Pages_Administration_Xe_Delete, L("Delete"));
            demoModels.CreateChildPermission(Group7PermissionsConst.Pages_Administration_Xe_Approve, L("Approve"));

            var loaiXe = pages.CreateChildPermission(Group7PermissionsConst.Pages_Administration_LoaiXe, L("LoaiXe"));
            loaiXe.CreateChildPermission(Group7PermissionsConst.Pages_Administration_LoaiXe_Add, L("Add"));
            loaiXe.CreateChildPermission(Group7PermissionsConst.Pages_Administration_LoaiXe_Update, L("Update"));
            loaiXe.CreateChildPermission(Group7PermissionsConst.Pages_Administration_LoaiXe_Delete, L("Delete"));
            loaiXe.CreateChildPermission(Group7PermissionsConst.Pages_Administration_LoaiXe_Approve, L("Approve"));
            loaiXe.CreateChildPermission(Group7PermissionsConst.Pages_Administration_LoaiXe_View, L("View"));

            var nhaCungUng = pages.CreateChildPermission(Group7PermissionsConst.Pages_Administration_NhaCungUng, L("NhaCungUng"));
            nhaCungUng.CreateChildPermission(Group7PermissionsConst.Pages_Administration_NhaCungUng_Add, L("Add"));
            nhaCungUng.CreateChildPermission(Group7PermissionsConst.Pages_Administration_NhaCungUng_Update, L("Update"));
            nhaCungUng.CreateChildPermission(Group7PermissionsConst.Pages_Administration_NhaCungUng_Delete, L("Delete"));
            nhaCungUng.CreateChildPermission(Group7PermissionsConst.Pages_Administration_NhaCungUng_Approve, L("Approve"));
            nhaCungUng.CreateChildPermission(Group7PermissionsConst.Pages_Administration_NhaCungUng_View, L("View"));

            var yeuCauSuaChua = pages.CreateChildPermission(Group7PermissionsConst.Pages_Administration_YCSC, L("YCSC"));
            yeuCauSuaChua.CreateChildPermission(Group7PermissionsConst.Pages_Administration_YCSC_Add, L("Add"));
            yeuCauSuaChua.CreateChildPermission(Group7PermissionsConst.Pages_Administration_YCSC_Update, L("Update"));
            yeuCauSuaChua.CreateChildPermission(Group7PermissionsConst.Pages_Administration_YCSC_Delete, L("Delete"));
            yeuCauSuaChua.CreateChildPermission(Group7PermissionsConst.Pages_Administration_YCSC_Approve, L("Approve"));
            yeuCauSuaChua.CreateChildPermission(Group7PermissionsConst.Pages_Administration_YCSC_View, L("View"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
