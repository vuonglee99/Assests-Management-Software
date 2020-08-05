import {Component, Injector, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {AppComponentBase} from '@shared/common/app-component-base';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {DVT_DTO, DVTServiceProxy} from '@shared/service-proxies/service-proxies';
import {LazyLoadEvent} from 'primeng/components/common/lazyloadevent';
import {Paginator} from 'primeng/components/paginator/paginator';
import {Table} from 'primeng/components/table/table';
import * as _ from 'lodash';

@Component({
    templateUrl: './dvt.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./dvt.component.css'],
})
export class DVT extends AppComponentBase implements OnInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    public readonly statusOptions = [
        {label: this.l('GR6_ACTIVE'), value: '1'},
        {label: this.l('GR6_INACTIVE'), value: '0'}
    ];

    readonly searchFieldOptions = [
        {label: this.l('GR6_DVT_CODE'), value: 'dvT_CODE'},
        {label: this.l('GR6_DVT_NAME'), value: 'dvT_NAME'},
        {label: this.l('GR6_DESC'), value: 'duT_DESC'}
    ];

    constructor(
        injector: Injector,
        private dvtServiceProxy: DVTServiceProxy) {
        super(injector);
    }

    isFetching: boolean;
    filterInput: DVT_DTO = new DVT_DTO();
    selectedItem: DVT_DTO = new DVT_DTO();


    detailSchema: DVT_DTO = new DVT_DTO();

    ngOnInit(): void {
        // this.filterInput.recorD_STATUS = '1';
        this.detailSchema.dvT_NAME = '';
        this.detailSchema.dvT_DESC = '';
        this.isFetching = false;

    }

    reloadPage(action: string): void {
        if (action === 'Delete') {
            if ((this.paginator.getPage() * this.paginator.rows + 1) == this.primengTableHelper.totalRecordsCount) {
                this.paginator.changePage(this.paginator.getPage() == 0 ? 1 : this.paginator.getPage() - 1);
            } else {
                this.paginator.changePage(this.paginator.getPage());
            }
        } else {
            this.paginator.changePage(this.paginator.getPage());
        }
    }

    resetSelection() {
        this.selectedItem = new DVT_DTO();
    }

    search(event?: LazyLoadEvent) {
        //Recognise sort event and handle
        try {
            const {sortField, sortOrder} = event;
            if (sortField !== undefined) {
                this.primengTableHelper.records = _.orderBy(
                    this.primengTableHelper.records,
                    [sortField],
                    (sortOrder > 0) ? ['asc'] : ['desc']
                );
            } else {
                this.fetchData(event, null);
            }
        } catch (e) {
            this.fetchData(event, null);
        }
    }

    fetchData(event: LazyLoadEvent, sortDirection: string) {
        this.resetSelection();
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this.dvtServiceProxy
            .cM_DVT_searchFilter(
                Number.isNaN(this.paginator.getPage()) ? 1 : (this.paginator.getPage() + 1),
                this.primengTableHelper.getMaxResultCount(this.paginator, event),
                this.filterInput.dvT_CODE,
                this.filterInput.dvT_NAME,
                this.filterInput.recorD_STATUS
            )
            .subscribe((res) => {
                    let rec = [];
                    const keys = Object.keys(res.value);
                    for (let index = 0; index < keys.length; index++) {
                        const key = keys[index];
                        rec[key] = res.value[key];
                    }
                    this.primengTableHelper.records = rec.map((val) => {
                        return new DVT_DTO({
                            dvT_ID: val.DVT_ID,
                            dvT_CODE: val.DVT_CODE,
                            dvT_NAME: val.DVT_NAME,
                            dvT_DESC: val.DVT_DESC,
                            notes: val.NOTES,
                            recorD_STATUS: val.RECORD_STATUS,
                            makeR_ID: val.MAKER_ID,
                            creatE_DT: val.CREATE_DT,
                            autH_STATUS: val.AUTH_STATUS,
                            checkeR_ID: val.CHECKER_ID,
                            approvE_DT: val.APPROVE_DT,
                            brancH_CREATE: null
                        });
                    });
                    this.primengTableHelper.totalRecordsCount = res.totaL_RECORD;
                    this.primengTableHelper.hideLoadingIndicator();
                }
            );
    }

    addNewClick() {
        this.navigate(['/app/admin/donvitinh-add-detail']);
    }

    delete() {
        this.dvtServiceProxy.cM_DVT_Delete(this.selectedItem.dvT_ID).subscribe((res) => {
            this.reloadPage('Delete');
            this.message.success(this.l('GR6_DELETED_MESSAGE'));
        });
    }

    viewDetail() {
        this.navigate([`/app/admin/donvitinh/${this.selectedItem.dvT_ID}`]);
    }

    goToEditPage() {
        this.navigate([
            `/app/admin/donvitinh-update-detail/${this.selectedItem.dvT_ID}`,
        ]);
    }

    handleSearch(event: any) {
        console.log(event);
        Object.keys(event).forEach(key => {
            this.filterInput[key] = event[key];
        });
        this.search();
    }
}
