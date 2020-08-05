using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group4.AbpZeroTemplate.Application
{
    public class Group4AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group4AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group4AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var group4 = pages.CreateChildPermission("Pages.Group4", L("Group4"));


            var apartmentModels = group4.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Apartment, L("Apartment"));
            apartmentModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Apartment_Add, L("Create"));
            apartmentModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Apartment_Update, L("Edit"));
            apartmentModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Apartment_View, L("View"));
            apartmentModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Apartment_Delete, L("Delete"));
            apartmentModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Apartment_Approve, L("Approve"));


            var branchModels = group4.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Branch, L("Branch"));
            branchModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Branch_Add, L("Create"));
            branchModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Branch_Update, L("Edit"));
            branchModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Branch_View, L("View"));
            branchModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Branch_Delete, L("Delete"));
            branchModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_Branch_Approve, L("Approve"));

            var apartmentTypeModels = group4.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentType, L("ApartmentType"));
            apartmentTypeModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentType_Add, L("Create"));
            apartmentTypeModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentType_Update, L("Edit"));
            apartmentTypeModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentType_View, L("View"));
            apartmentTypeModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentType_Delete, L("Delete"));
            apartmentTypeModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentType_Approve, L("Approve"));

            var apartmentResidentModels = group4.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentResident, L("ApartmentResident"));
            apartmentResidentModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentResident_View, L("View"));
            apartmentResidentModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentResident_Add, L("Create"));
            apartmentResidentModels.CreateChildPermission(Group4PermissionsConst.Pages_Administration_ApartmentResident_Delete, L("Delete"));
            
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
