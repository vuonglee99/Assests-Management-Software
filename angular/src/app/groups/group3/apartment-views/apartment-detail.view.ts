import { Component, ViewEncapsulation, Injector, OnInit, ViewChild, AfterViewInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { SessionServiceProxy, ApartmentTableDTO, ApartmentsServiceProxy, ApartmentCreateRequest, ApartmentUpdateRequest, ApartmentApproveRequest, ApartmentPagingSearchDTO, EnumeratedTypeTableDTO, EnumeratedTypesServiceProxy, EnumeratedTypeCreateRequest } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent, Paginator } from "primeng/primeng";
import { Table } from "primeng/table";
import { SearchableSelectComponentItem } from "../components/searchable-select.component";
import { CreateEnumModalComponent } from "../components/create-enum-modal.component";

@Component({
    templateUrl: './apartment-detail.view.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../styles.css'],
})
export class ApartmentDetailView extends AppComponentBase implements OnInit, AfterViewInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('modal') modal: CreateEnumModalComponent;

    pageState: number = 1;
    inputModel: ApartmentTableDTO = new ApartmentTableDTO();
    selection: ApartmentPagingSearchDTO = undefined;
    apartmentTypes: SearchableSelectComponentItem[] = [];
    Group3Statuses: SearchableSelectComponentItem[] = [
        new SearchableSelectComponentItem('0', 'Đang đóng'),
        new SearchableSelectComponentItem('1', 'Đang mở'),
    ];

    constructor(
        injector: Injector,
        private apartmentsService: ApartmentsServiceProxy,
        private sessionService: SessionServiceProxy,
        private enumeratedTypesService: EnumeratedTypesServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.pageState = this.getRouteData('pageState');
        this.inputModel.apartmenT_ID = '';
        this.inputModel.flooR_ID = this.getRouteParam('floorId');
        this.enumeratedTypesService.getByType('ApartmentType')
            .subscribe(result => this.apartmentTypes = result.map(
                x => new SearchableSelectComponentItem(x.code, x.value)
            ));

        if (this.pageState !== 1) {
            this.inputModel.apartmenT_ID = this.getRouteParam('apartmentId');
            this.apartmentsService.getByID(this.inputModel.apartmenT_ID)
                .subscribe(result => this.inputModel = result);
        }
    }

    ngAfterViewInit(): void { this.getItems(); }

    onNavigateBackClick() {
        this.navigate([`/app/admin/buildings/${this.getRouteParam('buildingId')}/floor-detail/${this.inputModel.flooR_ID}`]);
    }

    onCancelItemClick() {
        this.apartmentsService.getByID(this.inputModel.apartmenT_ID)
            .subscribe(result => {
                this.inputModel = result;
                this.pageState = 0;
            });
    }

    async onCreateItemClick() {
        const { apartmenT_ID, ...createModel } = this.inputModel;
        const user = (await this.sessionService.getCurrentLoginInformations().toPromise()).user;
        createModel.makeR_ID = user.userName;
        const response = await this.apartmentsService.create(new ApartmentCreateRequest(createModel)).toPromise();
        if (response.RESULT === -1)
            this.message.error(response.ERROR_DESC);
        else {
            this.message.success(response.ERROR_DESC);
        }
        this.onNavigateBackClick();
    }

    onModifyItemClick() { this.pageState = 2; }

    onDeleteItemClick() {
        this.message.confirm(
            this.l('ApartmentDeleteWarningMessage', this.inputModel.apartmenT_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.apartmentsService.delete(this.inputModel.apartmenT_ID)
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
        this.apartmentsService.update(new ApartmentUpdateRequest(this.inputModel))
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
        const response = await this.apartmentsService.approve(new ApartmentApproveRequest({
            apartmenT_ID: this.inputModel.apartmenT_ID,
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

    async onApartmentTypeDelete() {
        let result = await this.enumeratedTypesService.getByType('ApartmentType').toPromise();
        let apartmentType = result.find(x => x.code === this.inputModel.apartmenttypE_ID);
        let response = await this.enumeratedTypesService.delete(apartmentType.id).toPromise();
        result = await this.enumeratedTypesService.getByType('ApartmentType').toPromise();
        this.apartmentTypes = result.map(
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
            type: 'ApartmentType',
            value: value,
            label: value,
        })).toPromise();
        let result = await this.enumeratedTypesService.getByType('ApartmentType').toPromise();
        this.apartmentTypes = result.map(
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
        if (!this.inputModel.apartmenT_CODE || this.inputModel.apartmenT_CODE === '')
            return false;
        if (!this.inputModel.apartmenT_NAME || this.inputModel.apartmenT_NAME === '')
            return false;
        if (!this.inputModel.apartmenttypE_ID || this.inputModel.apartmenttypE_ID === '')
            return false;
        if (!this.inputModel.flooR_ID || this.inputModel.flooR_ID === '')
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

        this.apartmentsService.pagingSearch(
            '', '', '',
            this.inputModel.flooR_ID,
            this.inputModel.apartmenT_ID,
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
}