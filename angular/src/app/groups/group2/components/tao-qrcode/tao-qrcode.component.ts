import { appModuleAnimation } from "@shared/animations/routerTransition";
import {AppComponentBase} from '@shared/common/app-component-base';
import {Component, Injector, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {ICM_DEV_DTO,ThietBiServiceProxy,DeviceServiceProxy,CM_DEV_DTO} from '@shared/service-proxies/service-proxies';
import {LazyLoadEvent} from '@node_modules/primeng/components/common/lazyloadevent';
import {Table} from '@node_modules/primeng/components/table/table';
import {Paginator} from '@node_modules/primeng/components/paginator/paginator';
import * as moment from 'moment';
//import {NgxQRCodeModule} from  'ngx-qrcode2';


@Component({
  selector: 'app-tao-qrcode',
  templateUrl: './tao-qrcode.component.html',
  styleUrls: ['./tao-qrcode.component.css']
})
export class TaoQrcodeComponent extends AppComponentBase implements OnInit {

	@ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
   
	  public isLoading = false;
    public loaded=false;
    public recordsnow:any[] = [];
    public records: any[] = [];
    public currentRecords: any[] = [];
    public dataoQRcode=false;
    public ThietBiSelect:any={};
    public linkQR:string;
    public pageSize:number=5;
    public recordsToPrint:ThietBi[]=[];
    public recordsCheck:ThietBi[]=[];
    public records1=[];
    public records2=[];
    public records3=[];
    selectedTinhTrang: string = '';
    inputTenTB:string='';
    inputMaTB:string='';
    inputDonVi:string='';
    public dataoQRcodeAll=false;


  constructor(injector: Injector,
                public thietbiservice:DeviceServiceProxy ) {
  	 super(injector);
        this.isLoading = false;
        this.linkQR="https://api.qrserver.com/v1/create-qr-code/?data=Hello&amp";
                 }
  ngOnInit() {
  	this.getThieBiKK();
  }
  async getThieBiKK()
    {
      this.isLoading = true;
      let cm_dev_dto=new CM_DEV_DTO;
      cm_dev_dto.devicE_ID="";
      cm_dev_dto.devicE_CODE="";
      cm_dev_dto.devicE_NAME="";
      cm_dev_dto.brancH_ID="";
      cm_dev_dto.maintenancE_CYCLE="";
      cm_dev_dto.recorD_STATUS="";
      cm_dev_dto.activE_STATUS="";
      //cm_dev_dto.datE_BUY
      //cm_dev_dto.brancH_ID="";
      //cm_dev_dto.brancH_ID="";
      cm_dev_dto.descriptions="";
      cm_dev_dto.produceR_ID="";
      //cm_dev_dto.yeaR_PRODUCTION=;
      cm_dev_dto.serial="";
      cm_dev_dto.makeR_ID="";
      cm_dev_dto.checkeR_ID="";
      //cm_dev_dto.brancH_ID="";
      cm_dev_dto.brancH_NAME="";

      console.log(cm_dev_dto);
      // this.thietbiservice.dEVICE_Search("1",cm_dev_dto).subscribe((data:any)=>{
      this.thietbiservice.g2_DEVICE_GETALL().subscribe((data: any) => {
            this.recordsnow = data;
            
            //console.log("Hello");
            this.records=this.recordsnow;
            this.isLoading = false;
            
           //console.log("Hello"+this.records[0]);
      })
    }

     onSearch()
     {
      var dataFilter=this.recordsnow;
      //filter ma
      if(this.inputMaTB !=='')
      {
        dataFilter = dataFilter.filter(eq => eq.devicE_CODE!=null&&eq.devicE_CODE.toLowerCase().indexOf(this.inputMaTB.toLowerCase())!==-1)
      }
      //filter ten
      if(this.inputTenTB !=='')
      {
        dataFilter = dataFilter.filter(eq=> eq.devicE_NAME.toLowerCase().indexOf(this.inputTenTB.toLowerCase())!==-1)
      }
      //filter don vi
      if(this.inputDonVi!=='')
      {        
        dataFilter = dataFilter.filter(eq => eq.brancH_NAME!=null&&eq.brancH_NAME.toLowerCase().indexOf(this.inputDonVi.toLowerCase())!==-1)        
        
      }    
      // filter trang thai
      if(this.selectedTinhTrang!=='')
      {
        // console.log("hu hong")
        //dataFilter = dataFilter.filter(eq=> eq.activE_STATUS===this.selectedTinhTrang)
        dataFilter = dataFilter.filter(eq=> eq.activE_STATUS.toLowerCase().indexOf(this.selectedTinhTrang.toLowerCase())!==-1)
      }
  
      this.records=dataFilter
  
     }

    IsChecked(obj:any)
    {
        if(this.recordsCheck==[])
        return false;
      if(this.recordsCheck.filter(eq => eq==obj).length>0 )
        return true;
      else
        return false;
    }

    onChecked(obj: any, isChecked: boolean){
      //console.log(obj, isChecked); // {}, true || false
      if(isChecked)
      {
        this.recordsCheck.push(obj);
      }
      else
      {
        this.recordsCheck=this.recordsCheck.filter(eq => eq!==obj)  
      }
      // console.log(this.recordsCheck);
      //this.recordsToPrint=this.recordsCheck;
      this.TaoQRAll(this.recordsCheck);
      this.xuliRecordstoPrint();
      this.dataoQRcodeAll=true;
      
    }

    TaoQR(record:any)
    {
      this.ThietBiSelect=record;
      this.linkQR='https://api.qrserver.com/v1/create-qr-code/?data={"MaTB":"'+this.ThietBiSelect.devicE_CODE+'","TenTB":"'+
      this.ThietBiSelect.devicE_NAME+'","DonVi":"'+this.ThietBiSelect.brancH_NAME+'"}'+'&amp;size=100x100';
      this.dataoQRcode=true;
    }
    BoChonAll()
    {
      this.recordsCheck=[];
      this.recordsToPrint=[];
      this.xuliRecordstoPrint();
    }

    TaoQRAll(records:any[])
    {         
       // code...
       this.recordsCheck=records;
      this.recordsToPrint=[];
      for (var i = 0; i < records.length; i++) {
        // code...
        let linkQR='https://api.qrserver.com/v1/create-qr-code/?data={"MaTB":"'+records[i].devicE_CODE+'","TenTB":"'+
      records[i].devicE_NAME+'","DonVi":"'+records[i].brancH_NAME+'"}'+'&amp;size=100x100'; 
      this.recordsToPrint[i]=new ThietBi(records[i],linkQR);
       
      }
      this.dataoQRcodeAll=true;  
      this.xuliRecordstoPrint();    
    }
    xuliRecordstoPrint()
    {
      this.records1=[];
      this.records2=[];
      this.records3=[];
      for(let i=0;i<this.recordsToPrint.length;i++)
      {
        if (i%3==0)
        this.records1.push(this.recordsToPrint[i]);
        else
        if(i%3==1)
        this.records2.push(this.recordsToPrint[i])
        else
        this.records3.push(this.recordsToPrint[i])
      }
    }
    

printToCart(printSectionId: string){       
        let popupWinindow
        let innerContents = document.getElementById(printSectionId).innerHTML;
        popupWinindow = window.open('', '_blank', 'width=600,height=700,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
        popupWinindow.document.open();
        popupWinindow.document.write('<html><head><link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous"></head><body onload="window.print()">' + innerContents + '</html>');
        popupWinindow.document.close();
        
  }
  
  selectChangeTinhTrang(event: any) {
    this.selectedTinhTrang=event.target.value;
    if(this.selectedTinhTrang==='none')
    {
      this.selectedTinhTrang='';

    }
    if(this.selectedTinhTrang==='Bình thường')
    {
      this.selectedTinhTrang='1';
    }
    if(this.selectedTinhTrang==='Hư hỏng')
    {
      this.selectedTinhTrang='0';
    }
    if(this.selectedTinhTrang==='Đang sửa chữa')
    {
      this.selectedTinhTrang='2';
    }
    //console.log(this.selectedTinhTrang);
    //console.log(parseInt(this.selectedTinhTrang));

  }
  inputMaTBChange(event: any){
this.inputMaTB=event.target.value;


}
inputTenTBChange(event: any){
this.inputTenTB=event.target.value;
}
inputDonViChange(event: any){
this.inputDonVi=event.target.value;
}

  selectChangeTinhTrangSoVoiSaoKe(event:any){}
 }
 class ThietBi implements ICM_DEV_DTO {
  devicE_ID!: string | undefined;
  devicE_CODE!: string | undefined;
  devicE_NAME!: string | undefined;
  brancH_ID!: string | undefined;
  maintenancE_CYCLE!: string | undefined;
  recorD_STATUS!: string | undefined;
  activE_STATUS!: string | undefined;
  datE_BUY!: moment.Moment | undefined;
  datE_WARRANTY_BEGIN!: moment.Moment | undefined;
  datE_WARRANTY_END!: moment.Moment | undefined;
  descriptions!: string | undefined;
  produceR_ID!: string | undefined;
  yeaR_PRODUCTION!: number | undefined;
  serial!: string | undefined;
  makeR_ID!: string | undefined;
  checkeR_ID!: string | undefined;
  creatE_DT!: moment.Moment | undefined;
  approvE_DT!: moment.Moment | undefined;
  autH_STATUS!: string | undefined;
  brancH_NAME!: string | undefined;
  linkQR!:string;
constructor(data?: ICM_DEV_DTO,link?:string) {
      if (data) {
          for (var property in data) {
              if (data.hasOwnProperty(property))
                  (<any>this)[property] = (<any>data)[property];
          }
      }
      this.linkQR=link;

}
}



