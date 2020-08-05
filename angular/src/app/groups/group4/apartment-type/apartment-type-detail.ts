import { ViewEncapsulation, Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { BranchServiceProxy, CM_BRANCH_DTO, ApartmentTypeServiceProxy, ApartmentTypeDTO, SessionServiceProxy } from "@shared/service-proxies/service-proxies";
import { Response } from "@angular/http";
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { NgForm } from "@angular/forms";



@Component({
    templateUrl: './apartment-type-detail.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../group4-style.css']
})
export class ApartmentTypeDetailComponent extends AppComponentBase {

    @ViewChild('formRef1') formApType: NgForm;
    inputModel = new ApartmentTypeDTO();
    editPageState: string;



    constructor(injector: Injector, private apartmentTypeService: ApartmentTypeServiceProxy, private sessionService: SessionServiceProxy) {
        super(injector);
        this.inputModel.apartmenT_TYPE_ID = this.getRouteParam('id');
        this.editPageState = this.getRouteData('editPageState');

    }

    ngOnInit() {
        if (this.editPageState == 'add') {
        }
        if (this.editPageState == 'view' || this.editPageState == 'edit') {

            this.apartmentTypeService.apartmentTypeByID(this.getRouteParam('id')).toPromise().then(res => {
                this.inputModel = res;

            });
        }
    }

    back() {
        this.navigate(['/app/admin/apartment-type']);

    }

    update() {
        this.navigate([`/app/admin/apartment-type-edit/${this.inputModel.apartmenT_TYPE_ID}`])
    }

    view() {
        this.navigate([`/app/admin/apartment-type-view/${this.inputModel.apartmenT_TYPE_ID}`])
    }
    delete() {
        this.message.confirm('', this.l('g4DeleteWarningMessage', this.inputModel.apartmenT_TYPE_NAME), (isConfirmed) => {
            if (isConfirmed) {

                this.apartmentTypeService.apartmentTypeDelete(this.inputModel.apartmenT_TYPE_ID)
                    .subscribe((response) => {
                        if (response[0]['RESULT'] == '0') {
                            this.message.success(this.l('g4SuccessfullyDeleted'));
                            this.back();
                        } else {
                            this.message.error(response[0]['ERROR_DESC']);
                        }
                    });
            }
        });
    }

    approve() {
        // let userID;
        // this.sessionService.getCurrentLoginInformations().subscribe(id => userID = id);
        // this.apartmentTypeService.apartmentTypeApprove(this.inputModel.apartmenT_TYPE_ID, userID).subscribe(res=>{
            
        // })
    }

    save(formRef1) {
        console.log(formRef1);
        console.log(this.inputModel.apartmenT_TYPE_CODE);
        this.touchedForm();
        if (formRef1.valid == true) {
            if (this.editPageState == 'add') this.saveAdd();
            if (this.editPageState == 'edit') this.saveEdit();

        }
        else {
            this.message.error(this.l('g4SaveErrorMessage'));
        }
    }

    saveAdd() {
        this.apartmentTypeService.apartmentTypeInsert(this.inputModel).subscribe(response => {
            if (response[0]['RESULT'] == '0') {
                this.message.success(this.l('g4SuccessfullyAdded'));
                this.navigate(['/app/admin/apartment-type-add']);
            }
            else if (response[0]['RESULT'] == '-1') {
                this.message.error(response[0]['ERROR_DESC']);
            }
        })
    }
    saveEdit() {
        this.apartmentTypeService.apartmentTypeUpdate(this.inputModel).subscribe(response => {
            if (response[0]['RESULT'] == '0') {
                this.message.success(this.l('g4SuccessfullyUpdated'));
                this.navigate([`/app/admin/apartment-type-view/${this.inputModel.apartmenT_TYPE_ID}`])
            }
            else if (response[0]['RESULT'] == '-1') {
                this.message.error(response[0]['ERROR_DESC']);
            }
        })
    }
    touchedForm() {
        //this.formApType.form.controls['apartmenT_TYPE_CODE'].markAsTouched();
        this.formApType.form.controls['apartmenT_TYPE_NAME'].markAsTouched();
    }
}
