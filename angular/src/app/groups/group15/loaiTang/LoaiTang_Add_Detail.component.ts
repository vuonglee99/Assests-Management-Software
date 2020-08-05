import { Component, Input, Injector, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormsModule, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FloorTypeCreate_DTO, FloorTypeServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group15ModalService } from '../modal/modal.service'
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

@Component({
  selector: 'app-loai-tang-add-detail',
  templateUrl: './LoaiTang_Add_Detail.component.html',
  styleUrls: ["../modal_styles.less", "../styles.css"]
})

export class LoaiTang_Add_DetailComponent extends AppComponentBase implements OnInit {

  inputModel: FloorTypeCreate_DTO = new FloorTypeCreate_DTO();

  editPageState: string;

  isSpecialName: boolean;
  isSpecialDesc: boolean;
  isSpecialCode: boolean;
  maxLengthCode: boolean;
  maxLengthName: boolean;
  maxLengthDesc: boolean;
  isEmptyCode: boolean;
  isEmptyName: boolean;

  canAdd: boolean;
  isLoading: boolean;

  editable = null;
  onCancelEdit = () => { };

  constructor(injector: Injector, private loaiTangService: FloorTypeServiceProxy, private _location: Location, private modalService: Group15ModalService) {
    super(injector);
    this.editPageState = this.getRouteData('editPageState');
    this.inputModel.floorType_DESC = "";
    this.inputModel.floorType_NAME = "";
    this.inputModel.floorType_CODE = "";

    this.maxLengthCode = false;
    this.isSpecialCode = false;
    this.isSpecialName = false;
    this.isSpecialDesc = false;
    this.maxLengthDesc = false;
    this.maxLengthName = false;
    this.maxLengthCode = false;
    this.isEmptyCode = false;
    this.isEmptyName = false;

    this.canAdd = false;
    this.isLoading = false;

  }

  ngOnInit(): void {
    // this.generateNewCODE();
  }

  async saveFloorType(event?: LazyLoadEvent) {
    console.log("Add FloorType_Detail function has been called");

    if (this.editPageState == 'add') {
      console.log(this.inputModel);
      this.isLoading = true;
      this.loaiTangService.create(this.inputModel).subscribe(response => {
        console.log(response.code);
        if (response.code === "0") {
          this.isLoading = false;
          this.message.success(this.l('Group15_AddSuccessfully'));
        }
        else {
          if (response.code === "-1") {
            this.isLoading = false;
            this.message.error(this.l('FloorTypeCodeExistError'));
          }
          if (response.code === "-2") {
            this.isLoading = false;
            this.message.error(this.l('FloorTypeNameExistError'));
          }
        }
      }
      )
    }

  }

  //additional features
  checkEmptyFields() {

    if (String(this.inputModel.floorType_CODE) === ""
      || String(this.inputModel.floorType_CODE) === null) {
      this.isEmptyCode = true;
      this.canAdd = false;
    }
    else
      this.isEmptyCode = false;

    if (String(this.inputModel.floorType_NAME) === ""
      || String(this.inputModel.floorType_NAME) == null) {
      this.isEmptyName = true;
      this.canAdd = false;
    }
    else this.isEmptyName = false;

  }

  checkSpecialName() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.floorType_NAME) ||
      this.inputModel.floorType_NAME.indexOf("`") != -1 ||
      this.inputModel.floorType_NAME.indexOf("~") != -1) {
      this.isSpecialName = true;
      this.canAdd = false;
    }
    else {
      this.isSpecialName = false;
    }
  }

  checkSpecialDesc() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.floorType_DESC) ||
      this.inputModel.floorType_DESC.indexOf("`") != -1 ||
      this.inputModel.floorType_DESC.indexOf("~") != -1) {
      this.isSpecialDesc = true;
      this.canAdd = false;
    }
    else {
      this.isSpecialDesc = false;
    }
  }

  checkSpecialCode() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.floorType_CODE) ||
      this.inputModel.floorType_CODE.indexOf("`") != -1 ||
      this.inputModel.floorType_CODE.indexOf("~") != -1) {
      this.isSpecialCode = true;
      this.canAdd = false;
    }
    else {
      this.isSpecialCode = false;
    }
  }

  checkValidInput() {
    this.canAdd = true;
    if (this.inputModel.floorType_NAME.length > 100) {
      this.maxLengthName = true;
      this.canAdd = false;
    }
    else {
      this.maxLengthName = false;
    }

    if (this.inputModel.floorType_DESC.length > 1000) {
      this.maxLengthDesc = true;
      this.canAdd = false;
    }
    else {
      this.maxLengthDesc = false;
    }

    if (this.inputModel.floorType_CODE.length > 20) {
      this.maxLengthCode = true;
      this.canAdd = false;
    }
    else {
      this.maxLengthCode = false;
    }

    this.checkSpecialName();
    this.checkSpecialDesc();
    this.checkSpecialCode();
    this.checkEmptyFields();
  }

  //navigate
  turnBack() {
    this.navigate(['/app/admin/loaitang']);
  }

}
