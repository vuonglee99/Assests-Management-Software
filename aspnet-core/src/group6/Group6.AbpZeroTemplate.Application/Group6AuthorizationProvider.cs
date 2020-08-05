using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group6.AbpZeroTemplate.Application
{
    public class Group6AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group6AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group6AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var group6 = pages.CreateChildPermission("Pages.Group6", L("Group6"));


            var donvitinhModels = group6.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DVT, L("DVT"));
            donvitinhModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DVT_Delete, L("Delete"));

            var douutienModels = group6.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DoUuTien, L("DoUuTien"));
            douutienModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DoUuTien_Delete, L("Delete"));
            douutienModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DoUuTien_Create, L("Create"));
            douutienModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DoUuTien_SearchFilter, L("SearchFilter"));
            douutienModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DoUuTien_ById, L("ById"));
            douutienModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DoUuTien_Update, L("Update"));

            var ttycscModels = group6.CreateChildPermission(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua, L("TrangThaiYeuCauSuaChua"));
            ttycscModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_Delete, L("Delete"));
            ttycscModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_Create, L("Create"));
            ttycscModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_SearchFilter, L("SearchFilter"));
            ttycscModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_ById, L("ById"));
            ttycscModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_Update, L("Update"));
            ttycscModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_TrangThaiYeuCauSuaChua_Approve, L("Approve"));

            //var dasboardYCSCModels = group6.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DashboardYCSC, L("DashboardYCSC"));
            //dasboardYCSCModels.CreateChildPermission(Group6PermissionsConst.Pages_Administration_DashboardYCSC_Statistic, L("Statistic"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
