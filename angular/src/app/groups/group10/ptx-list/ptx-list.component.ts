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
    PTXServiceProxy,
    XeServiceProxy,
    NTXServiceProxy,
    PhieuThueXe_DTO,
} from "@shared/service-proxies/service-proxies";
import { Paginator } from "primeng/components/paginator/paginator";
import { Table } from "primeng/components/table/table";
import { LazyLoadEvent } from "primeng/components/common/lazyloadevent";
import { indexOf } from "lodash";
import { ExcelExportService } from "../service/excelexport.service";
import * as moment from "moment";

@Component({
    templateUrl: "./ptx-list.component.html",
    styleUrls: ["../ntx-list/ntx-list.component.css"],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class PtxListComponent extends AppComponentBase implements OnInit {
    @ViewChild("dataTable") dataTable: Table;
    @ViewChild("paginator") paginator: Paginator;
    /**
     *
     */
    records: PhieuThueXe_DTO[] = [];
    cols: any[];
    ntxCode: string = null;
    ntxName: string = null;
    xeCode: string = null;
    xeName: string = null;
    license: string = null;
    selectedPTX: PhieuThueXe_DTO;

    listSelectedPtx: PhieuThueXe_DTO[] = [];

    filterInput: PhieuThueXe_DTO = new PhieuThueXe_DTO();

    constructor(injector: Injector, private ptxService: PTXServiceProxy, private ntxService: NTXServiceProxy,  private xeService: XeServiceProxy, private exportService: ExcelExportService) {
        super(injector);
    }

    ngOnInit(): void {
        this.ntxCode = null;
        this.xeName = null;
        this.xeCode = null;
        this.ntxName = null;
        this.license = null;

        this.search();

        // this.cols = [
        //     { field: "ptX_ID", name: "STT", header: "STT" },
        //     { field: "ntX_ID", name: "ntX_ID", header: "Người thuê xe" },
        //     { field: "xE_ID", name: "xE_ID", header: "Xe" },
        //     { field: "ptX_RENT_DT", name: "ptX_RENT_DT", header: "Ngày thuê" },
        //     { field: "ptX_EXP_DT", name: "ptX_EXP_DT", header: "Hạn trả" },
        //     { field: "ptX_RETURN_DT", name: "ptX_RETURN_DT", header: "Ngày trả" },
        //     { field: "ptX_PRICE", name: "ptX_PRICE", header: "Giá thuê" },
        // ];

    }

    selectRow() {
        console.log(this.selectedPTX);
    }

    add() {
        this.navigate(["/app/admin/thue-xe-add"]);
    }

    detail() {
        if (this.selectedPTX == null) {
            this.message.error(this.l('Group10_DETAIL_CHOSE'));
        } else {
            this.navigate(["/app/admin/thue-xe-detail", this.selectedPTX.ptX_CODE]);
        }
    }

    delete() {
        if (this.selectedPTX == null) {
            this.message.error(this.l('Group10_DELETE_CHOSE'));
        } else {
            this.message.confirm(
                this.l('AreYouSure'),
                this.l('Group10_DELETE_WARNING'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.ptxService
                            .phieuThueXe_Delete(this.selectedPTX.ptX_ID)
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
        this.ptxService
            .phieuThueXe_Search(this.ntxCode?this.ntxCode:'',this.ntxName?this.ntxName:'',this.xeCode?this.xeCode:'',this.xeName?this.xeName:'',this.license?this.license:'')
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                    result.items.forEach((i,index)=>{
                    this.ntxService.nguoiThueXe_ById(i.ntX_ID).subscribe(x=>{
                        result.items[index].ntX_ID = x.ntX_NAME;
                    });
                    result.items.forEach((i,index)=>{
                        this.xeService.xE_ByID(i.xE_ID).subscribe(x=>{
                           result.items[index].xE_ID = x.xE_LICENSE_PLATE;
                       });
                    });
                // this.primengTableHelper.records.forEach((i,index)=>{
                //      this.ntxService.nguoiThueXe_ById(i.ntX_ID).subscribe(x=>{
                //         this.primengTableHelper.records[index].ntX_ID = x.ntX_NAME;
                //     });
                    // this.xeService.cM_XE_ById(i.xE_ID).subscribe(x=>{
                    //     this.primengTableHelper.records[index].ntX_ID = x.xE_NAME;
                    // })
                });

                this.primengTableHelper.hideLoadingIndicator();
                this.records = result.items;

            },()=>{},()=>{
                this.primengTableHelper.records = this.records ;
            });
        console.log(this.records);
    }
    exportList: any[];
    export() {

        //const header = ['stt', 'nguoithue', 'xe', 'ngaythue', 'hantra', 'ngaytra', 'giathue'];

        //   ntxCode: string = null;
        //   ntxName: string = null;
        //   xeCode: string = null;
        //   xeName: string = null;
        //   license: string = null;
        let i = 1;
        this.exportList = []
        this.primengTableHelper.records.forEach(element => {


            this.ntxService.nguoiThueXe_ById(element.ntX_ID).subscribe(x => {
                element.ntX_ID = x.ntX_NAME;
            });

                this.xeService.xE_ByID(element.xE_ID).subscribe(x => {
                    element.xE_ID = x.xE_LICENSE_PLATE;
                });

            console.log("Giá trị i: " + i);
            console.log(element);

            var a = {
                stt: i.toString(),
                nguoithue: element.ntX_ID,
                xe: element.xE_ID,
                ngaythue: moment(element.ptX_RENT_DT).format("DD/MM/YYYY"),
                hantra: moment(element.ptX_EXP_DT).format("DD/MM/YYYY"),
                ngaytra: moment(element.ptX_RETURN_DT).format("DD/MM/YYYY"),
                giathue: element.ptX_PRICE + " VNĐ",


            }
            this.exportList.push(a);
            i = i+1;
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
                        this.exportService.generateExcelDstx(this.exportList);

                    }
                }
            );


        }
        // console.log(this.exportList);
        this.search();
    }
}
