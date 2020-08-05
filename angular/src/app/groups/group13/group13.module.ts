import { NgModule } from '@angular/core';

import { MenuClientServiceProxy, DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group13ServiceProxyModule } from './group13-service-proxy.module';
import { Group13RoutingModule } from './group13-routing.module';
import { XeListComponent } from './xe/xe-list.component';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { XeDetailComponent } from './xe/xe-detail.component';

@NgModule({
    imports: [
        Group13RoutingModule,
        Group13ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule
    ],
    declarations: [
        XeListComponent, XeDetailComponent
    ],
    providers: [
        DemoModelServiceProxy
    ]
})
export class Group13Module { }
