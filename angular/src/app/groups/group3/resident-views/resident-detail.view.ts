import { Component, ViewEncapsulation, Injector, OnInit, ViewChild, AfterViewInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { ResidentTableDTO, ResidentsServiceProxy, ResidentUpdateRequest, ResidentApproveRequest, ResidentCreateRequest, SessionServiceProxy, FloorsServiceProxy, FloorPagingSearchDTO, ApartmentsServiceProxy, BuildingsServiceProxy } from "@shared/service-proxies/service-proxies";
import { SearchableSelectComponentItem } from "../components/searchable-select.component";
import * as moment from "moment";

@Component({
    templateUrl: './resident-detail.view.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../styles.css'],
})
export class ResidentDetailView extends AppComponentBase implements OnInit {
    pageState: number = 1;
    inputModel: ResidentTableDTO = new ResidentTableDTO();
    selection: FloorPagingSearchDTO = undefined;
    filter = { buildingId: '', floorId: '' }
    buildingsList: SearchableSelectComponentItem[] = [];
    floorsList: SearchableSelectComponentItem[] = [];
    apartmentsList: SearchableSelectComponentItem[] = [];

    constructor(
        injector: Injector,
        private buildingsService: BuildingsServiceProxy,
        private floorsService: FloorsServiceProxy,
        private apartmentsService: ApartmentsServiceProxy,
        private residentsService: ResidentsServiceProxy,
        private sessionService: SessionServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.pageState = this.getRouteData('pageState');
        this.inputModel.residenT_ID = '';
        this.inputModel.residenT_BIRTH = moment();

        this.gettingResident();
        this.buildingsService.pagingSearch('', '', '1', '1', '', 0, 500)
            .subscribe(result => this.buildingsList = result.items
                .map(x => new SearchableSelectComponentItem(
                    x.buildinG_ID, x.buildinG_CODE + ' - ' + x.buildinG_NAME
                ))
            );
    }

    async gettingResident() {
        if (this.pageState !== 1) {
            let resident = await this.residentsService.getByID(this.getRouteParam('residentId')).toPromise();
            let apartmetns = await this.apartmentsService.pagingSearch('', '', '', '', '', '1', '1', '', 0, 500).toPromise();
            this.apartmentsList = apartmetns.items
                .filter(x => x.apartmenT_ID === resident.cuR_APARTMENT)
                .map(x => new SearchableSelectComponentItem(x.apartmenT_ID, x.apartmenT_CODE + ' - ' + x.apartmenT_NAME));
            this.inputModel = resident;
        }
    }

    onNavigateBackClick() {
        this.navigate(['/app/admin/residents']);
    }

    onCancelItemClick() {
        this.gettingResident();
        this.pageState = 0;
    }

    async onCreateItemClick() {
        const { residenT_ID, ...createModel } = this.inputModel;
        const user = (await this.sessionService.getCurrentLoginInformations().toPromise()).user;
        createModel.makeR_ID = user.userName;
        const response = await this.residentsService.create(new ResidentCreateRequest(createModel)).toPromise();
        if (response.RESULT === -1)
            this.message.error(response.ERROR_DESC);
        else {
            this.message.success(response.ERROR_DESC);
            this.navigate([`/app/admin/resident-detail/${response.RESULT}`]);
        }
    }

    onModifyItemClick() { this.pageState = 2; }

    onDeleteItemClick() {
        this.message.confirm(
            this.l('ResidentDeleteWarningMessage', this.inputModel.residenT_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.residentsService.delete(this.inputModel.residenT_ID)
                        .subscribe(response => {
                            this.onNavigateBackClick();
                            if (response.RESULT === -1)
                                this.message.error(response.ERROR_DESC);
                            else {
                                this.message.success(response.ERROR_DESC);
                            }
                        });
                }
            }
        );
    }

    onUpdateItemClick() {
        this.residentsService.update(new ResidentUpdateRequest(this.inputModel))
            .subscribe(response => {
                if (response.RESULT === -1)
                    this.message.error(response.ERROR_DESC);
                else {
                    this.message.success(response.ERROR_DESC);
                    this.onNavigateBackClick();
                }
            });
    }

    async onApprove(authStatus: string) {
        const user = (await this.sessionService.getCurrentLoginInformations().toPromise()).user;
        const response = await this.residentsService.approve(new ResidentApproveRequest({
            residenT_ID: this.inputModel.residenT_ID,
            autH_STATUS: authStatus,
            checkeR_ID: user.userName,
        })).toPromise();
        if (response.RESULT === -1)
            this.message.error(response.ERROR_DESC);
        else {
            this.message.success(response.ERROR_DESC);
            this.onNavigateBackClick();
        }
    }

    isInputValidated() {
        if (!this.inputModel.residenT_CODE || this.inputModel.residenT_CODE === '')
            return false;
        if (!this.inputModel.residenT_NAME || this.inputModel.residenT_NAME === '')
            return false;
        if (!this.inputModel.residenT_BIRTH)
            return false;
        if (!this.inputModel.residenT_PHONE || this.inputModel.residenT_PHONE === '')
            return false;
        if (!this.inputModel.residenT_IDCARD || this.inputModel.residenT_IDCARD === '')
            return false;
        return true;
    }

    onBuildingSelected(type) {
        if (type === 'building') {
            if (!this.filter.buildingId || this.filter.buildingId === '') return;
            this.floorsService.pagingSearch('', '', '', this.filter.buildingId, '1', '1', '', 0, 500)
                .subscribe(result => this.floorsList = result.items.map(
                    x => new SearchableSelectComponentItem(
                        x.flooR_ID, x.flooR_ID + ' - ' + x.flooR_NAME
                    )
                ));
        }
        if (type === 'floor') {
            if (!this.filter.floorId || this.filter.floorId === '') return;
            this.apartmentsService.pagingSearch('', '', '', this.filter.buildingId, this.filter.floorId, '1', '1', '', 0, 500)
                .subscribe(result => this.apartmentsList = result.items.map(
                    x => new SearchableSelectComponentItem(
                        x.apartmenT_ID, x.apartmenT_CODE + ' - ' + x.apartmenT_NAME
                    )
                ));
        }
    }

}