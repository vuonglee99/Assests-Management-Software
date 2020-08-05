import {Component, Injector, OnInit, ViewEncapsulation} from '@angular/core';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {AppComponentBase} from '@shared/common/app-component-base';
import {TrangThaiYeuCauSuaChuaServiceProxy, TTYCSC_DTO} from '@shared/service-proxies/service-proxies';
import {ActivatedRoute} from '@angular/router';
import {TTYCSC_URL, viRegStr} from '../utils/constant';

const moment = require('moment');

@Component({
    templateUrl: './TTYCSC-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./TTYCSC-detail.component.css'],
})
export class TTYCSCDetailComponent extends AppComponentBase implements OnInit {
    contentId: string;
    detailModel: TTYCSC_DTO = new TTYCSC_DTO();
    detailBak: TTYCSC_DTO = new TTYCSC_DTO();

    initFlag: boolean;
    editPageState: string;
    viRegStr: string = viRegStr;

    constructor(
        injector: Injector,
        private repairReqStatusService: TrangThaiYeuCauSuaChuaServiceProxy,
        private route: ActivatedRoute
    ) {
        super(injector);
        this.editPageState = this.getRouteData('editPageState');
    }

    ngOnInit() {
        this.detailModel.ttycsC_CODE = '';
        this.detailModel.ttycsC_CODE = '';
        this.detailModel.ttycsC_DESC = '';
        this.detailModel.ttycsC_NAME = '';
        this.initFlag = false;
        this.contentId = this.route.snapshot.paramMap.get('id');

        this.repairReqStatusService.tTYCSC_ById(this.contentId).subscribe((res) => {
            this.detailModel = res;
            // @ts-ignore
            this.detailBak = {...res};
            this.initFlag = true;
        });
    }


    onGoBack() {
        this.navigate([TTYCSC_URL]);
    }

    onDelete() {
        this.repairReqStatusService.tTYCSC_Delete(this.contentId).subscribe((res) => {
            this.message.success('Đã xóa trạng thái!');
            this.onGoBack();
        });
    }

    handleEdit() {
        this.editPageState = 'EDIT';
    }

    handleApprove() {
        const {ttycsC_ID, ttycsC_CODE, ttycsC_DESC, ttycsC_NAME, approvE_DT, autH_STATUS, checkeR_ID, creatE_DT, makeR_ID, notes, recorD_STATUS, typE_APPROVE} = this.detailModel;
        this.repairReqStatusService.tTYCSC_Approve(ttycsC_ID, ttycsC_CODE, ttycsC_NAME, ttycsC_DESC, null, typE_APPROVE, 1).subscribe(res => {
            this.message.info(this.l('GR6_REQUEST_APPROVED'));
        });
    }

    handleDeny() {
        const {ttycsC_ID, ttycsC_CODE, ttycsC_DESC, ttycsC_NAME, approvE_DT, autH_STATUS, checkeR_ID, creatE_DT, makeR_ID, notes, recorD_STATUS, typE_APPROVE} = this.detailModel;
        this.repairReqStatusService.tTYCSC_Approve(ttycsC_ID, ttycsC_CODE, ttycsC_NAME, ttycsC_DESC, null, typE_APPROVE, 0).subscribe(res => {
            this.message.info(this.l('GR6_REQUEST_DENIED'));
        });

    }

    submitChange() {
        const {ttycsC_ID, ttycsC_CODE, ttycsC_DESC, ttycsC_NAME, approvE_DT, autH_STATUS, checkeR_ID, creatE_DT, makeR_ID, notes, recorD_STATUS} = this.detailModel;
        this.repairReqStatusService.tTYCSC_Create(ttycsC_ID, ttycsC_CODE, ttycsC_NAME, ttycsC_DESC, notes, '1', null, moment(), undefined, checkeR_ID, approvE_DT, 'Update')
            .subscribe((res) => {
                this.message.success(this.l('GR6_REQUEST_SENT'));
                $('#DETAIL_TTYCSC_FORM').modal('hide');
            });
    }

    handleCancel() {
        // @ts-ignore
        this.detailModel = {...this.detailBak};
        this.editPageState = 'VIEW';
    }

    isRequest(): boolean {
        const {autH_STATUS, recorD_STATUS} = this.detailBak;
        const isActiveRecord = recorD_STATUS === '1' && autH_STATUS === 'A';
        return !isActiveRecord;
    }

    getTypeApprove(): string {
        switch (this.detailBak.typE_APPROVE) {
            case 'Create':
                return this.l('GR6_CREATE');
            case 'Update':
                return this.l('GR6_EDIT');
            case 'Delete':
                return this.l('GR6_DELETE');
            default:
                return '';
        }
    }

    isActive(): boolean {
        return this.detailBak.recorD_STATUS === '1' && this.detailBak.autH_STATUS === 'A';
    }
}
