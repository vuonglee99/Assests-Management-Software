import {AppComponentBase} from '@shared/common/app-component-base';
import {Component, Injector, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {LOAI_XE_DTO, LoaiXeServiceProxy} from '@shared/service-proxies/service-proxies';
import {LazyLoadEvent} from '@node_modules/primeng/components/common/lazyloadevent';
import {Table} from '@node_modules/primeng/components/table/table';
import debounce from '@app/groups/group7/debounce.js';
import {Paginator} from '@node_modules/primeng/components/paginator/paginator';
import {PrimengTableHelper} from '@shared/helpers/PrimengTableHelper';
import {Form} from '@angular/forms';

@Component({
    templateUrl: './loai-xe.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class LoaiXeComponent extends AppComponentBase implements OnInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    public isLoading = false;
    public records: any[] = [];
    public currentRecords: any[] = [];
    public checkedList: any = {};

    filterCode: string;
    filterName: string;
    selectedRecords: LOAI_XE_DTO[] = [];

    constructor(injector: Injector,
                private loaiXeService: LoaiXeServiceProxy) {
        super(injector);
        this.isLoading = false;
        this.getRecords();
    }

    ngOnInit(): void {
        let hello = true;
    }

    onSearch(event?) {
        event.preventDefault();

        let value = '';
        let type = '';

        console.log(this.filterName, this.filterCode);

        if (this.filterName) {
            type = 'LX_NAME';
            value = this.filterName;
        } else if (this.filterCode) {
            type = 'LX_CODE';
            value = this.filterCode;
        }

        this.searchRecord(value, type);
    }

    async getRecords(event?: LazyLoadEvent) {
        this.searchRecord(null, null);
    }

    check(loaiXeID, event: any) {
        this.checkedList[loaiXeID] = event.target.checked;
        console.log('Checked list', this.checkedList);
    }

    async bulkDelete() {
        if (this.selectedRecords.length <= 0) {
            this.message.info('Vui lòng chọn loại xe cần xóa.');
            return;
        }
        this.message.confirm(
            'Xác nhận xóa',
            'Bạn có chắc muốn xóa những dữ liệu đã chọn không?',
            async (isConfirmed) => {
                const promises = [];
                for (const record of this.selectedRecords) {
                    promises.push(this.deleteItem(record.lX_ID));
                }
                const results = await Promise.all(promises);
                let successCount = 0;
                for (const result of results) {
                    if (result) {
                        successCount++;
                    }
                }
                if (successCount > 0) {
                    this.notify.success(`Đã xóa ${successCount} mục!`);
                    this.getRecords();
                    return;
                }
                this.message.error('Xóa thất bại.');
            }
        );

    }

    async deleteItem(loaiXeID) {
        const promise = new Promise((resolve) => {
            this.loaiXeService.loaiXeDelete(loaiXeID).subscribe((data: any) => {
               resolve(true);
            });
        });
        return await promise;
    }

    async searchRecord(keyword, type) {
        // const result: any = await this.loaiXeService.searchRecord(keyword);
        // this.records = result.result;
        this.isLoading = true;
        this.loaiXeService.loaiXeSearchAlternative(keyword, type).subscribe((data) => {
            this.primengTableHelper.totalRecordsCount = data.length;
            this.primengTableHelper.records = data;
            this.primengTableHelper.hideLoadingIndicator();
            // this.changePage();
            this.isLoading = false;
        });
    }

    directToAdd() {
        this.navigate(['/app/admin/loai-xe-add']);
    }

    viewDetail(id, e) {
        if (e) {
            e.preventDefault();
        }
        this.navigate(['/app/admin/loai-xe-view/' + id]);
    }

    viewSelectedRecord() {
        if (this.selectedRecords.length >= 1) {
            this.viewDetail(this.selectedRecords[0].lX_ID, null);
        }
    }

    changePage(event?) {
        const page = this.paginator.getPage();
        const pageCount = this.paginator.rows;
        const startRowId = Math.max(0, page * pageCount);
        const endRowId = Math.min(this.records.length, startRowId + pageCount);
        this.currentRecords = this.records.slice(startRowId, endRowId);
        console.log(page, pageCount, startRowId, endRowId, this.currentRecords, this.records);
        this.checkedList = {};
    }
}
