import { Component, Input, OnInit, Injector, ViewChild } from '@angular/core';
import { Location } from '@angular/common'
import { FloorServiceProxy, Floor_DTO, FloorType_DTO, FloorTypeServiceProxy, ApartmentServiceProxy, ApartmentPagingSearchDTO, ApartmentDTO, BuildingsServiceProxy, BuildingPagingSearchDTO, Apartment_ServiceProxy, Apartment_DTO } from '@shared/service-proxies/service-proxies';
import { Group15ModalService } from '../modal/modal.service'
import { AppComponentBase } from '@shared/common/app-component-base';
import { load, element } from '@angular/core/src/render3/instructions';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Status_DTO } from '../group15-service-proxy.module';
import { FileDownloadService } from '@shared/utils/file-download.service';
// import { Paginator } from 'primeng/primeng';

@Component({
  selector: 'app-floor-list-detail',
  templateUrl: './Tang_List.component.html',
  styleUrls: ["../styles.css"]
})

export class Tang_ListComponent extends AppComponentBase implements OnInit {

  @ViewChild('dataTable') dataTable: Table;
  @ViewChild('paginator') paginator: Paginator;

  isLoading: boolean;
  isSearching: boolean;
  isChanggingPage: boolean;
  currentInteractList:Floor_DTO[] = [];
  allItem:Floor_DTO[] = [];
  tempItems: Floor_DTO[] = [];
  selectedItem: Floor_DTO =  new Floor_DTO();


  temporaryModal = {
    floorCode: "",
    floorName: ""
  }

  allFloorType: FloorType_DTO[] = [];
  selectedFloorType: FloorType_DTO = new FloorType_DTO();
  selectAllFloorType: FloorType_DTO = new FloorType_DTO();

  allStatus: Status_DTO[] = [];
  tempStatus: Status_DTO = new Status_DTO();
  tempStatus2: Status_DTO = new Status_DTO();
  tempStatus3: Status_DTO = new Status_DTO();
  tempStatus4: Status_DTO = new Status_DTO();
  tempStatus5: Status_DTO = new Status_DTO();
  selectedStatus: Status_DTO = new Status_DTO();

  allBuilding: BuildingPagingSearchDTO[] = [];
  countUApprovedBuilding: number = 0;
  countApprovedBuilding: number = 0;
  selectedBuilding: BuildingPagingSearchDTO = new BuildingPagingSearchDTO();

  allFloors: Floor_DTO[] = [];
  selectedFloor: Floor_DTO = new Floor_DTO();
  countApprovedFloor: number = 0;
  countUApprovedFloor: number = 0;

  allApartment: Apartment_DTO[] = [];

  countApprovedApartment: number = 0;
  countUApprovedApartment: number = 0;

  maxPage: number;
  pageSize: number;
  pageNumber:number;
  first: number = 1;
  buildingID: string = "";
  fileDownloadService: FileDownloadService = new FileDownloadService();

  constructor(injector: Injector, private tangService: FloorServiceProxy, private loaiTangService: FloorTypeServiceProxy, private apartmentService: ApartmentServiceProxy, private toanhaService: BuildingsServiceProxy,private aps: Apartment_ServiceProxy) {
    super(injector);
    this.selectedItem  =  new Floor_DTO();
  }

  ngOnInit() {
    this.isSearching = false;
    this.pageNumber = 0;
    this.pageSize = 10;
    this.isLoading = false;
    this.getAllFloorType();
    this.getAllBuilding();
    this.getAllFloor();
    this.countFloor();
    this.tempStatus.status_ID = "-1";
    this.tempStatus.status_NAME = this.l('Group15_All');
    this.allStatus.push(this.tempStatus);
    this.tempStatus2.status_ID = "PA";
    this.tempStatus2.status_NAME = this.l('Group15_NotApproved');
    this.allStatus.push(this.tempStatus2);
    this.tempStatus3.status_ID = "A";
    this.tempStatus3.status_NAME = this.l('Group15_Approved');
    this.allStatus.push(this.tempStatus3);
    this.tempStatus4.status_ID = "PU";
    this.tempStatus4.status_NAME = this.l('WaitingForApproveUpdate');
    this.allStatus.push(this.tempStatus4);
    this.tempStatus5.status_ID = "PD";
    this.tempStatus5.status_NAME = this.l('WaitingForApproveDelete');
    this.allStatus.push(this.tempStatus5);
    this.selectedStatus = this.tempStatus;
  }


  countFloor() {
    this.countApprovedFloor = 0;
    this.countUApprovedFloor = 0;
    console.log("count");
    this.tangService.getByBuildingId(this.selectedBuilding.buildinG_ID)
    .subscribe(response => {
      this.allFloors = response;
      response.forEach(element => {
          if(element.autH_STATUS==="A") {
            this.countApprovedFloor++;
          }
          else{
            this.countUApprovedFloor++;
          }
      });
    });
    this.selectedFloor = this.allFloors[0];
    this.countApartment();
  }
  
  countApartment() {
    console.log(this.allFloors);
    this.allApartment = [];
    this.countApprovedApartment = 0;
    this.countUApprovedApartment = 0;
    this.aps.getAll()
    .subscribe(response => {
      let temp = response;
      temp.forEach(element => {
        if(this.selectedBuilding.buildinG_ID && this.selectedFloor.floor_ID && element.buildinG_ID === this.selectedBuilding.buildinG_ID
          && element.floor_ID === this.selectedFloor.floor_ID) {
            this.allApartment.push(element);
            if(element.autH_STATUS === "A") {
              this.countApprovedApartment++;
            }
            else {
              this.countUApprovedApartment++;
            }
          }
      });
    })
  }

