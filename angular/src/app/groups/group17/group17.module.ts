import { NgModule } from '@angular/core';

import { MenuClientServiceProxy, DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group17ServiceProxyModule } from './group17-service-proxy.module';
import { Group17RoutingModule } from './group17-routing.module';
import { XeListComponent } from './xe/xe-list.component';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { XeDetailComponent } from './xe/xe-detail.component';

@NgModule({
    imports: [
        Group17RoutingModule,
        Group17ServiceProxyModule,
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
export class Group17Module { }
