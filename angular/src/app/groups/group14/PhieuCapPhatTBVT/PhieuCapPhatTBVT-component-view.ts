import { ViewEncapsulation, Component, Injector, OnInit, ChangeDetectionStrategy, ViewChild } from "@angular/core";
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { NhanVienServiceProxy, NHANVIEN_DTO, NHANVIEN_FILTER,NHANVIEN_DEP_NAME_DTO,PhieuCapPhatTBVTServiceProxy,PCPTBVT_DTO,PCPTBVT_SEARCH_DTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { P } from "@angular/core/src/render3";
import * as moment from 'moment';
import { FileDownloadService } from "@shared/utils/file-download.service";

@Component({
    templateUrl: './PhieuCapPhatTBVT-component-view.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./PhieuCapPhatTBVT-component.css']
})
export class PhieuCapPhatTBVTComponent extends AppComponentBase implements OnInit{
    
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    //Filters
    stt=null;
    maPCP = null;
    pbNhan = null;
    ngayTao = null;
    approve='';
    department: NHANVIEN_DEP_NAME_DTO[];
    selectedItem:PCPTBVT_DTO = new PCPTBVT_DTO();
     
    skip: number | undefined;
    take: number | undefined;
    
    sorting = null;
    desc: boolean | undefined;

    date1:Date;
    date2:Date;
    advancedFiltersAreShown;
     myMoment: moment.Moment = moment("someDate");
    phongBanList: string[];
    trangThaiList: number[] = [0, 1]
    records:PCPTBVT_DTO[] = [];
    filterInput: PCPTBVT_DTO = new PCPTBVT_DTO();
    cols = [
        { field: '', header: this.l('G14RowNumber') },
        { field: 'pcptbvT_MA_PCP', header: this.l('AllocationBillID') },
        { field: 'pcptbvT_DEP_NAME', header: this.l('AllocationBillDepartment') },
        { field: 'creatE_DT', header: this.l('AllocationBillCreateDate') },
        { field: 'autH_STATUS', header: this.l('AllocationBillApprovalStatus') },
         
    ];
    constructor(
        injector: Injector,
        private PCPService: PhieuCapPhatTBVTServiceProxy,
        private nhanVienServiceProxy:NhanVienServiceProxy,
        private fdlService:FileDownloadService) 
    {
        super(injector);
        // this.getAllDepartments();
    }
    ngAfterViewInit() {
        setTimeout(() => {
          this.nhanVienServiceProxy.getDepName().subscribe(result => {
            this.department = result;
          });
        });
      }
    ngOnInit(): void {
        
        this.getPCP();
    }
    searchPCP()
    {
        this.selectedItem=null;
        this.primengTableHelper.showLoadingIndicator();
        if (this.dataTable.sortField) {
            this.sorting = this.dataTable.sortField;
            if (this.dataTable.sortOrder === 1) {
                this.desc = false;
            } else if (this.dataTable.sortOrder === -1) {
                this.desc = true;
            }
        }

        // Filters
        let filters:PCPTBVT_SEARCH_DTO = new PCPTBVT_SEARCH_DTO();
  
        filters.pcptbvT_MA_PCP = this.maPCP;
        filters.pcptbvT_DEP_ID = this.pbNhan;
        //filters.approvE_DT=this.approve;
        filters.autH_STATUS = this.approve;
        filters.creatE_DT_START = this.date1 ? moment(this.date1) : null;
        filters.creatE_DT_END = this.date2 ? moment(this.date2) : null;
   

        this.PCPService.searchPCPTBVT(
            filters,
            this.sorting,
            this.desc,undefined,undefined
        ).subscribe(result => {
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.hideLoadingIndicator();
        });

    }
    getPCP(event?: LazyLoadEvent) {
     
        this.selectedItem=null;
        this.primengTableHelper.showLoadingIndicator();
   
        if (this.dataTable.sortField) {
            this.sorting = this.dataTable.sortField;
            if (this.dataTable.sortOrder === 1) {
                this.desc = false;
            } else if (this.dataTable.sortOrder === -1) {
                this.desc = true;
            }
        }

        // Filters
        let filters:PCPTBVT_DTO = new PCPTBVT_DTO();
        filters.pcptbvT_ID = this.stt;
        filters.pcptbvT_MA_PCP = this.maPCP;
        filters.pcptbvT_DEP_ID = this.pbNhan;
        filters.creatE_DT=this.ngayTao;
        //filters.approvE_DT=this.approve;
        filters.autH_STATUS=this.approve;
      

        this.PCPService.filterPCPTBVT(
            filters,
            this.sorting,
            this.desc,undefined,undefined
        ).subscribe(result => {
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.hideLoadingIndicator();
        });
        //  this.PCPService.getAllPCPTBVT().subscribe(result => {
        //     this.primengTableHelper.records = result;
        //     this.primengTableHelper.totalRecordsCount = result.length;
        //     this.primengTableHelper.hideLoadingIndicator();
        // });
    }
    selectRow(){
        console.log(this.selectedItem);
     }
  
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    deletePCP(Pcp:PCPTBVT_DTO): void {
        this.message.confirm(
            this.l('AllocationBillDeleteWarningMessage', this.selectedItem.pcptbvT_MA_PCP),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.PCPService.deletePCPTBVT(this.selectedItem.pcptbvT_MA_PCP)
                        .subscribe(() => {
                           this.getPCP();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                            
                        });
                }
            }
        );
    }
    createPCP()
    {
        this.navigate(['/app/admin/capphattbvt-add']);
    }
    updatePCP()
    {
        this.navigate(['/app/admin/capphattbvt-update/'+this.selectedItem.pcptbvT_MA_PCP]);
    }
    exportToExcel(): void {
        this.PCPService.exportPCPTBVT(this.filterInput)
            .subscribe(result => {
                this.fdlService.downloadTempFile(result);
            });
    }

    detailPCP()
    {   
        this.navigate(['/app/admin/chitietcapphattbvt/'+this.selectedItem.pcptbvT_MA_PCP]);
    }
    // createNhanVien() {
    //     this.navigate(['/app/admin/chitietnhanvien-add']);
    // }


    details() {
        if (this.selectedItem == undefined || this.selectedItem == null ) {
          this.message.warn("Vui lòng chọn phiếu cấp phát", "Thông báo")
        } else {
          this.navigate([`/app/admin/chitietcapphattbvt/${this.selectedItem.pcptbvT_MA_PCP}`])
        }
    }
}