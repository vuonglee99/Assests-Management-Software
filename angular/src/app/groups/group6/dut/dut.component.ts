//Updated
import {Component, Injector, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {AppComponentBase} from '@shared/common/app-component-base';
import {appModuleAnimation} from '@shared/animations/routerTransition';
import {DoUuTienServiceProxy, DUT_DTO,} from '@shared/service-proxies/service-proxies';
import {LazyLoadEvent} from 'primeng/components/common/lazyloadevent';
import {Paginator} from 'primeng/components/paginator/paginator';
import {Table} from 'primeng/components/table/table';
import viewDUT from './viewDUT';
import regex, {viRegStr} from './utils/constant';
import * as _ from 'lodash';

@Component({
    templateUrl: './dut.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./dut.component.css'],
})
export class DUT extends AppComponentBase implements OnInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    public readonly statusOptions = [
        {label: this.l('GR6_ACTIVE'), value: '1'},
        {label: this.l('GR6_INACTIVE'), value: '0'}
    ];

    readonly searchFieldOptions = [
        {label: this.l('GR6_DUT_CODE'), value: 'duT_CODE'},
        {label: this.l('GR6_DUT_NAME'), value: 'duT_NAME'},
        {label: this.l('GR6_DESC'), value: 'duT_DESC'}
    ];

    constructor(
        injector: Injector,
        private dutService: DoUuTienServiceProxy) {
        super(injector);
    }

    filterInput: viewDUT = new viewDUT();
    selectedTTYCSC: viewDUT = new viewDUT();

    //Min level - max number :))

    defaultLevel: number;
    viRegstr: string = viRegStr;

    // NOTE InfoModal properties
    detailTitle = 'Thêm mới độ ưu tiên';
    detailSchema: DUT_DTO = new DUT_DTO();
    modalAction = '';
    isFetching: boolean;

    ngOnInit(): void {
        this.filterInput.recorD_STATUS = '1';
        this.modalAction = 'CREATE';
        this.detailSchema.duT_NAME = '';
        this.detailSchema.duT_DESC = '';
        this.detailSchema.duT_CODE = '';
        this.dutService.dUT_GetValueGenCode().subscribe((res) => {
            this.detailSchema.duT_CODE = res;
        });
    }

    reloadPage(action: string): void {
        if (action === 'Delete') {
            if ((this.paginator.getPage() * this.paginator.rows + 1) == this.primengTableHelper.totalRecordsCount) {
                this.paginator.changePage(this.paginator.getPage() == 0 ? 1 : this.paginator.getPage() - 1);
            } else {
                this.paginator.changePage(this.paginator.getPage());
            }
        } else {
            // if (((this.paginator.getPage() + 1) * this.paginator.rows) == this.primengTableHelper.totalRecordsCount)
            //     {
            //         this.primengTableHelper.totalRecordsCount ++;
            //         this.paginator.changePage(this.paginator.getPage() + 1);
            //     }
            // else
            this.paginator.changePage(this.paginator.getPage());
        }
    }

    resetSelection() {
        this.selectedTTYCSC = new viewDUT();
    }

    search(event?: LazyLoadEvent) {
        try {
            //Recognise sort event and handle
            const {sortField, sortOrder} = event;
            if (sortField !== undefined) {
                if (sortField === 'duT_NAME') {
                    this.fetchData(event, (sortOrder > 0) ? 'asc' : 'desc');
                }
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
        this.dutService
            .dUT_SearchFilter(
                Number.isNaN(this.paginator.getPage()) ? 1 : (this.paginator.getPage() + 1),
                this.primengTableHelper.getMaxResultCount(this.paginator, event),
                this.filterInput.duT_NAME,
                this.filterInput.duT_CODE,
                this.filterInput.duT_DESC,
                this.filterInput.recorD_STATUS,
                sortDirection
            )
            .subscribe((res) => {
                    console.log(res);
                    let rec = [];
                    const keys = Object.keys(res.value);
                    for (let index = 0; index < keys.length; index++) {
                        const key = keys[index];
                        rec[key] = res.value[key];
                    }
                    this.primengTableHelper.records = rec.map((val) => {
                        return new viewDUT({
                            duT_ID: val.DUT_ID,
                            duT_CODE: val.DUT_CODE,
                            duT_NAME: val.DUT_NAME,
                            duT_DESC: val.DUT_DESC,
                            notes: val.NOTES,
                            recorD_STATUS: val.RECORD_STATUS,
                            makeR_ID: val.MAKER_ID,
                            creatE_DT: val.CREATE_DT,
                            autH_STATUS: val.AUTH_STATUS,
                            checkeR_ID: val.CHECKER_ID,
                            approvE_DT: val.APPROVE_DT,
                            RowNo: val.RowNo,
                            duT_LEVEL: val.DUT_LEVEL
                        });
                    });

                    this.primengTableHelper.totalRecordsCount = res.totaL_RECORD;
                    this.primengTableHelper.hideLoadingIndicator();
                    this.defaultLevel = res.totaL_RECORD + 1;
                }
            );
    }

    addNewClick() {
        this.isFetching = false;
        console.log(this.detailSchema);
        if (this.detailSchema.duT_CODE) {
            $('#modalInfo').modal('show');
            return;
        }

        this.modalAction = 'CREATE';
        this.detailSchema.duT_NAME = '';
        this.detailSchema.duT_DESC = '';
        this.detailSchema.duT_CODE = '';
        this.dutService.dUT_GetValueGenCode().subscribe((res) => {
            this.detailSchema.duT_CODE = res;
            $('#modalInfo').modal('show');
        });
        this.defaultLevel = this.primengTableHelper.records.length + 1;
    }

    addNew() {
        this.isFetching = true;
        const {
            duT_CODE,
            duT_NAME,
            duT_DESC,
            duT_LEVEL,
            notes,
            recorD_STATUS,
            makeR_ID,
            creatE_DT,
            autH_STATUS,
            checkeR_ID,
            approvE_DT,
        } = this.detailSchema;

        this.dutService
            .dUT_Create(
                duT_CODE,
                duT_NAME,
                duT_DESC,
                duT_LEVEL,
                notes,
                recorD_STATUS,
                makeR_ID,
                creatE_DT,
                autH_STATUS,
                checkeR_ID,
                approvE_DT
            )
            .subscribe((res) => {
                this.reloadPage('Create');
                this.message.success('Đã thêm độ ưu tiên mới');
                this.detailSchema = new DUT_DTO();
                $('#modalInfo').modal('hide');
                this.isFetching = false;
            });
    }

    delete() {
        this.dutService.dUT_Delete(this.selectedTTYCSC.duT_ID).subscribe((res) => {
            this.reloadPage('Delete');
            this.message.success('Đã xóa độ ưu tiên');
        });
    }

    validateInput(event) {
        let str = String.fromCharCode(event.charCode);
        const reunicode = regex;
        return reunicode.test(str);
    }

    //NOTE ViewDetailHandler
    viewDetail() {
        this.navigate([`/app/admin/douutien/${this.selectedTTYCSC.duT_ID}`]);
    }

    goToEditPage() {
        this.navigate([`/app/admin/douutien/${this.selectedTTYCSC.duT_ID}/edit`]);
    }

    handleSearch(event: any) {
        console.log(event);
        Object.keys(event).forEach(key => {
            this.filterInput[key] = event[key];
        });
        this.search();
    }
}
