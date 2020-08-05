import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { EquipmentServiceProxy, DeviceServiceProxy, CM_EQUIP_DTO } from 'shared/service-proxies/service-proxies';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import * as moment from "moment";
@Component({
  selector: 'app-them-cong-viec',
  templateUrl: './them-cong-viec.component.html',
  styleUrls: ['./them-cong-viec.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class ThemCongViecComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector, private DeviceServiceProxy: DeviceServiceProxy, private EquipmentServiceProxy: EquipmentServiceProxy, private formBuilder: FormBuilder) {
    super(injector);
  }
  listNameDevice: string[] = [];
  formData: FormGroup;
  isUpdate=false;
  ngOnInit(): void {
    this.getListName();
    this.filterInput.prioritY_ORDER = '';
    this.filterInput.kinD_FIX = '';
    this.filterInput.recorD_STATUS = '';
    this.formData = this.formBuilder.group({
      wO_CODE: [this.filterInput.wO_CODE, [Validators.required]],
      devicE_NAME: [this.filterInput.devicE_NAME, [Validators.required]],
      recorD_STATUS: [this.filterInput.recorD_STATUS, [Validators.required]],
      fixer: [this.filterInput.fixer, [Validators.required]]

    });
  }
  filterInput = new CM_EQUIP_DTO;
  isChooseUser = false;
  submitted = false;;

  changeChooseUser() {
    this.isChooseUser = !this.isChooseUser;
  }

  getListName() {
    this.DeviceServiceProxy.dEVICE_Get_All_Name().subscribe(response => {
      this.listNameDevice = response;
      console.log(this.listNameDevice)
    })
  }
  datE_IN;
  datE_OUT
  // disabledInput() {
  //   $(":disabled").prop('disabled', false);
  // }
  onGetChoseUser(value) {
    this.filterInput.fixer = value;
    this.isChooseUser = false;
  }

  // save info work order
  save() {
    this.submitted = true;
   
    if (this.formData.invalid) {
      this.message.error('Bạn vui lòng nhập lại đầy đủ các thông tin cần thiết!','Thông báo');
      return;
    }
    this.filterInput.datE_IN = moment(this.datE_IN,  "YYYY-MM-DD").add('days', 1);
    this.filterInput.datE_OUT = moment(this.datE_OUT,  "YYYY-MM-DD").add('days', 1);

    this.EquipmentServiceProxy.cM_Equipment_Insert(this.filterInput).subscribe(response => {
      if (response[0]['RESULT'] == '0') {
        this.message.success('Thêm mới thành công');
        this.filterInput = new CM_EQUIP_DTO();
        this.submitted = false;
        this.isUpdate=true;
      }
      else if (response[0]['RESULT'] == '-1') {
        this.message.error(response[0]['ErrorDesc']);
        this.isUpdate=false;
      }
      
     
    })
  }

  get f() { return this.formData.controls; }
}
