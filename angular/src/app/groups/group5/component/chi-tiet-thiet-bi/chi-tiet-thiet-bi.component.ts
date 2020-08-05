import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { DeviceServiceProxy, CM_DEV_DTO } from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
@Component({
  templateUrl: './chi-tiet-thiet-bi.component.html',
  styleUrls: ['./chi-tiet-thiet-bi.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class ChiTietThietBiComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector, private DeviceServiceProxy: DeviceServiceProxy,private formBuilder : FormBuilder) {
    super(injector);
    this.editPageState = this.getRouteData('editPageState');

    this.filterInput.brancH_ID='';
    this.filterInput.devicE_CODE='';
    this.filterInput.devicE_NAME='';
    this.filterInput.activE_STATUS='';
  }
  records: CM_DEV_DTO[] = [];
  submitted = false;
  currentID: string;
  //các field cần render ra với header là header của table và field và field của db
  cols = [

    { field: 'devicE_ID', header: 'STT' },
    { field: 'devicE_CODE', header: 'Mã thiết bị' },
    { field: 'devicE_NAME', header: 'Tên thiết bị' },
    { field: 'activE_STATUS', header: 'Trạng thái' },
    { field: 'brancH_ID', header: 'Đơn vị' },
  ];

  listSelectRow: CM_DEV_DTO[]=[];
  filterInput: CM_DEV_DTO = new CM_DEV_DTO();
  datE_BUY: string = "";
  datE_WARRANTY_BEGIN: string = "";
  datE_WARRANTY_END: string = "";

  formData:FormGroup;

  //required
  isEmtyDataSubmit={
    serial:0,devicE_NAME:0,brancH_ID:0
  }
  
  //is update then disabled calendar
  isUPdate;
  //edit 
  editPageState:string="";
  //varibale to conver year fillterInput
  fullYear: Date;
  ngOnInit(): void {
    
    this.search();

    this.getIdUSer();
    this.formData = this.formBuilder.group({
      devicE_CODE: [ {value:this.filterInput.devicE_CODE, disabled: this.editPageState === "view" ? true : false}, [Validators.required]],
      devicE_NAME: [ {value:this.filterInput.devicE_NAME, disabled: this.editPageState === "view" ? true : false}, [Validators.required]],
      brancH_ID: [{value:this.filterInput.brancH_ID, disabled: this.editPageState === "view" ? true : false}, [Validators.required]],
      serial: [{value:this.filterInput.serial, disabled: this.editPageState === "view" ? true : false}, [Validators.required]],
  });
  }

  isEdit() {
    if (this.editPageState == "edit") {
       
        if(this.filterInput.autH_STATUS==null){
          this.isUPdate = true;

        }
        else{
          this.isUPdate = false;

        }
   
    }
    else if (this.editPageState == "view") {
      this.isUPdate = true;
    }
  }

  addNew() {
    this.navigate(['/app/admin/them-thiet-bi']);
  }

  search() {
    this.listSelectRow = null;
    this.filterInput.devicE_CODE = this.getRouteParam("id");

    this.DeviceServiceProxy.dEVICE_Search('1',this.filterInput).subscribe(response => {
      this.filterInput = response[0];
      this.isEdit();
      if (this.filterInput.datE_BUY != null) {
        this.datE_BUY = moment(this.filterInput.datE_BUY).format('MM/DD/YYYY');
      }
      if (this.filterInput.datE_WARRANTY_BEGIN != null) {
        this.datE_WARRANTY_BEGIN = moment(this.filterInput.datE_WARRANTY_BEGIN).format('MM/DD/YYYY');
      }
      if (this.filterInput.datE_WARRANTY_END != null) {
        this.datE_WARRANTY_END = moment(this.filterInput.datE_WARRANTY_END).format('MM/DD/YYYY');
      }



    })

  }


  listBranch;
  year = new Date().getFullYear();
  get_branch() {
    this.search();

    this.DeviceServiceProxy.dEVICE_Get_All_Branch().subscribe(responseListBranch => {
      this.listBranch = responseListBranch;
      console.log(responseListBranch)
    })
  }

  update() {
    // $(":disabled").prop('disabled', false);
    this.isUPdate = false;
  }
  getIdUSer(){
    this.DeviceServiceProxy.get_Current_ID().subscribe(response=>{
      this.currentID = response['result'];
      console.log(response) 
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


  save() {
    this.submitted = true;
    var dateBuynew = new Date(this.datE_BUY);
    var warantyBeginnew = new Date(this.datE_WARRANTY_BEGIN);
    var warrantyEndnew = new Date(this.datE_WARRANTY_END);
    


    // console.log(this.compareDateBeforeUpdate(dateBuy, warantyBegin, warrantyEnd))
    if (this.compareDateBeforeUpdate(dateBuynew, warantyBeginnew, warrantyEndnew) == 1) {

      

      this.filterInput.datE_BUY = moment(new Date(dateBuynew.getFullYear(), dateBuynew.getMonth(), dateBuynew.getDate() + 1));
      this.filterInput.datE_WARRANTY_BEGIN = moment(new Date(warantyBeginnew.getFullYear(), warantyBeginnew.getMonth(), warantyBeginnew.getDate() + 1));
      this.filterInput.datE_WARRANTY_END = moment(new Date(warrantyEndnew.getFullYear(), warrantyEndnew.getMonth(), warrantyEndnew.getDate() + 1));

      console.log(this.filterInput)

      this.DeviceServiceProxy.dEVICE_Update(this.filterInput).subscribe(response => {

        if (response[0]['RESULT'] == '0') {
          this.message.success('Chỉnh sửa thành công');
        }
        else if (response[0]['RESULT'] == '-1') {
          this.message.error(response[0]['ErrorDesc']);
        }
        this.isUPdate = true;
        this.formData.disable(this.isUPdate);
      })
    }

  }

  isEmty(value:string){
    if(value.length == 0 || value == undefined || value == ""){
      return 1;
    }
    return 0;
  }


  delete(){

  }
  get f() { return this.formData.controls; }

  approve(value){
    this.DeviceServiceProxy.dEVICE_Approve(value,this.filterInput.devicE_ID).subscribe(response=>{
      console.log(response)
      if (response['Result'] == '0' || response['Result'] == '1') {
        this.message.success(response['ErrorDesc'],'Thông báo');
      }
      else if (response['Result'] == '-1') {
        this.message.error(response['ErrorDesc'],'Thông báo');
      }
    })
  }
}


