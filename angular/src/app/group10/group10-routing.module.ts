import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { XeListComponent } from "./xe/xe-list.component";
import { XeDetailComponent } from "./xe/xe-detail.component";
// import { NsxComponent } from "@app/testcpn/nsx/nsx.component";
import { NsxListComponent } from "./nsx/nsx-list.component";
import { NsxEditComponent } from "./nsx/nsx-edit.component";
import { NsxAddComponent } from "./nsx/nsx-add.component";
import { NsxDeleteComponent } from "./nsx/nsx-delete.component";
import { NsxDetailComponent } from "./nsx/nsx-detail.component";

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: "",
                children: [
                    {
                        path: "xe",
                        component: XeListComponent,
                        data: { permission: "Pages.Group10.Xe" },
                    },
                    {
                        path: "xe-add",
                        component: XeDetailComponent,
                        data: {
                            permission: "Pages.Group10.Xe.Add",
                            editPageState: "add",
                        },
                    },
                    {
                        path: "xe-edit",
                        component: XeDetailComponent,
                        data: {
                            permission: "Pages.Group10.Xe.Edit",
                            editPageState: "edit",
                        },
                    },
                    {
                        path: "xe-view",
                        component: XeDetailComponent,
                        data: {
                            permission: "Pages.Group10.Xe.View",
                            editPageState: "view",
                        },
                    },
                    {
                        path: "nha-san-xuat",
                        component: NsxListComponent,
                        data: {
                            editPageState: "view",
                        },
                    },
                    {
                        path: "nsx-edit",
                        component: NsxEditComponent,
                        data: {
                            permission: "Pages.Group10.NSX.Update",
                            editPageState: "edit",
                        },
                    },
                    {
                        path: "nsx-add",
                        component: NsxAddComponent,
                        data: {
                            permission: "Pages.Group10.NSX.Add",
                            editPageState: "add",
                        },
                    },
                    {
                        path: "nsx-delete",
                        component: NsxDeleteComponent,
                        data: {
                            permission: "Pages.Group10.NSX.Delete",
                            editPageState: "delete",
                        },
                    },
                    {
                        path: "nsx-detail",
                        component: NsxDetailComponent,
                        data: {
                            permission: "Pages.Group10.NSX.Detail",
                            editPageState: "detail",
                        },
                    },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class Group10RoutingModule {}
