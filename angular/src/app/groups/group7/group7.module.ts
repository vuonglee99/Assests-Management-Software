import { NgModule } from "@angular/core";

import {
    MenuClientServiceProxy,
    DemoModelServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { Group7ServiceProxyModule } from "./group7-service-proxy.module";
import { Group7RoutingModule } from "./group7-routing.module";
import { TableModule } from "primeng/table";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { CommonModule } from "@angular/common";
import { UtilsModule } from "@shared/utils/utils.module";
import { PaginatorModule } from "@node_modules/primeng/components/paginator/paginator";
import { XeListComponent } from "./xe/xe-list.component";
import { XeDetailComponent } from "./xe/xe-detail.component";
import { LoaiXeComponent } from "./loai_xe/loai-xe.component";
import { NhaCungUngDetailComponent } from "./nha-cung-ung/nha-cung-ung-detail.component";
import {SuaChuaTbvtytListComponent} from '@app/groups/group7/sua-chua-tbvtyt/sua-chua-tbvtyt-list.component';
import {SuaChuaTbvtytDetailComponent} from '@app/groups/group7/sua-chua-tbvtyt/sua-chua-tbvtyt-detail.component';

@NgModule({
    imports: [
        Group7RoutingModule,
        Group7ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
        UtilsModule,
        PaginatorModule,
    ],
    declarations: [
        XeListComponent,
        XeDetailComponent,
        LoaiXeComponent,
        NhaCungUngDetailComponent,
        SuaChuaTbvtytListComponent,
        SuaChuaTbvtytDetailComponent,
    ],
    providers: [DemoModelServiceProxy],
})
export class Group7Module {}
