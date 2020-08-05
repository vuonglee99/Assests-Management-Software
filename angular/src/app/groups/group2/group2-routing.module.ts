import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoaiXeDetailComponent } from './components/loai-xe-detail/loai-xe-detail.component';
import { LoaiXeAddComponent } from './components/loai-xe-add/loai-xe-add.component';
import  {KiemkeMobileComponent} from './components/kiemke-mobile/kiemke-mobile.component';
import  {TaoQrcodeComponent} from './components/tao-qrcode/tao-qrcode.component';
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    // { path: 'xe', component: XeListComponent, data: { permission: 'Pages.Group2.Xe' } },
                    // { path: 'xe-add', component: XeDetailComponent, data: { permission: 'Pages.Group2.Xe.Add', editPageState: "add" } },
                    // { path: 'xe-edit', component: XeDetailComponent, data: { permission: 'Pages.Group2.Xe.Edit', editPageState: "edit" } },
                    // { path: 'xe-view', component: XeDetailComponent, data: { permission: 'Pages.Group2.Xe.View', editPageState: "view" } },

                    { path: 'loai-xe-view/:id', component: LoaiXeDetailComponent, data: { permission: 'Pages.Group2.LoaiXe.View',editPageState: "view"  } },
                    { path: 'loai-xe-add', component: LoaiXeAddComponent, data: { permission: 'Pages.Group2.LoaiXe.Add',editPageState: "add"  } },
                    {path: 'kiemke-mobile/:id',component:KiemkeMobileComponent,data:{permission:'Pages.Group2.KiemKe.View',editPageState:"view"}},
                    {path: 'tao-qrcode',component:TaoQrcodeComponent,data:{permission:'Pages.Group2.KiemKe.TaoQR',editPageState:"view"}}
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group2RoutingModule { }
