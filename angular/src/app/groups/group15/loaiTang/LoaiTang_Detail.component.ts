import { Component, Input, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Location } from '@angular/common'
import { Group15ModalService } from '../modal/modal.service'
import { FloorTypeServiceProxy, FloorType_DTO } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Status_DTO } from '../group15-service-proxy.module';
@Component({
  selector: 'app-FloorType-detail',
  templateUrl: './LoaiTang_Detail.component.html',
  styleUrls: ["../styles.css", "../modal_styles.less"]
})

export class LoaiTang_DetailComponent extends AppComponentBase implements OnInit {
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
  editable=null;
  onCancelEdit=()=>{};
  maxLengthCode=false;
  isSpecialCode=false;

  selectedStatus: Status_DTO = new Status_DTO();
  allStatus: Status_DTO[] = [];
  tempStatus2: Status_DTO = new Status_DTO();
  tempStatus3: Status_DTO = new Status_DTO();
  tempStatus4: Status_DTO = new Status_DTO();
  tempStatus5: Status_DTO = new Status_DTO();

  isDelete: boolean = false;
  isUpdate: boolean = false;
  approved: boolean = false;

  disable = null; mode = null; update_FloorType=null;


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
    this.loaiTangService.getById(this.Id).subscribe(response => {
      // let isExist = false;
      console.log(response);
      this.inputModel = response;
      if(response.autH_STATUS === "A") {
        this.approved = true;
        this.selectedStatus = this.tempStatus3;
      }
      else {
        this.selectedStatus = this.tempStatus2;
      }
      if(response.deletE_REQUESTED === "1")  {
        this.isDelete = true;
        this.isUpdate = false;
        this.approved = false;
        this.selectedStatus = this.tempStatus5;
        this.isLoading = false;
        return;
      }
      if(response.autH_STATUS === "PA") {
        this.approved = false;
        this.isDelete = false;
        this.isUpdate = false;
        this.selectedStatus = this.tempStatus2;
        this.isLoading = false;
        return;
      }
      if(response.autH_STATUS === "PU") {
        this.approved = false;
        this.isDelete = false;
        this.isUpdate = true;
        this.selectedStatus = this.tempStatus4;
        this.isLoading = false;
        return;
      }
      // isExist = true
    });
    this.loaiTangService.getApproveOfId(this.Id)
    .subscribe(response=> {
      console.log(response);
      if(response.floorType_ID) {
        this.inputModel = response;
        this.approved = false;
        this.selectedStatus = this.tempStatus4;
        this.isUpdate = true;
        console.log(response.deletE_REQUESTED);
      }
    })
    this.isLoading = false;
  }

  //navigate area
  navigate_addNewFloorType() {
    this.navigate(['/app/admin/loaitang-add-detail']);
  }

  async approveFloorType() {
    this.isLoading = true;
    console.log(this.Id);
    if(this.isDelete) {
      this.loaiTangService.approveDelete(this.inputModel.floorType_ID)
      .subscribe((res) => {
        if(res.error) {
          this.message.error(this.l('Error'));
        }
        else {
          this.message.success(this.l('Group15_ApproveSuccessfully'));
          this.approved = true;
          this.selectedStatus = this.tempStatus3;
        }
      });
      this.isLoading = false;
      this.navigate(['/app/admin/loaitang']);
      return; 
    }
    if(this.isUpdate) {
      this.loaiTangService.approveUpdate(this.inputModel.floorType_ID)
      .subscribe((res) => {
        if(res.error) {
          this.message.error(this.l('Error'));
        }
        else {
          this.message.success(this.l('Group15_ApproveSuccessfully'));
          this.approved = true;
          this.selectedStatus = this.tempStatus3;
        }
      }) 
    }
    else {
      this.loaiTangService.approveAdd(this.inputModel.floorType_ID)
      .subscribe((res) => {
        if(res.error) {
          this.message.error(this.l('Error'));
        }
        else {
          this.message.success(this.l('Group15_ApproveSuccessfully'));
          this.approved = true;
          this.selectedStatus = this.tempStatus3;
        }
      })
    }
    this.isLoading = false;
    this.getFloorTypebyId();
  }

  async denyFloorType() {
    this.isLoading = true;
    if(this.isDelete) {
      this.loaiTangService.cancelApproveDelete(this.inputModel.floorType_ID)
      .subscribe((res) => {
        if(res.error) {
          this.message.error("Error");
        }
        else {
          this.message.success(this.l('Group15_ApproveSuccessfully'));
          this.approved = true;
          this.selectedStatus = this.tempStatus3;
        }
      });
      this.isLoading = false;
      return;
    }
    if(this.isUpdate) {
      this.loaiTangService.cancelApproveUpdate(this.inputModel.floorType_ID)
      .subscribe((res) => {
        if(res.error) {
          this.message.error(this.l('Error'));
        }
        else {
          this.message.success(this.l('Group15_ApproveSuccessfully'));
          this.approved = true;
          this.selectedStatus = this.tempStatus3;
        }
      }) 
    }
    else {
      this.loaiTangService.cancelApproveAdd(this.inputModel.floorType_ID)
      .subscribe((res) => {
        if(res.error) {
          this.message.error(this.l('Error'));
        }
        else {
          this.message.success(this.l('Group15_ApproveSuccessfully'));
          this.approved = true;
          this.selectedStatus = this.tempStatus3;
        }
      })
    }
    this.isLoading = false;
  }

  async delete_FloorType() {
    console.log("A");
    this.isLoading = true;
    this.message.confirm(
      this.l('FloorTypeDeleteWarningMessage', this.inputModel.floorType_CODE),
      this.l('Group15_AreYouSure'),
      (isConfirmed) => {
        if (isConfirmed) {
          this.loaiTangService.delete(this.inputModel.floorType_ID)
            .subscribe(() => {
              this.isLoading = false;
              this.message.success(this.l('Group15_DeleteSuccessfully'));
              this.turnBack();
            });
        }

        this.isLoading = false;
      }
    );
    this.isLoading = false;
  }


  navigateToUpdate() {
    this.navigate(['/app/admin/loaitang-update-detail/' + this.inputModel.floorType_ID]);
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
