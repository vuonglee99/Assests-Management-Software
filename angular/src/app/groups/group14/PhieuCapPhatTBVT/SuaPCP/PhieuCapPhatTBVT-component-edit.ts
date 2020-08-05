import { ViewEncapsulation, Component, Injector, OnInit, ViewChild, OnChanges } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import * as moment from "moment";
import { EquipmentServiceProxy, CM_EQUIP_DTO, PCPTBVT_INSERT_DTO, THIETBIVATTU_DTO, ThietBiVatTuServiceProxy, PCPTBVT_UPDATE_DTO, } from 'shared/service-proxies/service-proxies';
import { NhanVienServiceProxy, NHANVIEN_DTO, NHANVIEN_FILTER, PhieuCapPhatTBVTServiceProxy, PCPTBVT_DTO, PCPTBVT_SEARCH_DTO, NHANVIEN_DEP_NAME_DTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from "primeng/api";
import { Table } from "primeng/table";
import { ActivatedRoute } from "@angular/router";
@Component({
  selector: 'app-them-cong-viec',
  templateUrl: './PhieuCapPhatTBVT-component-edit.html',
  styleUrls: ['../PhieuCapPhatTBVT-component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class SuaPhieuCapPhatComponent extends AppComponentBase implements OnInit {
  @ViewChild('dataTable') dataTable: Table;
  filterInput: PCPTBVT_UPDATE_DTO = new PCPTBVT_UPDATE_DTO();
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
  currentPCP: PCPTBVT_DTO = new PCPTBVT_DTO();


  selectedItem: THIETBIVATTU_DTO[] = [];
  defaultItem: THIETBIVATTU_DTO[] = [];
  constructor(injector: Injector, private route: ActivatedRoute, private PCPService: PhieuCapPhatTBVTServiceProxy, private nhanVienServiceProxy: NhanVienServiceProxy, private thietBiVatTuService: ThietBiVatTuServiceProxy) {
    super(injector);

  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.nhanVienServiceProxy.getDepName().subscribe(result => {
        this.department = result;
      });
    });


    ;
  }
  ngOnInit(): void {


    this.checkValidInput();
    this.date7 = moment().toDate();
    this.authStatus = 0;

  }
  getThietBiVatTu(event?: LazyLoadEvent) {


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
      result.items.map(tbvt => {
        if (tbvt.tbvT_PCPTBVT_ID === this.route.snapshot.paramMap.get("id")) {
          this.selectedItem.push(tbvt);
          this.defaultItem.push(tbvt);
        }
      });
      this.primengTableHelper.records = result.items;
      this.primengTableHelper.totalRecordsCount = result.totalCount;

    });
    // let filterss: THIETBIVATTU_DTO = new THIETBIVATTU_DTO();
    // filterss.tbvT_PCPTBVT_ID = this.route.snapshot.paramMap.get("id");
    // this.thietBiVatTuService.searchThietBiVatTu(
    //     filterss,
    //     undefined,
    //     false,undefined,undefined
    // ).subscribe(result => {
    //     this.selectedItem = result.items;

    // });
    let filter: PCPTBVT_DTO = new PCPTBVT_DTO();
    filter.pcptbvT_MA_PCP = this.route.snapshot.paramMap.get("id");
    this.PCPService.filterPCPTBVT(
      filter,
      null,
      false, undefined, undefined
    ).subscribe(result => {
      this.currentPCP = result.items[0];
      this.filterInput.pcptbvT_MA_PCP=result.items[0].pcptbvT_MA_PCP;
      this.filterInput.pcptbvT_GHI_CHU=result.items[0].pcptbvT_GHI_CHU;
      this.filterInput.pcptbvT_DEP_ID=result.items[0].pcptbvT_DEP_ID;
      this.date3=result.items[0].creatE_DT.toDate();
      this.primengTableHelper.hideLoadingIndicator();
    });
    console.log(this.selectedItem);
    console.log(this.currentPCP.pcptbvT_MA_PCP)

  }
  updatePCP() {
    // for (let index = 0; index < this.defaultItem.length; index++) {
    //   let element = this.defaultItem[index];
    //   for (let k = 0; k < this.selectedItem.length; k++) {
    //     let elem = this.selectedItem[k];
    //     if(element===elem)
    //     {
    //       this.defaultItem.splice(index,1);
    //     }
        
    //   }
      
    // }
    console.log(this.defaultItem.length);
    console.log(this.selectedItem.length);
    if (this.canAdd) {
      this.PCPService.updatePCPTBVT(this.filterInput).subscribe(response => {
        this.message.success(this.l('AllocationBillUpdateSucess'));
      });
      this.defaultItem.map(thietbi => {
        this.thietBiVatTuService.removeTBVTFromPCPTBVT(thietbi.tbvT_MA_TBVT, this.filterInput.pcptbvT_MA_PCP).subscribe(response => {
        });
      });
      setTimeout(()=>{
      this.selectedItem.map(thietbi => {
        this.thietBiVatTuService.addTBVTToPCPTBVT(thietbi.tbvT_MA_TBVT, this.filterInput.pcptbvT_MA_PCP).subscribe(response => {
          this.message.success(this.l('AllocationBillUpdateSucess'));
        });
      });
    },100);
    }
    else {
      this.message.error(this.l('AllocationBillUpdateDenied'));
    }
  }
  cancel() {
    this.navigate(['/app/admin/capphattbvt']);
    // console.log(this.filterInput);
    // console.log(this.selectedItem);
    // console.log(this.defaultItem);
    // console.log(this.currentPCP.creatE_DT);
    // console.log(this.date3);
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

}
