import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { DeviceServiceProxy, CM_DEV_DTO ,PhongBanServiceProxy} from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  templateUrl: './them-thiet-bi.component.html',
  styleUrls: ['./them-thiet-bi.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class ThemThietBiComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector, private DeviceServiceProxy: DeviceServiceProxy,private PhongBanServiceProxy:PhongBanServiceProxy,private formBuilder: FormBuilder,) {
    super(injector);
    this.filterInput.maintenancE_CYCLE = '';
    this.filterInput.activE_STATUS='';
  }
  records: CM_DEV_DTO[] = [];
  //các field cần render ra với header là header của table và field và field của db
  cols = [

    { field: 'devicE_ID', header: 'STT' },
    { field: 'devicE_CODE', header: 'Mã thiết bị' },
    { field: 'devicE_NAME', header: 'Tên thiết bị' },
    { field: 'activE_STATUS', header: 'Trạng thái' },
    { field: 'brancH_ID', header: 'Đơn vị' },
  ];

  branchId_dropdown;
  listSelectRow: CM_DEV_DTO[]=[];
  filterInput: CM_DEV_DTO = new CM_DEV_DTO();
  datE_BUY: string = "";
  datE_WARRANTY_BEGIN: string = "";
  datE_WARRANTY_END: string = "";

  //is submit
  submitted = false;
  //form for data submit
  formData: FormGroup;
  //current year
  year = new Date().getFullYear();


  fullYear: Date;
  ngOnInit(): void {
    this.getAllBranchFromCurren();

    this.formData = this.formBuilder.group({
      devicE_CODE: [ this.filterInput.devicE_CODE, [Validators.required]],
      devicE_NAME: [ this.filterInput.devicE_NAME, [Validators.required]],
      brancH_ID: [this.filterInput.brancH_ID, [Validators.required]],
      serial: [this.filterInput.serial, [Validators.required]],
      maintenancE_CYCLE: [ this.filterInput.maintenancE_CYCLE, [Validators.required]],
      datE_BUY: [ this.filterInput.datE_BUY, [Validators.required]],
      datE_WARRANTY_BEGIN: [this.filterInput.datE_WARRANTY_BEGIN, [Validators.required]],
      datE_WARRANTY_END: [this.filterInput.datE_WARRANTY_END, [Validators.required]],
      activE_STATUS: [this.filterInput.activE_STATUS, [Validators.required]],

  });

  }

  listBranch;
  getAllBranchFromCurren(){

    this.PhongBanServiceProxy.dEPARTMENT_GET_USER_BRANCH_BY_USERID().subscribe(response=>{
      console.log(response);
      if(response){
        this.PhongBanServiceProxy.dEPARTMENT_GET_BRANCH_ID_NAME_BYID(response['brancH_ID']).subscribe(responesListBranch=>{
          this.listBranch = responesListBranch;
          console.log(this.listBranch)
        })
      }
    })
    
  }
 //validate date before update device
  compareDate(date1, date2) {

    if (date1 > date2)
      return -1;
    else
      return 0;
  }

  compareDateBeforeUpdate(date1, date2, date3) {
    if (this.compareDate(date1, date3) == -1) {
      this.notify.warn("Ngày mua phải nhỏ hơn ngày hết bảo hành","Thông báo");
      return 0;
    } else if (this.compareDate(date1, date2) == -1) {
      this.notify.warn("Ngày mua phải nhỏ hơn ngày bảo hành","Thông báo");
      return 0;
    } else if (this.compareDate(date2, date3) == -1) {
      this.notify.warn("Ngày bảo hành phải nhỏ hơn ngày hết bảo hành","Thông báo");
      return 0;
    }
    return 1;

  }

  get_branch() {

    this.DeviceServiceProxy.dEVICE_Get_All_Branch().subscribe(responseListBranch => {
      this.listBranch = responseListBranch;
    })
  }


  validateDataSubmit(data : CM_DEV_DTO){
    if(data.devicE_NAME =='' || data.devicE_NAME == null){
      this.message.warn("Vui lòng chọn tên thiết bị");
      return 0;
    }
    if(data.brancH_ID ==''  || data.brancH_ID == null){
      this.message.warn("Vui lòng chọn đơn vị quản lý");
      return 0;
    }
    if(data.serial =='' || data.serial == null){
      this.message.warn("Vui lòng nhập serial");
      return 0;
    }
    return 1;
  }

  save() {
    this.submitted = true;
    var dateBuynew = new Date(this.datE_BUY);
    var warantyBeginnew = new Date(this.datE_WARRANTY_BEGIN);
    var warrantyEndnew = new Date(this.datE_WARRANTY_END);
    
    if (this.formData.invalid) {
      this.message.error('Bạn vui lòng nhập lại đầy đủ các thông tin cần thiết!','Thông báo');
      return;
  }

    if (this.compareDateBeforeUpdate(dateBuynew, warantyBeginnew, warrantyEndnew) == 1) {
      this.filterInput.datE_BUY = moment(new Date(dateBuynew.getFullYear(), dateBuynew.getMonth(), dateBuynew.getDate() + 1));
      this.filterInput.datE_WARRANTY_BEGIN = moment(new Date(warantyBeginnew.getFullYear(), warantyBeginnew.getMonth(), warantyBeginnew.getDate() + 1));
      this.filterInput.datE_WARRANTY_END = moment(new Date(warrantyEndnew.getFullYear(), warrantyEndnew.getMonth(), warrantyEndnew.getDate() + 1));
      // console.log(this.filterInput)  
      this.filterInput.brancH_ID = this.branchId_dropdown.BRANCH_ID;

      console.log(this.filterInput);
      if(this.validateDataSubmit(this.filterInput)==1){
        this.DeviceServiceProxy.dEVICE_Insert(this.filterInput).subscribe(response => {

          if (response[0]['RESULT'] == '0') {
            this.message.success('Thêm mới thành công');
            this.filterInput= new CM_DEV_DTO();
            this.datE_BUY=""; 
            this.datE_WARRANTY_BEGIN="";
            this.datE_WARRANTY_END="";
            this.submitted= false;
          }
          else if (response[0]['RESULT'] == '-1') {
            this.message.error(response[0]['ErrorDesc']);
          }
        })
      }
     
    }

  }

  
  get f() { return this.formData.controls; }

}



