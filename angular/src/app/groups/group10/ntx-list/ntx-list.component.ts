import {
    ViewEncapsulation,
    Component,
    Injector,
    OnInit,
    ViewChild,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {
    NTXServiceProxy,
    NguoiThueXe_DTO,
} from "@shared/service-proxies/service-proxies";
import { Paginator } from "primeng/components/paginator/paginator";
import { Table } from "primeng/components/table/table";
import { LazyLoadEvent } from "primeng/components/common/lazyloadevent";
import { ExcelExportService } from "../service/excelexport.service";
import * as moment from "moment";

@Component({
    templateUrl: "./ntx-list.component.html",
    styleUrls: ["./ntx-list.component.css"],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class NtxListComponent extends AppComponentBase implements OnInit {
    @ViewChild("dataTable") dataTable: Table;
    @ViewChild("paginator") paginator: Paginator;
    /**
     *
     */
    records: NguoiThueXe_DTO[] = [];
    cols: any[];

    selectedNTX: NguoiThueXe_DTO;

    listSelectedNtx: NguoiThueXe_DTO[] = [];

    filterInput: NguoiThueXe_DTO = new NguoiThueXe_DTO();

    constructor(injector: Injector, private ntxService: NTXServiceProxy, private exportService: ExcelExportService) {
        super(injector);
    }

    ngOnInit(): void {
        this.search();
    }

    selectRow() {
        console.log(this.selectedNTX);
    }

    add() {
        this.navigate(["/app/admin/ntx-add"]);
    }

    detail() {
        if (this.selectedNTX == null) {
            this.message.error(this.l('Group10_DETAIL_CHOSE'));
        } else {
            this.navigate(["/app/admin/ntx-detail", this.selectedNTX.ntX_ID]);
        }
    }
    update() {
        if (this.selectedNTX == null) {
            this.message.error(this.l('Group10_UPDATE_CHOSE'));
        } else {
            this.navigate(["/app/admin/ntx-edit", this.selectedNTX.ntX_ID]);
        }
    }

    delete() {
        if (this.selectedNTX == null) {
            this.message.error(this.l('Group10_DELETE_CHOSE'));
        } else {
            this.message.confirm(
                this.l('AreYouSure'),
                this.l('Group10_DELETE_WARNING'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.ntxService
                            .nguoiThueXe_Delete(this.selectedNTX.ntX_ID)
                            .subscribe((response) => {
                                if (response["RESULT"] == "0") {
                                    this.message.success(this.l('Group10_DELETE_COMPLETE'));
                                    this.reloadPage();
                                } else {
                                    this.message.error(response["ERROR_DESC"]);
                                }
                            });
                    }
                }
            );
        }
    }
    reloadPage(): void {
        this.search();
    }

    search() {
        this.ntxService
            .nguoiThueXe_Search(this.filterInput)
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });

        console.log(this.records);
    }

    exportList: any[];
    export() {

        let allowExport = 0;
        let i = 1;
        this.exportList = []
        this.primengTableHelper.records.forEach(element => {
            var a = {
                stt: i.toString(),
                hoten: element.ntX_NAME,
                diachi: element.ntX_ADDRESS,
                mantx: element.ntX_CODE,
                gioitinh: element.ntX_GENDER = "1" ? "Nam" : "Nữ",
                ngaysinh: moment(element.ntX_BIRTHDAY).format("DD/MM/YYYY"),
                cmnd: element.ntX_ID_CARD,
                gplx: element.ntX_LICENSE

            }
            this.exportList.push(a);
            i=i+1;
        });

        if (this.exportList.length == 0) {
            this.message.error( this.l('Group10_EXCEL_FAIL'));
        }
        else {
            this.message.confirm(
                this.l('Group10_EXCEL_REQUEST'),
                this.l('Group10_EXCEL_COMPLETE'),

                (isConfirmed) => {
                    if (isConfirmed) {
                        this.exportService.generateExcelNtx(this.exportList);
                        allowExport = 0;
                    }
                }
            );


        }



    }
}
