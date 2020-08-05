import { Component, Injector, ViewEncapsulation, ViewChild } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { NhanVienServiceProxy, NHANVIEN_DTO } from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
import { ConfirmationService } from "primeng/api";

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
    templateUrl: './staff-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./staff.less']
})
export class StaffDetailComponent extends AppComponentBase {
    submitted = false;
    inputModel: NHANVIEN_DTO = new NHANVIEN_DTO();
    editPageState: string;
    ngaycapDate: Date = null;
    titleHeader: string;

    submitState: string | null | undefined;

    phongBan_List: PhongBan[] = [
        new PhongBan({ TEN: "P01", MA_PB: "P01" }),
        new PhongBan({ TEN: "P02", MA_PB: "P02" }),
        new PhongBan({ TEN: "P03", MA_PB: "P03" }),
        new PhongBan({ TEN: "P04", MA_PB: "P04" }),
    ];

    thangthaiList: TrangThai[] = [
        new TrangThai({ TEN: "Hoạt động", MA_TT: "1" }),
        // new TrangThai({ TEN: "Nghỉ phép", MA_TT: "0" }),
        new TrangThai({ TEN: "Đã nghỉ việc", MA_TT: "0" }),
    ]

    constructor(injector: Injector,
        private nVService: NhanVienServiceProxy,
        private confirmationService: ConfirmationService
    ) {
        super(injector);
        this.inputModel.nV_MA_NV = this.getRouteParam('id');
        this.inputModel.nV_TRANG_THAI = 0;
        this.inputModel.nV_PHONG_BAN = "IT";
        this.inputModel.recorD_STATUS = '1';
        this.editPageState = this.getRouteData('editPageState');
        this.titleHeader = this.getRouteData('title');
    }

    ngOnInit(): void {
        if (!this.inputModel.nV_MA_NV) return;
        this.nVService.nhanVien_ById(this.inputModel.nV_MA_NV).subscribe(result => {
            console.log('result', result);
            this.inputModel.init(result[0]);
            console.log(this.inputModel);
            this.ngaycapDate = this.inputModel.nV_NGAY_CAP_CMND.toDate();
        });
    }

    onChange(e) {
        console.log(e)
    }

    validate(evt) {
        var theEvent = evt || window.event;

        // Handle paste
        if (theEvent.type === 'paste') {
            key = theEvent.clipboardData.getData('text/plain');
        } else {
            // Handle key press
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);
        }
        var regex = /[0-9]|\./;
        if (!regex.test(key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
        }
    }

    update() {
        this.navigate([`/app/admin/chitietnhanvien-edit/${this.inputModel.nV_MA_NV}`]);
    }

    backToList() {
        this.navigate([`/app/admin/nhanvien`]);
    }

    cancel() {
        this.navigate([`/app/admin/chitietnhanvien-view/${this.inputModel.nV_MA_NV}`]);
    }

    deleteCTNV() {
        this.confirmationService.confirm({
            message: 'Bạn có chắc muốn xóa nhân viên này không?',
            accept: () => {
                this.nVService.deleteNhanVien(this.inputModel.nV_MA_NV).subscribe(response => {
                    this.notify.info(this.l('Xóa thành công'));
                    this.navigate([`/app/admin/nhanvien`]);
                })
            }
        });

    }

    save(evt) {
        this.inputModel.nV_NGAY_CAP_CMND = moment(this.ngaycapDate);
        if (this.editPageState == 'view') return;
        else if (this.editPageState == 'add') {
            this.nVService.insertNhanVien(this.inputModel).subscribe(response => {
                if (!response.length) {
                    this.notify.info(this.l('Thêm mới thành công'));
                    this.navigate([`/app/admin/nhanvien`]);
                }
                else {
                    this.notify.error(this.l(response[0].ErrorDesc));
                }
            })
        }
        else {
            this.nVService.updateNhanVien(
                this.inputModel.nV_ID,
                this.inputModel.nV_MA_NV,
                this.inputModel.nV_TEN,
                this.inputModel.nV_PHONG_BAN,
                undefined,
                this.inputModel.nV_CHUC_VU || '',
                this.inputModel.nV_SDT || '',
                this.inputModel.nV_TRANG_THAI || 0,
                this.inputModel.nV_CMND || '',
                this.inputModel.nV_NGAY_CAP_CMND,
                this.inputModel.nV_NOI_CAP_CMND || '',
                this.inputModel.nV_MA_SO_THUE || '',
                this.inputModel.nV_EMAIL || '',
                this.inputModel.nV_DIA_CHI,
                this.inputModel.nV_MO_TA || '',
                '1', undefined, undefined, undefined, undefined, undefined,undefined
            ).subscribe(response => {
                if (response[0].Result == "0") {
                    this.notify.info(this.l('Cập nhật thành công'));
                    this.navigate([`/app/admin/nhanvien`]);
                }
                else {
                    this.notify.error(this.l(response[0].ErrorDesc));
                }
            })
        }

    }

}