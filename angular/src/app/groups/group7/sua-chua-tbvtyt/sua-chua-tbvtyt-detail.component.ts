import {Component, Injector, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {AppComponentBase} from '@shared/common/app-component-base';
import {FormGroup} from '@angular/forms';
import {
    NHANVIEN_DTO, NhanVienServiceProxy,
    PhongBanServiceProxy,
    THIETBIVATTU_DTO,
    ThietBiVatTuServiceProxy, YEU_CAU_SUA_CHUA_DTO,
    YeuCauSuaChuaServiceProxy
} from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';

@Component({
    templateUrl: './sua-chua-tbvtyt-detail.component.html',
    styleUrls: ['./sua-chua-tbvtyt-detail.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class SuaChuaTbvtytDetailComponent extends AppComponentBase implements OnInit {

    id: string;
    editPageState: string;
    suaChuaTbvtytForm: FormGroup;
    show = false;
    submitted = false;
    @ViewChild('f') formRef;
    inputModel = {
        'YCSC_MA_YCSC': undefined,
        'CREATE_DT': undefined,
        'YCSC_NGAYYC': undefined,
        'YCSC_NGAYKT': undefined,
        'YCSC_MOTA_NGUYENNHAN': undefined,
        'YCSC_YKIEN_LANHDAO': undefined,
        'YCSC_KQTINHTRANG': undefined,
        'YCSC_NGAYHT': undefined,
        'YCSC_TBVT_ID': undefined,
        'YCSC_NHANVIEN_SUACHUA': undefined,
        'NGUOILAP': undefined,
        'YCSC_ID': undefined,
        'NGUOIDUYET': undefined,
        'YCSC_TINHTRANG_DUYET': undefined,
    };
    thietBiList: THIETBIVATTU_DTO[] = [];
    nhanVienList: NHANVIEN_DTO[] = [];
    bd_FROM_DT: string;
    bd_TO_DT: string;
    hasData = false;
    newAdding = false;

    async ngOnInit() {
        if (this.editPageState == 'view' || this.editPageState == 'edit') {
            const res = await this.yeuCauSuaChuaService
                .yCSCById(this.id)
                .toPromise();

            if (
                res.RECORD_STATUS !== undefined &&
                res.RECORD_STATUS != '0'
            ) {
                this.inputModel = res as any;
                this.hasData = true;
            }
        }

        // Get select box
        const thietBiListPromise = this.thietBiVatTuServiceProxy.getAllThietBiVatTu().toPromise();
        const nhanVienListPromise = this.nhanVienSerivceProxy.getAllNhanVien().toPromise();
        this.thietBiList = await thietBiListPromise;
        this.nhanVienList = await nhanVienListPromise;

        console.log(this.thietBiList, this.nhanVienList);

        if (this.editPageState == 'add') {
            this.hasData = true;
        }
    }

    edit() {
        this.navigate(['app/admin/yeu-cau-sua-chua-tbvtyt-edit/' + this.id]);
    }

    constructor(injector: Injector,
                private yeuCauSuaChuaService: YeuCauSuaChuaServiceProxy,
                private thietBiVatTuServiceProxy: ThietBiVatTuServiceProxy,
                private nhanVienSerivceProxy: NhanVienServiceProxy,
    ) {
        super(injector);
        //Default value
        this.id = this.getRouteParam('id');
        this.editPageState = this.getRouteData('editPageState');
    }

    async onSave(_formRef, approve?) {
        if (!this.formRef.valid) {
            this.notify.error(this.l('Vui lòng nhập đầy đủ thông tin.'))
            return;
        }
        const dto = new YEU_CAU_SUA_CHUA_DTO();
        dto.ycsC_ID = this.inputModel.YCSC_ID;
        dto.ycsC_MA_YCSC = this.inputModel.YCSC_MA_YCSC;
        dto.ycsC_TBVT_ID = this.inputModel.YCSC_TBVT_ID;
        dto.ycsC_MOTA_NGUYENNHAN = this.inputModel.YCSC_MOTA_NGUYENNHAN;
        dto.ycsC_NHANVIEN_SUACHUA = this.inputModel.YCSC_NHANVIEN_SUACHUA;
        dto.ycsC_KQTINHTRANG = this.inputModel.YCSC_KQTINHTRANG;
        dto.ycsC_YKIEN_LANHDAO = this.inputModel.YCSC_YKIEN_LANHDAO;
        dto.ycsC_NGAYYC = moment(this.inputModel.YCSC_NGAYYC);
        dto.ycsC_NGAYKT = moment(this.inputModel.YCSC_NGAYKT);
        dto.ycsC_NGAYHT = moment(this.inputModel.YCSC_NGAYHT);
        dto.ycsC_TINHTRANG_DUYET = this.inputModel.YCSC_TINHTRANG_DUYET;

        if (approve) {
            dto.ycsC_TINHTRANG_DUYET = approve;
            dto.ycsC_NGUOIDUYET = '0'; // shit
        }

        if (this.editPageState == 'add') {
            dto.recorD_STATUS = null;
            dto.autH_STATUS = '0';
            dto.creatE_DT = moment();
            dto.ycsC_NGUOILAP = '0'; // shitttttttttttttt
        }

        try {
            if (this.editPageState == 'edit' || this.editPageState == 'view') {
                const [res] = await this.yeuCauSuaChuaService.yCSCUpdate(dto).toPromise();
                if (res.Result !== -1) {
                    if (approve) {
                        this.message.success('Thay đổi trạng thái duyệt thành công!');
                    }
                    this.message.success('Lưu thành công');
                    return;
                }
                this.message.error('Lỗi!');
                return;
            }
            if (this.editPageState == 'add') {
                const res = await this.yeuCauSuaChuaService.yCSCInsert(dto).toPromise();
                if (res.Result !== -1) {
                    this.message.success('Lưu thành công');
                    return;
                }
                this.message.error(res.ErrorDesc, 'Lỗi!');
                return;
            }
        } catch (e) {
            this.message.error('Lỗi!');
            console.error(e);
        }
    }

    delete() {
        if (!this.inputModel.YCSC_ID) {
            this.message.error(this.l('Vui lòng chọn yêu cầu.'), this.l('error'));
            return;
        }
        this.message.confirm(this.l('Bạn có chắc muốn xóa yêu cầu này?'), (isConfirmed) => {
            if (!isConfirmed) {
                return;
            }
            this.yeuCauSuaChuaService.yCSCDeleteSingle(this.inputModel.YCSC_ID).subscribe((res) => {
                this.message.success(this.l('Xóa thành công!'));
                this.navigate(['/app/admin/yeu-cau-sua-chua-tbvtyt']);
            });
        });
    }

    add() {
        this.navigate(['/app/admin/yeu-cau-sua-chua-tbvtyt-add']);
    }
}
