import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { XeListComponent } from './xe/xe-list.component';
import { XeDetailComponent } from './xe/xe-detail.component';
import {LoaiXeComponent} from '@app/groups/group7/loai_xe/loai-xe.component';
import { NhaCungUngDetailComponent } from './nha-cung-ung/nha-cung-ung-detail.component';
import {SuaChuaTbvtytDetailComponent} from '@app/groups/group7/sua-chua-tbvtyt/sua-chua-tbvtyt-detail.component';
import { SuaChuaTbvtytListComponent } from './sua-chua-tbvtyt/sua-chua-tbvtyt-list.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'xe', component: XeListComponent, data: { permission: 'Pages.Group7.Xe' } },
                    { path: 'xe-add', component: XeDetailComponent, data: { permission: 'Pages.Group7.Xe.Add', editPageState: "add" } },
                    { path: 'xe-edit', component: XeDetailComponent, data: { permission: 'Pages.Group7.Xe.Edit', editPageState: "edit" } },
                    { path: 'xe-view', component: XeDetailComponent, data: { permission: 'Pages.Group7.Xe.View', editPageState: "view" } },

                    { path: 'loai-xe', component: LoaiXeComponent, data: {permission: 'Pages.Group7.LoaiXe', editPageState: 'view' } },

                    { path: 'nha-cung-ung-add', component: NhaCungUngDetailComponent, data: { permission: 'Pages.Group7.NhaCungUng.Add', editPageState: 'add' } },
                    { path: 'nha-cung-ung-edit/:id', component: NhaCungUngDetailComponent, data: { permission: 'Pages.Group7.NhaCungUng.Update' /* .Update hay .Edit */, editPageState: 'edit' } },
                    { path: 'nha-cung-ung-view/:id', component: NhaCungUngDetailComponent, data: { permission: 'Pages.Group7.NhaCungUng.View', editPageState: 'view' } },

                    { path: 'yeu-cau-sua-chua-tbvtyt', component: SuaChuaTbvtytListComponent, data: { permission: 'Pages.Group7.YCSC' } },
                    { path: 'yeu-cau-sua-chua-tbvtyt-add', component: SuaChuaTbvtytDetailComponent, data: { permission: 'Pages.Group7.YCSC.Add', editPageState: 'add' } },
                    { path: 'yeu-cau-sua-chua-tbvtyt-edit/:id', component: SuaChuaTbvtytDetailComponent, data: { permission: 'Pages.Group7.YCSC.Update' /* .Update hay .Edit */, editPageState: 'edit' } },
                    { path: 'yeu-cau-sua-chua-tbvtyt-view/:id', component: SuaChuaTbvtytDetailComponent, data: { permission: 'Pages.Group7.YCSC.View', editPageState: 'view' } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group7RoutingModule { }
