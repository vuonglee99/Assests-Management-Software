import {Component, Injector, OnInit, ViewEncapsulation} from '@angular/core';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {AppComponentBase} from '@shared/common/app-component-base';
import {DoUuTienServiceProxy, DUT_DTO,} from '@shared/service-proxies/service-proxies';
import {ActivatedRoute} from '@angular/router';
import regex, {viRegStr} from './utils/constant';

@Component({
    templateUrl: './dut-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./dut-detail.component.css'],
})
export class DUTDetailComponent extends AppComponentBase implements OnInit {
    dutId: string;
    detailModel: DUT_DTO = new DUT_DTO();
    detailBak: DUT_DTO = new DUT_DTO();
    maxLevel: number;

    editPageState: string;
    viRegStr: string = viRegStr;

    constructor(
        injector: Injector,
        private dutService: DoUuTienServiceProxy,
        private route: ActivatedRoute
    ) {
        super(injector);
        this.editPageState = this.getRouteData('editPageState');
    }

    ngOnInit() {
        this.detailModel.duT_CODE = '';
        this.detailModel.duT_ID = '';
        this.detailModel.duT_DESC = '';
        this.detailModel.duT_NAME = '';
        this.maxLevel = 1;


        this.dutId = this.route.snapshot.paramMap.get('id');

        this.dutService.dUT_SearchFilter(1, 1, undefined, '1', undefined, '1', null).subscribe(res => {
            this.maxLevel = res.totaL_RECORD;
        });
        // console.log('this.dutId', this.dutId);
        this.dutService.dUT_ById(this.dutId).subscribe((res) => {
            this.detailModel = res;
            // @ts-ignore
            this.detailBak = {...res};
        });
        // this.dutService.dUT_GetLevel().subscribe(res => {
        //     console.log('Max DUT level', res);
        // });

        // $('#DETAIL_DUT_FORM').on('submit', function (event) {
        //     console.log(event);
        // });
    }


    onGoBack() {
        this.navigate(['/app/admin/douutien']);
    }

    onDelete() {
        this.dutService.dUT_Delete(this.dutId).subscribe((res) => {
            this.message.success('Đã xóa độ ưu tiên!');
            this.onGoBack();
        });
    }

    handleEdit() {
        this.editPageState = 'EDIT';
    }

    submitChange() {
        const {duT_ID, duT_CODE, duT_DESC, duT_NAME, duT_LEVEL, approvE_DT, autH_STATUS, checkeR_ID, creatE_DT, makeR_ID, notes, recorD_STATUS} = this.detailModel;

        const validateRes = regex.test(duT_NAME) && regex.test(duT_DESC);

        if (!validateRes) {
            this.message.error('Dữ liệu không hợp lệ');
            return;
        }

        this.dutService.dUT_Update(duT_ID, duT_CODE, duT_NAME, duT_DESC, duT_LEVEL, notes, recorD_STATUS, makeR_ID, creatE_DT, autH_STATUS, checkeR_ID, approvE_DT).subscribe(res => {
            // console.log(res);
            this.onGoBack();
            this.message.success('Đã lưu thành công');
        });
    }

    handleCancel() {
        // @ts-ignore
        this.detailModel = {...this.detailBak};
        this.editPageState = 'VIEW';
    }
}
