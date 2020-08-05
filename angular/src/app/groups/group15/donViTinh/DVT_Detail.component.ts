import { Component, Input, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Location } from '@angular/common'
import { Group15ModalService } from '../modal/modal.service'
import { DonViTinhServiceProxy, DVTServiceProxy, DVT_DTO } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-dvt-detail',
  templateUrl: './DVT_Detail.component.html',
  styleUrls: ["../styles.css", "../modal_styles.less"]
})

export class DVT_DetailComponent extends AppComponentBase implements OnInit {
  editPageState: string;
  Id;
  isLoading: boolean;
  //Use service and DTO of gr6
  detailModel: DVT_DTO;

  constructor(injector: Injector, private _location: Location, private dvtService: DVTServiceProxy, private modalService: Group15ModalService) {
    super(injector);
    // this.editPageState = this.getRouteData('editPageState');
    this.Id = this.getRouteParam('id');
    this.detailModel = new DVT_DTO();
    this.isLoading = false;
  }

  //search first
  ngOnInit(): void {
    this.getDVTbyId();
  }

  getDVTbyId() {
    this.dvtService.cM_DVT_ById(this.Id).subscribe(response => {
      this.detailModel = response;
      if (this.detailModel.dvT_CODE === "") {
        this.detailModel.dvT_CODE = this.detailModel.dvT_ID;
      }
    });
  }

  //handler modal area
  openModal(id: string) {
    this.modalService.open(id);
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  //navigate area
  navigate_addNewDVT() {
    this.navigate(['/app/admin/donvitinh-add-detail']);
  }

  navigate_updateDVT() {
    this.navigate(['/app/admin/donvitinh-update-detail/' + this.detailModel.dvT_ID]);
  }

  delete_DVT() {
    this.dvtService
      .cM_DVT_Delete(this.detailModel.dvT_ID)
      .subscribe((res) => {
        console.log("A");
        this.message.success("Xoá đơn vị tính thành công!");
      });
  }

  turnBack() {
    this._location.back();
  }

  closeModalAndTurnBack(id: string) {
    this.closeModal(id);
    this.turnBack();
  }
}
