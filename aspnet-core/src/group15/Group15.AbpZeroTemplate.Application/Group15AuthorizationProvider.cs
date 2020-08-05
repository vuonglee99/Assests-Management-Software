using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using GSoft.AbpZeroTemplate;

namespace Group15.AbpZeroTemplate.Application
{
    public class Group15AuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public Group15AuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public Group15AuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));
            var Group15 = pages.CreateChildPermission("Pages.Group15", L("Group15"));

            var donViTinhs = Group15.CreateChildPermission(Group15PermissionsConst.Pages_Administration_DVT, L("DonViTinh"));
            donViTinhs.CreateChildPermission(Group15PermissionsConst.Pages_Administration_DVT_Add, L("Create"));
            donViTinhs.CreateChildPermission(Group15PermissionsConst.Pages_Administration_DVT_Update, L("Update"));
            donViTinhs.CreateChildPermission(Group15PermissionsConst.Pages_Administration_DVT_GetAll, L("GetAll"));
            donViTinhs.CreateChildPermission(Group15PermissionsConst.Pages_Administration_DVT_CreateCode, L("CreateCode"));

            var floorType = Group15.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType, L("FloorType"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_GetAll, L("GetAll"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_GetById, L("GetById"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_Create, L("Create"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_Update, L("Update"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_Search, L("Search"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_Delete, L("Delete"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_ApproveAdd, L("ApproveAdd"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_CancelApproveAdd, L("CancelApproveAdd"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_ApproveUpdate, L("ApproveUpdate"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_CancelApproveUpdate, L("CancelApproveUpdate"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_GetApproveOfId, L("GetApproveOfId"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_ApproveDelete, L("ApproveDelete"));
            floorType.CreateChildPermission(Group15PermissionsConst.Pages_Administration_FloorType_CancelApproveDelete, L("CancelApproveDelete"));

            var floor = Group15.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor, L("Floor"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_GetAll, L("GetAll"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_GetByBuildingId, L("GetByBuildingId"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_GetById, L("GetById"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_Create, L("Create"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_Update, L("Update"));         
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_Delete, L("Delete"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_ApproveAdd, L("ApproveAdd"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_ApproveUpdate, L("ApproveUpdate"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_ApproveDelete, L("ApproveDelete"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_CancelApproveAdd, L("CancelApproveAdd"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_CancelApproveUpdate, L("CancelApproveUpdate"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_CancelApproveDelete, L("CancelApproveDelete"));
            floor.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Floor_GetApproveOfId, L("GetApproveOfId"));

            var apartment = Group15.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Apartment, L("Apartment_"));
            apartment.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Apartment_GetAll, L("GetAll_"));

            var demoModels = pages.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Xe, L("Xe"));
            demoModels.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Xe_Add, L("Create"));
            demoModels.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Xe_Update, L("Edit"));
            demoModels.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Xe_Delete, L("Delete"));
            demoModels.CreateChildPermission(Group15PermissionsConst.Pages_Administration_Xe_Approve, L("Approve"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
