import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ModelListComponent } from './Model/model-list.component';
import { ModelDetailComponent } from './Model/model-detail.component';
import { ModelAddComponent } from './Model/model-add.component';
import {KiemKeListComponent} from './kiemke/kiemke-list.component';
import {KiemKeAddComponent} from './kiemke/kiemke-add/kiemke-add.component';
import {KiemKeDetailComponent} from './kiemke/kiemke-detail/kiemke-detail.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'model', component: ModelListComponent, data: { permission: 'Pages.Group12.Model' } },
                    { path: 'model-add', component: ModelAddComponent, data: { permission: 'Pages.Group12.Model.Add', editPageState: "add" } },
                    { path: 'model-view/:modeL_ID', component: ModelDetailComponent, data: { permission: 'Pages.Group12.Model.View', editPageState: "update" } },
                ]
            },
            {
                path:'kiemke',
                children:[
                    { path: '', component: KiemKeListComponent, data: { permission: 'Pages.Group12.KiemKe' } },
                    { path: 'kiemke-add', component: KiemKeAddComponent, data: { permission: 'Pages.Group12.KiemKe.Create',editPageState: "create" } },
                    { path: 'kiemke-view/:kK_ID', component: KiemKeDetailComponent, data: { permission: 'Pages.Group12.KiemKe.View', editPageState: "update" } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group12RoutingModule { }
