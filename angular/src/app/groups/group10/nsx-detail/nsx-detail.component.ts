import { ViewEncapsulation, Component, Injector, TemplateRef} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { NSXServiceProxy, CM_NSX_DTO } from "@shared/service-proxies/service-proxies";
import { ModalModule } from "ngx-bootstrap/modal";
import { BsModalService, BsModalRef} from "ngx-bootstrap/modal";
import { FormBuilder, FormGroup, Validators,
    ValidationErrors, } from '@angular/forms';


@Component({
    templateUrl: './nsx-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./nsx-detail.component.css']
})
export class NsxDetailComponent extends AppComponentBase {

    /**
     *
     */

     isUpdate: boolean = false;
    title: string;
    modalRef: BsModalRef;
    registerForm: FormGroup;
    submitted = false;
    currentId: string;
    records: CM_NSX_DTO[] = [];

    inputModel: CM_NSX_DTO = new CM_NSX_DTO();

    selectedNsx: CM_NSX_DTO = new CM_NSX_DTO();
    maxName: number = null;
    maxCode: number = null;
    maxFrom: number = null;
    editPageState: string;

    constructor(injector: Injector,

        private formBuilder: FormBuilder,
        private nsxService: NSXServiceProxy, private modalService: BsModalService) {

        super(injector);
        this.title = 'Chi tiết nhà sản xuất';
        this.currentId = this.getRouteParam('id');
        this.editPageState = this.getRouteData('editPageState');
            if(this.editPageState == "view")
           {
            this.initData();

           }
            else if(this.editPageState == "edit")
            {

                this.initData();

                this.update();
            }


        this.nsxService.cM_NSX_GetSizeCol('NSX_NAME').subscribe(response => {
            this.maxName = Number(response) ;
            this.ngOnInit();
         })
         this.nsxService.cM_NSX_GetSizeCol('NSX_CODE').subscribe(response => {
            this.maxCode = Number(response) ;
            this.ngOnInit();
         })
         this.nsxService.cM_NSX_GetSizeCol('NSX_FROM').subscribe(response => {
            this.maxFrom = Number(response) ;
            this.ngOnInit();
         })

    }
    initData() {
            this.nsxService.cM_NSX_ById(this.currentId).subscribe(response => {
                this.inputModel = response;
                if(this.inputModel.nsX_NAME == null)
                {
                        this.message.error(this.l('Group10_ID_NOT_FOUND'))  ;
                        this.navigate(['/app/admin/nsx-list']);
                }
                this.ngOnInit();
            });
    }
    ngOnInit(): void {
       // this.show();


        this.registerForm = this.formBuilder.group({
            name: [this.inputModel.nsX_NAME, [Validators.required, Validators.maxLength(this.maxName)]],
            code: [this.inputModel.nsX_CODE, [Validators.required, Validators.maxLength(this.maxCode)]],
            from: [this.inputModel.nsX_FROM, [Validators.required, Validators.maxLength(this.maxFrom)]],
          });
          if(this.editPageState == "add")
           {
               this.disabledInput();
               this.title = 'Thêm mới nhà sản xuất';
           }
           else if(this.editPageState == "edit")
           this.disabledInput();

    }
    get f() { return this.registerForm.controls; }

    unDisabledInput(){
        $(":disabled").prop('disabled',false);
    }
    disabledInput(){
        $(":disabled").prop('disabled',false);
    }
    onSubmit() {
        Object.keys(this.registerForm.controls).forEach((key) => {
            const controlErrors: ValidationErrors = this.registerForm.get(key)
                .errors;
            if (controlErrors != null) {
                this.message.error(
                    this.l('Group10_ERROR_INPUT')
                );
            }
        });
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }


        this.inputModel.nsX_NAME = this.registerForm.value.name;
        this.inputModel.nsX_CODE = this.registerForm.value.code;
        this.inputModel.nsX_FROM = this.registerForm.value.from;



        if(this.isUpdate)
        {
            this.nsxService.cM_NSX_Update(this.inputModel).subscribe(response => {
                if (response['RESULT'] == '0') {
                    this.message.success(this.l('Group10_UPDATE_COMPLETE'));
                    this.back();
                    //this.ngOnInit();
                }
                else if(response['RESULT'] == '-1'){
                    this.message.error(response['ERROR_DESC']);
                }
            })
        }
        else if(this.editPageState == 'add'){
            this.inputModel.nsX_ID=null;
            this.nsxService.cM_NSX_Insert(this.inputModel).subscribe(response => {
                if (response['RESULT'] == '0') {
                    this.message.success(this.l('Group10_ADD_COMPLETE'));
                    this.back();
                    //this.ngOnInit();
                }
                else if(response['RESULT'] == '-1'){
                    this.message.error(response['ERROR_DESC']);
                }
            })
        }
        this.back();
    }


    // update(template: TemplateRef<any>, id: string ) {
    //     if (id != null) {
    //         this.isHidden=false;
    //         this.openModal(template, id, this.isHidden);
    //     } else {
    //         this.message.warn('Vui lòng chọn một đơn vị');
    //     }
    // }
    update(){
        this.disabledInput();
        this.title = 'Cập nhật nhà sản xuất';
        this.isUpdate = true;
    }

    delete() {
        this.message.confirm(
            this.l('AreYouSure'),
            this.l('Group10_DELETE_WARNING'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.nsxService.cM_NSX_Delete(
                        this.inputModel.nsX_ID
                    ).subscribe((response) => {
                        if (response["RESULT"] == "0") {
                            this.message.success(this.l('Group10_DELETE_COMPLETE'));
                            this.navigate(['/app/admin/nsx-list']);
                        } else {
                            this.message.error(response["ERROR_DESC"]);
                        }
                    });
                }
            }
        );
        this.back();
    }
    back(){
        this.isUpdate = false;
        this.navigate(['/app/admin/nsx-list']);
    }
    insert(){
        this.disabledInput();
        this.title = 'Thêm mới nhà sản xuất';
        this.isUpdate = false;
        this.inputModel.nsX_NAME = null;
        this.inputModel.nsX_FROM = null;
        this.inputModel.nsX_CODE = null;
        this.inputModel.nsX_ID = null;
        this.ngOnInit();
    }

}
