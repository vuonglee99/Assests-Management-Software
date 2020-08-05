import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MenuClientServiceProxy, DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { TableModule } from 'primeng/table';

import { ModalModule, TabsModule, TooltipModule, PopoverModule } from 'ngx-bootstrap';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';

import {InputTextareaModule} from 'primeng/inputtextarea';


import { Group14RoutingModule } from './group14-routing.module';
import { Group14ServiceProxyModule } from './group14-service-proxy.module';
import { NhanVienListComponent } from './nhanvien/nhanvien-list.component';
import { PaginatorModule, FileUploadModule as PrimeNgFileUploadModule, AutoCompleteModule, EditorModule, InputMaskModule, CalendarModule } from 'primeng/primeng';
import { FileUploadModule } from 'ng2-file-upload';
import { NhaCungUngListComponent } from './NhaCungUng/nhacungung-list.component';
import { ThietBiVatTuListComponent } from './ThietBiVatTu/thietbivattu-list.component';

import { ThemPhieuCapPhatComponent } from './PhieuCapPhatTBVT/ThemPCP/PhieuCapPhatTBVT-component-add';
import { SuaPhieuCapPhatComponent } from './PhieuCapPhatTBVT/SuaPCP/PhieuCapPhatTBVT-component-edit';

//import { NhaCungUngListComponent } from './NhaCungUng/nhacungung-list.component';
import { PhieuCapPhatTBVTComponent } from './PhieuCapPhatTBVT/PhieuCapPhatTBVT-component-view';
//import { NhaCungUngListComponent } from './NhaCungUng/nhacungung-list.component'
import { ChiTietPhieuCapPhatTBVTComponent } from './ChiTietPhieuCapPhatTBVT/chi-tiet-phieu-cap-phat-TBVT-component';;
@NgModule({
    imports: [
        Group14RoutingModule,
        Group14ServiceProxyModule,
        FormsModule,
        CommonModule,
        FileUploadModule,
        ModalModule.forRoot(),
        TabsModule.forRoot(),
        TooltipModule.forRoot(),
        PopoverModule.forRoot(),
        UtilsModule,
        AppCommonModule,
        TableModule,
        PaginatorModule,
        PrimeNgFileUploadModule,
        AutoCompleteModule,
        EditorModule,
        InputMaskModule,
        CalendarModule,
        InputTextareaModule
    ],
    declarations: [
        NhanVienListComponent,
        NhaCungUngListComponent,
        ThietBiVatTuListComponent,
        PhieuCapPhatTBVTComponent,
        ThemPhieuCapPhatComponent,
        SuaPhieuCapPhatComponent,
        ChiTietPhieuCapPhatTBVTComponent
    ],
    providers: [
        DemoModelServiceProxy
    ]
})
export class Group14Module { }
