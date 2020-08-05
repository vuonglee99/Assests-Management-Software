import {
    ViewEncapsulation,
    Component,
    Injector,
    OnInit,
    ViewChild,
} from '@angular/core';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {AppComponentBase} from '@shared/common/app-component-base';
import {
    YeuCauSuaChuaServiceProxy,
    YEU_CAU_SUA_CHUA_DTO,
} from '@shared/service-proxies/service-proxies';
import {Paginator} from 'primeng/components/paginator/paginator';
import {Table} from 'primeng/components/table/table';
import {LazyLoadEvent} from 'primeng/components/common/lazyloadevent';
import * as moment from 'moment';

@Component({
    templateUrl: './sua-chua-tbvtyt-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./sua-chua-tbvtyt-list.component.css'],
})
export class SuaChuaTbvtytListComponent extends AppComponentBase
    implements OnInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    /**
     *
     */
    records: YEU_CAU_SUA_CHUA_DTO[] = [];
    cols: any[];
    selectedYCSC: any;
    filterInput: YEU_CAU_SUA_CHUA_DTO = new YEU_CAU_SUA_CHUA_DTO();
    isLoading = false;

    constructor(
        injector: Injector,
        private suaChuaTbvtytService: YeuCauSuaChuaServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.search();
        this.cols = [
            {field: 'YCSC_MA_YCSC', name: 'YCSC_MA_YCSC', header: 'Mã Yêu cầu'},
            {field: 'CREATE_DT', name: 'CREATE_DT', header: 'Ngày tạo'},
            {field: 'TBVT_TEN', name: 'TBVT_TEN', header: 'Tên thiết bị'},
            {field: 'YCSC_TINHTRANG_DUYET', name: 'YCSC_TINHTRANG_DUYET', header: 'Tình trạng duyệt'},
        ];
    }
    viewDetail(YCSC_TBVT_ID) {
        this.navigate([
            'app/admin/yeu-cau-sua-chua-tbvtyt-view/' + YCSC_TBVT_ID,
        ]);
    }

    // delete(record: NSX_DTO) {
    //     this.message.confirm(
    //         this.l("Do you want to delete NSX", record.nsX_NAME),
    //         this.l("AreYouSure"),
    //         (isConfirmed) => {
    //             if (isConfirmed) {
    //                 this.ncuService.nSX_Delete(record.nsX_ID).subscribe(() => {
    //                     this.search();
    //                     this.notify.success(this.l("SuccessfullyDeleted"));
    //                 });
    //             }
    //         }
    //     );
    // }

    deleteSelectedRecord() {
        console.log(this.selectedYCSC);
        if (!this.selectedYCSC.YCSC_ID) {
            this.message.error(this.l('Vui lòng chọn yêu cầu.'), this.l('error'));
            return;
        }
        this.message.confirm(this.l('Bạn có chắc muốn xóa yêu cầu này?'), (isConfirmed) => {
            if (!isConfirmed) {
                return;
            }
            this.suaChuaTbvtytService.yCSCDeleteSingle(this.selectedYCSC.YCSC_ID).subscribe((res) => {
                this.message.success(this.l('Xóa thành công!'));
                this.search();
            });
        });
    }

    viewSelectedRecord() {
        if (!this.selectedYCSC.YCSC_MA_YCSC) {
            return;
        }
        this.navigate(['/app/admin/yeu-cau-sua-chua-tbvtyt-view/' + this.selectedYCSC.YCSC_MA_YCSC]);
    }

    search(value?: string, type?: string) {
        if (this.filterInput.ycsC_MA_YCSC) {
            value = this.filterInput.ycsC_MA_YCSC;
            type = 'YCSC_MA_YCSC';
        } else if (this.filterInput.ycsC_NGAYYC) {
            value = this.filterInput.ycsC_NGAYYC.toString();
            type = 'YCSC_NGAYYC';
        } else if (this.filterInput.ycsC_TINHTRANG_DUYET) {
            console.log(this.filterInput.ycsC_TINHTRANG_DUYET);
            value = this.filterInput.ycsC_TINHTRANG_DUYET.toString();
            type = 'YCSC_TINHTRANG_DUYET';
        }
        this.isLoading = true;
        this.suaChuaTbvtytService.yCSCSearchWithOption(value, type).subscribe((res) => {
            this.isLoading = false;
            this.primengTableHelper.totalRecordsCount = res.length;
            this.primengTableHelper.records = res;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    add() {
        this.navigate(['/app/admin/yeu-cau-sua-chua-tbvtyt-add']);
    }

}
