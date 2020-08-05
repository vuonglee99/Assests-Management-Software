import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { BD_DTO, BaoDuongServiceProxy } from "@shared/service-proxies/service-proxies";
import * as moment from "moment";


@Component({
    templateUrl: './baoduong-approve.component.html',
    //styleUrls: ['./baoduong-list.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class BaoDuongApproveComponent extends AppComponentBase implements OnInit {

    /**
     *
     */
    constructor(injector: Injector,
        private baoDuongService: BaoDuongServiceProxy) {
        super(injector);
    }

    loading: boolean = false;
    selectedBD: BD_DTO = new BD_DTO();
    filterInput: BD_DTO = new BD_DTO();
    cols: any[];
    indexs: number = 0;
    bD_TO_DT: string;
    bD_FROM_DT: string;
    ngOnInit(): void {
        this.loading = true;
        this.bD_FROM_DT = null;
        this.bD_TO_DT = null;
        this.search();
    }

    addNew() {
        this.navigate(['/app/admin/bao-duong-add']);
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
        this.filterInput.bD_FROM_DT = moment(this.bD_FROM_DT !== null ? this.bD_FROM_DT : '1753-01-01', "YYYY-MM-DD").add("days", 1);
        this.filterInput.bD_TO_DT = moment(this.bD_TO_DT != null ? this.bD_TO_DT : '9999-12-31', "YYYY-MM-DD").add("days", 1);
        this.baoDuongService.getAll(new BD_DTO()).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            this.bD_TO_DT = this.filterInput.bD_TO_DT.toISOString().substring(0, 10);
            this.bD_FROM_DT = this.filterInput.bD_FROM_DT.toISOString().substring(0, 10);
            this.loading = false;
        })
    }

    viewDetail() {
        if (!this.selectedBD.bD_ID) { this.message.info(this.l('ChooseMaintenanceToViewDetailMessage')) }
        this.navigate(['/app/admin/bao-duong-view', this.selectedBD.bD_ID]);
    }

    approve() {
        if (!this.selectedBD.bD_ID) {
            this.message.info(this.l('ChooseMaintenanceToApproveMessage'));
        }
        else if (this.selectedBD.autH_STATUS !== null) {
            this.message.info(this.l('ChooseAlreadyApprovedMessage'));
        }
        else {
            if (this.selectedBD.approvE_DT.toISOString().substring(0, 10) !== '0000-12-31') {
                this.message.confirm(
                    this.l('MaintenanceApproveDeleteWarningMessage', this.selectedBD.bD_CODE),
                    this.l('AreYouSure'),
                    (isConfirmed) => {
                        if (isConfirmed) {
                            this.baoDuongService.bD_Approve_Delete(this.selectedBD.bD_ID).subscribe(
                                (response) => {
                                    if (response.length === 0) {
                                        this.search();
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
                this.baoDuongService.isUpdating(this.selectedBD.bD_CODE).subscribe(result => {
                    if (result) {
                        //Duyệt update
                        this.message.confirm(
                            this.l('MaintenanceApproveUpdateWarningMessage', this.selectedBD.bD_CODE),
                            this.l('AreYouSure'),
                            (isConfirmed) => {
                                if (isConfirmed) {
                                    this.baoDuongService.bD_Approve_Update(this.selectedBD.bD_CODE).subscribe(
                                        (response) => {
                                            if (response.length === 0) {
                                                this.search();
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
                            this.l('MaintenanceApproveInsertWarningMessage', this.selectedBD.bD_CODE),
                            this.l('AreYouSure'),
                            (isConfirmed) => {
                                if (isConfirmed) {
                                    this.baoDuongService.bD_Approve_Insert(this.selectedBD.bD_ID).subscribe(
                                        (response) => {
                                            if (response.length === 0) {
                                                this.search();
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
    }

    goToListPage() {
        this.navigate(['/app/admin/bao-duong-list']);
    }

    disapprove() {
        if (!this.selectedBD.bD_ID) {
            this.message.info(this.l('ChooseMaintenanceToApproveMessage'));
        }
        else if (this.selectedBD.autH_STATUS !== null) {
            this.message.info(this.l('ChooseAlreadyApprovedMessage'));
        }
        else {
            if (this.selectedBD.approvE_DT.toISOString().substring(0, 10) !== '0000-12-31') {
                this.message.confirm(
                    this.l('MaintenanceDenyDeleteWarningMessage', this.selectedBD.bD_CODE),
                    this.l('AreYouSure'),
                    (isConfirmed) => {
                        if (isConfirmed) {
                            this.baoDuongService.bD_Deny_Delete(this.selectedBD.bD_ID).subscribe(
                                (response) => {
                                    if (response.length === 0) {
                                        this.search();
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
                this.baoDuongService.isUpdating(this.selectedBD.bD_CODE).subscribe(result => {
                    if (result) {
                        //Duyệt update
                        this.message.confirm(
                            this.l('MaintenanceDenyUpdateWarningMessage', this.selectedBD.bD_CODE),
                            this.l('AreYouSure'),
                            (isConfirmed) => {
                                if (isConfirmed) {
                                    this.baoDuongService.bD_Deny_Update(this.selectedBD.bD_CODE).subscribe(
                                        (response) => {
                                            if (response.length === 0) {
                                                this.search();
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
                            this.l('MaintenanceDenyInsertWarningMessage', this.selectedBD.bD_CODE),
                            this.l('AreYouSure'),
                            (isConfirmed) => {
                                if (isConfirmed) {
                                    this.baoDuongService.bD_Deny_Insert(this.selectedBD.bD_ID).subscribe(
                                        (response) => {
                                            if (response.length === 0) {
                                                this.search();
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
    }
}