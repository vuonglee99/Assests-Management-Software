import { Component, ViewEncapsulation, Injector, OnInit, ViewChild, AfterViewInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { BuildingsServiceProxy, BuildingUpdateRequest, BuildingApproveRequest, BuildingCreateRequest, SessionServiceProxy, FloorsServiceProxy, FloorTableDTO, ApartmentsServiceProxy, FloorCreateRequest, FloorUpdateRequest, FloorApproveRequest, ApartmentPagingSearchDTO, EnumeratedTypeTableDTO, EnumeratedTypesServiceProxy, EnumeratedTypeCreateRequest } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent, Paginator } from "primeng/primeng";
import { Table } from "primeng/table";
import { SearchableSelectComponentItem } from "../components/searchable-select.component";
import { CreateEnumModalComponent } from "../components/create-enum-modal.component";

@Component({
    templateUrl: './floor-detail.view.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../styles.css'],
})
export class FloorDetailView extends AppComponentBase implements OnInit, AfterViewInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('modal') modal: CreateEnumModalComponent;

    pageState: number = 1;
    inputModel: FloorTableDTO = new FloorTableDTO();
    selection: ApartmentPagingSearchDTO = undefined;
    floorTypes: SearchableSelectComponentItem[] = [];
    Group3Statuses: SearchableSelectComponentItem[] = [
        new SearchableSelectComponentItem('0', 'Đang đóng'),
        new SearchableSelectComponentItem('1', 'Đang mở'),
    ];

    constructor(
        injector: Injector,
        private floorsService: FloorsServiceProxy,
        private sessionService: SessionServiceProxy,
        private apartmentsService: ApartmentsServiceProxy,
        private enumeratedTypesService: EnumeratedTypesServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.pageState = this.getRouteData('pageState');
        this.inputModel.flooR_ID = '';
        this.inputModel.buildinG_ID = this.getRouteParam('buildingId');
        this.enumeratedTypesService.getByType('FloorType')
            .subscribe(result => this.floorTypes = result.map(
                x => new SearchableSelectComponentItem(x.code, x.value)
            ));

        if (this.pageState !== 1) {
            this.inputModel.flooR_ID = this.getRouteParam('floorId');
            this.floorsService.getByID(this.inputModel.flooR_ID)
                .subscribe(result => this.inputModel = result);
        }
    }

    ngAfterViewInit(): void { this.getItems(); }

    onNavigateBackClick() {
        this.navigate([`app/admin/building-detail/${this.inputModel.buildinG_ID}`]);
    }

    onCancelItemClick() {
        this.floorsService.getByID(this.inputModel.flooR_ID)
            .subscribe(result => {
                this.inputModel = result;
                this.pageState = 0;
            });
    }

    async onCreateItemClick() {
        const { flooR_ID, ...createModel } = this.inputModel;
        const user = (await this.sessionService.getCurrentLoginInformations().toPromise()).user;
        createModel.makeR_ID = user.userName;
        const response = await this.floorsService.create(new FloorCreateRequest(createModel)).toPromise();
        if (response.RESULT === -1)
            this.message.error(response.ERROR_DESC);
        else {
            this.message.success(response.ERROR_DESC);
            this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/floor-detail/${response.RESULT}`]);
        }
    }

    onModifyItemClick() { this.pageState = 2; }

    onDeleteItemClick() {
        this.message.confirm(
            this.l('FloorDeleteWarningMessage', this.inputModel.flooR_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.floorsService.delete(this.inputModel.flooR_ID)
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
        this.floorsService.update(new FloorUpdateRequest(this.inputModel))
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
        const response = await this.floorsService.approve(new FloorApproveRequest({
            flooR_ID: this.inputModel.flooR_ID,
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

    async onFloorTypeDelete() {
        let result = await this.enumeratedTypesService.getByType('FloorType').toPromise();
        let floorType = result.find(x => x.code === this.inputModel.floortypE_ID);
        let response = await this.enumeratedTypesService.delete(floorType.id).toPromise();
        result = await this.enumeratedTypesService.getByType('FloorType').toPromise();
        this.floorTypes = result.map(
            x => new SearchableSelectComponentItem(x.code, x.value)
        );
        if (response.RESULT === -1)
            this.notify.error(response.ERROR_DESC);
        else {
            this.notify.success(response.ERROR_DESC);
        }
    }

    async onModalSaveClick(value) {
        let response = await this.enumeratedTypesService.create(new EnumeratedTypeCreateRequest({
            type: 'FloorType',
            value: value,
            label: value,
        })).toPromise();
        let result = await this.enumeratedTypesService.getByType('FloorType').toPromise();
        this.floorTypes = result.map(
            x => new SearchableSelectComponentItem(x.code, x.value)
        );
        if (response.RESULT === -1)
            this.notify.error(response.ERROR_DESC);
        else {
            this.notify.success(response.ERROR_DESC);
        }
        this.modal.close();
    }

    isInputValidated() {
        if (!this.inputModel.flooR_CODE || this.inputModel.flooR_CODE === '')
            return false;
        if (!this.inputModel.flooR_NAME || this.inputModel.flooR_NAME === '')
            return false;
        if (!this.inputModel.floortypE_ID || this.inputModel.floortypE_ID === '')
            return false;
        if (!this.inputModel.buildinG_ID || this.inputModel.buildinG_ID === '')
            return false;
        return true;
    }

    //
    // List
    //
    getItems(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) return;
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this.apartmentsService.pagingSearch(
            '', '', '',
            this.inputModel.buildinG_ID,
            this.inputModel.flooR_ID,
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
            'Pages.Group03.Apartments.Create',
            'Pages.Group03.Apartments.Update',
            'Pages.Group03.Apartments.Delete',
            'Pages.Group03.Apartments.Approve'
        )) return false;
        return true;
    }

    createItem() {
        this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/${this.inputModel.flooR_ID}/apartment-create`]);
    }
    detailItem(item: ApartmentPagingSearchDTO) {
        this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/${this.inputModel.flooR_ID}/apartment-detail/${item.apartmenT_ID}`]);
    }
    modifyItem(item: ApartmentPagingSearchDTO) {
        this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/${this.inputModel.flooR_ID}/apartment-modify/${item.apartmenT_ID}`]);
    }
    deleteItem(item: ApartmentPagingSearchDTO) {
        this.message.confirm(
            this.l('ApartmentDeleteWarningMessage', item.apartmenT_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.apartmentsService.delete(item.apartmenT_ID)
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