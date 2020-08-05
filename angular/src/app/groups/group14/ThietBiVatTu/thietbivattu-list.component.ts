import { ViewEncapsulation, Component, Injector, OnInit, ChangeDetectionStrategy, ViewChild } from "@angular/core";
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { NhanVienServiceProxy, NHANVIEN_DTO, NHANVIEN_FILTER, ThietBiVatTuServiceProxy,THIETBIVATTU_DTO } from "@shared/service-proxies/service-proxies";
import { NhaCungUngServiceProxy, NHACUNGUNG_DTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { FileDownloadService } from "@shared/utils/file-download.service";


@Component({
    templateUrl: './thietbivattu-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../PhieuCapPhatTBVT/PhieuCapPhatTBVT-component.css']
})
export class ThietBiVatTuListComponent extends AppComponentBase implements OnInit{
    
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    //Filters
    maTBVT = null;
    tenTBVT = null;
    loai  = null;
    ngayMua = null;
    serial=null;
    tinhTrang=null;

    skip: number | undefined;
    take: number | undefined;
    
    sorting = null;
    desc: boolean | undefined;

   // trangThaiList: number[] = [0, 1]
    advancedFiltersAreShown;

    
    selectedItem: THIETBIVATTU_DTO= new THIETBIVATTU_DTO();
    records: THIETBIVATTU_DTO[] = [];
    filterInput: THIETBIVATTU_DTO = new THIETBIVATTU_DTO();

    constructor(
        injector: Injector,
        
        private thietBiVatTuService:ThietBiVatTuServiceProxy,
        private fdlService:FileDownloadService) 
    {
        super(injector);
        
    }
    getThietBiVatTu(event?: LazyLoadEvent) {
        // if (this.primengTableHelper.shouldResetPaging(event)) {
        //     this.paginator.changePage(0);
        //     return;
        // }
        this.selectedItem=null;
        this.primengTableHelper.showLoadingIndicator();
        
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
        let filters: THIETBIVATTU_DTO = new THIETBIVATTU_DTO();
        filters.tbvT_MA_TBVT = this.maTBVT;
        filters.tbvT_TEN = this.tenTBVT;
        filters.tbvT_LOAI = this.loai;
        filters.tbvT_SERIAL = this.serial;
        filters.tbvT_NGAY_MUA = this.ngayMua;
        filters.tbvT_TINH_TRANG_THIET_BI=this.tinhTrang;
    

        this.thietBiVatTuService.searchThietBiVatTu(
            filters,
            this.sorting,
            this.desc,
            0,
            100000
        ).subscribe(result => {
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }
    ngOnInit(): void{
        this.getThietBiVatTu();
    }
    createTBVT()
    {
        this.navigate(['/app/admin/thietbivattu-add']);
    }
    updateTBVT()
    {
        this.navigate(['/app/admin/thietbivattu-edit/'+this.selectedItem.tbvT_MA_TBVT]);
    }
    detailsTBVT()
    {
        this.navigate(['/app/admin/thietbivattu-view/'+this.selectedItem.tbvT_MA_TBVT]);
    }
    // reloadPage(): void {
    //     this.paginator.changePage(this.paginator.getPage());
    // }
    exportToExcel(): void {
   
            this.thietBiVatTuService.exportThietBiVatTu(this.filterInput)
            .subscribe(result => {
                this.fdlService.downloadTempFile(result);
            });
        
           
        }
    deleteThietBiVatTu(): void {
        this.message.confirm(
            this.l('EquipmentDeleteWarningMessage', this.selectedItem.tbvT_MA_TBVT),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.thietBiVatTuService.deleteThietBiVatTu(this.selectedItem.tbvT_MA_TBVT)
                        .subscribe(() => {
                            this.getThietBiVatTu()
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

}