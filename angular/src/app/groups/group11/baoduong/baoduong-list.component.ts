import { ViewEncapsulation, Component, Injector, OnInit, ViewChild, ElementRef } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { BD_DTO, BaoDuongServiceProxy, DateToStringOutput } from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
import * as XLSX from 'xlsx';
//import { parseDate } from "ngx-bootstrap";
import { toDate } from "@angular/common/src/i18n/format_date";
import { a } from "@angular/core/src/render3";


@Component({
    templateUrl: './baoduong-list.component.html',
    //styleUrls: ['./baoduong-list.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class BaoDuongListComponent extends AppComponentBase implements OnInit {

    /**
     *
     */
    constructor(injector: Injector,
        private baoDuongService: BaoDuongServiceProxy) {
        super(injector);
    }

    @ViewChild('TABLE') TABLE: ElementRef;
    loading: boolean = false;
    show: boolean = false;
    selectedBD: BD_DTO = new BD_DTO();
    filterInput: BD_DTO = new BD_DTO();
    cols: any[];
    indexs: number = 0;
    bD_TO_DT: string;
    bD_FROM_DT: string;
    ngOnInit(): void {
        this.loading = true;
        this.bD_FROM_DT = moment().toISOString().substring(0, 10);
        this.bD_TO_DT = moment().toISOString().substring(0, 10);
        this.filterInput.recorD_STATUS = '1';
        this.getAll();
    }

    addNew() {
        this.navigate(['/app/admin/bao-duong-add']);
    }


    delete() {
        if (!this.selectedBD.bD_ID) {
            this.message.info(this.l("ChooseMaintenanceToDeleteMessage"));
        }
        else if (this.selectedBD.autH_STATUS === null) {
            this.message.error(this.l('WaitingForApproveMessage'));
        }
        else {
            this.message.confirm(
                this.l('MaintenanceDeleteWarningMessage', this.selectedBD.bD_CODE),
                this.l('AreYouSure'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.baoDuongService.isUpdating(this.selectedBD.bD_CODE).subscribe(
                            response => {
                                if (response) {
                                    this.message.error(this.l('WaitingForApproveMessage'));
                                }
                                else {
                                    this.loading = true;
                                    this.baoDuongService.bD_Delete(
                                        this.selectedBD.bD_ID
                                    ).subscribe((response) => {
                                        if (response.length === 0) {
                                            this.message.success("Yêu cầu xóa đã được thêm vào danh sách duyệt.");
                                            this.getAll();
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

    update() {
        if (!this.selectedBD.bD_ID) { this.message.info(this.l('ChooseMaintenanceToViewDetailMessage')) }
        this.navigate(['/app/admin/bao-duong-detail', this.selectedBD.bD_ID]);
    }

    getAll() {
        let filter = new BD_DTO();
        filter.bD_FROM_DT = moment('1753-01-01', "YYYY-MM-DD").add("days", 1);
        filter.bD_TO_DT = moment('9999-12-31', "YYYY-MM-DD").add("days", 1);
        this.baoDuongService.bD_Search(filter).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            this.loading = false;
        })
    }

    search() {
        this.filterInput.bD_FROM_DT = moment(this.bD_FROM_DT !== null ? this.bD_FROM_DT : '1753-01-01', "YYYY-MM-DD").add("days", 1);
        this.filterInput.bD_TO_DT = moment(this.bD_TO_DT != null ? this.bD_TO_DT : '9999-12-31', "YYYY-MM-DD").add("days", 1);
        this.baoDuongService.bD_Search(this.filterInput).subscribe(result => {
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

    exportToExcel(): void {
        //set width cells
        var wscols = [
            { wch: 6 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 }
        ];

        var headerRow = [this.l("MaintenanceNo"), this.l("MaintenanceCode"), this.l("MaintenanceCar"), this.l("MaintenanceGarage"), this.l("MaintenanceAddress"), this.l("MaintenanceFrom"), this.l("MaintenanceTo"), this.l("MaintenancePrice")];

        var arr = [];
        let i = 0;
        this.primengTableHelper.records.forEach((record) => {
            //     i++;
            //     let item = {
            //         "STT" : i,
            //         "Mã bảo dưỡng": record.bD_CODE,
            //         "Đơn vị bảo dưỡng": record.bD_GARAGE,
            //         "Địa điểm": record.bD_ADDRESS,
            //         "Từ ngày": record.bD_FROM_DT.format("DD/MM/YYYY"),
            //         "Đến ngày": record.bD_TO_DT.format("DD/MM/YYYY"),
            //         "Tổng chi phí(VND)": record.bD_PRICE 
            //     };

            // let item = {};
            // item[headerRow[0]] = i;
            // item[headerRow[1]] = record.bD_CODE;

            i++;
            let item = {};
            item[headerRow[0]] = i;
            item[headerRow[1]] = record.bD_CODE;
            item[headerRow[2]] = record.xE_ID;
            item[headerRow[3]] = record.bD_GARAGE;
            item[headerRow[4]] = record.bD_ADDRESS;
            item[headerRow[5]] = record.bD_FROM_DT.format("DD/MM/YYYY");
            item[headerRow[6]] = record.bD_TO_DT.format("DD/MM/YYYY");
            item[headerRow[7]] = record.bD_PRICE;
            arr.push(item);
        })

        const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(arr);
        const wb: XLSX.WorkBook = XLSX.utils.book_new();
        ws["!cols"] = wscols;
        XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
        XLSX.writeFile(wb, this.l('Group11Maintenance')+'.xlsx');
    }

    goToApprovePage() {
        this.navigate(['/app/admin/bao-duong-approve']);
    }

    onRowDblClick(event, rowData) { 
        this.navigate(['/app/admin/bao-duong-view', rowData.bD_ID]);
     }
}