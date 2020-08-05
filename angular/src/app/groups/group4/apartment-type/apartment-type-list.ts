import { ViewEncapsulation, Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { BranchServiceProxy, CM_BRANCH_DTO, ApartmentTypeServiceProxy, ApartmentTypeDTO, SessionServiceProxy } from "@shared/service-proxies/service-proxies";
import { Response } from "@angular/http";
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';



@Component({
    templateUrl: './apartment-type-list.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../group4-style.css']
})
export class ApartmentTypeListComponent extends AppComponentBase {

    @ViewChild('dt') table: Table;

    filterCodeText = '';
    filterNameText = '';
    records = [];
    selectedApartmentType: ApartmentTypeDTO;
    userID: number;


    constructor(injector: Injector, private apartmentTypeService: ApartmentTypeServiceProxy, private sessisonSevice: SessionServiceProxy) {
        super(injector);
        sessisonSevice.getCurrentLoginInformations().subscribe(userInfo => {
            this.userID =  parseInt(userInfo.user.id.toString());
        });
    }
    ngOnInit() {
        this.search();
    }
    loading=null;
    // event 
    add() {
        this.navigate(["/app/admin/apartment-type-add"]);
    }
    viewDetail() {
        if (this.selectedApartmentType != undefined) {
            this.navigate([`/app/admin/apartment-type-view/${this.selectedApartmentType.apartmenT_TYPE_ID}`])
        } else {
            this.message.warn(this.l('g4UnseletedApTypeWarning'));
        }
    }
    update() {

        if (this.selectedApartmentType != undefined) {
            this.navigate([`/app/admin/apartment-type-edit/${this.selectedApartmentType.apartmenT_TYPE_ID}`])
        } else {
            this.message.warn(this.l('g4UnseletedApTypeWarning'));
        }
    }
    delete() {
        if (this.selectedApartmentType != undefined) {
            this.message.confirm('', this.l('g4DeleteWarningMessage', this.selectedApartmentType.apartmenT_TYPE_NAME), (isConfirmed) => {
                if (isConfirmed) {

                    this.apartmentTypeService.apartmentTypeDelete(this.selectedApartmentType.apartmenT_TYPE_ID)
                        .subscribe(response => {
                            console.log(response);
                            if (response[0]['RESULT'] == '0') {
                                this.message.success(this.l('g4SuccessfullyDeleted'));
                                this.search();
                            } else {
                                this.message.error(response[0]['ERROR_DESC']);
                            }
                        });
                }
            });
        } else
            this.message.warn(this.l('g4UnseletedApTypeWarning'));

    }
    search() {
        this.apartmentTypeService.apartmentTypeSearch(this.filterCodeText, this.filterNameText,this.userID ).subscribe(res => {
            this.records = res;

        })
    }
    exportExcel() {
        this.apartmentTypeService.apartmentType_ExportExcel(this.records).subscribe(res => {
            if (res) this.message.success(this.l('g4ExportSuccess'));
            else this.message.error(this.l('g4ExportFalled'));
        });
    }

}