import { ViewEncapsulation, Component, Injector, OnInit, ViewChild, ElementRef } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { BranchServiceProxy, CM_BRANCH_DTO } from "@shared/service-proxies/service-proxies";
import { Response } from "@angular/http";
import { NgModel, FormGroup, FormControl, Validators, NgForm } from "@angular/forms";

@Component({
    templateUrl: './branch-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../group4-style.css']
})
export class BranchDetailComponent extends AppComponentBase {
    /**
     *
     */
    @ViewChild('formRef1') formBranch: NgForm;
    @ViewChild('dd') ddFathers: ElementRef;
    inputModel: CM_BRANCH_DTO = new CM_BRANCH_DTO();
    editPageState: string;
    branch_status: boolean = true;
    listFather = [];
    selectedFather: CM_BRANCH_DTO = new CM_BRANCH_DTO;

    constructor(injector: Injector, private branchService: BranchServiceProxy) {
        super(injector);
        //Default value
        this.inputModel.brancH_ID = this.getRouteParam('id');
        this.editPageState = this.getRouteData('editPageState');

        if (this.editPageState == 'add') {
            this.inputModel.brancH_TYPE = 'PGD';
            this.inputModel.brancH_STATUS = '0';
            this.refeshListFather();
        }
        // page Edit and View
        if (this.editPageState == 'view' || this.editPageState == 'edit') {
            this.branchService.branchById(this.getRouteParam('id')).subscribe(res => {
                this.inputModel = res;
                this.refeshListFather();
            });
        }
    }

    ngOnInit() {
    }

    refeshListFather() {
        this.branchService.cM_Branch_GetFatherBranchByBranchType(this.inputModel.brancH_TYPE).subscribe(res => {
            this.listFather = res;
            this.listFather.forEach(element => {
                if (element.brancH_ID == this.inputModel.fatheR_ID)
                    this.selectedFather = element;
            });
        });
    }


    // save
    save(formRef1) {
        this.touchedForm();
        this.beforeSave();
        if (formRef1.valid == true) {
            if (this.editPageState == 'add') this.saveAdd();
            if (this.editPageState == 'edit') this.saveEdit();
        }
        else {
            this.message.error(this.l('g4SaveErrorMessage'));
        }
    }

    onClick(type){
        console.log(type);
    }
    beforeSave() {
        this.inputModel.fatheR_ID = this.selectedFather.brancH_ID;
    }

    saveAdd() {
        this.branchService.branchInsert(this.inputModel).subscribe(response => {
            if (response[0]['RESULT'] == '0') {
                this.message.success(this.l('g4SuccessfullyAdded'));
            }
            else if (response[0]['RESULT'] == '-1') {
                this.message.error(response[0]['ERROR_DESC']);
            }
        })
    }
    saveEdit() {
        this.branchService.branchUpdate(this.inputModel).subscribe(response => {
            if (response[0]['RESULT'] == '0') {
                this.message.success(this.l('g4SuccessfullyUpdated'));
                this.view();
            }
            else if (response[0]['RESULT'] == '-1') {
                this.message.error(response[0]['ERROR_DESC']);
            }
        })
    }

    touchedForm() {
        this.formBranch.form.controls['brancH_CODE'].markAsTouched();
        this.formBranch.form.controls['brancH_NAME'].markAsTouched();
        this.formBranch.form.controls['tel'].markAsTouched();
        this.formBranch.form.controls['brancH_FAX'].markAsTouched();
        this.formBranch.form.controls['brancH_EMAIL'].markAsTouched();
        this.formBranch.form.controls['addr'].markAsTouched();
    }


    // toolbar
    back() {
        this.navigate(['/app/admin/branches']);
    }
    update() {
        this.navigate(['/app/admin/branch-edit', this.inputModel.brancH_ID]);
    }
    view() {
        this.navigate(['/app/admin/branch-view', this.inputModel.brancH_ID]);
    }

    delete() {
        this.message.confirm('', this.l('g4DeleteWarningMessage', this.inputModel.brancH_NAME), (isConfirmed) => {
            if (isConfirmed) {

                this.branchService.branchDelete(this.inputModel.brancH_ID)
                    .subscribe(response => {
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


    // event
    change_branch_type(e) {
        this.inputModel.brancH_TYPE = e.target.value;
        this.refeshListFather();
    }

}