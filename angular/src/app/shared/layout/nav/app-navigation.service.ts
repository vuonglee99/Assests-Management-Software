import {PermissionCheckerService} from '@abp/auth/permission-checker.service';
import {Injectable} from '@angular/core';
import {AppMenu} from './app-menu';
import {AppMenuItem} from './app-menu-item';

@Injectable()
export class AppNavigationService {
    constructor(private _permissionService: PermissionCheckerService) {
    }

    getMenu(): AppMenu {
        return new AppMenu('MainMenu', 'MainMenu', [
            new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),
            new AppMenuItem('Administration', '', 'flaticon-interface-8', '', [
                new AppMenuItem('MenuClient', 'Pages.Administration.MenuClient', 'flaticon-menu-1', '/app/gwebsite/menu-client'),
                new AppMenuItem('DemoModel', 'Pages.Administration.DemoModel', 'flaticon-menu-1', '/app/gwebsite/demo-model'),
                new AppMenuItem('Xe', 'Pages.Group0.Xe', 'flaticon-menu-1', '/app/admin/xe'),
                // new AppMenuItem("Nhà cung ứng", null, "flaticon-menu-1", "/app/admin/nha-cung-ung"),
                new AppMenuItem('Nhân viên', 'Pages.Group14.NhanVien', 'flaticon-users', '/app/admin/nhanvien'),
                new AppMenuItem('Nhà cung ứng', 'Pages.Group14.NhaCungUng', 'flaticon-truck', '/app/admin/nhacungung'),
                new AppMenuItem('Thiết bị vật tư', 'Pages.Group14.ThietBiVatTu', 'flaticon-tool', '/app/admin/thietbivattu'),
                new AppMenuItem('Phiếu cấp phát', 'Pages.Group14.PCPTBVT', 'flaticon-truck', '/app/admin/capphattbvt'),
                new AppMenuItem('Đơn vị tính', 'Pages.Group15.DONVITINH.GetAll', 'flaticon-menu-1', '/app/admin/donvitinh'),
                new AppMenuItem('Phòng ban', 'Pages.Group5.PhongBan', 'flaticon-menu-1', '/app/admin/phong-ban'),
                new AppMenuItem('Model', 'Pages.Group12.Model', 'flaticon-menu-1', '/app/admin/model'),
                new AppMenuItem('Branches', 'Pages.Group3.Branches', 'flaticon-menu-1', '/app/admin/branches'),
                new AppMenuItem('Loại xe', 'Pages.Group7.LoaiXe', 'flaticon-menu-1', '/app/admin/loai-xe'),
                new AppMenuItem('Thống Kê Tòa', 'Pages.Group15.APARTMENT_.GetAll', 'flaticon-menu-1', '/app/admin/thongketoanha'),
                new AppMenuItem('Loại tầng', 'Pages.Group15.FLOORTYPE', 'flaticon-menu-1', '/app/admin/loaitang'),
                new AppMenuItem('Chi Tiết Nhân Viên', 'Pages.Group8.ChiTietNhanVien', 'flaticon-menu-1', '/app/admin/chitietnhanvien'),
                new AppMenuItem('Group10_NSX_LIST_TITLE', "Pages.Group10.NSX", "flaticon-menu-1", "/app/admin/nsx-list"),
                new AppMenuItem('Bảo trì bảo dưỡng', 'Pages.Group5.Equipment', 'flaticon-menu-1', '/app/admin/bao-tri-bao-duong'),
                new AppMenuItem('Thiết bị', 'Pages.Group5.Device', 'flaticon-menu-1', '/app/admin/thiet-bi'),
                new AppMenuItem('Độ ưu tiên', 'Pages.Group6.DoUuTien.SearchFilter', 'flaticon-menu-1', '/app/admin/douutien'),
                new AppMenuItem('Trạng thái YCSC', null, 'flaticon-menu-1', '/app/admin/trangthaiyeucausuachua'),
                new AppMenuItem('Thống kê yêu cầu sửa chữa', null, 'flaticon-menu-1', '/app/admin/statistics/yeu-cau-sua-chua'),

                new AppMenuItem('Tạo QR kiểm kê', null, 'flaticon-menu-1', '/app/admin/tao-qrcode'),
                new AppMenuItem('Kiểm kê', 'Pages.Group12.KiemKe', 'flaticon-menu-1', '/app/admin/kiemke'),
                new AppMenuItem('Sửa chữa TBVTYT', null, 'flaticon-edit-1', '/app/admin/yeu-cau-sua-chua-tbvtyt'),
                new AppMenuItem('Group10_NTX_LIST_TITLE', 'Pages.Group10.NTX', 'flaticon-menu-1', '/app/admin/ntx-list'),
                new AppMenuItem('CarRentalManagement', 'Pages.Group10.PTX', 'flaticon-menu-1', '/app/admin/ptx-list'),
                new AppMenuItem('Group11Car', 'Pages.Group11.Xe', 'flaticon-menu-1', '/app/admin/xe-group11'),
                // new AppMenuItem('Group11CarRental', 'Pages.Group11.Xe', 'flaticon-menu-1', '/app/admin/thue-xe-add'),
                new AppMenuItem('Group11Maintenance', 'Pages.Group11.BD', 'flaticon-menu-1', '/app/admin/bao-duong-list'),
                new AppMenuItem('BuildingsManagement', 'Pages.Group03.Buildings', 'flaticon-menu-1', '/app/admin/buildings'),
                new AppMenuItem('ResidentsManagement', 'Pages.Group03.Residents', 'flaticon-users', '/app/admin/residents'),
                new AppMenuItem('Thiết Bị Vật Tư', 'Pages.Group8.ChiTietNhanVien', 'flaticon-menu-1', '/app/admin/thietbivattu-add'),
                new AppMenuItem('g4ApartmentManagement', 'Pages.Group4.Apartment', 'flaticon-menu-1', '/app/admin/apartment'),
                new AppMenuItem('g4ApartmentTypeManagement', 'Pages.Group4.ApartmentType', 'flaticon-menu-1', '/app/admin/apartment-type'),
            ]),
            new AppMenuItem('Systems', '', 'flaticon-layers', '', [
                new AppMenuItem(
                    'OrganizationUnits',
                    'Pages.Administration.OrganizationUnits',
                    'flaticon-map',
                    '/app/admin/organization-units'
                ),
                new AppMenuItem(
                    'Roles',
                    'Pages.Administration.Roles',
                    'flaticon-suitcase',
                    '/app/admin/roles'
                ),
                new AppMenuItem(
                    'Users',
                    'Pages.Administration.Users',
                    'flaticon-users',
                    '/app/admin/users'
                ),
                new AppMenuItem(
                    'Languages',
                    'Pages.Administration.Languages',
                    'flaticon-tabs',
                    '/app/admin/languages'
                ),
                new AppMenuItem(
                    'AuditLogs',
                    'Pages.Administration.AuditLogs',
                    'flaticon-folder-1',
                    '/app/admin/auditLogs'
                ),
                new AppMenuItem(
                    'Maintenance',
                    'Pages.Administration.Host.Maintenance',
                    'flaticon-lock',
                    '/app/admin/maintenance'
                ),
                new AppMenuItem(
                    'Subscription',
                    'Pages.Administration.Tenant.SubscriptionManagement',
                    'flaticon-refresh',
                    '/app/admin/subscription-management'
                ),
                new AppMenuItem(
                    'VisualSettings',
                    'Pages.Administration.UiCustomization',
                    'flaticon-medical',
                    '/app/admin/ui-customization'
                ),
                new AppMenuItem(
                    'Settings',
                    'Pages.Administration.Host.Settings',
                    'flaticon-settings',
                    '/app/admin/hostSettings'
                ),
                new AppMenuItem(
                    'Settings',
                    'Pages.Administration.Tenant.Settings',
                    'flaticon-settings',
                    '/app/admin/tenantSettings'
                ),
            ]),
            new AppMenuItem(
                'DemoUiComponents',
                'Pages.DemoUiComponents',
                'flaticon-shapes',
                '/app/admin/demo-ui-components'
            ),
        ]);
    }

    checkChildMenuItemPermission(menuItem): boolean {
        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (
                subMenuItem.permissionName &&
                this._permissionService.isGranted(subMenuItem.permissionName)
            ) {
                return true;
            }

            if (subMenuItem.items && subMenuItem.items.length) {
                return this.checkChildMenuItemPermission(subMenuItem);
            } else if (!subMenuItem.permissionName) {
                return true;
            }
        }

        return false;
    }
}
