import { Component, Input, Injector, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormsModule, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FloorCreate_DTO, FloorServiceProxy, FloorType_DTO, FloorTypeServiceProxy, BuildingsServiceProxy, BuildingPagingSearchDTO } from '@shared/service-proxies/service-proxies';
import { Group15ModalService } from '../modal/modal.service'
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

@Component({
  selector: 'app-floor-add-detail',
  templateUrl: './Tang_Add_Detail.component.html',
  styleUrls: ["../modal_styles.less", "../styles.css"]
})

export class Tang_Add_DetailComponent extends AppComponentBase implements OnInit {

  inputModel: FloorCreate_DTO = new FloorCreate_DTO();
  editPageState: string;
  errorInfo: string;
  isSpecialName: boolean;
  isSpecialDesc: boolean;
  canAdd: boolean;
  isSpecialCode: boolean;
  maxLengthName: boolean;
  maxLengthCode: boolean;
  maxLengthDesc: boolean;
  allFloorType: FloorType_DTO[] = [];
  selectedFloorType: FloorType_DTO = new FloorType_DTO();

  inputDTO:FloorCreate_DTO = new FloorCreate_DTO();
  allStatus: Status[] = [];
  selectedStatus: Status = new Status();

  allBuilding: BuildingPagingSearchDTO[] = [];
  selectedBuilding: BuildingPagingSearchDTO = new BuildingPagingSearchDTO();

  listFloorURL: String = "";

  isLoading: boolean;
  isSpecialNote=null;
  maxLengthNote=null;

  constructor(injector: Injector, private tangService: FloorServiceProxy, private loaiTangService : FloorTypeServiceProxy, private toanhaService : BuildingsServiceProxy) {
    super(injector);
    this.editPageState = this.getRouteData('editPageState');
    this.inputModel.floor_NOTE = "";
    this.inputModel.floor_NAME = "";
    this.inputModel.floor_CODE = "";

    this.errorInfo = "";
    this.isSpecialName = false;
    this.isSpecialDesc = false;
    this.isSpecialCode = false;
    this.maxLengthDesc = false;
    this.maxLengthName = false;
    this.maxLengthCode = false;
    this.canAdd = false;
    this.isLoading = false;

  }

  ngOnInit(): void {
    this.getAllFloorType();
    this.getAllBuilding();
    this.listFloorURL = location.pathname.replace("tang-add-detail", "tang");
  }

  getAllFloorType() {
    this.loaiTangService.getAll().subscribe(
      response => {
         this.allFloorType = response;
        // let temp = response;
        // temp.forEach(element => {
        //   if(element.autH_STATUS === "A") {
        //     this.allFloorType.push(element);
        //   }
        // });
        console.log(this.allFloorType);
      }
    )
  }

  getAllBuilding() {
    this.toanhaService.pagingSearch("","","","","",0,1000).subscribe(
      response => {
        let buildingId = this.getRouteParam("buildingID");
        this.allBuilding = response.items;
        this.allBuilding.forEach(element => {
          if(element.buildinG_ID === buildingId) {
            this.selectedBuilding = element;
            return;
          }
        });
      }
    )
  }

  getAllStatus() {
    let s = new Status();
    s.id = "0";
    s.name =this.l('Group15_NotApproved');
    this.selectedStatus = s;
    this.allStatus.push(s);
    let s2 = new Status();
    s2.id = "1";
    s2.name =this.l('Group15_Approved');
    this.allStatus.push(s2);
  }

  async saveFloor(event?: LazyLoadEvent) {
    console.log("Add Floor_Detail function has been called");
  
    this.inputModel.building_ID = this.selectedBuilding.buildinG_ID;
    this.inputModel.floorType_ID = this.selectedFloorType.floorType_ID;
    this.inputModel.floor_STATUS = "0";
    this.isLoading = true;
    if (this.checkEmptyFields()) {
      this.isLoading = false; 
      this.message.warn(this.l('FieldMustBeNotEmpty'));
    }
    else {
      if (!this.canAdd) {

        this.message.warn(this.l('NeedToCompleteTheForm'));
        this.isLoading = false;
        return;
      }
      this.tangService.create(this.inputModel).subscribe(response => {
        this.isLoading = false;
        if (response.code === "0") {
          this.message.success(this.l('Group15_AddSuccessfully'));
          // this.turnBack();
        }
        else {
          this.errorInfo = response.error;
          this.message.error(this.errorInfo);
        }
      }
      )
    }
  }

  //additional features
  checkEmptyFields() {
    if (String(this.inputModel.floor_CODE) === "" || String(this.inputModel.floor_STATUS) === "" || String(this.inputModel.building_ID) === "" || 
    String(this.inputModel.floor_NAME) === "" || String(this.inputModel.floorType_ID) === "") {
      return true;
    }
    return false;
  }

  checkSpecialCode() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.floor_CODE) ||
      this.inputModel.floor_CODE.indexOf("`") != -1 ||
      this.inputModel.floor_CODE.indexOf("~") != -1) {
      this.isSpecialCode = true;
      this.canAdd = false;
    }
    else {
      this.isSpecialCode = false;
    }
  }

  checkSpecialName() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.floor_NAME) ||
      this.inputModel.floor_NAME.indexOf("`") != -1 ||
      this.inputModel.floor_NAME.indexOf("~") != -1) {
      this.isSpecialName = true;
      this.canAdd = false;
    }
    else {
      this.isSpecialName = false;
    }
  }

  checkSpecialDesc() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.floor_NOTE) ||
      this.inputModel.floor_NOTE.indexOf("`") != -1 ||
      this.inputModel.floor_NOTE.indexOf("~") != -1) {
      this.isSpecialDesc = true;
      this.canAdd = false;
    }
    else {
      this.isSpecialDesc = false;
    }
  }

  checkValidBuilding() {
    this.inputModel.building_ID = this.selectedBuilding.buildinG_CODE;
    this.checkValidInput();
  }

  checkValidInput() {
    this.canAdd = true;

    if (this.inputModel.floor_NAME.length > 100) {
      this.maxLengthName = true;
      this.canAdd = false;
    }
    else {
      this.maxLengthName = false;
    }
    if (this.inputModel.floor_NOTE.length > 1000) {
      this.maxLengthDesc = true;
      this.canAdd = false;
    }
    else {
      this.maxLengthDesc = false;
    }
    if (this.inputModel.floor_CODE.length > 20) {
      this.maxLengthCode = true;
      this.canAdd = false;
    }
    else {
      this.maxLengthCode = false;
    }

    this.checkSpecialName();
    this.checkSpecialDesc();
    this.checkSpecialCode();

    if (
      this.inputModel.floor_NAME.length > 100 ||
      this.inputModel.floor_NOTE.length > 1000 ||
      this.inputModel.floor_CODE.length>20 ||
      this.isSpecialName == true ||
      this.isSpecialDesc == true ||
      this.isSpecialCode == true)
      this.canAdd = false;
  }

  //navigate
  turnBack() {
    // this._location.back();
    this.navigate(['/app/admin/tang']);
  }

}


export class Status {
  id: String;
  name: String;
}