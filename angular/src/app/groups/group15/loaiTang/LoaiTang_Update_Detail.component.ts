import { Component, Input, Injector, OnInit } from '@angular/core';
import { Location } from '@angular/common'
import { FloorTypeUpdate_DTO, FloorType_DTO, FloorTypeServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group15ModalService } from '../modal/modal.service'
import { AppComponentBase } from '@shared/common/app-component-base';
import { load } from '@angular/core/src/render3/instructions';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Table } from 'primeng/components/table/table';
@Component({
  selector: 'app-FloorType-update-detail',
  templateUrl: './LoaiTang_Update_Detail.component.html',
  styleUrls: ["../styles.css", "../modal_styles.less"]
})

export class LoaiTang_Update_DetailComponent extends AppComponentBase implements OnInit {

  editPageState: string;
  Id;

  isSpecialName: boolean;
  isSpecialDesc: boolean;
  maxLengthName: boolean;
  maxLengthDesc: boolean;
  isEmptyName: boolean;
  canUpdate: boolean;
  isLoading: boolean;
  inputModel: FloorType_DTO;

  editable = null;
  onCancelEdit = () => { };
  maxLengthCode = false;
  isSpecialCode = false;

  disable = null; mode = null;

  constructor(injector: Injector, private _location: Location, private loaiTangService: FloorTypeServiceProxy, private modalService: Group15ModalService) {
    super(injector);
    this.editPageState = this.getRouteData('editPageState');
    this.Id = this.getRouteParam('id');
    this.inputModel = new FloorType_DTO();
    this.isLoading = false;
    this.isSpecialName = false;
    this.isSpecialDesc = false;
    this.maxLengthDesc = false;
    this.maxLengthName = false;
    this.isEmptyName = false;
    this.canUpdate = false;
    this.isLoading = false;
  }

  //search first
  ngOnInit(): void {
    this.getFloorTypebyId();
  }

  async getFloorTypebyId(event?: LazyLoadEvent) {
    this.isLoading = true;
    this.loaiTangService.getAll().subscribe(response => {

      let isExist = false;
      console.log(response);
      for (var i = 0; i < response.length; i++) {
        if (response[i]['floorType_ID'] === this.Id) {
          this.inputModel = response[i];
          isExist = true;
          this.isLoading = false;
          console.log(this.inputModel)
        }
      }

      if (!isExist) {
        this.isLoading = false;
        this.turnBack();
      }
    });
  }

  //navigate area
  navigate_addNewFloorType() {
    this.navigate(['/app/admin/loaitang-add-detail']);
  }

  async delete_FloorType() {
    this.isLoading = true;
    this.loaiTangService
      .delete(this.inputModel.floorType_ID)
      .subscribe((res) => {
        this.isLoading = false;
        this.message.success("Xoá loại tầng thành công!");
        this.turnBack();
      });
  }

  async update_FloorType() {
    this.isLoading = true;
    this.loaiTangService
      .update(this.inputModel)
      .subscribe((response => {
        this.isLoading = false;
        console.log(response.error)
        if (response.code === "0") {
          this.isLoading = false;
          this.message.success(this.l('Group15_UpdateSuccessfully'));
        }

        else {
          this.isLoading = false;
          this.message.error(this.l('FloorTypeNameExistError'));
        }

      }));
  }

  turnBack() {
    this.navigate(['/app/admin/loaitang']);
  }

  //additional features
  checkEmptyFields() {
    if (String(this.inputModel.floorType_NAME) === ""
      || String(this.inputModel.floorType_NAME) == null) {
      this.isEmptyName = true;
      this.canUpdate = false;
    }
    else this.isEmptyName = false;

  }

  checkSpecialName() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.floorType_NAME) ||
      this.inputModel.floorType_NAME.indexOf("`") != -1 ||
      this.inputModel.floorType_NAME.indexOf("~") != -1) {
      this.isSpecialName = true;
      this.canUpdate = false;
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
      this.canUpdate = false;
    }
    else {
      this.isSpecialDesc = false;
    }
  }

  // checkSpecialCode() {
  //   var format = /[&'"\\<>\/`]+/;
  //   if (format.test(this.inputModel.floorType_CODE) ||
  //     this.inputModel.floorType_CODE.indexOf("`") != -1 ||
  //     this.inputModel.floorType_CODE.indexOf("~") != -1) {
  //     // this.isSpecialCode = true;
  //     this.canUpdate = false;
  //   }
  //   else {
  //     this.isSpecialCode = false;
  //   }
  // }

  checkValidInput() {
    this.canUpdate = true;
    if (this.inputModel.floorType_NAME.length > 100) {
      this.maxLengthName = true;
      this.canUpdate = false;
    }
    else {
      this.maxLengthName = false;
    }

    if (this.inputModel.floorType_DESC.length > 1000) {
      this.maxLengthDesc = true;
      this.canUpdate = false;
    }
    else {
      this.maxLengthDesc = false;
    }

    // if (this.inputModel.floorType_CODE.length > 20) {
    //   this.maxLengthCode = true;
    //   this.canUpdate = false;
    // }
    // else {
    //   this.maxLengthCode = false;
    // }

    this.checkSpecialName();
    this.checkSpecialDesc();
    // this.checkSpecialCode();
    this.checkEmptyFields();
  }


}
