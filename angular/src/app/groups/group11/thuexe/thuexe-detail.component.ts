import { Component, ViewEncapsulation, OnInit, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { FormBuilder, FormGroup, Validators, FormControl } from "@angular/forms";
import { XeServiceProxy, XE_DTO, NTXServiceProxy, NguoiThueXe_DTO, PTX_DTO, PhieuThueXeServiceProxy } from "@shared/service-proxies/service-proxies";
import * as moment from "moment";

@Component({
    templateUrl: './thuexe-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ThueXeDetailComponent extends AppComponentBase implements OnInit {

    editPageState: string;
    thueXeForm: FormGroup;
    show: boolean = false;
    submitted = false;
    xe: XE_DTO = new XE_DTO();
    ntx: NguoiThueXe_DTO = new NguoiThueXe_DTO();
    xeList: XE_DTO[];
    ntxList: NguoiThueXe_DTO[];
    inputModel: PTX_DTO = new PTX_DTO();

    ptX_RENT_DT: string;
    ptX_EXP_DT: string;
    ptX_RETURN_DT: string;

    ngOnInit(): void {
        if (this.editPageState === "view") {
            this.loadData();
        }
        if (this.editPageState === "add") {
            this.ptX_RENT_DT = moment().toISOString().substring(0, 10);
            this.ptX_EXP_DT = moment().toISOString().substring(0, 10);
            this.ptX_RETURN_DT = moment().toISOString().substring(0, 10);
        }
        this.thueXeForm = this.formBuilder.group({
            code: [{ value: this.inputModel.ptX_CODE }, [Validators.required, Validators.pattern(/^[a-zA-Z0-9^_]+$/)]],
            ntx: [{ value: this.inputModel.ntX_ID }, [Validators.required]],
            xe: [{ value: this.inputModel.xE_ID }, [Validators.required]],
            price: [this.inputModel.ptX_PRICE, [Validators.required, Validators.pattern(/^[0-9]+$/)]],
            rentDT: [ {value: this.ptX_RENT_DT, disabled: this.editPageState === "add" ? true : false}, [Validators.required]],
            expDT: [this.ptX_EXP_DT, [Validators.required]],
            returnDT: [this.ptX_RETURN_DT, [Validators.required]],
            notes: [this.inputModel.ptX_NOTE, []],
        });
        //this.inputModel.xE_ID = 'XE000000000000000025';
        this.inputModel.ptX_ID = this.getRouteParam("id");
        this.initData();
    }

    initData() {
        this.xeService.xE_Search(new XE_DTO()).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.xeList = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        })
        this.ntxService
            .nguoiThueXe_Search(this.ntx)
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.ntxList = result.items;
                this.primengTableHelper.hideLoadingIndicator();
                this.show = true;
            });
    }

    loadData() {
        this.thueXeService.pTX_GetByCode(this.inputModel.ptX_CODE).subscribe(response => {
            this.inputModel = response[0];
            this.ptX_RENT_DT = this.inputModel.ptX_RENT_DT.toISOString().substring(0, 10);
            this.ptX_EXP_DT = this.inputModel.ptX_EXP_DT.toISOString().substring(0, 10);
            this.ptX_RETURN_DT = this.inputModel.ptX_RETURN_DT.toISOString().substring(0, 10);
        })
    }

    constructor(injector: Injector, private formBuilder: FormBuilder,
        private xeService: XeServiceProxy, private ntxService: NTXServiceProxy,
        private thueXeService: PhieuThueXeServiceProxy) {
        super(injector);
        this.inputModel.ptX_CODE = this.getRouteParam('id');
        this.editPageState = this.getRouteData('editPageState');
        this.show = false;
    }

    checkValidDate() {
        if (this.ptX_RENT_DT !== undefined) {
            this.inputModel.ptX_RENT_DT = moment(this.thueXeForm.getRawValue().rentDT, "YYYY-MM-DD").add('days', 1);
            if (this.ptX_EXP_DT !== undefined) {
                this.inputModel.ptX_EXP_DT = moment(this.thueXeForm.value.expDT, "YYYY-MM-DD").add('days', 1);
                this.inputModel.ptX_RENT_DT.isAfter(this.inputModel.ptX_EXP_DT) ?
                    this.thueXeForm.get('expDT').setErrors({ beforeRentDT: true }) :
                    this.thueXeForm.get('expDT').setErrors(null);
            }
            if (this.ptX_RETURN_DT !== undefined) {
                this.inputModel.ptX_RETURN_DT = moment(this.thueXeForm.value.returnDT, "YYYY-MM-DD").add('days', 1);
                this.inputModel.ptX_RENT_DT.isAfter(this.inputModel.ptX_RETURN_DT) ?
                    this.thueXeForm.get('returnDT').setErrors({ beforeRentDT: true }) :
                    this.thueXeForm.get('returnDT').setErrors(null);
            }
        }
    }

    save() {
        this.submitted = true;
        if (this.thueXeForm.invalid) {
            this.message.error('Bạn vui lòng nhập lại đầy đủ các thông tin!');
            return;
        }

        // this.inputModel.ptX_CODE = 'test008';
        this.inputModel.ptX_CODE = this.thueXeForm.getRawValue().code.trim();
        this.inputModel.ntX_ID = this.thueXeForm.getRawValue().ntx;
        this.inputModel.xE_ID = this.thueXeForm.getRawValue().xe;
        this.inputModel.ptX_PRICE = this.thueXeForm.value.price;
        this.inputModel.ptX_NOTE = this.thueXeForm.value.notes;

        this.inputModel.ptX_RENT_DT = moment(this.thueXeForm.getRawValue().rentDT, "YYYY-MM-DD").add('days', 1);
        this.inputModel.ptX_EXP_DT = moment(this.thueXeForm.value.expDT, "YYYY-MM-DD").add('days', 1);
        this.inputModel.ptX_RETURN_DT = moment(this.thueXeForm.value.returnDT, "YYYY-MM-DD").add('days', 1);

        this.inputModel.createD_DT = moment(this.thueXeForm.value.rentDT);
        this.inputModel.approvE_DT = moment(this.thueXeForm.value.rentDT);

        // if ( this.inputModel.ptX_RENT_DT.isAfter(this.inputModel.ptX_RETURN_DT) ) {
        //     this.message.error(this.l('InvalidRentDateError'));
        //     this.thueXeForm.get('returnDT').setErrors({ beforeRentDT: true });
        //     return;
        // }
        this.show = false;

        if (this.editPageState == 'add') {
            console.log(this.inputModel);
            this.thueXeService.pTX_Insert(this.inputModel).subscribe(response => {
                if (response.length === 0) {
                    this.message.success('Thêm mới thành công');
                    this.navigate(['/app/admin/ptx-list']);
                }
                else {
                    this.message.error(this.l('Mã phiếu thuê đã tồn tại hoặc đang chờ duyệt'));
                }
                this.show = true;
            })
        } else if (this.editPageState == 'view') {
            this.thueXeService.pTX_Update(this.inputModel).subscribe(response => {
                if (response.length === 0) {
                    this.message.success('Đã lưu thành công');
                }
                else {
                    this.message.error('Lưu thất bại, vui lòng kiểm tra lại');
                }
                this.loadData();
                this.show = true;
            })
        }
    }

    delete() {
        this.message.confirm(
            this.l('CarRentalDeleteWarningMessage', this.inputModel.ptX_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.show = false;
                    this.thueXeService.pTX_Delete(
                        this.inputModel.ptX_CODE
                    ).subscribe((response) => {
                        if (response.length === 0) {
                            this.message.success("Xóa thành công.");
                            window.history.back();
                        } else {
                            this.message.error(response["ERROR_DESC"]);
                        }
                    });
                }
            }
        );
    }

    numberOnly(event): boolean {
        const charCode = (event.which) ? event.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;

    }
    transformAmount(event): void {
        let re = /\,/gi;
        let str = event.target.value;
        let chuncks = str.replace(re, '').match(/.{1,3}/g);
        let new_value = chuncks.join(",");
        event.target.value = new_value;
    }

    get f() { return this.thueXeForm.controls; }
}
