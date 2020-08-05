import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { ModelXeServiceProxy, MODELXE_DTO } from "@shared/service-proxies/service-proxies";
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    templateUrl: './model-add.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ModelAddComponent extends AppComponentBase implements OnInit {
    /**
     *
     */
    constructor(injector: Injector,
        private modelService: ModelXeServiceProxy,
        private formBuider: FormBuilder) {
        super(injector);
        this.editPageState = this.getRouteData('editPageState');
        

    }

    inputModel: MODELXE_DTO = new MODELXE_DTO();
    submitted=null;
    editPageState: string;
    modelForm: FormGroup;
    modeL_CODE=null;
    modeL_NAME=null;

    ngOnInit() {
        //this.getModelCode();
        this.inputModel.modeL_TYPE="motorbike";
        this.inputModel.modeL_HSX="VietNam";
        this.modelForm = this.formBuider.group({
            'modeL_NAME': new FormControl('', [Validators.required, Validators.maxLength(100), Validators.minLength(2)]),
            'modeL_DMNL': new FormControl('', [Validators.required, Validators.pattern("^[0-9]*$")])
        })

        
    
    }
    save() {
        if (this.modelForm.valid) {
            if (this.editPageState == 'add') {
                this.inputModel.recorD_STATUS = "1";
                this.inputModel.autH_STATUS="0";
                var today=new Date();
                this.inputModel.approvE_DT=null;
                this.modelService.modelXe_Insert(this.inputModel).subscribe(response => {
                    if (response[0].RESULT == '0') {
                        this.message.success(this.l('Thêm mới thành công'));
                    }
                    else if (response[0].RESULT == '-1') {
                        this.message.error(response[0].ERROR_DESC);
                    }
                })
            }
        }else{
            this.message.error('Bạn phải hoàn thành các mục dữ liệu yêu cầu!');
        }
    }

    getModelCode() {
        this.modelService.modelXe_MakeCode('modelCode').subscribe(response => {
            this.inputModel.modeL_CODE = response[0].MODEL_CODE;
        })
    }
}