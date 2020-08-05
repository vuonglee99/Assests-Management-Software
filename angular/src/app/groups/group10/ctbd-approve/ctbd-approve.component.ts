import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
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
    BD_DTO,
    BaoDuongServiceProxy,
} from "@shared/service-proxies/service-proxies";
import * as moment from "moment";


@Component({
    templateUrl: './ctbd-approve.component.html',
    //styleUrls: ['./baoduong-list.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class CtbdApproveComponent extends AppComponentBase implements OnInit {

    /**
     *
     */

    loading: boolean = false;
    selectedCTBD: ChiTietBaoDuong_DTO = new ChiTietBaoDuong_DTO();
    filterInput: ChiTietBaoDuong_DTO = new ChiTietBaoDuong_DTO();
    inputModel: BD_DTO = new BD_DTO();
    cols: any[];
    indexs: number = 0;

    ngOnInit(): void {
        this.loading = true;

        this.search();
    }

    constructor(injector: Injector, private xeService: XeServiceProxy,
        private baoDuongService: BaoDuongServiceProxy,
        private ctbdService: CTBDServiceProxy) {
        super(injector);
        this.inputModel.bD_ID = this.getRouteParam('id');
    }

    loadData() {
        this.baoDuongService.bD_GetById(this.inputModel.bD_ID).subscribe(response => {
            this.inputModel = response[0];
        })

    }


    // delete() {
    //     if (!this.selectedBD.bD_ID) {
    //         this.message.info(this.l('ChooseMaintenanceToDeleteMessage'));
    //     }
    //     else {
    //         this.message.confirm(
    //             this.l('MaintenanceDeleteWarningMessage', this.selectedBD.bD_CODE),
    //             this.l('AreYouSure'),
    //             (isConfirmed) => {
    //                 if (isConfirmed) {
    //                     this.loading = true;
    //                     this.baoDuongService.bD_Delete(
    //                         this.selectedBD.bD_ID
    //                     ).subscribe((response) => {
    //                         if (response.length === 0) {
    //                             this.search();
    //                             this.message.success(this.l("SuccessfullyDeleted"));
    //                         } else {
    //                             this.message.error(response["ERROR_DESC"]);
    //                         }
    //                     });
    //                 }
    //             }
    //         );
    //     }
    // }

    search() {
        this.ctbdService
        .chiTietBaoDuong_ByBD_ID(this.inputModel.bD_ID)
        .subscribe((result) => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            this.loading = false;
        });
    }

    detailCTBD() {
        if (this.selectedCTBD != null) {
            this.navigate([
                "/app/admin/ctbd-detail",
                this.selectedCTBD.ctbD_ID,
            ]);
        } else this.message.error(this.l('Group10_DELETE_CHOSE'));
    }

    approve() {
        console.log(this.selectedCTBD.approvE_DT);
        if (!this.selectedCTBD.ctbD_ID) {
            this.message.info(this.l('ChooseMaintenanceToApproveMessage'));
        }
        else if (this.selectedCTBD.autH_STATUS !== null) {
            this.message.info(this.l('ChooseAlreadyApprovedMessage'));
        }
        else {
            if (this.selectedCTBD.approvE_DT.toISOString().substring(0, 10) !== '0000-12-31') {
                console.log('delete');
                this.ctbdService.chiTietBaoDuong_Approve_Delete(this.selectedCTBD.ctbD_CODE).subscribe(
                    (response) => {
                        if (response.length === 0) {
                            this.search();
                            this.message.success(this.l("SuccessfullyDeleteApproved"));
                        } else {
                            this.message.error(response["ERROR_DESC"]);
                        }
                    });
            }
            else {
                this.ctbdService.isUpdating(this.selectedCTBD.ctbD_CODE).subscribe(result => {
                    if (result) {
                        //Duyệt update
                        this.ctbdService.chiTietBaoDuong_Approve_Update(this.selectedCTBD.ctbD_CODE).subscribe(
                            (response) => {
                                if (response.length === 0) {
                                    this.search();
                                    this.message.success(this.l("SuccessfullyUpdateApproved"));
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    } else {
                        //Duyệt add new
                        console.log('insert');
                        this.ctbdService.chiTietBaoDuong_Approve_Insert(this.selectedCTBD.ctbD_ID).subscribe(
                            (response) => {
                                console.log(response.length);
                                if (response.length === 0) {
                                    this.search();
                                    this.message.success(this.l("SuccessfullyApproved"));
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    }
                })
            }
        }
    }

    goToListPage() {
        this.navigate(['/app/admin/bao-duong-view', this.inputModel.bD_ID]);
    }

    disapprove() {

        if (!this.selectedCTBD.ctbD_ID) {
    
            this.message.info(this.l('ChooseMaintenanceToApproveMessage'));
        }
        else if (this.selectedCTBD.autH_STATUS !== null) {
            this.message.info(this.l('ChooseAlreadyApprovedMessage'));
        }
        else {
            this.navigate(['/app/admin/ctbd-edit-approve', this.selectedCTBD.ctbD_ID]);
            this.message.info(this.l('Group10_REQUEST_REASON'));
            
        }
    }
}
