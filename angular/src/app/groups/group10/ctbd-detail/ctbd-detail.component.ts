import {
    ViewEncapsulation,
    Component,
    Injector,
    TemplateRef,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {
    NSXServiceProxy,
    NTXServiceProxy,
    CM_NSX_DTO,
    NguoiThueXe_DTO,
    XE_DTO,
    XeServiceProxy,
    CTBDServiceProxy,
    ChiTietBaoDuong_DTO,
} from "@shared/service-proxies/service-proxies";
import { ModalModule } from "ngx-bootstrap/modal";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import {
    FormBuilder,
    FormGroup,
    Validators,
    ValidationErrors,
} from "@angular/forms";
import * as moment from "moment";

@Component({
    templateUrl: "./ctbd-detail.component.html",
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ["./ctbd-detail.component.css"],
})
export class CtbdDetailComponent extends AppComponentBase {
    /**
     *
     */
    isShow = true;
    isUpdate: boolean = false;
    title: string;
    modalRef: BsModalRef;
    registerForm: FormGroup;
    submitted = false;
    currentId: string;
    isUpdating: boolean;

    currentBD_ID: string;
    currentCTBD_CODE: string;
    selectedGender: string = "1";

    records: NguoiThueXe_DTO[] = [];

    inputModel: ChiTietBaoDuong_DTO = new ChiTietBaoDuong_DTO();

    editPageState: string;

    constructor(
        injector: Injector,

        private formBuilder: FormBuilder,
        private ntxService: NTXServiceProxy,
        private modalService: BsModalService,
        private xeService: XeServiceProxy,
        private ctbdService: CTBDServiceProxy
    ) {
        super(injector);
        this.title = "Chi tiết bảo dưỡng ";
        this.currentId = this.getRouteParam("id");
        this.editPageState = this.getRouteData("editPageState");
        if (this.editPageState == "view") {
            this.initData();
        } else if (this.editPageState == "edit") {
            this.initData();
            setTimeout(() => {
                this.registerForm.controls['code'].disable();

            }, 1);
            this.update();
        }
        else if(this.editPageState =="approve")
        {
            this.initData();
            this.disabledInput();

        }
    }
    initData() {
        this.ctbdService
            .chiTietBaoDuong_GetById(this.currentId)
            .subscribe((response) => {
                this.inputModel = response;
                this.currentBD_ID = this.inputModel.bD_ID;
                this.currentCTBD_CODE = this.inputModel.ctbD_CODE;
                if (this.inputModel.ctbD_NAME == null) {
                    this.message.error(this.l('Group10_ID_NOT_FOUND'));
                    this.navigate(["/app/admin/bao-duong-view", this.currentBD_ID]);
                }
                else if(this.inputModel.recorD_STATUS == '0')
                {
                    this.isShow = false;
                }
                else if(this.inputModel.recorD_STATUS == '1')
                {
                    this.isShow = true;
                }
                this.ngOnInit();
                if (this.inputModel.autH_STATUS === null && this.inputModel.approvE_DT.toISOString().substring(0, 10) === '0000-12-31') {
                    this.ctbdService.isUpdating(this.inputModel.ctbD_CODE).subscribe((response) => {
                        this.isUpdating = response
                    })
                }
            });
    }
    ngOnInit(): void {
        // this.show();
        if (this.editPageState == "edit") {

            setTimeout(() => {
                this.registerForm.controls['code'].disable();

            }, 1);

        }
        this.registerForm = this.formBuilder.group({
            name: [
                this.inputModel.ctbD_NAME,
                [Validators.required, Validators.maxLength(50)],
            ],
            code: [
                this.inputModel.ctbD_CODE,
                [Validators.required, Validators.maxLength(20)],
            ],
            quantity: [
                this.inputModel.ctbD_QUANTITY,
                [Validators.required, Validators.pattern("^[0-9]*$")],
            ],
            unit_price: [
                this.inputModel.ctbD_UNIT_PRICE,
                [Validators.required, Validators.pattern("^[0-9]*$")],
            ],

            total_price: [
                {value:  this.inputModel.ctbD_TOTAL_PRICE, disabled: true},
                [Validators.required, Validators.pattern("^[0-9]*$")],
            ],
            reason: [
                
            this.inputModel.reason
                
            ]
        });

        if (this.editPageState == "add") {
            this.disabledInput();
            this.title = "Thêm mới CTBD";
        } else if (this.editPageState == "edit") this.disabledInput();
    }

    get f() {
        return this.registerForm.controls;
    }

    unDisabledInput() {
        $(":disabled").prop("disabled", false);
    }
    disabledInput() {
        $(":disabled").prop("disabled", false);
        setTimeout(() => {
            this.registerForm.controls['total_price'].disable();

        }, 1);
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
        if (this.isUpdate) {
            this.inputModel.ctbD_CODE = this.currentCTBD_CODE;
            this.inputModel.ctbD_NAME = this.registerForm.value.name;
            this.inputModel.ctbD_QUANTITY = this.registerForm.value.quantity;
            this.inputModel.ctbD_UNIT_PRICE = this.registerForm.value.unit_price;
            this.inputModel.ctbD_TOTAL_PRICE = this.registerForm.value.quantity*this.registerForm.value.unit_price;
            this.inputModel.reason= null;
            //alert(this.inputModel.ctbD_TOTAL_PRICE);
            this.ctbdService.isUpdating(this.inputModel.ctbD_CODE).subscribe(
                response => {
                    if (response) {
                        this.message.error(this.l('WaitingForApproveMessage'));
                    }
                    else {
                        // if(this.inputModel.approvE_DT.toISOString().substring(0, 10) !== '0000-12-31' && this.inputModel.autH_STATUS === null) {
                        //     this.message.error('Phiếu đang chờ duyệt');
                        // }
                        if (this.inputModel.autH_STATUS === null) {
                            this.message.error(this.l('WaitingForApproveMessage'));
                        } else {
                            this.ctbdService.chiTietBaoDuong_Update(this.inputModel).subscribe(response => {
                                if (response["RESULT"] == "0") {
                                    this.message.success(this.l('Group10_UPDATE_APPROVE'));
                                    this.navigate(['/app/admin/bao-duong-view', this.inputModel.bD_ID]);
                                }
                                else {
                                    this.message.error(response['ERROR_DESC']);
                                }
                            })
                        }
                    }
                });
        } else if (this.editPageState == "add") {
            this.inputModel.ctbD_CODE = this.registerForm.value.code;
            this.inputModel.ctbD_NAME = this.registerForm.value.name;
            this.inputModel.ctbD_QUANTITY = this.registerForm.value.quantity;
            this.inputModel.ctbD_UNIT_PRICE = this.registerForm.value.unit_price;
            this.inputModel.ctbD_TOTAL_PRICE = this.registerForm.value.quantity*this.registerForm.value.unit_price;
            this.inputModel.ctbD_ID = null;
            this.inputModel.reason=null;
            this.inputModel.bD_ID = this.getRouteParam("id");
            this.ctbdService
                .chiTietBaoDuong_Insert(this.inputModel)
                .subscribe((response) => {
                    if (response["RESULT"] == "0") {
                        this.message.success(this.l('Group10_ADD_APPROVE'));

                        //this.ngOnInit();
                    } else if (response["RESULT"] == "-1") {
                        this.message.error(response["ERROR_DESC"]);
                    }
                });
        } 
        
        if (this.isUpdate) {
            this.back_when_edit();
        } else this.back();
    }

    update() {
        this.disabledInput();
        this.title = "Cập nhật chi tiết bảo dưỡng";
        this.isUpdate = true;
        setTimeout(() => {
            this.registerForm.controls['code'].disable();

        }, 1);
    }

    delete() {
        this.message.confirm(
            this.l('AreYouSure'),
            this.l('Group10_DELETE_WARNING'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.ctbdService.isUpdating(this.inputModel.ctbD_CODE).subscribe(
                        response => {
                            if (response || this.inputModel.autH_STATUS === null) {
                                this.message.error(this.l('WaitingForApproveMessage'));
                            }
                            else {
                                this.ctbdService.chiTietBaoDuong_Delete(
                                    this.inputModel
                                ).subscribe((response) => {
                                    if (response["RESULT"] == "0") {
                                        this.message.success(this.l('Group10_DELETE_APPROVE'));
                                        this.navigate([
                                            "/app/admin/bao-duong-view",
                                            this.currentBD_ID,
                                        ]);
                                    } else {
                                        this.message.error(response["ERROR_DESC"]);
                                    }
                                });
                            }
                        });
                }
            }
        );
    }
    back() {
        this.isUpdate = false;
        this.navigate(["/app/admin/bao-duong-view", this.getRouteParam("id")]);
    }

    back_when_edit() {
        this.navigate(["/app/admin/bao-duong-view", this.inputModel.bD_ID]);
    }
    insert() {
        this.disabledInput();
        this.title = "Thêm mới CTBD";
        this.isUpdate = false;

        this.inputModel.ctbD_ID = null;
        this.inputModel.ctbD_NAME = null;
        this.inputModel.ctbD_QUANTITY = null;
        this.inputModel.ctbD_TOTAL_PRICE = null;
        this.inputModel.ctbD_UNIT_PRICE = null;
        this.inputModel.ctbD_CODE = null;
        this.inputModel.bD_ID = null;

        this.ngOnInit();
    }
    calTotalPrice()
    {
        this.inputModel.ctbD_CODE = this.currentCTBD_CODE==null?this.registerForm.value.code:this.currentCTBD_CODE;
        this.inputModel.ctbD_NAME = this.registerForm.value.name;
        this.inputModel.ctbD_QUANTITY = this.registerForm.value.quantity;
        this.inputModel.ctbD_UNIT_PRICE = this.registerForm.value.unit_price;
        this.inputModel.ctbD_TOTAL_PRICE = this.inputModel.ctbD_UNIT_PRICE * this.inputModel.ctbD_QUANTITY;
        this.ngOnInit();
    }

    approve() {
            if (this.inputModel.approvE_DT.toISOString().substring(0, 10) !== '0000-12-31') {
                console.log('delete');
                this.ctbdService.chiTietBaoDuong_Approve_Delete(this.inputModel.ctbD_CODE).subscribe(
                    (response) => {
                        if (response.length === 0) {
                            this.navigate(['/app/admin/ctbd-approve', this.currentBD_ID]);
                            this.message.success(this.l("SuccessfullyDeleteApproved"));
                        } else {
                            this.message.error(response["ERROR_DESC"]);
                        }
                    });
            }
            else {
                this.ctbdService.isUpdating(this.inputModel.ctbD_CODE).subscribe(result => {
                    if (result) {
                        //Duyệt update
                        this.ctbdService.chiTietBaoDuong_Approve_Update(this.inputModel.ctbD_CODE).subscribe(
                            (response) => {
                                if (response.length === 0) {
                                    this.navigate(['/app/admin/ctbd-approve', this.currentBD_ID]);
                                    this.message.success(this.l("SuccessfullyUpdateApproved"));
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    } else {
                        //Duyệt add new
                        console.log('insert');
                        this.ctbdService.chiTietBaoDuong_Approve_Insert(this.inputModel.ctbD_ID).subscribe(
                            (response) => {
                                console.log(response.length);
                                if (response.length === 0) {
                                    this.navigate(['/app/admin/ctbd-approve', this.currentBD_ID]);
                                    this.message.success(this.l("SuccessfullyApproved"));
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    }
                })
            }
        
    }

    nav_disapprove() {


            this.navigate(['/app/admin/ctbd-edit-approve', this.inputModel.ctbD_ID]);
            this.message.info(this.l('Group10_REQUEST_REASON'));
            
        
    }
    disapprove() {
        
            //alert("Lý do từ chối là: "+ this.registerForm.value.reason);

            if (this.inputModel.approvE_DT.toISOString().substring(0, 10) !== '0000-12-31') {
                this.ctbdService.chiTietBaoDuong_Deny_Delete(this.inputModel.ctbD_CODE, this.registerForm.value.reason == null ? 'Không rõ' : this.registerForm.value.reason).subscribe(
                    (response) => {
                        if (response.length === 0) {
                            this.navigate(['/app/admin/ctbd-approve', this.currentBD_ID]);
                            this.message.success(this.l("SuccessfullyDeleteDisApproved"));
                        } else {
                            this.message.error(response["ERROR_DESC"]);
                        }
                    });
            }
            else {
                this.ctbdService.isUpdating(this.inputModel.ctbD_CODE).subscribe(result => {
                    if (result) {
                        //Duyệt update
                        this.ctbdService.chiTietBaoDuong_Deny_Update(this.inputModel.ctbD_CODE, this.registerForm.value.reason == null ? 'Không rõ' : this.registerForm.value.reason).subscribe(
                            (response) => {
                                if (response.length === 0) {
                                    this.navigate(['/app/admin/ctbd-approve', this.currentBD_ID]);
                                    this.message.success(this.l("SuccessfullyUpdateDisApproved"));
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    } else {
                        //Duyệt add new
                        this.ctbdService.chiTietBaoDuong_Deny_Insert(this.inputModel.ctbD_ID, this.registerForm.value.reason == null ? 'Không rõ' : this.registerForm.value.reason).subscribe(
                            (response) => {
                                if (response.length === 0) {
                                    this.navigate(['/app/admin/ctbd-approve', this.currentBD_ID]);
                                    this.message.success(this.l("SuccessfullyDisApproved"));
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    }
                })
            }
        
    }
}
