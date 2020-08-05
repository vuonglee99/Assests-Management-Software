import { NgModule } from '@angular/core';

import { MenuClientServiceProxy, DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group11ServiceProxyModule } from './group11-service-proxy.module';
import { Group11RoutingModule } from './group11-routing.module';
import { XeListComponent } from './xe/xe-list.component';
import { TableModule } from 'primeng/table';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { XeDetailComponent } from './xe/xe-detail.component';
import { NSXListComponent } from './nsx/nsx-list.component';
import { ThueXeDetailComponent } from './thuexe/thuexe-detail.component';
import { BaoDuongListComponent } from './baoduong/baoduong-list.component';
import { BaoDuongDetailComponent } from './baoduong/baoduong-detail.component';
import { BaoDuongApproveComponent } from './baoduong/baoduong-approve.component';

@NgModule({
    imports: [
        Group11RoutingModule,
        Group11ServiceProxyModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TableModule
    ],
    declarations: [
        XeListComponent, XeDetailComponent, NSXListComponent, ThueXeDetailComponent, BaoDuongListComponent, BaoDuongDetailComponent, BaoDuongApproveComponent
    ],
    providers: [
        DemoModelServiceProxy
    ]
})
export class Group11Module { }
