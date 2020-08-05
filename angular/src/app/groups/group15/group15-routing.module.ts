import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

//sprint 1
import { DVT_DetailComponent } from './donViTinh/DVT_Detail.component'
import { DVT_Add_DetailComponent } from './donViTinh/DVT_Add_Detail.component'
import { DVT_Update_DetailComponent } from './donViTinh/DVT_Update_Detail.component'

//sprint 2
import { LoaiTang_DetailComponent } from './loaiTang/LoaiTang_Detail.component'
import { LoaiTang_ListComponent } from './loaiTang/LoaiTang_List.component'
import { LoaiTang_Add_DetailComponent } from './loaiTang/LoaiTang_Add_Detail.component'
import { LoaiTang_Update_DetailComponent } from './loaiTang/LoaiTang_Update_Detail.component'

//sprint 3
import { Tang_ListComponent } from './tang/Tang_List.component'
import { Tang_Update_DetailComponent } from './tang/Tang_Update_Detail.component';
import { Tang_Add_DetailComponent } from './tang/Tang_Add_Detail.component';
import { Tang_DetailComponent } from './tang/Tang_Detail.component';
import { Thong_KeComponent } from './thongke/Thong_Ke.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    //sprint 1
                    { path: 'donvitinh-detail/:id', component: DVT_DetailComponent, data: { permission: null, editPageState: "detail" } },
                    { path: 'donvitinh-add-detail', component: DVT_Add_DetailComponent, data: { permission: 'Pages.Group15.DONVITINH.Add', editPageState: "add" } },
                    { path: 'donvitinh-update-detail/:id', component: DVT_Update_DetailComponent, data: { permission: 'Pages.Group15.DONVITINH.Update', editPageState: "update" } },

                    //sprint 2
                    { path: 'loaitang', component: LoaiTang_ListComponent, data: { permission: 'Pages.Group15.FLOORTYPE', editPageState: "getall" } },
                    { path: 'loaitang-detail/:id', component: LoaiTang_DetailComponent, data: { permission: null, editPageState: "detail" } },
                    { path: 'loaitang-add-detail', component: LoaiTang_Add_DetailComponent, data: { permission: 'Pages.Group15.FLOORTYPE.Create', editPageState: "add" } },
                    { path: 'loaitang-update-detail/:id', component: LoaiTang_Update_DetailComponent, data: { permission: 'Pages.Group15.FLOORTYPE.Update', editPageState: "update" } },

                    //sprint 3
                    { path: 'buildings/:buildingID/tang', component: Tang_ListComponent, data: { permission: null, editPageState: "getall" } },
                    { path: 'buildings/:buildingID/tang-update-detail/:id', component: Tang_Update_DetailComponent, data: { permission: null, editPageState: "getall" } },
                    { path: 'buildings/:buildingID/tang-add-detail', component: Tang_Add_DetailComponent, data: { permission: null, editPageState: "getall" } },
                    { path: 'buildings/:buildingID/tang-detail/:id', component: Tang_DetailComponent, data: { permission: null, editPageState: "getall" } },
            
                    { path: 'thongketoanha', component: Thong_KeComponent, data: { permission: null, editPageState: "getall" } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group15RoutingModule { }
