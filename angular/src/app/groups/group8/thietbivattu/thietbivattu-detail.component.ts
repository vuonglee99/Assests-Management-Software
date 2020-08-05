import { Component, Injector, ViewEncapsulation, ViewChild } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { ChiTietThietBiVatTuServiceProxy, ThietBiVatTu, PhuKien_DTO } from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
import { ConfirmationService } from "primeng/api";

class YESNO {
    TEN: string | null | undefined;
    MA_YN: string | null | undefined;

    constructor(data) {
        this.TEN = data["TEN"];
        this.MA_YN = data["MA_YN"];
    }
}

class THANG {
    TEN: string | null | undefined;
    MA_T: string | null | undefined;

    constructor(data) {
        this.TEN = data["TEN"];
        this.MA_T = data["MA_T"];
    }
}

class LOAI {
    TEN: string | null | undefined;
    MA_L: string | null | undefined;

    constructor(data) {
        this.TEN = data["TEN"];
        this.MA_L = data["MA_L"];
    }
}

@Component({
    templateUrl: './thietbivattu-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./thietbi.less']
})
export class ThietBiDetailComponent extends AppComponentBase {
    submitted = false;
    inputModel: ThietBiVatTu = new ThietBiVatTu();
    editPageState: string;
    titleHeader: string;
    ngaymuaDate: Date = null;
    tungayDate: Date = null;
    denngayDate: Date = null;
    isCanBaoDuong: Boolean = false;
    isNhapTheoLo: Boolean = false;
    displayDialog: Boolean = false;
    phukienModel: PhuKien_DTO = new PhuKien_DTO();
    newPhuKien: Boolean = false;
    selectedPhuKien: PhuKien_DTO;

    cols = [
        { field: 'phU_KIEN_ID', header: 'ID' },
        { field: 'phU_KIEN_TBVT_ID', header: 'ID Phụ Kiện TBVT' },
        { field: 'phU_KIEN_MA_PK', header: 'Mã Phụ Kiện' },
        { field: 'phU_KIEN_TEN', header: 'Tên Phụ Kiện' },
        { field: 'phU_KIEN_DVT', header: 'Đơn Vị Tính' },
        { field: 'phU_KIEN_SO_LUONG', header: 'Số Lượng' },
        { field: 'phU_KIEN_GHI_CHU', header: 'Ghi Chú' },
    ];

    YESNO_LIST: YESNO[] = [
        new YESNO({ TEN: "Yes", MA_YN: "1" }),
        new YESNO({ TEN: "No", MA_YN: "0" }),
    ]

    THANG_LIST: THANG[] = [
        new THANG({ TEN: "1 tháng", MA_T: '1 tháng' }),
        new THANG({ TEN: "3 tháng", MA_T: '3 tháng' }),
        new THANG({ TEN: "6 tháng", MA_T: '6 tháng' }),
        new THANG({ TEN: "12 tháng", MA_T: '12 tháng' }),
    ]

    LOAI_LIST: LOAI[] = [
        new LOAI({ TEN: "Vật tư", MA_YN: "L01" }),
        new LOAI({ TEN: "Thiết bị", MA_YN: "L02" }),
    ]

    submitState: string | null | undefined;

    constructor(injector: Injector,
        private cTTBVService: ChiTietThietBiVatTuServiceProxy,
        private confirmationService: ConfirmationService
    ) {
        super(injector);
        this.inputModel.tbvT_ID = this.getRouteParam('id');
        this.inputModel.recorD_STATUS = "1";
        this.inputModel.tbvT_CAN_BAO_DUONG = this.isCanBaoDuong ? '1' : '0';
        this.inputModel.tbvT_NHAP_THEO_LO = this.isNhapTheoLo ? '1' : '0';
        this.editPageState = this.getRouteData('editPageState');
        this.titleHeader = this.getRouteData('title');
    }

