import { Component, ViewEncapsulation, Injector, ViewChild, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { Paginator, LazyLoadEvent } from "primeng/primeng";
import { Table } from "primeng/table";
import { ResidentsServiceProxy, ResidentPagingSearchDTO, ApartmentsServiceProxy, FloorsServiceProxy, BuildingsServiceProxy } from "@shared/service-proxies/service-proxies";
import { SearchableSelectComponentItem } from "../components/searchable-select.component";

@Component({
    templateUrl: './residents-list.view.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../styles.css']
})
export class ResidentsListView extends AppComponentBase implements OnInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    filter = { buildingId: '', floorId: '', apartmentId: '', code: '', name: '', idcard: '' }
    selection: ResidentPagingSearchDTO = undefined;
    buildingsList: SearchableSelectComponentItem[] = [];
    floorsList: SearchableSelectComponentItem[] = [];
    apartmentsList: SearchableSelectComponentItem[] = [];


    constructor(
        injector: Injector,
        private service: ResidentsServiceProxy,
        private buildingsService: BuildingsServiceProxy,
        private floorsService: FloorsServiceProxy,
        private apartmentsService: ApartmentsServiceProxy,
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.buildingsService.pagingSearch('', '', '1', '1', '', 0, 500)
            .subscribe(result => this.buildingsList = result.items
                .map(x => new SearchableSelectComponentItem(
                    x.buildinG_ID, x.buildinG_CODE + ' - ' + x.buildinG_NAME
                ))
            );
    }

    getItems(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this.service.pagingSearch(
            this.filter.buildingId,
            this.filter.floorId,
            this.filter.apartmentId,
            this.filter.code,
            this.filter.name,
            this.filter.idcard,
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

    createItem() {
        this.navigate([`/app/admin/resident-create`]);
    }
    detailItem(item: ResidentPagingSearchDTO) {
        this.navigate([`/app/admin/resident-detail/${item.residenT_ID}`]);
    }
    modifyItem(item: ResidentPagingSearchDTO) {
        this.navigate([`/app/admin/resident-modify/${item.residenT_ID}`]);
    }
    deleteItem(item: ResidentPagingSearchDTO) {
        this.message.confirm(
            this.l('ResidentDeleteWarningMessage', item.residenT_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.service.delete(item.residenT_ID)
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

    checkPermissionOnlyView() {
        if (this.isGrantedAny(
            'Pages.Group03.Residents.Create',
            'Pages.Group03.Residents.Update',
            'Pages.Group03.Residents.Delete',
            'Pages.Group03.Residents.Approve'
        )) return false;
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