import { ViewEncapsulation, Component, Injector, OnInit, ChangeDetectionStrategy, ViewChild } from "@angular/core";
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { NhanVienServiceProxy, NHANVIEN_DTO, NHANVIEN_FILTER } from "@shared/service-proxies/service-proxies";
import { NhaCungUngServiceProxy, NHACUNGUNG_DTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { FileDownloadService } from "@shared/utils/file-download.service";


@Component({
    templateUrl: './nhacungung-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../PhieuCapPhatTBVT/PhieuCapPhatTBVT-component.css']
})
export class NhaCungUngListComponent extends AppComponentBase implements OnInit {
    
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    //Filters
    maNCU = null;
    tenNCU = null;
    diaChi = null;
    maSoThue = null;
    sdt=null;
    email=null;

    skip: number | undefined;
    take: number | undefined;
    
    sorting = null;
    desc: boolean | undefined;


    advancedFiltersAreShown;

    selectedItem:NHACUNGUNG_DTO = new NHACUNGUNG_DTO();
    
    records: NHACUNGUNG_DTO[] = [];
    filterInput: NHACUNGUNG_DTO = new NHACUNGUNG_DTO();

    constructor(
        injector: Injector,
        
        private nhaCungUngService:NhaCungUngServiceProxy,
        private fdlService:FileDownloadService) 
    {
        super(injector);
        
    }
    ngOnInit(): void {

        this.getNhaCungUng();
    }
    getNhaCungUng(event?: LazyLoadEvent) {
        // if (this.primengTableHelper.shouldResetPaging(event)) {
        //     this.paginator.changePage(0);
        //     return;
        // }

        this.primengTableHelper.showLoadingIndicator();
        this.selectedItem=null;
        // Number of records skipped
        // if (this.paginator.first)
        //     this.skip = this.paginator.first;
        // else if (!event)
        //     this.skip = 0;
        // else 
        //     this.skip = event.first;
        
        // // Number of rows shown
        // if (this.paginator.rows)
        //     this.take = this.paginator.rows;
        // else if (!event)
        //     this.take = 0;
        // else
        //     this.take = event.rows;

        // Table sorting
        if (this.dataTable.sortField) {
            this.sorting = this.dataTable.sortField;
            if (this.dataTable.sortOrder === 1) {
                this.desc = false;
            } else if (this.dataTable.sortOrder === -1) {
                this.desc = true;
            }
        }

        // Filters
        let filters: NHACUNGUNG_DTO = new NHACUNGUNG_DTO();
        filters.ncU_MA_NCU = this.maNCU;
        filters.ncU_TEN = this.tenNCU;
        filters.ncU_EMAIL_NGUOI_LIEN_HE = this.email;
        filters.ncU_SDT = this.sdt;
        filters.ncU_MA_SO_THUE = this.maSoThue;
        filters.ncU_DIA_CHI=this.diaChi;
    

        this.nhaCungUngService.searchNhaCungUng(
            filters,
            this.sorting,
            this.desc,
            undefined,undefined
            
        ).subscribe(result => {
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    insertNCU()
    {

    }
    // reloadPage(): void {
    //     this.paginator.changePage(this.paginator.getPage());
    // }

    deleteNhaCungUNg(): void {
        this.message.confirm(
            this.l('SupplierDeleteWarningMessage', this.selectedItem.ncU_MA_NCU),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.nhaCungUngService.deleteNhaCungUng(this.selectedItem.ncU_MA_NCU)
                        .subscribe(() => {
                            this.getNhaCungUng();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
    exportToExcel(): void {
    
            this.nhaCungUngService.exportNhaCungUng(this.filterInput)
            .subscribe(result => {
                this.fdlService.downloadTempFile(result);
            });
        
           
        }
    updateNCU()
    {
        this.navigate(['/app/admin/nha-cung-ung-edit/'+this.selectedItem.ncU_MA_NCU]);
    }
    detailsNCU()
    {
        this.navigate(['/app/admin/nha-cung-ung-view/'+this.selectedItem.ncU_MA_NCU]);
    }
    createNhaCungUng() {
        this.navigate(['/app/admin/nha-cung-ung-add']);
    }
}