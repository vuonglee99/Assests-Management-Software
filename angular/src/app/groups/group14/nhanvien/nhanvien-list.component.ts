import { ViewEncapsulation, Component, Injector, OnInit, ChangeDetectionStrategy, ViewChild } from "@angular/core";
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { NhanVienServiceProxy, NHANVIEN_DTO, NHANVIEN_FILTER,NHANVIEN_DEP_NAME_DTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { FileDownloadService } from '@shared/utils/file-download.service';

@Component({
    templateUrl: './nhanvien-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../PhieuCapPhatTBVT/PhieuCapPhatTBVT-component.css']
})
export class NhanVienListComponent extends AppComponentBase  implements OnInit{
    
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    //Filters
    maNhanVien = null;
    tenNhanVien = null;
    phongBan = null;
    trangThai: number = null;

    skip: number | undefined;
    take: number | undefined;
    
    sorting = null;
    desc: boolean | undefined;

    selectedItem:NHANVIEN_DTO= new NHANVIEN_DTO();
    advancedFiltersAreShown;

    phongBanList: NHANVIEN_DEP_NAME_DTO[];
    trangThaiList: number[] = [0, 1]
    records: NHANVIEN_DTO[] = [];
    filterInput: NHANVIEN_FILTER = new NHANVIEN_FILTER();
    filter:NHANVIEN_DTO = new NHANVIEN_DTO();
    constructor(
        injector: Injector,
        private nhanVienService: NhanVienServiceProxy,
        private fdlService:FileDownloadService) 
    {
        super(injector);
        this.getAllDepartments();
    }
    exportToExcel(): void {
  
            this.nhanVienService.exportNhanVien(this.filter)
            .subscribe(result => {
                this.fdlService.downloadTempFile(result);
            });
        
           
        }
    getNhanVien(event?: LazyLoadEvent) {
        // if (this.primengTableHelper.shouldResetPaging(event)) {
        //     this.paginator.changePage(0);
        //     return;
        // }

        this.primengTableHelper.showLoadingIndicator();
        
        // // Number of records skipped
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
        let filters: NHANVIEN_FILTER = new NHANVIEN_FILTER();
        filters.maNV = this.maNhanVien;
        filters.tenNhanVien = this.tenNhanVien;
        filters.phongBan = this.phongBan;
        filters.trangThai = this.trangThai;
        // filters.skip = this.skip;
        filters.orderBy = this.sorting;
        // filters.take = this.take;
        filters.desc = this.desc;

        this.nhanVienService.searchNhanVien(
            filters,
        ).subscribe(result => {
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    ngOnInit():void{
        this.getNhanVien();
    }

    getAllDepartments() {
        // Get all departments
        this.nhanVienService.getDepName()
            .subscribe(result => {
                this.phongBanList = result;
        });

        var tmp = this.phongBanList;
    }
    // reloadPage(): void {
    //     this.paginator.changePage(this.paginator.getPage());
    // }

    deleteNhanVien(): void {
        this.message.confirm(
            this.l('EmployeeDeleteWarningMessage', this.selectedItem.nV_MA_NV),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.nhanVienService.deleteNhanVien(this.selectedItem.nV_MA_NV)
                        .subscribe(() => {
                            this,this.getNhanVien()
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    createNhanVien() {
        this.navigate(['/app/admin/chitietnhanvien-add']);
    }
    updateNhanVien() {
        this.navigate(['/app/admin/chitietnhanvien-edit'+this.selectedItem.nV_MA_NV]);
    }
    detailsNhanVien() {
        this.navigate(['/app/admin/chitietnhanvien-view'+this.selectedItem.nV_MA_NV]);
    }
}