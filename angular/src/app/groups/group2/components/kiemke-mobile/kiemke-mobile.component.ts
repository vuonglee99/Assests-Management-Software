import { appModuleAnimation } from "@shared/animations/routerTransition";
import {AppComponentBase} from '@shared/common/app-component-base';
import {Component, Injector, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import { KiemKeServiceProxy, KIEM_KE_DTO, ChiTietBanKiemKeServiceProxy, API_BASE_URL} from '@shared/service-proxies/service-proxies';
import {LazyLoadEvent} from '@node_modules/primeng/components/common/lazyloadevent';
import {Table} from '@node_modules/primeng/components/table/table';
import {Paginator} from '@node_modules/primeng/components/paginator/paginator';
import { Router, ActivatedRoute,Params } from "@angular/router";
import { async } from "@angular/core/testing";
import  {ExcelService} from '../../services/excel-service'
import { FileDownloadService } from "@shared/utils/file-download.service";
import { AppConsts } from '@shared/AppConsts';

import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
@Component({
  selector: 'app-kiemke-mobile',
  templateUrl: './kiemke-mobile.component.html',
  styleUrls: ['./kiemke-mobile.component.css'],
  encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],

})
export class KiemkeMobileComponent extends AppComponentBase implements OnInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

	  public isLoading = false;
    public loaded=false;
    public recordsnow: any[] = [];
    public records: any[] = [];
    public currentRecords: any[] = [];
    public Bankiemke= new KIEM_KE_DTO();

    public pageSize:number=5;
    public DuSoVoiSaoKe:number=0;
    public ThuaSoVoiSaoKe:number=0;
    public ThieuSoVoiSaoKe:number=0;
   
    selectedTinhTrang: string = '';
    selectedTinhTrangDuThieuThua: string ='';
    inputTenTB:string='';
    inputMaTB:string='';
    selectedDonVi:string='';
    private fdlService: FileDownloadService;
    dateNow:string ='';
  constructor(injector: Injector,
                public chitietKiemKeservice:ChiTietBanKiemKeServiceProxy
                ,public KiemKeService:KiemKeServiceProxy,
                public activatedRouteService:ActivatedRoute,
                public excelService:ExcelService ) {
  	 super(injector);
        this.isLoading = false;
                 }

  ngOnInit() :void{
    
        this.activatedRouteService.params.subscribe(async (data:Params)=>{
            let id=data['id'];
            let bkkID= this.getChiTietKK(id);
            // console.log('****BKKCODE: '+bkkCode);
            
        }
        );
  }
