import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { ModelXeServiceProxy, MODELXE_DTO } from "@shared/service-proxies/service-proxies";
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    templateUrl: './model-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ModelDetailComponent extends AppComponentBase {
    /**
     *
     */
    constructor(injector: Injector,
        private modelService: ModelXeServiceProxy,
        private formBuider: FormBuilder) {
        super(injector);
        this.inputModel.modeL_ID = this.getRouteParam('modeL_ID');
        this.editPageState = this.getRouteData('editPageState');
        console.log(this.editPageState);
        this.modelForm = this.formBuider.group({
            'modeL_CODE': new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(15)]),
            'modeL_NAME': new FormControl('', [Validators.required, Validators.maxLength(100), Validators.minLength(2)]),
            'modeL_DMNL': new FormControl('', [Validators.required, Validators.pattern("^[0-9]*$")])
        })
    }

    inputModel: MODELXE_DTO = new MODELXE_DTO();
    clonedModels: { [s: string]: MODELXE_DTO; } = {};
    modelForm: FormGroup;
    editPageState: string;
    isEdit: boolean = false;
    submitted = false;
    isUnApproved:boolean=false;
    editing:boolean=false;
    f=null;
    modeL_CODE=null;
    modeL_NAME=null;
    ngOnInit() {
        this.modelService.modelXe_ById(this.inputModel.modeL_ID).subscribe(response => {
            this.inputModel = response;
            console.log(this.inputModel);
            if(this.inputModel.autH_STATUS !='1') 
                this.isUnApproved=true ;
            else this.isUnApproved=false;
        })
    }

    save() {
        this.submitted = true;
        if (this.modelForm.invalid) {
            return;
        }
        if (this.editPageState == 'update') {
            this.modelService.modelXe_Update(this.inputModel).subscribe(response => {
                if (response[0].RESULT == '0') {
                    this.message.success(this.l('Sửa thành công!'));
                    this.isEdit=false;
                }
                else if (response[0].RESULT == '-1') {
                    this.message.error(response[0].ERROR_DESC);
                }
            })
        }

    }

    delete() {
        this.message.confirm(
            this.l('Group12_Notification'),
            this.l('Group12_DeleteModelWaringMessage'),
            (isConfirned) => {
                if (isConfirned) {
                    this.inputModel.autH_STATUS = '0';
                    this.inputModel.recorD_STATUS = '0';
                    this.modelService.modelXe_Update(this.inputModel).subscribe(res => {
                        if (res[0].RESULT != '0')
                            this.message.info(this.l('Group12_DeleteSuccess'), this.l('Group12_Notification'));
                        else this.message.error(res[0].ERROR_DESC);

                    });
                }
            }
        )
    }

    approve(isApprove: boolean) {
        isApprove?(this.inputModel.autH_STATUS='1', this.isUnApproved=false): this.inputModel.autH_STATUS='-1';

        this.modelService.modelXe_Update(this.inputModel).subscribe(response => {
            if (response[0].RESULT == '0') {
                if(isApprove)
                    this.message.success(this.l('Group12_CheckSuccess'));
                else this.message.success(this.l('Group12_DenyApproveSuccessfully'));
                if(this.inputModel.recorD_STATUS=='0' && isApprove)
                    this.navigate(['app/admin/model']);
            }
            else if (response[0].RESULT == '-1') {
                this.message.error(response[0].ERROR_DESC);
            }
        })
    }
}

    