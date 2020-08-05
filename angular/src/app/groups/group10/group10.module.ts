import { NgModule } from "@angular/core";

import {
    MenuClientServiceProxy,
    DemoModelServiceProxy,
    NTXServiceProxy,
    NSXServiceProxy,
    PTXServiceProxy,
    CTBDServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { Group10ServiceProxyModule } from "./group10-service-proxy.module";
import { Group10RoutingModule } from "./group10-routing.module";
import { NsxDetailComponent } from "./nsx-detail/nsx-detail.component";
import { NsxListComponent } from "./nsx-list/nsx-list.component";
import { TableModule } from "primeng/table";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { CommonModule } from "@angular/common";
import { ModalModule } from "ngx-bootstrap/modal";
import { NtxListComponent } from "./ntx-list/ntx-list.component";
import { NtxDetailComponent } from "./ntx-detail/ntx-detail.component";
import { PtxListComponent } from "./ptx-list/ptx-list.component";

import { CtbdDetailComponent } from "./ctbd-detail/ctbd-detail.component";
import { CtbdApproveComponent } from "./ctbd-approve/ctbd-approve.component";

@NgModule({
    imports: [
        Group10RoutingModule,
        Group10ServiceProxyModule,
        CommonModule,
        ReactiveFormsModule,
        FormsModule,
        TableModule,
        ModalModule.forRoot(),
    ],
    declarations: [
        NsxDetailComponent,
        NsxListComponent,
        NtxListComponent,
        NtxDetailComponent,
        PtxListComponent,
        CtbdDetailComponent,
        CtbdApproveComponent,
    ],
    providers: [
        DemoModelServiceProxy,
        NTXServiceProxy,
        NSXServiceProxy,
        PTXServiceProxy,
        NTXServiceProxy,
        CTBDServiceProxy,
    ],
})
export class Group10Module {}
