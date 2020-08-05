import { Component, Input, Injector, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormsModule, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FloorCreate_DTO, FloorServiceProxy, FloorType_DTO, Floor_DTO, FloorTypeServiceProxy, BuildingsServiceProxy, BuildingPagingSearchDTO, FloorsServiceProxy, FloorApproveRequest, ApartmentDTO, ApartmentServiceProxy, FloorUpdate_DTO } from '@shared/service-proxies/service-proxies';
import { Group15ModalService } from '../modal/modal.service'
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Status } from './Tang_Add_Detail.component'
import { Status_DTO } from '../group15-service-proxy.module';

@Component({
  selector: 'app-floor-detail',
  templateUrl: './Tang_Detail.component.html',
  styleUrls: ["../styles.css", "../modal_styles.less"]
})

export class Tang_DetailComponent extends AppComponentBase implements OnInit {
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

  selectedStatus: Status_DTO = new Status_DTO();
  allStatus: Status_DTO[] = [];
  tempStatus2: Status_DTO = new Status_DTO();
  tempStatus3: Status_DTO = new Status_DTO();
  tempStatus4: Status_DTO = new Status_DTO();
  tempStatus5: Status_DTO = new Status_DTO();

  allBuilding: BuildingPagingSearchDTO[] = [];
  selectedBuilding: BuildingPagingSearchDTO = new BuildingPagingSearchDTO();

  allApartment: ApartmentDTO[] = [];

  countApprovedApartment: number = 0;
  countUApprovedApartment: number = 0;
  parentUrl: string;

  isLoading: boolean;
  //Use service and DTO of gr6
  detailModel: Floor_DTO;
  Id;
  inputModel: FloorUpdate_DTO = new FloorUpdate_DTO();

  approved: boolean = false;
  isUpdate: boolean = false;
  isDelete: boolean = false;
  isSpecialNote = null; maxLengthNote = null; checkValidBuilding = null; checkValidInput=null;
  constructor(injector: Injector, private _location: Location, private tangService: FloorServiceProxy, private modalService: Group15ModalService, private toanhaService: BuildingsServiceProxy, private apartmentService: ApartmentServiceProxy) {
    super(injector);
    this.editPageState = this.getRouteData('editPageState');
    this.Id = this.getRouteParam('id');
    this.detailModel = new Floor_DTO();

    this.detailModel.floor_NOTE = "";
    this.detailModel.floor_NAME = "";
    this.detailModel.floor_CODE = "";

    this.errorInfo = "";
    this.isSpecialName = false;
    this.isSpecialDesc = false;
    this.isSpecialCode = false;
    this.maxLengthDesc = false;
    this.maxLengthName = false;
    this.maxLengthCode = false;
    this.canAdd = false;
    this.isLoading = false;

    this.tempStatus2.status_ID = "0";
    this.tempStatus2.status_NAME = this.l('Group15_NotApproved');
    this.allStatus.push(this.tempStatus2);
    this.tempStatus3.status_ID = "1";
    this.tempStatus3.status_NAME = this.l('Group15_Approved');
    this.allStatus.push(this.tempStatus3);
    this.tempStatus4.status_ID = "2";
    this.tempStatus4.status_NAME = this.l('WaitingForApproveUpdate');
    this.allStatus.push(this.tempStatus4);
    this.tempStatus5.status_ID = "3";
    this.tempStatus5.status_NAME = this.l('WaitingForApproveDelete');
    this.allStatus.push(this.tempStatus5);
  }

  //search first
  ngOnInit(): void {
    //this.getAllApartment();
    this.getFloorbyId();
    this.getAllBuilding();
  }

  /*
  getAllApartment() {
    this.apartmentService.apartmentSearch("", "", "", "", "", "", "")
      .subscribe(
        response => {
          let temp = response;
          temp.forEach(element => {
            if (element.floor_ID === this.Id) {
              this.allApartment.push(element);
              if (element.autH_STATUS === "A") {
                this.countApprovedApartment++;
              }
              else {
                this.countUApprovedApartment++;
              }
            }
          });
          console.log(response);
        }
      )
  }
  */

  getAllBuilding() {
    this.toanhaService.pagingSearch("", "", "", "", "", 0, 1000).subscribe(
      response => {
        let buildingId = this.getRouteParam("buildingID");
        this.allBuilding = response.items;
        this.allBuilding.forEach(element => {
          if (element.buildinG_ID === buildingId) {
            this.selectedBuilding = element;
            return;
          }
        });
      }
    )
  }

