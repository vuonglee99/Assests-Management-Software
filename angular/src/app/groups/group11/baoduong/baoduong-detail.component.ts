import { Component, ViewEncapsulation, OnInit, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { FormBuilder, FormGroup, Validators, FormControl } from "@angular/forms";
import { BD_DTO, BaoDuongServiceProxy, XE_DTO, XeServiceProxy, ChiTietBaoDuong_DTO, CTBDServiceProxy } from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
import { ExcelExportService} from '../../group10/service/excelexport.service';

@Component({
    templateUrl: './baoduong-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class BaoDuongDetailComponent extends AppComponentBase implements OnInit {

    editPageState: string;
    baoDuongForm: FormGroup;
    show: boolean = false;
    submitted = false;
    bd: BD_DTO = new BD_DTO();
    inputModel: BD_DTO = new BD_DTO();
    xeList: XE_DTO[];
    bd_FROM_DT: string;
    bd_TO_DT: string;
    isUpdating: boolean;


    //CTBD
    selectedCTBD: ChiTietBaoDuong_DTO;
    listCTBD: ChiTietBaoDuong_DTO[] = [];
    filterInput: ChiTietBaoDuong_DTO = new ChiTietBaoDuong_DTO();


    ngOnInit(): void {
        this.show = false;
        this.baoDuongForm = this.formBuilder.group({
            code: [{ value: this.inputModel.bD_CODE, disabled: this.editPageState === "add" ? false : true }, [Validators.required, this.noWhitespaceValidator, Validators.pattern(/^[a-zA-Z0-9^_]+$/)]],
            xe: [{ value: this.inputModel.xE_ID, disabled: this.editPageState === "add" ? false : true }, [Validators.required, this.noWhitespaceValidator]],
            garage: [{ value: this.inputModel.bD_GARAGE, disabled: this.editPageState === "view" ? true : false }, [Validators.required, this.noWhitespaceValidator]],
            address: [{ value: this.inputModel.bD_ADDRESS, disabled: this.editPageState === "view" ? true : false }, [Validators.required, this.noWhitespaceValidator]],
            fromDT: [{ value: this.bd_FROM_DT, disabled: this.editPageState === "view" ? true : false }, [Validators.required]],
            toDT: [{ value: this.bd_TO_DT, disabled: this.editPageState === "view" ? true : false }, [Validators.required]],
        });
        this.initData();
    }

    noWhitespaceValidator(control: FormControl) {
        const isWhitespace = (control.value || '').trim().length === 0;
        const isValid = !isWhitespace;
        return isValid ? null : { 'whitespace': true };
    }

    initData() {
        let xe = new XE_DTO();
        xe.autH_STATUS = 'A';
        this.xeService.xE_Search(xe).subscribe(result => {
            this.xeList = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            if (this.editPageState === "view" || this.editPageState === "update") {
                this.loadData();
                this.getListCtBd();
            }
            this.show = true;
        })
        if (this.editPageState === "add") {
            this.bd_FROM_DT = moment().toISOString().substring(0, 10);
            this.bd_TO_DT = moment().toISOString().substring(0, 10);
        }
        
        // this.ntxService
        //     .nguoiThueXe_Search(this.ntx)
        //     .subscribe((result) => {
        //         this.primengTableHelper.totalRecordsCount = result.totalCount;
        //         this.ntxList = result.items;
        //         this.primengTableHelper.hideLoadingIndicator();
        //         this.show = true;
        //     });
    }

    loadData() {
        this.baoDuongService.bD_GetById(this.inputModel.bD_ID).subscribe(response => {
            this.inputModel = response[0];
            this.bd_FROM_DT = this.inputModel.bD_FROM_DT.toISOString().substring(0, 10);
            this.bd_TO_DT = this.inputModel.bD_TO_DT.toISOString().substring(0, 10);

            if (this.inputModel.autH_STATUS === null && this.inputModel.approvE_DT.toISOString().substring(0, 10) === '0000-12-31') {
                this.baoDuongService.isUpdating(this.inputModel.bD_CODE).subscribe((response) => {
                    this.isUpdating = response
                })
            }
            console.log(this.xeList);
            this.xeService.xE_ByID(this.inputModel.xE_ID).subscribe(response => {
                this.xeList.unshift(response);
                console.log(this.xeList);
            })
        })
    }

    constructor(injector: Injector, private formBuilder: FormBuilder, private xeService: XeServiceProxy,
        private baoDuongService: BaoDuongServiceProxy,
        private ctbdService: CTBDServiceProxy, private exportService:ExcelExportService) {
        super(injector);
        this.inputModel.bD_ID = this.getRouteParam('id');
        this.inputModel.xE_ID = this.getRouteParam('xeId');
        this.editPageState = this.getRouteData('editPageState');
        this.show = false;
    }

    approve() {
        if (this.inputModel.approvE_DT.toISOString().substring(0, 10) !== '0000-12-31') {
            this.message.confirm(
                this.l('MaintenanceApproveDeleteWarningMessage', this.inputModel.bD_CODE),
                this.l('AreYouSure'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.baoDuongService.bD_Approve_Delete(this.inputModel.bD_ID).subscribe(
                            (response) => {
                                if (response.length === 0) {
                                    this.navigate(['/app/admin/bao-duong-approve']);
                                    this.message.success(this.l("SuccessfullyDeleteApproved"));
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    }
                }
            );
        }
        else {
            this.baoDuongService.isUpdating(this.inputModel.bD_CODE).subscribe(result => {
                if (result) {
                    //Duyệt update
                    this.message.confirm(
                        this.l('MaintenanceApproveUpdateWarningMessage', this.inputModel.bD_CODE),
                        this.l('AreYouSure'),
                        (isConfirmed) => {
                            if (isConfirmed) {
                                this.baoDuongService.bD_Approve_Update(this.inputModel.bD_CODE).subscribe(
                                    (response) => {
                                        if (response.length === 0) {
                                            this.navigate(['/app/admin/bao-duong-approve']);
                                            this.message.success(this.l("SuccessfullyUpdateApproved"));
                                        } else {
                                            this.message.error(response["ERROR_DESC"]);
                                        }
                                    });
                            }
                        }
                    );

                } else {
                    //Duyệt add new
                    this.message.confirm(
                        this.l('MaintenanceApproveInsertWarningMessage', this.inputModel.bD_CODE),
                        this.l('AreYouSure'),
                        (isConfirmed) => {
                            if (isConfirmed) {
                                this.baoDuongService.bD_Approve_Insert(this.inputModel.bD_ID).subscribe(
                                    (response) => {
                                        if (response.length === 0) {
                                            this.navigate(['/app/admin/bao-duong-approve']);
                                            this.message.success(this.l("SuccessfullyApproved"));
                                        } else {
                                            this.message.error(response["ERROR_DESC"]);
                                        }
                                    });
                            }
                        }
                    );

                }
            })
        }
    }

    disapprove() {
        if (this.inputModel.approvE_DT.toISOString().substring(0, 10) !== '0000-12-31') {
            this.message.confirm(
                this.l('MaintenanceDenyDeleteWarningMessage', this.inputModel.bD_CODE),
                this.l('AreYouSure'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.baoDuongService.bD_Deny_Delete(this.inputModel.bD_ID).subscribe(
                            (response) => {
                                if (response.length === 0) {
                                    this.navigate(['/app/admin/bao-duong-approve']);
                                    this.message.success(this.l("SuccessfullyDeleteDisApproved"));
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    }
                }
            );
        }
        else {
            this.baoDuongService.isUpdating(this.inputModel.bD_CODE).subscribe(result => {
                if (result) {
                    //Duyệt update
                    this.message.confirm(
                        this.l('MaintenanceDenyUpdateWarningMessage', this.inputModel.bD_CODE),
                        this.l('AreYouSure'),
                        (isConfirmed) => {
                            if (isConfirmed) {
                                this.baoDuongService.bD_Deny_Update(this.inputModel.bD_CODE).subscribe(
                                    (response) => {
                                        if (response.length === 0) {
                                            this.navigate(['/app/admin/bao-duong-approve']);
                                            this.message.success(this.l("SuccessfullyUpdateDisApproved"));
                                        } else {
                                            this.message.error(response["ERROR_DESC"]);
                                        }
                                    });
                            }
                        }
                    );
                } else {
                    //Duyệt add new
                    this.message.confirm(
                        this.l('MaintenanceDenyInsertWarningMessage', this.inputModel.bD_CODE),
                        this.l('AreYouSure'),
                        (isConfirmed) => {
                            if (isConfirmed) {
                                this.baoDuongService.bD_Deny_Insert(this.inputModel.bD_ID).subscribe(
                                    (response) => {
                                        if (response.length === 0) {
                                            this.navigate(['/app/admin/bao-duong-approve']);
                                            this.message.success(this.l("SuccessfullyDisApproved"));
                                        } else {
                                            this.message.error(response["ERROR_DESC"]);
                                        }
                                    });
                            }
                        }
                    );
                }
            })
        }
    }

    goToDetailPage() {
        this.navigate(['/app/admin/bao-duong-detail', this.inputModel.bD_ID]);
    }
    save() {
        this.submitted = true;
        if (this.baoDuongForm.invalid) {
            this.message.error(this.l('Group11FormInvalid'));
            return;
        }


        this.inputModel.bD_CODE = this.baoDuongForm.getRawValue().code.trim();
        this.inputModel.xE_ID = this.baoDuongForm.getRawValue().xe;
        this.inputModel.bD_GARAGE = this.baoDuongForm.value.garage.trim();
        this.inputModel.bD_ADDRESS = this.baoDuongForm.value.address.trim();


        this.inputModel.bD_FROM_DT = moment(this.baoDuongForm.value.fromDT, "YYYY-MM-DD").add('days', 1);
        this.inputModel.bD_TO_DT = moment(this.baoDuongForm.value.toDT, "YYYY-MM-DD").add('days', 1);

        if (this.inputModel.bD_FROM_DT.isAfter(this.inputModel.bD_TO_DT)) {
            this.message.error(this.l('InvalidFinishDateError'));
            this.baoDuongForm.get('toDT').setErrors({ beforeFromDT: true });
            return;
        }
        this.show = false;

        if (this.editPageState === 'add') {
            this.baoDuongService.bD_Insert(this.inputModel).subscribe(response => {
                if (response.length === 0) {
                    this.message.success(this.l('AddNewSuccessMessage'));
                    this.navigate(['/app/admin/bao-duong-list']);
                }
                else {
                    this.message.error(this.l('ExistedMaintenanceCodeError'));
                }
                this.show = true;
            })
        } else if (this.editPageState === 'update') {
            this.baoDuongService.isUpdating(this.inputModel.bD_CODE).subscribe(
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
                            this.baoDuongService.bD_Update(this.inputModel).subscribe(response => {
                                if (response.length === 0) {
                                    this.message.success(this.l('InQueueUpdateMessage'));
                                    this.navigate(['/app/admin/bao-duong-list']);
                                }
                                else {
                                    this.message.error(response['ERROR_DESC']);
                                }
                                this.loadData();
                            })
                        }
                    }
                }
            )
            this.show = true;
        }
    }

    delete() {
        this.message.confirm(
            this.l('MaintenanceDeleteWarningMessage', this.inputModel.bD_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.baoDuongService.isUpdating(this.inputModel.bD_CODE).subscribe(
                        response => {
                            if (response || this.inputModel.autH_STATUS === null) {
                                this.message.error(this.l('WaitingForApproveMessage'));
                            }
                            else {
                                this.show = false;
                                this.baoDuongService.bD_Delete(
                                    this.inputModel.bD_ID
                                ).subscribe((response) => {
                                    if (response.length === 0) {
                                        this.message.success(this.l("InQueueDeleteMessage"));
                                        this.navigate(['/app/admin/bao-duong-list']);
                                    } else {
                                        this.message.error(response["ERROR_DESC"]);
                                    }
                                });
                            }
                        }
                    )
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

    get f() { return this.baoDuongForm.controls; }




    //CTBD
    addCTBD() {
        this.navigate(["/app/admin/ctbd-add", this.inputModel.bD_ID]);
    }

    detailCTBD() {
        if (this.selectedCTBD != null) {
            this.navigate([
                "/app/admin/ctbd-detail",
                this.selectedCTBD.ctbD_ID,
            ]);
        } else this.message.error(this.l('Group10_DETAIL_CHOSE'));
    }
    getListCtBd() {
        this.ctbdService
        .chiTietBaoDuong_GetApprove_ByBD_ID(this.inputModel.bD_ID)
        .subscribe((result) => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }
    searchCTBD() {
        this.ctbdService
            .chiTietBaoDuong_Search(this.filterInput.ctbD_NAME, this.inputModel.bD_ID)
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });

    }

    updateCTBD() {
        if (this.selectedCTBD != null) {
            this.navigate(["/app/admin/ctbd-edit", this.selectedCTBD.ctbD_ID]);
        } else {
            this.message.error(this.l('Group10_UPDATE_CHOSE'));
        }
    }
    deleteCTBD() {
        if (this.selectedCTBD == null) {
            this.message.error(this.l('Group10_DELETE_CHOSE'));
        } else {
            this.message.confirm(
                this.l('AreYouSure'),
                    this.l('Group10_DELETE_WARNING'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.ctbdService.isUpdating(this.selectedCTBD.ctbD_CODE).subscribe(
                            response => {
                                if (response || this.selectedCTBD.autH_STATUS === null) {
                                    this.message.error(this.l('WaitingForApproveMessage'));
                                }
                                else {
                                    this.ctbdService.chiTietBaoDuong_Delete(
                                        this.selectedCTBD
                                    ).subscribe((response) => {
                                        if (response["RESULT"] == "0") {
                                            this.message.success(this.l('Group10_DELETE_APPROVE'));
                                            this.getListCtBd();
                                        } else {
                                            this.message.error(response["ERROR_DESC"]);
                                        }
                                    });
                                }
                            }
                        )
                    }
                }
            );
        }
    }

    goToCTBDApprovePage() {
        this.navigate(['/app/admin/ctbd-approve', this.inputModel.bD_ID]);
    }
    //gr10
    exportList: any[];
    export()
    {
        var i = 1;
        this.exportList =[];
        this.primengTableHelper.records.forEach(element => {
            var a = {
                stt: i.toString(),
                madichvu:element.ctbD_CODE,
                tendichvu: element.ctbD_NAME,
                soluong: element.ctbD_QUANTITY,
                dongia: element.ctbD_UNIT_PRICE +" VNĐ",
                thanhtien: element.ctbD_TOTAL_PRICE + " VNĐ",
                mabaoduong: this.inputModel.bD_ID
            }
            this.exportList.push(a);
            i++;

        });
        if (this.exportList.length == 0) {
            this.message.error(this.l('Group10_EXCEL_FAIL'));
        }
        else {
            this.message.confirm(
                this.l('Group10_EXCEL_REQUEST'),
                this.l('Group10_EXCEL_COMPLETE'),

                (isConfirmed) => {
                    if (isConfirmed) {
                        this.exportService.generateExcelChiTietBaoDuong(this.exportList,this.inputModel.xE_ID, this.inputModel.bD_PRICE,this.inputModel.bD_GARAGE, this.inputModel.bD_ADDRESS,this.inputModel.bD_FROM_DT, this.inputModel.bD_ID)


                    }
                }
            );


        }

    }
}
