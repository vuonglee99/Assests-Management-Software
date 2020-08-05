import { NgModule } from '@angular/core';

import { MenuClientServiceProxy, DemoModelServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group8ServiceProxyModule } from './group8-service-proxy.module';
import { Group8RoutingModule } from './group8-routing.module';
// import { XeListComponent } from './xe/xe-list.component';
import { TableModule } from 'primeng/components/table/table';
import { AutoCompleteModule, EditorModule, FileUploadModule as PrimeNgFileUploadModule, InputMaskModule, PaginatorModule } from 'primeng/primeng';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { CalendarModule } from 'primeng/calendar';
import { PanelModule } from 'primeng/panel';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';

import { StaffDetailComponent } from './staff/staff-detail.component'
import { StaffListComponent } from './staff/staff-list.component'
import { ThietBiDetailComponent } from './thietbivattu/thietbivattu-detail.component'
import { Group8InputComponent } from './components/input/input.component';
import { Group8SelectComponent } from './components/select/select.component';
import { Group8DatePickerComponent } from './components/date-picker/date-picker.component';
// import { XeDetailComponent } from './xe/xe-detail.component';

@NgModule({
    imports: [
        Group8RoutingModule,
        Group8ServiceProxyModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TableModule,
        PaginatorModule,
        CalendarModule,
        ConfirmDialogModule,
        PanelModule,
        InputTextModule,
        DropdownModule,
        CheckboxModule,
        InputTextareaModule,
        ButtonModule,
        DialogModule,
    ],
    declarations: [
        StaffListComponent,
        StaffDetailComponent,
        ThietBiDetailComponent,
        Group8InputComponent,
        Group8SelectComponent,
        Group8DatePickerComponent,
    ],
    providers: [
        DemoModelServiceProxy,
        ConfirmationService
    ]
})
export class Group8Module { }
