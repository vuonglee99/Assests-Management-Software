//Updated
import {Component, Injector, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {AppComponentBase} from '@shared/common/app-component-base';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {TrangThaiYeuCauSuaChuaServiceProxy, TTYCSC_DTO,} from '@shared/service-proxies/service-proxies';
import {LazyLoadEvent} from 'primeng/components/common/lazyloadevent';
import {Paginator} from 'primeng/components/paginator/paginator';
import {Table} from 'primeng/components/table/table';
import {TTYCSC_URL, viRegStr} from '../utils/constant';
import * as _ from 'lodash';

import * as moment from 'moment';

@Component({
    templateUrl: './TTYCSC.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./TTYCSC.component.css'],
})
export class TTYCSC extends AppComponentBase implements OnInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    readonly viRegstr = viRegStr;
    readonly actions = {
        CREATE: 'Create',
        UPDATE: 'Update',
        DELETE: 'Delete'
    };

    readonly authStatusOptions = [
        {label: this.l('GR6_ACTIVE'), value: '_'},
        {label: this.l('GR6_APPROVED'), value: 'A'},
        {label: this.l('GR6_PENDING'), value: undefined},
        {label: this.l('GR6_DENIED'), value: 'U'},
        {label: this.l('GR6_ALL'), value: '*'}
    ];

    readonly searchFieldOptions = [
        {label: this.l('GR6_TTYCSC_CODE'), value: 'ttycsC_CODE'},
        {label: this.l('GR6_TTYCSC_NAME'), value: 'ttycsC_NAME'},
        {label: this.l('GR6_DESC'), value: 'ttycsC_DESC'}
    ];

    constructor(
        injector: Injector,
        private repairReqStatusService: TrangThaiYeuCauSuaChuaServiceProxy) {
        super(injector);
    }

    searchField: string;
    searchValue: any;

    isFetching: boolean;
    filterInput: TTYCSC_DTO = new TTYCSC_DTO();
    selectedItem: TTYCSC_DTO = new TTYCSC_DTO();

    defaultLevel: number;

    // NOTE InfoModal properties
    detailSchema: TTYCSC_DTO = new TTYCSC_DTO();

    ngOnInit(): void {
        this.filterInput = new TTYCSC_DTO();
        this.filterInput.autH_STATUS = '_';
        // this.approveStatus = '_';

        this.searchField = 'ttycsC_CODE';
        this.detailSchema.ttycsC_NAME = '';
        this.detailSchema.ttycsC_DESC = '';
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
        this.selectedItem = new TTYCSC_DTO();
    }

    search(event?: LazyLoadEvent) {
        if (this.searchField) {
            this.filterInput[this.searchField] = this.searchValue;
        }
        console.log(this.filterInput);
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
        this.repairReqStatusService
            .tTYCSC_SearchFilter(
                Number.isNaN(this.paginator.getPage()) ? 1 : (this.paginator.getPage() + 1),
                this.primengTableHelper.getMaxResultCount(this.paginator, event),
                this.filterInput.ttycsC_NAME,
                this.filterInput.ttycsC_CODE,
                this.filterInput.ttycsC_DESC,
                this.filterInput.autH_STATUS
            )
            .subscribe((res) => {
                    // console.log('all data', res.value);
                    // alert('hit');
                    let rec = [];
                    const keys = Object.keys(res.value);
                    for (let index = 0; index < keys.length; index++) {
                        const key = keys[index];
                        rec[key] = res.value[key];
                    }

                    this.primengTableHelper.records = rec.map((val) => {
                        return new TTYCSC_DTO({
                            ttycsC_ID: val.TTYCSC_ID,
                            ttycsC_CODE: val.TTYCSC_CODE,
                            ttycsC_NAME: val.TTYCSC_NAME,
                            ttycsC_DESC: val.TTYCSC_DESC,
                            notes: val.NOTES,
                            recorD_STATUS: val.RECORD_STATUS,
                            makeR_ID: val.MAKER_ID,
                            creatE_DT: val.CREATE_DT,
                            autH_STATUS: val.AUTH_STATUS,
                            checkeR_ID: val.CHECKER_ID,
                            approvE_DT: val.APPROVE_DT,
                            typE_APPROVE: val.TYPE_APPROVE
                        });
                    });

                    this.primengTableHelper.totalRecordsCount = res.totaL_RECORD;
                    this.primengTableHelper.hideLoadingIndicator();
                    this.defaultLevel = res.totaL_RECORD + 1;
                }
            );
    }

    addNewClick() {
        this.repairReqStatusService.tTYCSC_GetValueGenCode().subscribe((res) => {
            this.detailSchema = new TTYCSC_DTO();
            this.detailSchema.ttycsC_NAME = '';
            this.detailSchema.ttycsC_DESC = '';
            this.detailSchema.ttycsC_CODE = res;
            this.isFetching = false;
            $('#modalInfo').modal('show');
        });
    }

    addNew() {
        this.isFetching = true;
        const {ttycsC_CODE, ttycsC_NAME, ttycsC_DESC, notes, autH_STATUS, checkeR_ID, approvE_DT} = this.detailSchema;
        this.repairReqStatusService.tTYCSC_Create(null, ttycsC_CODE, ttycsC_NAME, ttycsC_DESC, notes, '1', null, moment(), autH_STATUS, checkeR_ID, approvE_DT, this.actions.CREATE)
            .subscribe((res) => {
                this.reloadPage('Create');
                this.message.success(this.l('GR6_REQUEST_SENT'));
                $('#modalInfo').modal('hide');
                this.isFetching = false;
            });
    }

    delete() {
        const {ttycsC_ID, ttycsC_CODE, ttycsC_NAME, ttycsC_DESC, notes} = this.selectedItem;
        this.repairReqStatusService.tTYCSC_Create(ttycsC_ID, ttycsC_CODE, ttycsC_NAME, ttycsC_DESC, notes, '0', null, moment(), undefined, null, null, this.actions.DELETE)
            .subscribe((res) => {
                this.reloadPage('Create');
                this.message.success(this.l('GR6_REQUEST_SENT'));
                $('#modalInfo').modal('hide');
                this.isFetching = false;
            });
    }

    viewDetail() {
        this.navigate([`${TTYCSC_URL}/${this.selectedItem.ttycsC_ID}`]);
    }

    goToEditPage() {
        this.navigate([`${TTYCSC_URL}/${this.selectedItem.ttycsC_ID}/edit`]);
    }

    isRequest(): boolean {
        const {autH_STATUS, recorD_STATUS} = this.selectedItem;
        const isActiveRecord = recorD_STATUS === '1' && autH_STATUS === 'A';
        return !isActiveRecord;
    }

    handleSearch(event: any) {
        Object.keys(event).forEach(key => {
            this.filterInput[key] = event[key];
        });
        this.search();
    }
}
