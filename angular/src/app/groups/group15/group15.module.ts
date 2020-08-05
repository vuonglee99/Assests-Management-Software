import { NgModule } from '@angular/core';

import { MenuClientServiceProxy, DemoModelServiceProxy, BuildingsServiceProxy, ApartmentServiceProxy, FloorServiceProxy, Apartment_ServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group15ServiceProxyModule } from './group15-service-proxy.module';
import { Group15RoutingModule } from './group15-routing.module';

import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';


//sprint 1
import { DVT_DetailComponent } from './donViTinh/DVT_Detail.component'
// import { DVT_ListComponent } from './donViTinh/DVT_List.component'
import { DVT_Add_DetailComponent } from './donViTinh/DVT_Add_Detail.component'
import { DVT_Update_DetailComponent } from './donViTinh/DVT_Update_Detail.component'

//sprint 2
import { LoaiTang_DetailComponent } from './loaiTang/LoaiTang_Detail.component'
import { LoaiTang_ListComponent } from './loaiTang/LoaiTang_List.component'
import { LoaiTang_Add_DetailComponent } from './loaiTang/LoaiTang_Add_Detail.component'
import { LoaiTang_Update_DetailComponent } from './loaiTang/LoaiTang_Update_Detail.component'

//sprint 3
import {Tang_ListComponent} from './tang/Tang_List.component'
import {Tang_Add_DetailComponent} from './tang/Tang_Add_Detail.component'
//modal service (unused)
import { Group15ModalComponent } from './modal/modal.component'
import { Group15ModalService } from './modal/modal.service'

//primeng
import { UtilsModule } from '@shared/utils/utils.module';
import { PaginatorModule } from '@node_modules/primeng/components/paginator/paginator';
import { Tang_DetailComponent } from './tang/Tang_Detail.component';
import { Tang_Update_DetailComponent } from './tang/Tang_Update_Detail.component';
import { Thong_KeComponent } from './thongke/Thong_Ke.component';
@NgModule({
    imports: [
        Group15RoutingModule,
        Group15ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
        PaginatorModule, UtilsModule
    ],
    declarations: [
        //sprint 1
        DVT_DetailComponent,
        DVT_Add_DetailComponent,
        DVT_Update_DetailComponent,

        //sprint 2
        LoaiTang_DetailComponent,
        LoaiTang_Add_DetailComponent,
        LoaiTang_Update_DetailComponent,
        LoaiTang_ListComponent,

        //sprint 3
        Tang_ListComponent,
        Tang_Add_DetailComponent,
        Tang_DetailComponent,
        Tang_Update_DetailComponent,
        Thong_KeComponent,
        //modal service
        Group15ModalComponent,


    ],
    providers: [
        DemoModelServiceProxy, Group15ModalService,
        BuildingsServiceProxy, ApartmentServiceProxy,
        FloorServiceProxy, Apartment_ServiceProxy
    ]
})
export class Group15Module { }
