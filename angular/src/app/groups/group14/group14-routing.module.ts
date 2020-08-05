import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NhanVienListComponent } from './nhanvien/nhanvien-list.component';
import { StaffDetailComponent } from '../group8/staff/staff-detail.component';
import { NhaCungUngListComponent } from './NhaCungUng/nhacungung-list.component';
import { ThietBiVatTuListComponent } from './ThietBiVatTu/thietbivattu-list.component';
import {PhieuCapPhatTBVTComponent} from './PhieuCapPhatTBVT/PhieuCapPhatTBVT-component-view';
import { ThemPhieuCapPhatComponent } from './PhieuCapPhatTBVT/ThemPCP/PhieuCapPhatTBVT-component-add';
import { SuaPhieuCapPhatComponent } from './PhieuCapPhatTBVT/SuaPCP/PhieuCapPhatTBVT-component-edit';
import { ChiTietPhieuCapPhatTBVTComponent } from './ChiTietPhieuCapPhatTBVT/chi-tiet-phieu-cap-phat-TBVT-component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    {
                        path: 'nhanvien',
                        component: NhanVienListComponent,
                        data: { permission: 'Pages.Group14.NhanVien' }
                    },
                    { path: 'chitietnhanvien-add', component: StaffDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.Insert', editPageState: "add", title: 'Thêm mới nhân viên' } },
                    { path: 'chitietnhanvien-edit/:id', component: StaffDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.Update', editPageState: "edit", title: 'Chi tiết nhân viên' } },
                    { path: 'chitietnhanvien-view/:id', component: StaffDetailComponent, data: { permission: 'Pages.Group8.ChiTietNhanVien.ById', editPageState: "view", title: 'Chi tiết nhân viên' } },
                    {
                        path: 'nhacungung',
                        component: NhaCungUngListComponent,
                        data: { permission: 'Pages.Group14.NhaCungUng' }
                    },
                    {
                        path: 'thietbivattu',
                        component: ThietBiVatTuListComponent,
                        data: { permission: 'Pages.Group14.ThietBiVatTu' }
                    },
                    {
                        path:'capphattbvt',
                        component: PhieuCapPhatTBVTComponent,
                        data:{permission:'Pages.Group14.PCPTBVT'}
                    },
                    {
                        path:'capphattbvt-add',
                        component: ThemPhieuCapPhatComponent,
                        data:{permission:'Pages.Group14.PCPTBVT.Insert'}
                    },
                    {
                        path:'capphattbvt-update/:id',
                        component: SuaPhieuCapPhatComponent,
                        data:{permission:'Pages.Group14.PCPTBVT.Update'}
                    },
                    { 
                        path: 'chitietcapphattbvt/:id' , 
                        component : ChiTietPhieuCapPhatTBVTComponent , 
                        data: { permission: "Pages.Group14.PCPTBVT.ById" , editPageState: "view" } 
                    }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group14RoutingModule { }
