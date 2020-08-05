import {Component, Injector, OnInit, ViewEncapsulation} from '@angular/core';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {AppComponentBase} from '@shared/common/app-component-base';
import {DVT_DTO, DVTServiceProxy} from '@shared/service-proxies/service-proxies';
import {ActivatedRoute} from '@angular/router';
import {DVT_URL, viRegStr} from '../utils/constant';

@Component({
    templateUrl: './dvt-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./dvt-detail.component.css'],
})
export class DVT_DetailComponent extends AppComponentBase implements OnInit {
    contentId: string;
    detailModel: DVT_DTO = new DVT_DTO();
    detailBak: DVT_DTO = new DVT_DTO();

    initFlag: boolean;
    editPageState: string;
    viRegStr: string = viRegStr;

    constructor(
        injector: Injector,
        private mService: DVTServiceProxy,
        private route: ActivatedRoute
    ) {
        super(injector);
        this.editPageState = this.getRouteData('editPageState');
    }

    ngOnInit() {
        this.detailModel.dvT_CODE = '';
        this.detailModel.dvT_NAME = '';
        this.detailModel.dvT_DESC = '';
        this.initFlag = false;
        this.contentId = this.route.snapshot.paramMap.get('id');

        this.mService.cM_DVT_ById(this.contentId).subscribe((res) => {
            this.detailModel = res;
            // @ts-ignore
            this.detailBak = {...res};
            this.initFlag = true;
        });
    }

    onGoBack() {
        this.navigate([DVT_URL]);
    }

    onDelete() {
        this.mService.cM_DVT_Delete(this.contentId).subscribe((res) => {
            this.message.success('Đã xóa đơn vị tính!');
            this.onGoBack();
        });
    }

    handleEdit() {
        this.navigate([`/app/admin/donvitinh-update-detail/${this.contentId}`]);
    }

    handleCancel() {
        // @ts-ignore
        this.detailModel = {...this.detailBak};
        this.editPageState = 'VIEW';
    }
}
