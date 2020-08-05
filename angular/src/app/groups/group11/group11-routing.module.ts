import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { XeListComponent } from './xe/xe-list.component';
import { XeDetailComponent } from './xe/xe-detail.component';
import { NSXListComponent } from './nsx/nsx-list.component';
import { ThueXeDetailComponent } from './thuexe/thuexe-detail.component';
import { BaoDuongListComponent } from './baoduong/baoduong-list.component';
import { BaoDuongDetailComponent } from './baoduong/baoduong-detail.component';
import { BaoDuongApproveComponent } from './baoduong/baoduong-approve.component';
import { CtbdDetailComponent } from '../group10/ctbd-detail/ctbd-detail.component';
import { CtbdApproveComponent } from '../group10/ctbd-approve/ctbd-approve.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'xe-group11', component: XeListComponent, data: { permission: 'Pages.Group11.Xe' } },
                    { path: 'xe-group11-add', component: XeDetailComponent, data: { permission: 'Pages.Group11.Xe.Add', editPageState: "add" } },
                    { path: 'xe-group11-detail/:id', component: XeDetailComponent, data: { permission: 'Pages.Group11.Xe', editPageState: "view" } },
                    { path: 'thue-xe-add', component: ThueXeDetailComponent, data: { permission: 'Pages.Group11.PTX.Insert', editPageState: "add" } },
                    { path: 'thue-xe-detail/:id', component: ThueXeDetailComponent, data: { permission: 'Pages.Group11.PTX.Update', editPageState: "view" } },
                    { path: 'bao-duong-list', component: BaoDuongListComponent, data: { permission: 'Pages.Group11.BD' } },
                    { path: 'bao-duong-approve', component: BaoDuongApproveComponent, data: { permission: 'Pages.Group11.BD.Approve', editPageState: "approve" } },
                    { path: 'bao-duong-add/:xeId', component: BaoDuongDetailComponent, data: { permission: 'Pages.Group11.BD.Insert', editPageState: "add" } },
                    { path: 'bao-duong-add', component: BaoDuongDetailComponent, data: { permission: 'Pages.Group11.BD.Insert', editPageState: "add" } },
                    { path: 'bao-duong-view/:id', component: BaoDuongDetailComponent, data: { permission: 'Pages.Group11.BD', editPageState: "view" } },
                    { path: 'bao-duong-detail/:id', component: BaoDuongDetailComponent, data: { permission: 'Pages.Group11.BD.Update', editPageState: "update" } },
                    { path: "ctbd-detail/:id", component: CtbdDetailComponent, data: { permission: "Pages.Group10.CTBD.Detail", editPageState: "view" } },
                    { path: "ctbd-edit/:id", component: CtbdDetailComponent, data: { permission: "Pages.Group10.CTBD.Update", editPageState: "edit" } },
                    { path: "ctbd-add/:id", component: CtbdDetailComponent, data: { permission: "Pages.Group10.CTBD.Add", editPageState: "add" } },
                    { path: "ctbd-approve/:id", component: CtbdApproveComponent, data: { permission: "Pages.Group10.CTBD.Approve", editPageState: "approve" } },
                    { path: 'nsx', component: NSXListComponent, data: { permission: 'Pages.Group11.NSX' } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group11RoutingModule { }
