import { ViewEncapsulation, Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { NhaSanXuatServiceProxy, NSX_DTO } from "@shared/service-proxies/service-proxies";
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

@Component({
    templateUrl: './nsx-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class NSXListComponent extends AppComponentBase implements OnInit {

    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    /**
     *
     */
    records: NSX_DTO[] = [];
    cols: any[];
    selectedNSX: NSX_DTO;
    filterInput: NSX_DTO = new NSX_DTO();

    constructor(injector: Injector,
        private nsxService: NhaSanXuatServiceProxy) {
        super(injector);
    }


    ngOnInit(): void {
        this.search();
        this.cols = [
            { field: 'nsX_CODE', name: 'nsX_CODE', header: 'Mã NSX' },
            { field: 'nsX_NAME', name: 'nsX_NAME', header: 'Tên NSX' },
            { field: 'nsX_FROM', name: 'nsX_FROM',header: 'Xuất xứ'},
           ]
    }

    delete(record: NSX_DTO) {
        this.message.confirm(
            this.l('Do you want to delete NSX', record.nsX_NAME),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.nsxService.nSX_Delete(record.nsX_ID)
                        .subscribe(() => {
                            this.search();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    search() {
        this.nsxService.nSX_Search(this.filterInput).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
        console.log(this.records);
    }

}