import { NgModule } from '@angular/core';

import { MenuClientServiceProxy, DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group5ServiceProxyModule } from './group5-service-proxy.module';
import { Group5RoutingModule } from './group5-routing.module';
import { TableModule } from 'primeng/table';
import {CalendarModule} from 'primeng/calendar';
import {DropdownModule} from 'primeng/dropdown';
import { FormsModule,ReactiveFormsModule  } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { PhongBanComponent } from './component/phongban/phong-ban.component';
import { ChiTietPhongBanComponent } from './component/chi-tiet-phong-ban/chi-tiet-phong-ban.component';
import {BaoTriBaoDuongThietBiComponent} from './component/bao-tri-bao-duong-thiet-bi/bao-tri-bao-duong-thiet-bi.component';
import {ChiTietBaoTriBaoDuongComponent} from './component/chi-tiet-bao-tri-bao-duong/chi-tiet-bao-tri-bao-duong.component';
import {ThemCongViecComponent} from './component/them-cong-viec/them-cong-viec.component';
import {ThietBiComponent} from './component/thiet-bi/thiet-bi.component';
import {ThemThietBiComponent} from './component/them-thiet-bi/them-thiet-bi.component';
import {PageListUserComponent} from './component/page-list-user/page-list-user.component';
import {ChiTietThietBiComponent} from './component/chi-tiet-thiet-bi/chi-tiet-thiet-bi.component';
import { from } from 'rxjs';

@NgModule({
    imports: [
        Group5RoutingModule,
        Group5ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
        CalendarModule,
        DropdownModule,
        ReactiveFormsModule,
    ],
    declarations: [
        PhongBanComponent,ChiTietPhongBanComponent,BaoTriBaoDuongThietBiComponent,ChiTietBaoTriBaoDuongComponent,ThemCongViecComponent,
        ThietBiComponent,ThemThietBiComponent,PageListUserComponent,ChiTietThietBiComponent
    ],
    providers: [
        DemoModelServiceProxy
    ]
})
export class Group5Module { }
