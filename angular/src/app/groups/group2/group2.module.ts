import { NgModule } from '@angular/core';

import { MenuClientServiceProxy, DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group2ServiceProxyModule } from './group2-service-proxy.module';
import { Group2RoutingModule } from './group2-routing.module';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { LoaiXeAddComponent } from './components/loai-xe-add/loai-xe-add.component';
import { LoaiXeDetailComponent } from './components/loai-xe-detail/loai-xe-detail.component';
import  {KiemkeMobileComponent} from './components/kiemke-mobile/kiemke-mobile.component';
import  {TaoQrcodeComponent} from './components/tao-qrcode/tao-qrcode.component';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule } from '@angular/material/icon';
//import { XeDetailComponent } from './components/xe/xe-detail.component';
//import {NgxQRCodeModule} from  'ngx-qrcode2';


@NgModule({
    imports: [
        Group2RoutingModule,
        Group2ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
        MatButtonModule,
        MatIconModule
        //NgxQRCodeModule,
    ],
    declarations: [
        LoaiXeAddComponent,
        LoaiXeDetailComponent,
        KiemkeMobileComponent,
        TaoQrcodeComponent,

    ],
    providers: [
        DemoModelServiceProxy
    ]
})
export class Group2Module {


 }
