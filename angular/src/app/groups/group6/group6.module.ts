import {NgModule} from '@angular/core';

import {
    DemoModelServiceProxy,
    DoUuTienServiceProxy,
    TrangThaiYeuCauSuaChuaServiceProxy,
    DashboardYCSCServiceProxy,
} from '@shared/service-proxies/service-proxies';
import {Group6ServiceProxyModule} from './Helpers/group6-service-proxy.module';
import {Group6RoutingModule} from './Helpers/group6-routing.module';
import {TableModule} from 'primeng/table';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {ButtonModule} from 'primeng/button';
import {PaginatorModule} from 'primeng/paginator';
import {CardModule} from 'primeng/card';
import {InputTextModule} from 'primeng/inputtext';
import {DUT} from './dut/dut.component';
import {DUTDetailComponent} from './dut/dut-detail.component';
import {InputTextareaModule} from '@node_modules/primeng/components/inputtextarea/inputtextarea';
import {TTYCSC} from '@app/groups/group6/TTYCSC/TTYCSC.component';
import {TTYCSCDetailComponent} from '@app/groups/group6/TTYCSC/TTYCSC-detail.component';
import {DVT} from '@app/groups/group6/dvt/dvt.component';
import {DVT_DetailComponent} from '@app/groups/group6/dvt/dvt-detail.component';
import {GR6_Dashboard} from '@app/groups/group6/TTYCSC/dashboard.component';
import { SearchBarComponent } from './common/search-bar/search-bar.component';
import { AntTagComponent } from './common/ant-tag/ant-tag.component';
import { ExportDialogComponent } from './common/export-dialog/export-dialog.component';

@NgModule({
    imports: [
        Group6RoutingModule,
        Group6ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
        ButtonModule,
        PaginatorModule,
        CardModule,
        InputTextModule,
        InputTextareaModule,
    ],
    declarations: [
        DVT,
        DVT_DetailComponent,
        DUT,
        DUTDetailComponent,
        TTYCSC,
        TTYCSCDetailComponent,
        GR6_Dashboard,
        SearchBarComponent,
        AntTagComponent,
        ExportDialogComponent
    ],
    providers: [DemoModelServiceProxy, DoUuTienServiceProxy, TrangThaiYeuCauSuaChuaServiceProxy, DashboardYCSCServiceProxy],
})
export class Group6Module {
}