  async getAllBuilding() {
    this.isLoading =true;
    this.toanhaService.pagingSearch("","","","","",0,1000).subscribe(
      response => {
        let buildingId = this.getRouteParam("buildingID");
        this.allBuilding = response.items;
        // this.selectedBuilding = this.allBuilding[0];
        this.allBuilding.forEach(element => {
          if(element.autH_STATUS === "A") {
            this.countApprovedBuilding++;
          }
          else {
            this.countUApprovedBuilding++;
          }
        });
      }
    );
    this.isLoading = false;
  }


  async getAllFloorType() {
    this.isLoading =true;
    this.loaiTangService.getAll().subscribe(
      response => {
        this.allFloorType = response;
        this.selectAllFloorType.floorType_NAME =this.l('Group15_All');
        this.selectedFloorType.floorType_ID = "";
        this.allFloorType.unshift(this.selectAllFloorType);
        this.selectedFloorType = this.allFloorType[0];
        console.log(this.allFloorType);
      }
    );
    this.isLoading = false;
  }

  async getAllFloor() {
    if(!this.isSearching && !this.isChanggingPage) {
        this.isLoading = true;
        this.buildingID = this.getRouteParam("buildingID");
        this.tangService.getAll().subscribe(
          response => {
            this.allItem = [];
            this.tempItems = response;
            this.tempItems.forEach(element => {
              if(element.building_ID === this.buildingID) {
                this.allItem.push(element);
              }
              else {
                console.log(element.building_ID);
              }
            });
            if((this.pageNumber*this.pageSize) > this.allItem.length) {
                this.pageNumber = this.pageNumber - 1;
            }
            var startRowId = Math.max(0, this.pageNumber * this.pageSize);
            var endRowId = Math.min(this.allItem.length, startRowId + this.pageSize);
            var temp = this.allItem;
            this.currentInteractList = temp.slice(startRowId, endRowId);
            this.maxPage = this.allItem.length / this.pageSize + 1;
            this.isLoading = false;
          });
      return;
    }
    if((this.pageNumber*this.pageSize) > this.allItem.length) {
      this.pageNumber = this.pageNumber - 1;
    }
    var startRowId = Math.max(0, this.pageNumber * this.pageSize);
    var endRowId = Math.min(this.allItem.length, startRowId + this.pageSize);
    var temp = this.allItem;
    this.currentInteractList = temp.slice(startRowId, endRowId);
    this.maxPage = this.allItem.length / this.pageSize + 1;
    this.isLoading = false;
  }
  filterTang(element: Floor_DTO, index, array) {
    let maTang = (<HTMLInputElement>document.getElementById("matang")).value;
    let tenTang = (<HTMLInputElement>document.getElementById("tentang")).value;
    return element.floor_CODE.toUpperCase().includes(maTang.toUpperCase())
        && element.floor_NAME.toUpperCase().includes(tenTang.toUpperCase());
  }

  searchFloor() {
    this.currentInteractList = [];
    let filterByNameAndCode = this.allItem.filter(this.filterTang);
    this.isSearching = (this.currentInteractList.length === this.allItem.length) ? false : true;
    // if(this.isSearching) {
    //   this.pageNumber = 0;
    //   this.pageSize = 10;
    // }
    this.currentInteractList = filterByNameAndCode;
    if(!(this.selectedFloorType.floorType_NAME === this.l('Group15_All'))) {
      console.log("not equal");
      let temp = this.currentInteractList;
      this.currentInteractList = [];
      temp.forEach(element => {
        if(this.selectedFloorType.floorType_ID === element.floorType_ID) {
          this.currentInteractList.push(element);
        }
      });
    }

    if(!(this.selectedStatus.status_NAME === this.l('Group15_All'))) {
      let temp = this.currentInteractList;
      this.currentInteractList = [];
      temp.forEach(element => {
        if((this.selectedStatus.status_ID === element.autH_STATUS) && this.selectedStatus.status_ID === "A") {
          this.currentInteractList.push(element);
        }
        if(this.selectedStatus.status_ID!= "A" && element.autH_STATUS != "A") {
          this.currentInteractList.push(element);
        }
      });
    }
    this.pageSize = 10;
  }

  loadWithPage(event) {
    this.pageNumber = event.page;
    this.pageSize = event.rows;
    this.isChanggingPage = true;
    this.getAllFloor();
    this.isChanggingPage = false;
  }

  // navigate area
  navigate_updateFloor() {
    this.navigate(["/app/admin/buildings/"+ this.getRouteParam("buildingID") + "/tang-update-detail/"  + this.selectedItem.floor_ID]);
  }

  navigate_addNewFloor() {
    this.navigate(["/app/admin/buildings/"+ this.getRouteParam("buildingID") +"/tang-add-detail"]);
  }

  navigate_detailFloor() {
    this.navigate(["/app/admin/buildings/"+ this.getRouteParam("buildingID") + "/tang-detail/" + this.selectedItem.floor_ID]);
  }

  async delete_Floor() {
    this.isLoading = true;

    this.tangService
      .delete(this.selectedItem.floor_ID)
      .subscribe((res) => {
        this.message.success(this.l('Group15_DeleteSuccessfully'));
        this.isLoading = false;
        this.getAllFloor();
      });
  }

  async exportFloor() {
    this.isLoading = true;
    let buildingID = this.getRouteParam("buildingID");
    this.tangService.export(buildingID)
    .subscribe(res=> {
      this.fileDownloadService.downloadTempFile(res);
      this.message.success(this.l('Group15_ExportSuccessfully'));
    }) 
    this.isLoading = false;
  }
}
