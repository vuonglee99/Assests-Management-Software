import { ViewEncapsulation, Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import * as moment from "moment";
import { EquipmentServiceProxy, CM_EQUIP_DTO, PCPTBVT_INSERT_DTO, THIETBIVATTU_DTO, ThietBiVatTuServiceProxy, } from 'shared/service-proxies/service-proxies';
import { NhanVienServiceProxy, NHANVIEN_DTO, NHANVIEN_FILTER, PhieuCapPhatTBVTServiceProxy, PCPTBVT_DTO, PCPTBVT_SEARCH_DTO, NHANVIEN_DEP_NAME_DTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from "primeng/api";
import { Table } from "primeng/table";

@Component({
  selector: 'app-them-cong-viec',
  templateUrl: './PhieuCapPhatTBVT-component-add.html',
  styleUrls: ['./PhieuCapPhatTBVT-component-add.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class ThemPhieuCapPhatComponent extends AppComponentBase implements OnInit {
  @ViewChild('dataTable') dataTable: Table;
  filterInput: PCPTBVT_INSERT_DTO = new PCPTBVT_INSERT_DTO();
  authStatus: number;
  date3: Date;
  date7: Date;
  department: NHANVIEN_DEP_NAME_DTO[];
  skip: number | undefined;
  take: number | undefined;

  sorting = null;
  desc: boolean | undefined;

  // trangThaiList: number[] = [0, 1]

  editPageState: string;
  maxLengthName: boolean = false;
  maxLengthDesc: boolean = false;
  isSpecialName: boolean = false;
  isSpecialDesc: boolean = false;
  canAdd: boolean = false;

  selectedItem: THIETBIVATTU_DTO[] = [];
  constructor(injector: Injector, private PCPService: PhieuCapPhatTBVTServiceProxy, private nhanVienServiceProxy: NhanVienServiceProxy, private thietBiVatTuService: ThietBiVatTuServiceProxy) {
    super(injector);

  }
  ngAfterViewInit() {
    setTimeout(() => {
      this.nhanVienServiceProxy.getDepName().subscribe(result => {
        this.department = result;
      });
    });
  }
  ngOnInit(): void {
    this.date3 = moment().toDate();
    this.date7 = moment().toDate();
    this.authStatus = 0;

  }
  getThietBiVatTu(event?: LazyLoadEvent) {

    this.selectedItem = null;
    this.primengTableHelper.showLoadingIndicator();

    if (this.dataTable.sortField) {
      this.sorting = this.dataTable.sortField;
      if (this.dataTable.sortOrder === 1) {
        this.desc = false;
      } else if (this.dataTable.sortOrder === -1) {
        this.desc = true;
      }
    }

    // Filters
    let filters: THIETBIVATTU_DTO = new THIETBIVATTU_DTO();


    this.thietBiVatTuService.searchThietBiVatTu(
      filters,
      this.sorting,
      this.desc,
      undefined,
      undefined
    ).subscribe(result => {
      this.primengTableHelper.records = result.items;
      this.primengTableHelper.totalRecordsCount = result.totalCount;
      this.primengTableHelper.hideLoadingIndicator();
    });
  }
  insertPCP() {
    if (this.canAdd) {
      this.PCPService.insertPCPTBVT(this.filterInput).subscribe(response => {
        this.message.success(this.l('AllocationBillInsertSuccess'));
      });
      this.selectedItem.map(thietbi => {
        this.thietBiVatTuService.addTBVTToPCPTBVT(thietbi.tbvT_MA_TBVT, this.filterInput.pcptbvT_MA_PCP).subscribe(response => {
          this.message.success(this.l('AllocationBillInsertSuccess'));
        });
      });
    }
    else
    {
      this.message.error(this.l('AllocationBillInsertDenied'));
    }
  }
  cancel() {
    this.navigate(['/app/admin/capphattbvt']);
  }
  checkSpecialName() {
    var format = /[&'"\\<>\/`]+!#@%^&*{}~/;
    if (format.test(this.filterInput.pcptbvT_MA_PCP) ||
      this.filterInput.pcptbvT_MA_PCP.indexOf("`") != -1 ||
      this.filterInput.pcptbvT_MA_PCP.indexOf("~") != -1) {
      this.isSpecialName = true;
      this.canAdd = false;
    }
    else {
      this.isSpecialName = false;
    }

  }

  checkSpecialDesc() {
    var format = /[&'"\\<>\/`]+!#@%^&*{}~/;
    if (format.test(this.filterInput.pcptbvT_GHI_CHU) ||
      this.filterInput.pcptbvT_GHI_CHU.indexOf("`") != -1 ||
      this.filterInput.pcptbvT_GHI_CHU.indexOf("~") != -1) {
      this.isSpecialDesc = true;
      this.canAdd = false;
    }
    else {
      this.isSpecialDesc = false;
    }

  }

  checkValidInput() {
    this.canAdd = true;
    console.log(this.filterInput.pcptbvT_GHI_CHU);
    console.log(this.filterInput.pcptbvT_MA_PCP);
    if (this.filterInput.pcptbvT_MA_PCP.length > 128) {
      this.maxLengthName = true;
      this.canAdd = false;
    }
    else {
      this.maxLengthName = false;
    }
    if (this.filterInput.pcptbvT_GHI_CHU.length > 256) {
      this.maxLengthDesc = true;
      this.canAdd = false;
    }
    else {
      this.maxLengthDesc = false;
    }

    this.checkSpecialName();
    this.checkSpecialDesc();


    if (
      this.filterInput.pcptbvT_MA_PCP == null ||
      this.filterInput.pcptbvT_GHI_CHU == null ||
      this.filterInput.pcptbvT_MA_PCP == '' ||
      this.filterInput.pcptbvT_GHI_CHU == '' ||
      this.filterInput.pcptbvT_DEP_ID == null ||
      this.filterInput.pcptbvT_DEP_ID == '' ||
      this.filterInput.pcptbvT_MA_PCP.length > 128 ||
      this.filterInput.pcptbvT_GHI_CHU.length > 256 ||
      this.isSpecialName == true ||
      this.isSpecialDesc == true)
      this.canAdd = false;
  }

  // filterInput = new CM_EQUIP_DTO;
  // isChooseUser = false;

  // changeChooseUser(){
  //   this.isChooseUser = !this.isChooseUser;
  // }

  // disabledInput() {
  //   $(":disabled").prop('disabled', false);
  // }
  // onGetChoseUser(value){
  //   this.filterInput.fixer = value;
  //   this.isChooseUser=false;
  // }

  // save info work order
  // saveWorkOrder(){
  //   console.log(this.filterInput);
  // }
}
