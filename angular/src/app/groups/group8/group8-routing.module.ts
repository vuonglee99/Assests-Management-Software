import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
// import { XeListComponent } from './xe/xe-list.component';
// import { XeDetailComponent } from './xe/xe-detail.component';
import { StaffListComponent } from './staff/staff-list.component'
import { StaffDetailComponent } from './staff/staff-detail.component'
import { ThietBiDetailComponent } from './thietbivattu/thietbivattu-detail.component'


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'chitietnhanvien', component: StaffListComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien' } },
                    { path: 'chitietnhanvien-add', component: StaffDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.Insert', editPageState: "add", title: 'Thêm mới nhân viên' } },
                    { path: 'chitietnhanvien-edit/:id', component: StaffDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.Update', editPageState: "edit", title: 'Chi tiết nhân viên' } },
                    { path: 'chitietnhanvien-view/:id', component: StaffDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.ById', editPageState: "view", title: 'Chi tiết nhân viên' } },
                    { path: 'thietbivattu-add', component: ThietBiDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.Insert', editPageState: "add", title: 'Thêm mới thiết bị' } },
                    { path: 'thietbivattu-edit/:id', component: ThietBiDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.Update', editPageState: "edit", title: 'Thông tin chi tiết' } },
                    { path: 'thietbivattu-view/:id', component: ThietBiDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.ById', editPageState: "view", title: 'Thông tin chi tiết' } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group8RoutingModule { }
