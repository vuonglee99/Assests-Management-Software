import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { Group3RoutingModule } from "./group3-routing.module";
import { Group3ServiceProxyModule } from "./group3-service-proxy.module";
import { BranchesListComponent } from "./branches/branchesList.component";
import { BranchStatusComboComponent } from "./branches/branch-status-combo";
import { BranchServiceProxy } from "@shared/service-proxies/service-proxies";
import { PaginatorModule, InputMaskModule, FileUploadModule, AutoCompleteModule, EditorModule } from 'primeng/primeng';
import { BuildingsServiceProxy, FloorsServiceProxy, ApartmentsServiceProxy, EnumeratedTypesServiceProxy, ResidentsServiceProxy } from '@shared/service-proxies/service-proxies';
import { BuildingsListView } from './buidling-views/buildings-list.view';
import { DatatableToolbarComponent } from './components/datatable-toolbar.component';
import { ViewHeadingComponent } from './components/view-heading.component';
import { ModalModule, TooltipModule, PopoverModule, TabsModule } from 'ngx-bootstrap';
import { AdminRoutingModule } from '@app/admin/admin-routing.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { BuildingDetailView } from './buidling-views/building-detail.view';
import { DetailViewToolbarComponent } from './components/detailview-toolbar.component';
import { ApproveStatusComponent } from './components/approve-status.component';
import { FloorDetailView } from './floor-views/floor-detail.view';
import { SearchableSelectComponent } from './components/searchable-select.component';
import { CreateEnumModalComponent } from './components/create-enum-modal.component';
import { ApartmentDetailView } from './apartment-views/apartment-detail.view';
import { NumericInputComponent } from './components/numeric-input.component';
import { ResidentsListView } from './resident-views/residents-list.view';
import { ResidentDetailView } from './resident-views/resident-detail.view';
import { DatePickerComponent } from './components/date-picker.component';


@NgModule({
    imports: [
        Group3RoutingModule,
        Group3ServiceProxyModule,
        CommonModule,
        FormsModule,
        TableModule,
        PaginatorModule,
        UtilsModule,
        TableModule,
        ModalModule,
        PaginatorModule,
        InputMaskModule,
    ],
    declarations: [
        BranchesListComponent,
        BranchStatusComboComponent,
        //Components
        DatatableToolbarComponent,
        ViewHeadingComponent,
        DetailViewToolbarComponent,
        ApproveStatusComponent,
        SearchableSelectComponent,
        CreateEnumModalComponent,
        NumericInputComponent,
        DatePickerComponent,
        //Views
        BuildingsListView,
        BuildingDetailView,
        FloorDetailView,
        ApartmentDetailView,
        ResidentsListView,
        ResidentDetailView,
    ],
    providers: [
        BranchServiceProxy,
        BuildingsServiceProxy,
        FloorsServiceProxy,
        ApartmentsServiceProxy,
        EnumeratedTypesServiceProxy,
        ResidentsServiceProxy,
    ]
})
export class Group3Module { }