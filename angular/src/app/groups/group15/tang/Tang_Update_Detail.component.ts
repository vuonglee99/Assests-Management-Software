import { Component, Input, Injector, OnInit } from '@angular/core';
import { Location } from '@angular/common'
import { FloorUpdate_DTO, FloorServiceProxy, Floor_DTO, FloorType_DTO, FloorTypeServiceProxy, BuildingsServiceProxy, BuildingPagingSearchDTO } from '@shared/service-proxies/service-proxies';
import { Group15ModalService } from '../modal/modal.service'
import { AppComponentBase } from '@shared/common/app-component-base';
import { load } from '@angular/core/src/render3/instructions';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Table } from 'primeng/components/table/table';
import { Status_DTO } from '../group15-service-proxy.module';
@Component({
  selector: 'app-floor-update-detail',
  templateUrl: './Tang_Update_Detail.component.html',
  styleUrls: ["../styles.css", "../modal_styles.less"]
})

export class Tang_Update_DetailComponent extends AppComponentBase implements OnInit {

  inputModel: FloorUpdate_DTO;
  Id;
  unparsedContent;
  parsedContent;
  editPageState: string;
  errorInfo: string;
  isSpecialName: boolean;
  isSpecialDesc: boolean;
  isSpecialCode: boolean;
  canUpdate: boolean;
  maxLengthName: boolean;
  maxLengthDesc: boolean;
  maxLengthCode=null;
  isSpecialNote=null;
  maxLengthNote=null;
  allFloorType: FloorType_DTO[] = [];
  selectedFloorType: FloorType_DTO = new FloorType_DTO();

  allStatus: Status_DTO[] = [];
  tempStatus2: Status_DTO = new Status_DTO();
  tempStatus3: Status_DTO = new Status_DTO();
  selectedStatus: Status_DTO = new Status_DTO();

  currentFloor: Floor_DTO = new Floor_DTO();

  allBuilding: BuildingPagingSearchDTO[] = [];
  selectedBuilding: BuildingPagingSearchDTO = new BuildingPagingSearchDTO();

  isLoading: boolean;

constructor(private injector: Injector, private _location: Location, private tangService: FloorServiceProxy, private loaiTangSerice: FloorTypeServiceProxy, private toanhaService : BuildingsServiceProxy) {
    super(injector);
    this.Id = this.getRouteParam('id');
    this.inputModel = new FloorUpdate_DTO;
    this.editPageState = this.getRouteData('editPageState');
    // this.inputModel.type = null;
    this.inputModel.floor_NAME = null;
    this.inputModel.floor_NOTE = null;
    this.inputModel.floor_ID = null;
    this.errorInfo = "";
    this.isSpecialName = false;
    this.isSpecialDesc = false;
    this.maxLengthDesc = false;
    this.maxLengthName = false;
    this.canUpdate = true;
    this.isLoading = false;
    this.tempStatus2.status_ID = "0";
    this.tempStatus2.status_NAME = this.l('Group15_NotApproved');
    this.allStatus.push(this.tempStatus2);
    this.tempStatus3.status_ID = "1";
    this.tempStatus3.status_NAME = this.l('Group15_Approved');
    this.allStatus.push(this.tempStatus3);
  }

  ngOnInit(): void {
    this.getFloorbyId();
    this.getAllFloorType();
    this.getAllBuilding();
  }

  async getFloorbyId(event?: LazyLoadEvent) {
    console.log(this.editPageState);
    this.isLoading = true;
    this.tangService.getById(this.Id).subscribe(
      response => {
        console.log(response);
        this.currentFloor = response;
        this.inputModel.building_ID = this.currentFloor.building_ID;
        this.inputModel.floorType_ID = this.currentFloor.floorType_ID;
        this.inputModel.floor_CODE = this.currentFloor.floor_CODE;
        this.inputModel.floor_ID = this.currentFloor.floor_ID;
        this.inputModel.floor_NAME = this.currentFloor.floor_NAME;
        this.inputModel.floor_NOTE = this.currentFloor.floor_NOTE;
        this.inputModel.floor_STATUS = this.currentFloor.floor_STATUS;
        if(response.autH_STATUS === "A") {
          this.selectedStatus = this.tempStatus3;
        }
        else {
          this.selectedStatus = this.tempStatus2;
        }
        this.isLoading = false;
      }
    );
  }

  getAllBuilding() {
    this.toanhaService.pagingSearch("","","","","",0,1000).subscribe(
      response => {
        this.allBuilding = response.items;
        this.allBuilding.forEach(element => {
          if(element.buildinG_ID === this.currentFloor.building_ID) {
            this.selectedBuilding = element;
            return;
          }
        });
      }
    )
  }
  getAllFloorType() {
    this.loaiTangSerice.getAll().subscribe(
      response => {
        this.allFloorType = response;
        this.allFloorType.forEach(element => {
          if(element.floorType_ID === this.currentFloor.floorType_ID) {
            this.selectedFloorType = element;
            return;
          }
        });
      }
    )
  }

  turnback () {
    this._location.back();
  }

  async updateFloor(event?: LazyLoadEvent) {
  
    this.inputModel.floorType_ID = this.selectedFloorType.floorType_ID;
    this.inputModel.floor_STATUS = this.selectedStatus.status_ID;
    if (this.checkEmptyFields()) {
      this.isLoading = false;
      this.message.warn(this.l('FieldMustBeNotEmpty'));
      return;
    } else {
      if (!this.canUpdate) {
        this.isLoading = false;
        this.errorInfo = this.l('NeedToCompleteTheForm');
        this.message.warn(this.errorInfo);
        return;
      }
      this.isLoading = true;
      this.tangService.update(this.inputModel).subscribe(
        response => {
          console.log("Updated");
          if (response.code === "0") {
            this.isLoading = false;
            this.message.success(this.l('Group15_UpdateSuccessfully'));
          }
          else {
            this.isLoading = false;
            if(response.code == "-2") {
              this.message.warn(this.l('ThisFloorIsWaitingForApproval'));
            }
            else {
              this.message.warn(response.error);
            }
          }
        }
      )
    }
  }

  //chekk empty fields
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
      this.canUpdate = false;``
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
      this.canUpdate = false;
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
      this.canUpdate = false;
    }
    else {
      this.isSpecialDesc = false;
    }
  }

  checkValidBuilding() {
    this.inputModel.building_ID = this.selectedBuilding.buildinG_ID;
    this.checkValidInput();
  }

  checkValidInput() {
    this.canUpdate = true;

    if (this.inputModel.floor_NAME.length > 100) {
      this.maxLengthName = true;
      this.canUpdate = false;
    }
    else {
      this.maxLengthName = false;
    }
    if (this.inputModel.floor_NOTE.length > 1000) {
      this.maxLengthDesc = true;
      this.canUpdate = false;
    }
    else {
      this.maxLengthDesc = false;
    }

    this.checkSpecialName();
    this.checkSpecialDesc();
    this.checkSpecialCode();

    if (
      this.inputModel.floor_NAME.length > 100 ||
      this.inputModel.floor_NOTE.length > 1000 ||
      this.inputModel.floor_CODE.length > 20 ||
      this.isSpecialName == true ||
      this.isSpecialDesc == true ||
      this.isSpecialCode == true)
      this.canUpdate = false;
  }

}
