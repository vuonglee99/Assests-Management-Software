import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { PhongBanServiceProxy, CM_PHONGBAN_DTO,DeviceServiceProxy } from "@shared/service-proxies/service-proxies";
import {ExportService} from '../_services/_services.component';


@Component({
    templateUrl: './phong-ban.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./phong-ban.component.css']
})
export class PhongBanComponent extends AppComponentBase implements OnInit {

    /**
     *
     */
    constructor(injector: Injector,
        private PhongBanServiceProxy: PhongBanServiceProxy,
        private ExportService:ExportService,
        private DeviceServiceProxy:DeviceServiceProxy
    ) {
        super(injector);
        this.filterInput.recorD_STATUS='';
    }

    records: CM_PHONGBAN_DTO[] = [];
    isGetAll: boolean = false;
    isLoading : false;
    //các field cần render ra với header là header của table và field và field của db
    cols = [
        { field: 'No', header: 'STT' },
        { field: 'deP_CODE', header: 'Mã phòng ban' },
        { field: 'deP_NAME', header: 'Tên phòng ban' },
        { field: 'tel', header: 'Số điện thoại' },
        { field: 'recorD_STATUS', header: 'Trạng thái' },
        { field: 'autH_STATUS', header: 'Trạng thái duyệt' },
        { field: 'brancH_NAME', header: 'Đơn vị' },
        { field: 'notes', header: 'Mô tả' }
    ];
    filterInput: CM_PHONGBAN_DTO = new CM_PHONGBAN_DTO();


    ngOnInit(): void {

        this.get_branch();
    }

    addNew() {
        // this.navigate(['/app/admin/xe-add']);
    }

    update() {

    }

    listSelectRow: CM_PHONGBAN_DTO[] = [];
    delete() {
       let value = 0;
        // if( (this.filterInput.autH_STATUS == null && this.filterInput.approvE_DT !=null ) || ((this.filterInput.autH_STATUS == null && this.filterInput.approvE_DT ==null ))){
        //     this.message.error("Đang chờ duyệt",'Thông báo')
        //     return;
        // }
        this.listSelectRow.forEach(element => {

            if((element.autH_STATUS == null && element.approvE_DT !=null ) || ((element.autH_STATUS == null && element.approvE_DT ==null ))){value = 1;}
            if (this.listId == null)
                this.listId = element.deP_ID;
            else
                this.listId = this.listId + ',' + element.deP_ID;
        });

        console.log(value)
        if (this.listId == null) {
            this.message.error("Không có dữ liệu để xóa")
        }
        else if(value == 0){
            this.message.confirm(
                'Xác nhận xóa',
                'Bạn có chắc chắn muốn xóa dữ liệu này?',
                (isConfirned) => {
                    if (isConfirned) {
                        this.PhongBanServiceProxy.dEPARTMENT_Delete(this.listId).subscribe(response => {
                            if (response['Result'] == '0') {
                                this.message.success('Xóa thành công');
                                this.get_branch();
                                this.listId = null;
                            } else {
                                this.message.error(response['ErrorDesc']);
                                this.get_branch();
                                this.listId = null;
                            }
                        })
                    } else {
                        this.listId = null;
                    }
                }
            )
        }
        else if(value == 1){
            this.message.error("Đang chờ duyệt",'Thông báo')    
        }
    }

    search() {
        this.listSelectRow = null;
        this.PhongBanServiceProxy.dEPARTMENT_Search(this.isGetAll==true?'0':'1',this.filterInput).subscribe(response => {

            let i = 1;
            response.forEach(item => {
                item["No"] = i++;
            })
            this.records = response;

        })
    }


    branchs;
    listBranch;
    get_branch() {
        this.PhongBanServiceProxy.dEPARTMENT_GET_USER_BRANCH_BY_USERID().subscribe(responseBranch => {
            this.filterInput.brancH_ID = responseBranch.brancH_ID;
            this.search();
            this.PhongBanServiceProxy.dEPARTMENT_GET_BRANCH_ID_NAME_BYID(responseBranch['brancH_ID']).subscribe(responseListBranch => {
                this.listBranch = responseListBranch;
            })
        })
    }


    currentId;
    listId;
    selectRow() {
        console.log(this.listSelectRow);
    }
    viewDetail() {
        if (this.listSelectRow.length == 0) {
            this.message.warn("Vui lòng chọn phòng ban", "Thông báo")
        }
        else if (this.listSelectRow.length > 1) {
            this.message.warn("Chỉ được chọn một phòng ban", "Thông báo")
        } else {
            this.navigate(['/app/admin/chi-tiet-phong-ban', { id: this.currentId }])
        }

    }
    export() {
        this.ExportService.exportExcel(this.records,'Danh sách phòng ban');
    }

}
