import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { XeServiceProxy, XE_DTO } from "@shared/service-proxies/service-proxies";


@Component({
    templateUrl: './xe-list.component.html',
    styleUrls: ['./xe-list.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class XeListComponent extends AppComponentBase implements OnInit {

    /**
     *
     */
    constructor(injector: Injector,
        private xeService: XeServiceProxy) {
        super(injector);
    }

    loading: boolean = false;
    selectedXe: XE_DTO = new XE_DTO();
    filterInput: XE_DTO = new XE_DTO();
    cols: any[];
    indexs: number = 0;

    ngOnInit(): void {
        this.cols = [
            { field: 'index', name: 'index', header: 'STT' },
            { field: 'xE_CODE', name: 'xE_CODE', header: 'Mã xe' },
            { field: 'xE_NAME', name: 'xE_NAME', header: 'Tên xe' },
            { field: 'xE_COLOR', name: 'xE_COLOR', header: 'Màu xe' },
            { field: 'xE_MANUFACTURER', name: 'xE_MANUFACTURER', header: 'Hãng sản xuất' },
            { field: 'xE_LICENSE_PLATE', name: 'xE_LICENSE_PLATE', header: 'Biển số xe' },
        ]
        this.loading = true;
        this.search();

    }

    addNew() {
        this.navigate(['/app/admin/xe-group11-add']);
    }

    maintain(record) {
        this.navigate(['/app/admin/bao-duong-add', record]);
    }

    onRowDblClick(event, rowData) { 
        this.navigate(['/app/admin/xe-group11-detail', rowData.xE_CODE]);
     }

    delete() {
        if (!this.selectedXe.xE_CODE) {
            this.message.info("Vui lòng chọn xe cần xóa");
        }
        else {
            this.message.confirm(
                this.l('CarDeleteWarningMessage', this.selectedXe.xE_CODE),
                this.l('AreYouSure'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.loading = true;
                        this.xeService.xE_Delete(
                            this.selectedXe.xE_ID
                        ).subscribe((response) => {
                            if (response.length === 0) {
                                this.message.success("Xóa thành công.");
                            } else {
                                this.message.error(response["ERROR_DESC"]);
                            }
                            this.search();
                        });
                    }
                }
            );
        }
    }

    search() {
        this.xeService.xE_Search(this.filterInput).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            this.loading = false;
        })
    }

    viewDetail() {
        if(!this.selectedXe.xE_CODE) {this.message.info("Vui lòng chọn xe cần xem thông tin")}
        this.navigate(['/app/admin/xe-group11-detail', this.selectedXe.xE_CODE]);
    }
}