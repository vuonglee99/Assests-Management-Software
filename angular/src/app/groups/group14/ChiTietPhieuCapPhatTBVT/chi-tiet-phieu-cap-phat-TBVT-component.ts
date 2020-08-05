import { ViewEncapsulation, Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { DatePipe } from '@angular/common';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Table } from "primeng/components/table/table";

import { PhieuCapPhatTBVTServiceProxy, PCPTBVT_DTO, ThietBiVatTuServiceProxy,THIETBIVATTU_DTO, PCPTBVT_APPROVE_DTO } from "@shared/service-proxies/service-proxies";

@Component({
  templateUrl: './chi-tiet-phieu-cap-phat-TBVT-component.html',
  styleUrls: ['./chi-tiet-phieu-cap-phat-TBVT-component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class ChiTietPhieuCapPhatTBVTComponent extends AppComponentBase implements OnInit {

  @ViewChild('dataTable') dataTable: Table;
  approveInput:PCPTBVT_APPROVE_DTO= new PCPTBVT_APPROVE_DTO();
  records: PCPTBVT_DTO[] = [];
  listSelectRow: PCPTBVT_DTO;
  filterInput: PCPTBVT_DTO = new PCPTBVT_DTO();
  ngayTao: Date = null;
  editPageState: string;
  
  skip: number | undefined;
  take: number | undefined;
  sorting: string | undefined;
  desc: boolean | undefined;
  selectedItem:any;
  approve: string | undefined;

  cols = [
    { field: 'pcptbvT_MA_PCP', header: this.l('AllocationBillID') },
    { field: 'creatE_DT', header: this.l('AllocationBillCreateDate') },
    { field: 'pcptbvT_DEP_ID', header: this.l('AllocationBillDepartment') },
    { field: 'makeR_ID', header: this.l('AllocationBiAllocationBillMakerIDlApprovalStatus') },
    { field: 'pcptbvT_GHI_CHU', header: this.l('AllocationBillNotes') },
    { field: 'autH_STATUS', header: this.l('AllocationBillApprovalStatus') },
    { field: 'checkeR_ID', header: this.l('AllocationBillCheckerID') },
  ];

  thietBiCols = [
    { field: 'Index', header: this.l('G14RowNumber') },
    { field: 'tbvT_MA_TBVT', header: this.l('EquipmentID') },
    { field: 'tbvT_TEN', header: this.l('EquipmentName') },
    { field: 'tbvT_LOAI', header: this.l('EquipmentType') },
    { field: 'tbvT_NGAY_MUA', header: this.l('EquipmentBuyDate') },
    { field: 'tbvT_TINH_TRANG_THIET_BI', header: this.l('EquipmentCondition') },
  ];

  constructor(injector: Injector, 
              private PhieuCapPhatTBVTServiceProxy: PhieuCapPhatTBVTServiceProxy,
              private ThietBiVatTuServiceProxy: ThietBiVatTuServiceProxy) {
    super(injector);
    this.filterInput.pcptbvT_MA_PCP = this.getRouteParam('id');
    this.approveInput.pcptbvT_MA_PCP=this.getRouteParam('id');
    this.editPageState = this.getRouteData('editPageState');
  }

  ngOnInit(): void {
    if (!this.filterInput.pcptbvT_MA_PCP) return;
    this.PhieuCapPhatTBVTServiceProxy.filterPCPTBVT(this.filterInput, undefined, undefined, undefined, undefined).subscribe(result => {
        console.log('result', result);
        this.filterInput.init(result.items[0]);
        console.log(this.filterInput);
        this.ngayTao = this.filterInput.creatE_DT.toDate();
        if(this.filterInput.autH_STATUS==='U')
        {
          this.approve=this.l('AllocationBillDenied');
        }
        else if (this.filterInput.autH_STATUS==='1')
        {
          this.approve=this.l('AllocationBillApproved');
        }
        else if (this.filterInput.autH_STATUS==='0')
        {
          this.approve=this.l('AllocationBillNotApproved');
        }
        // this.approve = this.filterInput.autH_STATUS === '0' ? this.l('AllocationBillNotApproved') || === 'U' ? this.l('AllocationBillDenied') : this.l('AllocationBillApproved');
    });
    this.getDSThietBiVatTu();
  }

  update(){
    $(":disabled").prop('disabled',false);
  }

  getDSThietBiVatTu(event?: LazyLoadEvent) {
    this.primengTableHelper.showLoadingIndicator();

    // Table sorting
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
    filters.tbvT_PCPTBVT_ID = this.getRouteParam('id');


    this.ThietBiVatTuServiceProxy.searchThietBiVatTu(
        filters,
        this.sorting,
        this.desc,
        undefined, undefined
    ).subscribe(result => {
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.totalRecordsCount = result.totalCount;
        this.primengTableHelper.hideLoadingIndicator();
    });
  }
  return()
  {
    this.navigate(['/app/admin/capphattbvt']);
  }
  updatePCP()
  {
    this.navigate(['/app/admin/capphattbvt-update/'+this.filterInput.pcptbvT_MA_PCP]);
  }
  approvePCP()
  {
    this.PhieuCapPhatTBVTServiceProxy.approvePCPTBVT(this.approveInput).subscribe(response=>{
      this.message.success(this.l('AllocationBillApprovedSuccessfully'));
    })
  }
  denyPCP()
  {
    this.PhieuCapPhatTBVTServiceProxy.denyPCPTBVT(this.approveInput).subscribe(response=>{
      this.message.success(this.l('AllocationBillDeniedSuccessfully'));
    })
  }
  save(){
    // this.DeviceServiceProxy.dEVICE_Update(this.filterInput).subscribe(response=>{
    //   console.log(response);
    // })
  }

}

