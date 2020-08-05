
// import { ViewEncapsulation, Component, Injector, TemplateRef} from "@angular/core";
// import { appModuleAnimation } from "@shared/animations/routerTransition";
// import { AppComponentBase } from "@shared/common/app-component-base";
// import { NSXServiceProxy, CM_NSX_DTO } from "@shared/service-proxies/service-proxies";


// @Component({
//     templateUrl: './nsx-list.component.html',
//     encapsulation: ViewEncapsulation.None,
//     animations: [appModuleAnimation()],
//     styleUrls: ['../nsx-detail/nsx-detail.component.css']
// })
// export class NsxListComponent extends AppComponentBase {

//     /**
//      *
//      */

//     constructor(injector: Injector,
//         private nsxService: NSXServiceProxy) {
//         super(injector);

//     }

//     records: CM_NSX_DTO[] = [];

//     inputModel: CM_NSX_DTO = new CM_NSX_DTO();

//     selectedNsx: CM_NSX_DTO = new CM_NSX_DTO();


//     ngOnInit(): void {
//         this.show();
//     }

//     detail(id: string ) {
//         if (id != null) {
//             this.navigate(['/app/admin/nsx-detail', id]);
//         } else {
//             this.message.warn('Vui lòng chọn một đơn vị');
//         }
//     }
//     update( id: string ) {

//     }
//     add(){
//         this.navigate(['/app/admin/nsx-add']);
//     }

//     delete() {
//             if (this.selectedNsx.nsX_ID != null) {
//                 this.message.confirm('Xác nhận xóa', 'Bạn có chắc muốn xóa dữ liệu này ?', (isConfirmed) => {
//                     if (isConfirmed) {
//                         this.nsxService.cM_NSX_Delete(this.selectedNsx.nsX_ID).toPromise().then(() => this.ngOnInit());
//                         this.message.success('Xóa thành công');
//                         this.selectedNsx = new CM_NSX_DTO();
//                     }
//                 });
//             } else {
//                 this.message.warn('Vui lòng chọn đơn vị cần xóa');
//             }
//     }
//     show() {
//         this.nsxService.cM_NSX_Show(this.inputModel).subscribe(response => {
//             this.records = response;
//         })
//     }
//     selectedRow(nsx: CM_NSX_DTO) {
//         this.selectedNsx = nsx;
//     }

// }

import { ViewEncapsulation, Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { NSXServiceProxy, CM_NSX_DTO } from "@shared/service-proxies/service-proxies";
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

@Component({
    templateUrl: './nsx-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class NsxListComponent extends AppComponentBase implements OnInit {

    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    /**
     *
     */
    records: CM_NSX_DTO[] = [];
    cols: any[];
    loading: boolean;
    selectedNSX: CM_NSX_DTO;
    filterInput: CM_NSX_DTO = new CM_NSX_DTO();

    constructor(injector: Injector,
        private nsxService: NSXServiceProxy) {
        super(injector);
    }


    ngOnInit(): void {
        this.loading = true;
        this.filterInput.nsX_NAME = null;
        this.filterInput.nsX_CODE = null;
        this.filterInput.recorD_STATUS = null;
        this.filterInput.nsX_ID = null;
        this.filterInput.nsX_FROM = null;
        this.search();
        this.cols = [
            { field: 'nsX_CODE', name: 'nsX_CODE', header: 'Mã NSX' },
            { field: 'nsX_NAME', name: 'nsX_NAME', header: 'Tên NSX' },
            { field: 'nsX_FROM', name: 'nsX_FROM',header: 'Xuất xứ'},
           ]
    }
    add(){
                this.navigate(['/app/admin/nsx-add']);
            }
    detail() {
        if (this.selectedNSX == null) {
            this.message.error(this.l('Group10_DETAIL_CHOSE'));
        } else {
            this.navigate(['/app/admin/nsx-detail', this.selectedNSX.nsX_ID]);
        }

    }
    update() {
        if (this.selectedNSX == null) {
            this.message.error(this.l('Group10_UPDATE_CHOSE'));
        } else {
            this.navigate(['/app/admin/nsx-edit', this.selectedNSX.nsX_ID]);
        }
    }
    delete() {
        if (this.selectedNSX == null) {
            this.message.error(this.l('Group10_DELETE_CHOSE'));
        } else {
            this.message.confirm(
                this.l('AreYouSure'),
                    this.l('Group10_DELETE_WARNING'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.loading = true;
                        this.nsxService.cM_NSX_Delete(
                            this.selectedNSX.nsX_ID
                        ).subscribe((response) => {
                            if (response.length !== 0) {
                                this.message.success(this.l('Group10_DELETE_COMPLETE'));
                                this.search();
                            } else {
                                this.message.error(response["ERROR_DESC"]);
                            }
                        });
                    }
                }
            );
        }

        // this.message.confirm(
        //     this.l('Do you want to delete NSX', record.nsX_NAME),
        //     this.l('AreYouSure'),
        //     (isConfirmed) => {
        //         if (isConfirmed) {
        //             this.nsxService.cM_NSX_Delete(record.nsX_ID)
        //                 .subscribe(() => {
        //                     this.reloadPage();
        //                     this.message.success(this.l('SuccessfullyDeleted'));
        //                 });
        //         }
        //     }
        // );
    }

    search() {
        this.loading= true;
        this.nsxService.cM_NSX_Search(this.filterInput).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            this.loading= false;
        });
    }

}
