import { NgModule } from "@angular/core";

import {
    MenuClientServiceProxy,
    DemoModelServiceProxy,
    XeServiceProxy,
    NSXServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { Group10ServiceProxyModule } from "./group10-service-proxy.module";
import { Group10RoutingModule } from "./group10-routing.module";
import { XeListComponent } from "./xe/xe-list.component";
import { TableModule } from "primeng/table";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { CommonModule } from "@angular/common";
import { XeDetailComponent } from "./xe/xe-detail.component";
import { NsxListComponent } from "./nsx/nsx-list.component";
import { NsxEditComponent } from "./nsx/nsx-edit.component";
import { NsxAddComponent } from "./nsx/nsx-add.component";
import { NsxDeleteComponent } from "./nsx/nsx-delete.component";
import { NsxDetailComponent } from "./nsx/nsx-detail.component";

@NgModule({
    imports: [
        Group10RoutingModule,
        Group10ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
    ],
    declarations: [
        XeListComponent,
        XeDetailComponent,
        NsxListComponent,
        NsxEditComponent,
        NsxAddComponent,
        NsxDeleteComponent,
        NsxDetailComponent,
    ],
    providers: [DemoModelServiceProxy, XeServiceProxy, NSXServiceProxy],
})
export class Group10Module {}
