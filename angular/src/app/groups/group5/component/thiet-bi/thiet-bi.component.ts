
import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { DeviceServiceProxy, CM_DEV_DTO, PhongBanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ArrayType } from "@angular/compiler";
import { throwIfEmpty } from "rxjs/operators";
import { ExportService } from '../_services/_services.component';



@Component({
  templateUrl: './thiet-bi.component.html',
  styleUrls: ['./thiet-bi.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class ThietBiComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector,
    private DeviceServiceProxy: DeviceServiceProxy,
    private PhongBanServiceProxy: PhongBanServiceProxy,
    private ExportService: ExportService
  ) {
    super(injector);

    this.filterInput.brancH_ID = '';
    this.filterInput.devicE_CODE = '';
    this.filterInput.devicE_NAME = '';
    this.filterInput.activE_STATUS = '';

  }
  isLoading = true;
  records: CM_DEV_DTO[] = [];
  //get all data from branchID
  isGetAll: boolean = false;

  //các field cần render ra với header là header của table và field và field của db
  cols = [

    { field: 'No', header: 'STT' },
    { field: 'devicE_CODE', header: 'Mã thiết bị' },
    { field: 'devicE_NAME', header: 'Tên thiết bị' },
    { field: 'activE_STATUS', header: 'Hoạt động' },
    { field: 'autH_STATUS', header: 'Trạng thái duyệt' },
    { field: 'brancH_NAME', header: 'Đơn vị' },
  ];

  listSelectRow: CM_DEV_DTO[] = null;
  filterInput: CM_DEV_DTO = new CM_DEV_DTO();

  ngOnInit(): void {
    this.getAllBranchFromCurren();
    this.search();
  }

  addNew() {
    this.navigate(['/app/admin/them-thiet-bi']);
  }
  listId;
  delete() {
    let value = 0;
    this.listSelectRow.forEach(element => {
      if ((element.autH_STATUS == null && element.approvE_DT != null) || ((element.autH_STATUS == null && element.approvE_DT == null))) { value = 1; }
      if (this.listId == null)
        this.listId = element.devicE_ID;
      else
        this.listId = this.listId + ',' + element.devicE_ID;
    });
    if (this.listId == null) {
      this.message.error("Không có dữ liệu để xóa")
    }

    else if (value == 0) {
      this.message.confirm(
        'Xác nhận xóa',
        'Bạn có chắc chắn muốn xóa dữ liệu này?',
        (isConfirned) => {
          if (isConfirned) {
            this.DeviceServiceProxy.dEVICE_Delete(this.listId).subscribe(response => {
              console.log(this.filterInput)
              console.log(response)
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
    } else if (value == 1) {
      this.message.error("Đang chờ duyệt", 'Thông báo')
    }
  }
  viewDetail() {
    console.log(this.listSelectRow);
    if (this.listSelectRow == undefined || this.listSelectRow == null) {
      this.message.warn("Vui lòng chọn công việc")
    }
    else if (this.listSelectRow.length > 1) {
      this.message.warn("Chỉ được chọn một công việc")
    } else if (this.listSelectRow.length == 0) {
      this.message.warn("Vui lòng chọn công việc")
    } else {
      this.navigate(['/app/admin/chi-tiet-thiet-bi', { id: this.listSelectRow[0].devicE_CODE }])
    }
  }
  search() {
    this.listSelectRow = null;

    this.DeviceServiceProxy.dEVICE_Search(this.isGetAll == true ? '0' : '1', this.filterInput).subscribe(response => {

      this.records = response;
      this.isLoading = false;

    })

  }
  updateBranchID() {
    this.filterInput.brancH_ID = this.selectecdBranch.BRANCH_ID;
    this.search();
  }
  i = 0;
  listBranch;
  selectecdBranch;
  getAllBranchFromCurren() {

    this.PhongBanServiceProxy.dEPARTMENT_GET_USER_BRANCH_BY_USERID().subscribe(response => {
      if (response) {
        this.PhongBanServiceProxy.dEPARTMENT_GET_BRANCH_ID_NAME_BYID(response['brancH_ID']).subscribe(responesListBranch => {
          this.listBranch = responesListBranch;
          console.log(this.listBranch)
          this.filterInput.brancH_ID = this.listBranch[0].BRANCH_ID;
        })
      }
    })

  }

  get_branch() {


    this.DeviceServiceProxy.dEVICE_Get_All_Branch().subscribe(responseListBranch => {
      this.listBranch = responseListBranch;
    })
    this.search();
  }

  selectRow() {

  }
  update() {
    this.navigate(['/app/admin/chinh-sua-thiet-bi', { id: this.listSelectRow[0].devicE_CODE }])
  }
  export() {
    this.ExportService.exportExcel(this.records, 'Danh sach thiet bi');
  }

}

