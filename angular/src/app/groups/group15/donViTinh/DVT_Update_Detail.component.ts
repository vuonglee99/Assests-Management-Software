import { Component, Input, Injector, OnInit } from '@angular/core';
import { Location } from '@angular/common'
import { DVTUpdate_DTO, DonViTinhServiceProxy } from '@shared/service-proxies/service-proxies';
import { Group15ModalService } from '../modal/modal.service'
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-dvt-update-detail',
  templateUrl: './DVT_Update_Detail.component.html',
  styleUrls: ["../styles.css", "../modal_styles.less"]
})

export class DVT_Update_DetailComponent extends AppComponentBase implements OnInit {

  editPageState: string;
  Id;

  isSpecialName: boolean;
  isSpecialDesc: boolean;
  maxLengthName: boolean;
  maxLengthDesc: boolean;
  isEmptyName: boolean;
  canUpdate: boolean;
  isLoading: boolean;
  inputModel: DVTUpdate_DTO;
  editable=null;
  maxLengthCode=false;
  isSpecialCode=false;
  onCancelEdit=()=>{}
  constructor(injector: Injector, private _location: Location, private donViTinhService: DonViTinhServiceProxy, private modalService: Group15ModalService) {
    super(injector);
    this.editPageState = this.getRouteData('editPageState');
    this.Id = this.getRouteParam('id');
    this.inputModel = new DVTUpdate_DTO();
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
    this.getDVTUpdatebyId();
  }

  async getDVTUpdatebyId(event?: any) {
    this.isLoading = true;
    this.donViTinhService.all().subscribe(response => {

      let isExist = false;
      console.log(response);
      for (var i = 0; i < response.length; i++) {
        if (response[i]['dvT_ID'] === this.Id) {
          this.inputModel = response[i];
          isExist = true;
          this.isLoading = false;
          console.log(this.inputModel)
        }
      }

      if (!isExist) {
        this.isLoading = false;
        // this.turnBack();
      }
    });
  }

  //navigate area
  navigate_addNewDVTUpdate() {
    this.navigate(['/app/admin/loaitang-add-detail']);
  }

  async update_Unit() {
    this.isLoading = true;
    this.donViTinhService
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
          this.message.error(this.l('UnitNameExistError'));
        }

      }));
  }

  turnBack() {
    this.navigate(['/app/admin/loaitang']);
  }

  //additional features
  checkEmptyFields() {
    if (String(this.inputModel.dvT_NAME) === ""
      || String(this.inputModel.dvT_NAME) == null) {
      this.isEmptyName = true;
      this.canUpdate = false;
    }
    else this.isEmptyName = false;

  }

  checkSpecialName() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.dvT_NAME) ||
      this.inputModel.dvT_NAME.indexOf("`") != -1 ||
      this.inputModel.dvT_NAME.indexOf("~") != -1) {
      this.isSpecialName = true;
      this.canUpdate = false;
    }
    else {
      this.isSpecialName = false;
    }
  }

  checkSpecialDesc() {
    var format = /[&'"\\<>\/`]+/;
    if (format.test(this.inputModel.dvT_DESC) ||
      this.inputModel.dvT_DESC.indexOf("`") != -1 ||
      this.inputModel.dvT_DESC.indexOf("~") != -1) {
      this.isSpecialDesc = true;
      this.canUpdate = false;
    }
    else {
      this.isSpecialDesc = false;
    }
  }

  // checkSpecialCode() {
  //   var format = /[&'"\\<>\/`]+/;
  //   if (format.test(this.inputModel.dvT_CODE) ||
  //     this.inputModel.dvT_CODE.indexOf("`") != -1 ||
  //     this.inputModel.dvT_CODE.indexOf("~") != -1) {
  //     // this.isSpecialCode = true;
  //     this.canUpdate = false;
  //   }
  //   else {
  //     this.isSpecialCode = false;
  //   }
  // }

  checkValidInput() {
    this.canUpdate = true;
    if (this.inputModel.dvT_NAME.length > 100) {
      this.maxLengthName = true;
      this.canUpdate = false;
    }
    else {
      this.maxLengthName = false;
    }

    if (this.inputModel.dvT_DESC.length > 1000) {
      this.maxLengthDesc = true;
      this.canUpdate = false;
    }
    else {
      this.maxLengthDesc = false;
    }

    // if (this.inputModel.dvT_CODE.length > 20) {
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
