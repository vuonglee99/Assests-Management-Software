import { NgModule } from '@angular/core';

import { MenuClientServiceProxy, DemoModelServiceProxy,KiemKeServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group12ServiceProxyModule } from './group12-service-proxy.module';
import { Group12RoutingModule } from './group12-routing.module';
import { ModelListComponent } from './Model/model-list.component';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ModelDetailComponent } from './Model/model-detail.component';
import { ModelAddComponent } from './Model/model-add.component';
import { MessageModule } from 'primeng/message';
import {KiemKeListComponent} from './kiemke/kiemke-list.component';
import {DropdownModule} from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import {KiemKeAddComponent} from './kiemke/kiemke-add/kiemke-add.component';
import {  KiemKeDetailComponent} from './kiemke/kiemke-detail/kiemke-detail.component';
import {TooltipModule} from 'primeng/tooltip';

@NgModule({
    imports: [
        Group12RoutingModule,
        Group12ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
        ReactiveFormsModule,
        MessageModule,
        CalendarModule,
        DropdownModule,
        CardModule,
        TooltipModule
        
    ],
    declarations: [
        ModelListComponent, 
        ModelDetailComponent,
        ModelAddComponent,
        KiemKeListComponent,
        KiemKeAddComponent,
        KiemKeDetailComponent,

    ],
    providers: [
        DemoModelServiceProxy,
        KiemKeServiceProxy
    ]
})
export class Group12Module { }
