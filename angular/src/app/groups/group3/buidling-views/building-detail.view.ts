import { Component, ViewEncapsulation, Injector, OnInit, ViewChild, AfterViewInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { BuildingTableDTO, BuildingsServiceProxy, BuildingUpdateRequest, BuildingApproveRequest, BuildingCreateRequest, SessionServiceProxy, FloorsServiceProxy, FloorPagingSearchDTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent, Paginator } from "primeng/primeng";
import { Table } from "primeng/table";

@Component({
    templateUrl: './building-detail.view.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../styles.css'],
})
export class BuildingDetailView extends AppComponentBase implements OnInit, AfterViewInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    pageState: number = 1;
    inputModel: BuildingTableDTO = new BuildingTableDTO();
    selection: FloorPagingSearchDTO = undefined;

    constructor(
        injector: Injector,
        private buildingsService: BuildingsServiceProxy,
        private sessionService: SessionServiceProxy,
        private floorsService: FloorsServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.pageState = this.getRouteData('pageState');
        this.inputModel.buildinG_ID = '';
        if (this.pageState !== 1) {
            this.inputModel.buildinG_ID = this.getRouteParam('buildingId');
            this.buildingsService.getByID(this.inputModel.buildinG_ID)
                .subscribe(result => this.inputModel = result);
        }
    }

    ngAfterViewInit(): void { this.getItems(); }

    onNavigateBackClick() {
        this.navigate(['/app/admin/buildings']);
    }

    onCancelItemClick() {
        this.buildingsService.getByID(this.inputModel.buildinG_ID)
            .subscribe(result => {
                this.inputModel = result;
                this.pageState = 0;
            });
    }

    async onCreateItemClick() {
        const { buildinG_ID, ...createModel } = this.inputModel;
        const user = (await this.sessionService.getCurrentLoginInformations().toPromise()).user;
        createModel.makeR_ID = user.userName;
        const response = await this.buildingsService.create(new BuildingCreateRequest(createModel)).toPromise();
        if (response.RESULT === -1)
            this.message.error(response.ERROR_DESC);
        else {
            this.message.success(response.ERROR_DESC);
            this.navigate([`/app/admin/building-detail/${response.RESULT}`]);
        }
    }

    onModifyItemClick() { this.pageState = 2; }

    onDeleteItemClick() {
        this.message.confirm(
            this.l('BuildingDeleteWarningMessage', this.inputModel.buildinG_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.buildingsService.delete(this.inputModel.buildinG_ID)
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
        this.buildingsService.update(new BuildingUpdateRequest(this.inputModel))
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
        const response = await this.buildingsService.approve(new BuildingApproveRequest({
            buildinG_ID: this.inputModel.buildinG_ID,
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
        if (!this.inputModel.buildinG_CODE || this.inputModel.buildinG_CODE === '')
            return false;
        if (!this.inputModel.buildinG_NAME || this.inputModel.buildinG_NAME === '')
            return false;
        if (!this.inputModel.address || this.inputModel.address === '')
            return false;
        return true;
    }

    getItems(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) return;
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this.floorsService.pagingSearch(
            '', '', '',
            this.inputModel.buildinG_ID,
            this.checkPermissionOnlyView() ? '1' : '',
            this.checkPermissionOnlyView() ? '1' : '',
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    checkPermissionOnlyView() {
        if (this.isGrantedAny(
            'Pages.Group03.Floors.Create',
            'Pages.Group03.Floors.Update',
            'Pages.Group03.Floors.Delete',
            'Pages.Group03.Floors.Approve'
        )) return false;
        return true;
    }

    createItem() {
        this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/floor-create`]);
    }
    detailItem(item: FloorPagingSearchDTO) {
        this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/floor-detail/${item.flooR_ID}`]);

    }
    modifyItem(item: FloorPagingSearchDTO) {
        this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/floor-modify/${item.flooR_ID}`]);
    }
    deleteItem(item: FloorPagingSearchDTO) {
        this.message.confirm(
            this.l('FloorDeleteWarningMessage', item.flooR_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.floorsService.delete(item.flooR_ID)
                        .subscribe(response => {
                            this.getItems();
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

    exportItems() {

    }
}