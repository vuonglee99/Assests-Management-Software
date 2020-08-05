import { Component, Input, OnInit, Injector, ViewChild } from '@angular/core';
import { Location } from '@angular/common'
import { FloorTypeServiceProxy, FloorType_DTO } from '@shared/service-proxies/service-proxies';
import { Group15ModalService } from '../modal/modal.service'
import { AppComponentBase } from '@shared/common/app-component-base';
import { load } from '@angular/core/src/render3/instructions';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Status_DTO } from '../group15-service-proxy.module';
// import { Paginator } from 'primeng/primeng';

@Component({
  selector: 'app-loaitang-list-detail',
  templateUrl: './LoaiTang_List.component.html',
  styleUrls: ["../styles.css"]
})

export class LoaiTang_ListComponent extends AppComponentBase implements OnInit {

  @ViewChild('dataTable') dataTable: Table;
  @ViewChild('paginator') paginator: Paginator;

  isLoading: boolean;
  isSearching: boolean;
  isChanggingPage: boolean;
  currentInteractList: FloorType_DTO[] = [];
  allItem: FloorType_DTO[] = [];
  selectedItem: FloorType_DTO = new FloorType_DTO();
  isAnyItemSelected: boolean;

  filter_DTO = {
    floorType_CODE: "",
    floorType_NAME: ""
  }

  //for approving:
  allStatus: Status_DTO[] = [];
  NormalStatus: Status_DTO = new Status_DTO();
  WaitForAdding: Status_DTO = new Status_DTO();
  WaitForUpdating: Status_DTO = new Status_DTO();
  WaitForDeleting: Status_DTO = new Status_DTO();
  selectedStatus: Status_DTO = new Status_DTO();

  maxPage: number;
  pageSize: number;
  pageNumber: number;
  first: number = 1;

  constructor(injector: Injector, private loaiTangService: FloorTypeServiceProxy) {
    super(injector);
    this.isAnyItemSelected = false;
    this.selectedItem = new FloorType_DTO();
  }

  ngOnInit() {
    //init filter for approving:
    this.NormalStatus.status_ID = "-1";
    this.NormalStatus.status_NAME = this.l('Group15_NormalStatus');
    this.allStatus.push(this.NormalStatus);
    this.WaitForAdding.status_ID = "0";
    this.WaitForAdding.status_NAME = this.l('Group15_WaitingForAdding');
    this.allStatus.push(this.WaitForAdding);
    this.WaitForUpdating.status_ID = "1";
    this.WaitForUpdating.status_NAME = this.l('Group15_WaitingForUpdating');
    this.WaitForDeleting.status_ID = "2";
    this.WaitForDeleting.status_NAME = this.l('Group15_WaitingForDeleting');
    this.selectedStatus = this.NormalStatus;
    
    this.getAllFloorType();
  }

  getAllFloorType() {
    // if (!this.isSearching && !this.isChanggingPage) {
    this.isLoading = true;

    this.loaiTangService.getAll().subscribe(
      response => {
        console.log(response);
        this.allItem = response;
        this.primengTableHelper.totalRecordsCount = response.length;
        this.primengTableHelper.records = response;
        this.primengTableHelper.hideLoadingIndicator();
        this.isLoading = false;
        this.selectedItem = new FloorType_DTO();
      });
    this.isLoading = false;
  }

  filterLoaiTang(element: FloorType_DTO, index, array) {
    let maLoaiTang = (<HTMLInputElement>document.getElementById("maloaitang")).value;
    let tenLoaiTang = (<HTMLInputElement>document.getElementById("tenloaitang")).value;
    return element.floorType_CODE.toUpperCase().includes(maLoaiTang.toUpperCase())
      && element.floorType_NAME.toUpperCase().includes(tenLoaiTang.toUpperCase());
  }

  searchFloorType() {

    this.currentInteractList = this.allItem.filter(
      this.filterLoaiTang
    );
    this.isSearching = (this.currentInteractList.length === this.allItem.length) ? false : true;
    this.primengTableHelper.totalRecordsCount = this.currentInteractList.length;
    this.primengTableHelper.records = this.currentInteractList;
    this.primengTableHelper.hideLoadingIndicator();
    this.isLoading = false;

    if (this.isSearching) {
      this.pageNumber = 0;
      this.pageSize = 10;
    }
    console.log("current filter: " + this.currentInteractList);
  }

  loadWithPage(event) {
    this.pageNumber = event.page;
    this.pageSize = event.rows;
    this.isChanggingPage = true;
    // console.log("page number: " + this.pageNumber + "page size: " + this.pageSize);
    this.getAllFloorType();
    this.isChanggingPage = false;
  }

  // navigate area
  navigate_updateFloorType() {
    this.navigate(['/app/admin/loaitang-update-detail/' + this.selectedItem.floorType_ID]);
  }

  navigate_addNewFloorType() {
    this.navigate(['/app/admin/loaitang-add-detail']);
  }

  navigate_detailFloorType() {
    this.navigate(['/app/admin/loaitang-detail/' + this.selectedItem.floorType_ID]);
  }

  async delete_FloorType() {
    console.log("A");
    this.isLoading = true;
    this.message.confirm(
      this.l('FloorTypeDeleteWarningMessage', this.selectedItem.floorType_CODE),
      this.l('Group15_AreYouSure'),
      (isConfirmed) => {
        if (isConfirmed) {
          this.loaiTangService.delete(this.selectedItem.floorType_ID)
            .subscribe(() => {
              this.isLoading = false;
              this.notify.success(this.l('Group15_DeleteSuccessfully'));
              this.getAllFloorType();
            });
        }

        this.isLoading = false;
      }
    );
    this.isLoading = false;
  }


}