import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PhongBanComponent } from './component/phongban/phong-ban.component';
import { ChiTietPhongBanComponent } from './component/chi-tiet-phong-ban/chi-tiet-phong-ban.component';
import {BaoTriBaoDuongThietBiComponent} from './component/bao-tri-bao-duong-thiet-bi/bao-tri-bao-duong-thiet-bi.component';
import {ChiTietBaoTriBaoDuongComponent} from './component/chi-tiet-bao-tri-bao-duong/chi-tiet-bao-tri-bao-duong.component';
import {ThemCongViecComponent} from './component/them-cong-viec/them-cong-viec.component';
import {ThietBiComponent} from './component/thiet-bi/thiet-bi.component';
import {ThemThietBiComponent} from './component/them-thiet-bi/them-thiet-bi.component';
import {ChiTietThietBiComponent} from './component/chi-tiet-thiet-bi/chi-tiet-thiet-bi.component';
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'phong-ban', component: PhongBanComponent, data: { permission: 'Pages.Group5.PhongBan' } },
                    { path: 'chi-tiet-phong-ban', component: ChiTietPhongBanComponent, data: { permission: 'Pages.Group5.PhongBan' } },
                    { path: 'bao-tri-bao-duong' , component : BaoTriBaoDuongThietBiComponent , data: { permission: 'Pages.Group5.Equipment' } },
                    { path: 'chi-tiet-bao-tri' , component : ChiTietBaoTriBaoDuongComponent , data: { permission: 'Pages.Group5.Equipment.View' } },
                    { path: 'them-cong-viec' , component : ThemCongViecComponent , data: { permission: 'Pages.Group5.Equipment.Add' , editPageState: "add" } },
                    { path: 'thiet-bi' , component : ThietBiComponent , data: { permission: 'Pages.Group5.Device.View' } },
                    { path: 'them-thiet-bi' , component : ThemThietBiComponent , data: { permission: 'Pages.Group5.Device.Add' , editPageState: "add" } },
                    { path: 'chi-tiet-thiet-bi' , component : ChiTietThietBiComponent , data: { permission: 'Pages.Group5.Device.View' , editPageState: "view" } },
                    { path: 'chinh-sua-thiet-bi' , component : ChiTietThietBiComponent , data: { permission: 'Pages.Group5.Device.Update' , editPageState: "edit" } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group5RoutingModule { }