    ngOnInit(): void {
        if (!this.inputModel.tbvT_ID) return;
        this.cTTBVService.getById(this.inputModel.tbvT_ID.toString()).subscribe(result => {
            this.inputModel.init(result);
            this.isCanBaoDuong = this.inputModel.tbvT_CAN_BAO_DUONG === '1';
            this.isNhapTheoLo = this.inputModel.tbvT_NHAP_THEO_LO === '1';
            this.ngaymuaDate = moment(this.inputModel.tbvT_NGAY_MUA).toDate();
            this.tungayDate = moment(this.inputModel.tbvT_NGAY_TINH_BAO_HANH).toDate();
            this.denngayDate = moment(this.inputModel.tbvT_NGAY_KET_THUC_BAO_HANH).toDate();
        });
    }

    showDialog() {
        this.displayDialog = true;
        this.phukienModel = new PhuKien_DTO();
        this.newPhuKien = true;
    }

    addNewPhuKien() {
        let listPhuKien = [...this.inputModel.tbvT_PhuKien];
        if (this.newPhuKien) {
            listPhuKien.push(this.phukienModel);
        } else {
            listPhuKien[this.inputModel.tbvT_PhuKien.indexOf(this.selectedPhuKien)] = this.phukienModel;
        }
        this.inputModel.tbvT_PhuKien = listPhuKien;
        this.phukienModel = new PhuKien_DTO();
        this.displayDialog = false;
    }

    cancelAddPhuKien() {
        this.phukienModel = new PhuKien_DTO();
        this.displayDialog = false;
    }

    clonePhuKien(pk: PhuKien_DTO): PhuKien_DTO {
        let clonePhuKien = new PhuKien_DTO();
        for (let prop in pk) {
            clonePhuKien[prop] = pk[prop];
        }
        console.log(clonePhuKien);
        return clonePhuKien;
    }

    onRowSelect(event) {
        this.newPhuKien = false;
        console.log(event);
        this.phukienModel = this.clonePhuKien(event.data);
        this.displayDialog = true;
    }

    deletePhuKien() {
        let index = this.inputModel.tbvT_PhuKien.indexOf(this.selectedPhuKien);
        this.inputModel.tbvT_PhuKien = this.inputModel.tbvT_PhuKien.filter((val, i) => i != index);
        this.phukienModel = new PhuKien_DTO();
        this.displayDialog = false;
    }

    onChange(e) {
        console.log(e)
    }

    update() {
        this.navigate([`/app/admin/thietbivattu-edit/${this.inputModel.tbvT_MA_TBVT}`]);
    }

    backToList() {
        this.navigate([`/app/admin/thietbivattu`]);
    }

    cancel() {
        this.navigate([`/app/admin/thietbivattu-view/${this.inputModel.tbvT_MA_TBVT}`]);
    }

    delete() {
        this.confirmationService.confirm({
            message: 'Bạn có chắc muốn xóa nhân viên này không?',
            accept: () => {
                this.cTTBVService.delete(this.inputModel.tbvT_ID.toString()).subscribe(response => {
                    this.notify.info(this.l('Xóa thành công'));
                    this.navigate([`/app/admin/thietbivattu`]);
                })
            }
        });

    }

    save() {
        this.inputModel.tbvT_NGAY_MUA = moment.utc(this.ngaymuaDate);
        this.inputModel.tbvT_NGAY_TINH_BAO_HANH = moment.utc(this.tungayDate);
        this.inputModel.tbvT_NGAY_KET_THUC_BAO_HANH = moment.utc(this.denngayDate);
        if (this.editPageState == 'view') return;
        else if (this.editPageState == 'add') {
            this.cTTBVService.insert(this.inputModel).subscribe(response => {
                if (response.length !== 0) {
                    this.notify.info(this.l('Thêm mới thành công'));
                    this.navigate([`/app/admin/thietbivattu`]);
                }
                else {
                    this.notify.error(this.l(response[0].ErrorDesc));
                }
            })
        }
        else {
            this.cTTBVService.update(this.inputModel).subscribe(response => {
                if (response[0].Result == "0") {
                    this.notify.info(this.l('Cập nhật thành công'));
                    this.navigate([`/app/admin/thietbivattu`]);
                }
                else {
                    this.notify.error(this.l(response[0].ErrorDesc));
                }
            })
        }

    }

}