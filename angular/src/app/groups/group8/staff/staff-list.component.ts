import { Component, Injector, ViewEncapsulation, ViewChild } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { NhanVienServiceProxy, NHANVIEN_DTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';

class PhongBan {
    TEN: string | null | undefined;
    MA_PB: string | null | undefined;

    constructor(data) {
        this.TEN = data["TEN"];
        this.MA_PB = data["MA_PB"];
    }
}

class TrangThai {
    TEN: string | null | undefined;
    MA_TT: string | null | undefined;

    constructor(data) {
        this.TEN = data["TEN"];
        this.MA_TT = data["MA_TT"];
    }
}


@Component({
    templateUrl: './staff-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./staff.less'],
})
export class StaffListComponent extends AppComponentBase {
    /**
     *
     */
    @ViewChild('dataTable') dataTable: Table;

    records: NHANVIEN_DTO[] = [];

    cols: any[];

    isPaging: boolean;

    phongBan_List: PhongBan[] = [
        new PhongBan({ TEN: "IT", MA_PB: "IT" }),
        new PhongBan({ TEN: "Tài Chính", MA_PB: "TAI_CHINH" }),
        new PhongBan({ TEN: "Kế Toán", MA_PB: "KE_TOAN" }),
        new PhongBan({ TEN: "Quản lý nhân sự", MA_PB: "QUAN_LY_NHAN_SU" }),
    ];
    thangthaiList: TrangThai[] = [
        new TrangThai({ TEN: "Hoạt động", MA_TT: 0 }),
        new TrangThai({ TEN: "Nghỉ phép", MA_TT: 1 }),
        new TrangThai({ TEN: "Không hoạt động", MA_TT: 2 }),
    ]

    constructor(injector: Injector,
        private nVService: NhanVienServiceProxy,
    ) {
        super(injector);

    }


    ngOnInit(): void {
        this.nVService.getAllNhanVien().subscribe(result => {
            this.records = result;
            this.records.forEach(row =>
                row.nV_PHONG_BAN = this.phongBan_List.find(item => item.MA_PB == row.nV_PHONG_BAN).TEN)
            this.isPaging = result.length !== 0;

        });
        this.cols = [
            { field: 'CTNV_MANV', header: 'Mã Nhân Viên' },
            { field: 'CTNV_TEN_NV', header: 'Tên Nhân Viên' },
            { field: 'CTNV_CHUC_VU', header: 'Chức Vụ' },
            { field: 'CTNV_PHONG_BAN', header: 'Phòng Ban' },
            { field: 'CTNV_SDT', header: 'Số Điện Thoại' },
            { field: 'CTNV_TRANG_THAI', header: 'Trạng Thái Hoạt Động' },
        ];
    }

    // /**
    //  * Hàm get danh sách MenuClient
    //  * @param event
    //  */
    // getAllNhanVien(event?: LazyLoadEvent) {
    //     if (!this.dataTable) {
    //         return;
    //     }

    //     //show loading trong gridview
    //     this.primengTableHelper.showLoadingIndicator();

    //     /**
    //      * Sử dụng _apiService để call các api của backend
    //      */
    //     this.cTNVService.ctnV_GetAll().subscribe(result => {
    //         this.primengTableHelper.records = result;
    //         this.primengTableHelper.totalRecordsCount = result.length;
    //         this.primengTableHelper.hideLoadingIndicator();
    //     });
    // }

    createNhanVien() {
        this.navigate(['/app/admin/chitietnhanvien-add']);
    }

    viewDetail(ctnV_MANV: string) {
        this.navigate([`/app/admin/chitietnhanvien-view/${ctnV_MANV}`]);
    }
}