exportAsXLSX():void {
  
   //this.excelService.exportAsExcelFile(this.recordsnow,this.Bankiemke, 'ChitieKiemKe'+this.Bankiemke.kK_CODE);
  this.chitietKiemKeservice.exportExcel(this.Bankiemke.kK_CODE)
    .subscribe(result => {
      const url = AppConsts.remoteServiceBaseUrl + '/File/DownloadTempFile?fileType=' + result.fileType + '&fileToken=' + result.fileToken + '&fileName=' + result.fileName;
      location.href = url; //TODO: This causes reloading of same page in Firefox
    });
}
   async getChiTietKK(value:string) {//event?: LazyLoadEvent
     var bkkID=null;
      await this.KiemKeService.kiemKeById(value).subscribe((data: any) => {
        this.Bankiemke.init(data);
        
        this.getThieBiKK(this.Bankiemke.kK_CODE);
        this.loaded=true;
     });
    }
    async getThieBiKK(value:string)
    {
      this.isLoading = true;
      this.chitietKiemKeservice.chiTietKiemKe_Get(value).subscribe((data:any)=>{
            this.recordsnow = data;
            this.records=this.recordsnow;
            this.isLoading = false;
            
      })
    }
  selectChangeTinhTrang(event: any) {
    this.selectedTinhTrang=event.target.value;
  }
  selectChangeTinhTrangDuThieuThua(event: any) {
    this.selectedTinhTrangDuThieuThua = event.target.value;
  }
  inputMaTBChange(event: any){
this.inputMaTB=event.target.value;
}
  inputTenTBChange(event: any){
this.inputTenTB=event.target.value;
}
  selectDonViChange(event: any){
this.selectedDonVi=event.target.value;
}
  onSearch2()
  {
    
    var dataFilter=this.recordsnow;
    //filter ma
    if(this.inputMaTB !=='')
    {
      dataFilter = dataFilter.filter(eq => eq.ctbkK_MA_TB!=null&&eq.ctbkK_MA_TB.toLowerCase().indexOf(this.inputMaTB.toLowerCase())!==-1)
    }
    //filter ten
    if(this.inputTenTB !=='')
    {
      dataFilter = dataFilter.filter(eq=> eq.ctbkK_TEN_TB.toLowerCase().indexOf(this.inputTenTB.toLowerCase())!==-1)
    }
    //filter don vi
    if(this.selectedDonVi==='cungdonvi')
    {
      // console.log("Cung "+dataFilter[0].ctbkK_DV_QL)
      // console.log("Cung "+dataFilter[0].ctbkK_DV_QL+dataFilter[0].ctbkK_DV_QL.length)
      // console.log("Cung "+this.Bankiemke.kK_TENDONVI+this.Bankiemke.kK_TENDONVI.length)
      dataFilter = dataFilter.filter(eq => eq.ctbkK_DV_QL!=null&&eq.ctbkK_DV_QL.toLowerCase().replace(/\s/g, "")==this.Bankiemke.kK_TENDONVI.toLowerCase().replace(/\s/g, ""))
      
      
    }
    if(this.selectedDonVi==='khacdonvi')
    {
      dataFilter = dataFilter.filter(eq => eq.ctbkK_DV_QL==null||eq.ctbkK_DV_QL.toLowerCase().replace(/\s/g, "")!=this.Bankiemke.kK_TENDONVI.toLowerCase().replace(/\s/g, ""))
    }

    // filter trang thai
    if(this.selectedTinhTrang!=='')
    {
      dataFilter = dataFilter.filter(eq =>eq.ctbkK_TT_SAU===this.selectedTinhTrang)
    }

    if (this.selectedTinhTrangDuThieuThua!==''){
      console.log(this.selectedTinhTrangDuThieuThua);
      console.log(this.recordsnow);
      dataFilter = dataFilter.filter(eq => (eq.ctbkK_TT === this.selectedTinhTrangDuThieuThua || (this.selectedTinhTrangDuThieuThua === "Thiếu" && eq.ctbkK_TT == null)));
    }
    // if(this.selectedTinhTrang==='hu hong')
    // {
    //   console.log("hu hong")
    //   dataFilter = dataFilter.filter(eq=> eq.ctbkK_TT_SAU==="Hư hỏng")
    // }
    // if(this.selectedTinhTrang==='binh thuong')
    // {
    //   console.log("binh thuong")
    //   dataFilter = dataFilter.filter(eq=> eq.ctbkK_TT_SAU==="Bình thường")
    // }
    // if(this.selectedTinhTrang==='sua chua')
    // {
    //   console.log("sua chua")
    //   dataFilter = dataFilter.filter(eq=> eq.ctbkK_TT_SAU==="Sửa chữa")
    // }

    this.records=dataFilter

  }

  selectChangeTinhTrangSoVoiSaoKe(event: any) { }

  exportExcel(){
    
  }
  // downLoadFile(data: any, type: string) {
  //   let blob = new Blob([data], { type: type });
  //   let url = window.URL.createObjectURL(blob);
  //   let pwa = window.open(url);
  //   if (!pwa || pwa.closed || typeof pwa.closed == 'undefined') {
  //     alert('Please disable your Pop-up blocker and try again.');
  //   }
  // }
}
