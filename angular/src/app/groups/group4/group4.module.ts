import { NgModule } from '@angular/core';
import { BranchServiceProxy, ApartmentServiceProxy, ApartmentTypeServiceProxy, BuildingsServiceProxy, FloorServiceProxy, ApartmentResidentServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group4ServiceProxyModule } from './group4-service-proxy.module';
import { Group4RoutingModule } from './group4-routing.module';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { BranchDetailComponent } from './branch/branch-detail.component';
import { ApartmentListComponent } from './apartment/apartment-list.component';
import { ApartmentTypeListComponent } from './apartment-type/apartment-type-list';
import { PaginatorModule, DragDropModule } from 'primeng/primeng';
import { ApartmentDetailComponent } from './apartment/apartment-detail.component';
import { CheckboxModule } from 'primeng/checkbox';
import { ApartmentTypeDetailComponent } from './apartment-type/apartment-type-detail';
import {DropdownModule} from 'primeng/dropdown';
import { g4TenantComponent } from './apartment/g4-tenant.component';
import { ModalModule } from 'ngx-bootstrap';
import { toolbarList } from './g4-component/toolbar-info-page-list';
import { toolbarDetail } from './g4-component/toolbar-info-page-detail';
import {InputMaskModule} from 'primeng/inputmask';
import { approveDetail } from './g4-component/approve-detail';
import { CompareApComponent } from './g4-component/compare-ap';


// import { BranchListComponent } from './branch/branch-list.component';

@NgModule({
    imports: [
        Group4RoutingModule,
        Group4ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
        PaginatorModule,
        CheckboxModule,
        DragDropModule,
        ModalModule.forRoot(),
        InputMaskModule
       
    ],
    declarations: [
        BranchDetailComponent,
        ApartmentTypeListComponent,
        ApartmentTypeDetailComponent,
        ApartmentListComponent,
        ApartmentDetailComponent,
        g4TenantComponent,
        toolbarList,
        toolbarDetail,
        approveDetail,
        CompareApComponent
        
    ],
    providers: [
        BranchServiceProxy, ApartmentTypeServiceProxy,ApartmentServiceProxy,BuildingsServiceProxy,FloorServiceProxy,ApartmentResidentServiceProxy
    ]
})
export class Group4Module { }
