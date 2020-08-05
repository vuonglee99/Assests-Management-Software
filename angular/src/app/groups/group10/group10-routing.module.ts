import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { NsxDetailComponent } from "./nsx-detail/nsx-detail.component";
import { NsxListComponent } from "./nsx-list/nsx-list.component";
import { NtxListComponent } from "./ntx-list/ntx-list.component";
import { NtxDetailComponent } from "./ntx-detail/ntx-detail.component";
import { PtxListComponent } from "./ptx-list/ptx-list.component";

import { CtbdDetailComponent } from "./ctbd-detail/ctbd-detail.component";
import { CtbdApproveComponent } from "./ctbd-approve/ctbd-approve.component";

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: "",
                children: [
                    {
                        path: "nsx-list",
                        component: NsxListComponent,
                        data: { permission: "Pages.Group10.NSX" },
                    },
                    {
                        path: "nsx-detail/:id",
                        component: NsxDetailComponent,
                        data: {
                            permission: "Pages.Group10.NSX.Detail",
                            editPageState: "view",
                        },
                    },
                    {
                        path: "nsx-add",
                        component: NsxDetailComponent,
                        data: {
                            permission: "Pages.Group10.NSX.Add",
                            editPageState: "add",
                        },
                    },
                    {
                        path: "nsx-edit/:id",
                        component: NsxDetailComponent,
                        data: {
                            permission: "Pages.Group10.NSX.Update",
                            editPageState: "edit",
                        },
                    },
                    //{ path: 'xe-view', component: XeDetailComponent, data: { permission: 'Pages.Group10.Xe.View', editPageState: "view" } },
                    {
                        path: "ntx-list",
                        component: NtxListComponent,
                        data: { permission: "Pages.Group10.NTX" },
                    },
                    {
                        path: "ntx-detail/:id",
                        component: NtxDetailComponent,
                        data: {
                            permission: "Pages.Group10.NTX",
                            editPageState: "view",
                        },
                    },
                    {
                        path: "ntx-add",
                        component: NtxDetailComponent,
                        data: {
                            permission: "Pages.Group10.NTX.Add",
                            editPageState: "add",
                        },
                    },
                    {
                        path: "ntx-edit/:id",
                        component: NtxDetailComponent,
                        data: {
                            permission: "Pages.Group10.NTX.Update",
                            editPageState: "edit",
                        },
                    },
                    {
                        path: "ptx-list",
                        component: PtxListComponent,
                        data: { permission: "Pages.Group10.PTX" },
                    },
                    {
                        path: "ctbd-detail/:id",
                        component: CtbdDetailComponent,
                        data: {
                            permission: "Pages.Group10.CTBD.Detail",
                            editPageState: "view",
                        },
                    },
                    {
                        path: "ctbd-edit/:id",
                        component: CtbdDetailComponent,
                        data: {
                            permission: "Pages.Group10.CTBD.Update",
                            editPageState: "edit",
                        },
                    },
                    {
                        path: "ctbd-add/:id",
                        component: CtbdDetailComponent,
                        data: {
                            permission: "Pages.Group10.CTBD.Add",
                            editPageState: "add",
                        },
                    },
                    {
                        path: "ctbd-approve/:id",
                        component: CtbdApproveComponent,
                        data: {
                            permission: "Pages.Group10.CTBD.Approve",
                            editPageState: "approve",
                        },
                    },
                    {
                        path: "ctbd-edit-approve/:id",
                        component: CtbdDetailComponent,
                        data: {
                            permission: "Pages.Group10.CTBD.Approve",
                            editPageState: "approve",
                        },
                    },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class Group10RoutingModule {}
