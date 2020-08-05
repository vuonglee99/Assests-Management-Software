using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group3.AbpZeroTemplate.Application
{
    public class Group3AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group3AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group3AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group3 = pages.CreateChildPermission("Pages.Group3", L("Group3"));


            var demoModels = pages.CreateChildPermission(Group3PermissionsConst.Pages_Administration_Branches, L("Branches"));
            demoModels.CreateChildPermission(Group3PermissionsConst.Pages_Administration_Branches_Delete, L("Delete"));
            
            var group03 = pages.CreateChildPermission(Group3PermissionsConst.Group3, L("Group03"));

            // Buildings
            var Buildings = group03.CreateChildPermission(Group3PermissionsConst.Buildings, L("Buildings"));
            Buildings.CreateChildPermission(Group3PermissionsConst.Buildings_Create, L("Create"));
            Buildings.CreateChildPermission(Group3PermissionsConst.Buildings_Update, L("Update"));
            Buildings.CreateChildPermission(Group3PermissionsConst.Buildings_Delete, L("Delete"));
            Buildings.CreateChildPermission(Group3PermissionsConst.Buildings_Approve, L("Approve"));

            // Floors
            var Floors = group03.CreateChildPermission(Group3PermissionsConst.Floors, L("Floors"));
            Floors.CreateChildPermission(Group3PermissionsConst.Floors_Create, L("Create"));
            Floors.CreateChildPermission(Group3PermissionsConst.Floors_Update, L("Update"));
            Floors.CreateChildPermission(Group3PermissionsConst.Floors_Delete, L("Delete"));
            Floors.CreateChildPermission(Group3PermissionsConst.Floors_Approve, L("Approve"));

            // Apartments
            var Apartments = group03.CreateChildPermission(Group3PermissionsConst.Apartments, L("Apartments"));
            Apartments.CreateChildPermission(Group3PermissionsConst.Apartments_Create, L("Create"));
            Apartments.CreateChildPermission(Group3PermissionsConst.Apartments_Update, L("Update"));
            Apartments.CreateChildPermission(Group3PermissionsConst.Apartments_Delete, L("Delete"));
            Apartments.CreateChildPermission(Group3PermissionsConst.Apartments_Approve, L("Approve"));

            // Residents
            var Residents = group03.CreateChildPermission(Group3PermissionsConst.Residents, L("Residents"));
            Residents.CreateChildPermission(Group3PermissionsConst.Residents_Create, L("Create"));
            Residents.CreateChildPermission(Group3PermissionsConst.Residents_Update, L("Update"));
            Residents.CreateChildPermission(Group3PermissionsConst.Residents_Delete, L("Delete"));
            Residents.CreateChildPermission(Group3PermissionsConst.Residents_Approve, L("Approve"));

            // Contracts
            var Contracts = group03.CreateChildPermission(Group3PermissionsConst.Contracts, L("Contracts"));
            Contracts.CreateChildPermission(Group3PermissionsConst.Contracts_Create, L("Create"));
            Contracts.CreateChildPermission(Group3PermissionsConst.Contracts_Update, L("Update"));
            Contracts.CreateChildPermission(Group3PermissionsConst.Contracts_Delete, L("Delete"));
            Contracts.CreateChildPermission(Group3PermissionsConst.Contracts_Approve, L("Approve"));

            // ContractDetails
            var ContractDetails = group03.CreateChildPermission(Group3PermissionsConst.ContractDetails, L("ContractDetails"));
            ContractDetails.CreateChildPermission(Group3PermissionsConst.ContractDetails_Create, L("Create"));
            ContractDetails.CreateChildPermission(Group3PermissionsConst.ContractDetails_Update, L("Update"));
            ContractDetails.CreateChildPermission(Group3PermissionsConst.ContractDetails_Delete, L("Delete"));
            ContractDetails.CreateChildPermission(Group3PermissionsConst.ContractDetails_Approve, L("Approve"));

            // EnumeratedTypes
            var EnumeratedTypes = group03.CreateChildPermission(Group3PermissionsConst.EnumeratedTypes, L("EnumeratedTypes"));
            EnumeratedTypes.CreateChildPermission(Group3PermissionsConst.EnumeratedTypes_Create, L("Create"));
            EnumeratedTypes.CreateChildPermission(Group3PermissionsConst.EnumeratedTypes_Delete, L("Delete"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
