import { Component, ViewEncapsulation, Injector, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { Paginator, LazyLoadEvent } from "primeng/primeng";
import { Table } from "primeng/table";
import { BuildingsServiceProxy, BuildingPagingSearchDTO } from "@shared/service-proxies/service-proxies";

@Component({
    templateUrl: './buildings-list.view.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../styles.css']
})
export class BuildingsListView extends AppComponentBase {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    filter = { code: '', name: '' }
    selection: BuildingPagingSearchDTO = undefined;

    constructor(
        injector: Injector,
        private service: BuildingsServiceProxy
    ) {
        super(injector);
    }

    getItems(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this.service.pagingSearch(
            this.filter.code,
            this.filter.name,
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
        this.navigate([`/app/admin/building-create`]);
    }
    detailItem(item: BuildingPagingSearchDTO) {
        this.navigate([`/app/admin/building-detail/${item.buildinG_ID}`]);
    }
    modifyItem(item: BuildingPagingSearchDTO) {
        this.navigate([`/app/admin/building-modify/${item.buildinG_ID}`]);
    }
    deleteItem(item: BuildingPagingSearchDTO) {
        this.message.confirm(
            this.l('BuildingDeleteWarningMessage', item.buildinG_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.service.delete(item.buildinG_ID)
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
            'Pages.Group03.Buildings.Create',
            'Pages.Group03.Buildings.Update',
            'Pages.Group03.Buildings.Delete',
            'Pages.Group03.Buildings.Approve'
        )) return false;
        return true;
    }
}