  async getFloorbyId(event?: LazyLoadEvent) {
    this.isLoading = true;
    this.tangService.getById(this.Id).subscribe(response => {
      // let isExist = false;
      console.log(response);
      this.detailModel = response;
      let buildingId = this.getRouteParam("buildingID");
      this.parentUrl = "/app/admin/buildings/" + buildingId + "/tang";
      if (response.autH_STATUS === "A") {
        this.approved = true;
        this.selectedStatus = this.tempStatus3;
      }
      else {
        this.selectedStatus = this.tempStatus2;
      }
      if (response.deletE_REQUESTED === "1") {
        this.isDelete = true;
        this.isUpdate = false;
        this.approved = false;
        this.selectedStatus = this.tempStatus5;
        this.isLoading = false;
        return;
      }
      if (response.autH_STATUS === "PA") {
        this.approved = false;
        this.isDelete = false;
        this.isUpdate = false;
        this.selectedStatus = this.tempStatus2;
        this.isLoading = false;
        return;
      }
      if (response.autH_STATUS === "PU") {
        this.approved = false;
        this.isDelete = false;
        this.isUpdate = true;
        this.selectedStatus = this.tempStatus4;
        this.isLoading = false;
        return;
      }
      // isExist = true
    });
    this.tangService.getApproveOfId(this.Id)
      .subscribe(response => {
        console.log(response);
        if (response.floor_ID) {
          this.detailModel = response;
          this.approved = false;
          this.selectedStatus = this.tempStatus4;
          this.isUpdate = true;
          console.log(response.deletE_REQUESTED);
        }
      })
    this.isLoading = false;
  }

  //handler modal area
  openModal(id: string) {
    this.modalService.open(id);
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  navigateToParent() {
    this.navigate([this.parentUrl]);
  }

  //navigate area
  navigate_addNewFloor() {
    let buildingId = this.getRouteParam("buildingID");
    let url = '/app/admin/buildings/' + buildingId + '/tang-add-detail';
    if (!buildingId) {
      url = '/app/admin/buildings';
    }
    this.navigate([url]);
  }

  navigate_updateFloor() {
    let buildingId = this.getRouteParam("buildingID");
    let url = '/app/admin/buildings/' + buildingId + '/tang-update-detail/' + this.Id;
    if (!buildingId) {
      url = '/app/admin/buildings';
    }
    this.navigate([url]);
  }

  navigate_apartmentList() {
    this.navigate([location.pathname.replace("/tang-detail", "") + "/apartment"]);
  }

  navigate_apartmentAdd() {
    this.navigate([location.pathname.replace("/tang-detail", "") + "/apartment-add"]);
  }

  async delete_Floor(event?: LazyLoadEvent) {
    this.isLoading = true;
    this.tangService
      .delete(this.detailModel.floor_ID)
      .subscribe((res) => {
        this.isLoading = false;
        this.message.success(this.l('Group15_DeleteSuccessfully'));

        this.turnBack();
      });
  }

  async denyFloor() {
    this.isLoading = true;
    if (this.isDelete) {
      this.tangService.cancelApproveDelete(this.detailModel.floor_ID)
        .subscribe((res) => {
          if (res.error) {
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
    if (this.isUpdate) {
      this.tangService.cancelApproveUpdate(this.detailModel.floor_ID)
        .subscribe((res) => {
          if (res.error) {
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
      this.tangService.cancelApproveAdd(this.detailModel.floor_ID)
        .subscribe((res) => {
          if (res.error) {
            this.message.error(res.error);
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

  async approveFloor(event: LazyLoadEvent) {
    this.isLoading = true;
    console.log(this.Id);
    if (this.isDelete) {
      this.tangService.approveDelete(this.detailModel.floor_ID)
        .subscribe((res) => {
          if (res.error) {
            this.message.error(this.l('Error'));
          }
          else {
            this.message.success(this.l('Group15_ApproveSuccessfully'));
            this.approved = true;
            this.selectedStatus = this.tempStatus3;
          }
        });
      this.isLoading = false;
      this.navigateToParent();
      return;
    }
    if (this.isUpdate) {
      this.tangService.approveUpdate(this.detailModel.floor_ID)
        .subscribe((res) => {
          if (res.error) {
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
      this.tangService.approveAdd(this.detailModel.floor_ID)
        .subscribe((res) => {
          if (res.error) {
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
    this.getFloorbyId();
  }

  turnBack() {
    let buildingId = this.getRouteParam("buildingID");
    let url = '/app/admin/buildings/' + buildingId + '/tang';
  }

}
