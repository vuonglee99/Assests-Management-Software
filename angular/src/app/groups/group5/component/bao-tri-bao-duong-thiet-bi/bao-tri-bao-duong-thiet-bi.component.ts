import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {EquipmentServiceProxy , CM_EQUIP_DTO ,DeviceServiceProxy }  from "@shared/service-proxies/service-proxies";
import {ExportService} from '../_services/_services.component';
@Component({
  templateUrl: './bao-tri-bao-duong-thiet-bi.component.html',
  styleUrls: ['./bao-tri-bao-duong-thiet-bi.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class BaoTriBaoDuongThietBiComponent extends AppComponentBase implements OnInit{

  constructor(injector: Injector , private EquipmentServiceProxy: EquipmentServiceProxy,private DeviceServiceProxy : DeviceServiceProxy,private ExportService:ExportService) {
    super(injector);
    this.filterInput.wO_CODE='';
    this.filterInput.prioritY_ORDER='';
    this.filterInput.devicE_NAME='';
    this.filterInput.recorD_STATUS='';
    this.filterInput.kinD_FIX = '';
  }
  records: CM_EQUIP_DTO[] = [];
  filterInput: CM_EQUIP_DTO = new CM_EQUIP_DTO();
  listSelectRow:CM_EQUIP_DTO[] = [];
  isLoading=false;
 ngOnInit(): void {
    this.search();
    this.getListName();
  }
  isGetAll:false;

 
  //các field cần render ra với header là header của table và field và field của db
  cols = [
      { field: 'No', header: 'STT' },
      { field: 'wO_CODE', header: 'Mã yêu cầu' },
   
      { field: 'prioritY_ORDER', header: 'Độ ưu tiên' },
      { field: 'devicE_NAME', header: 'Tên thiết bị' },
      { field: 'recorD_STATUS', header: 'Trạng thái' },
      { field: 'autH_STATUS', header: 'Trạng thái duyệt' },
      { field: 'kinD_FIX', header: 'Loại' },
      { field: 'fixer', header: 'Nhân viên sữa chữa' },
      { field: 'descriptions', header: 'Mô tả' }
  ];
  branchs;
  listBranch;
  delete(){

  }

  viewDetail() {
    if(this.listSelectRow.length == 0 ){
        this.message.warn("Vui lòng chọn công việc","Thông báo")
    }
    else if(this.listSelectRow.length > 1){
        this.message.warn("Chỉ được chọn một công việc","Thông báo")
    }else{
        this.navigate(['/app/admin/chi-tiet-bao-tri'])  
    }
   
  }
  listNameDevice:string[]=[];
  getListName(){
    this.DeviceServiceProxy.dEVICE_Get_All_Name().subscribe(response=>{
      this.listNameDevice = response;
    })
  }


  search() {
    this.isLoading = true;
    console.log(this.filterInput);
    this.EquipmentServiceProxy.wORK_ORDER_Search(this.filterInput).subscribe(response => {
       
      this.isLoading = false;
        let i = 1;
        response.forEach(item=>{
          item["No"]=i++;
        })
        this.records = response;
    })
}


  addNew(){
    this.navigate(['app/admin/them-cong-viec']);
  }
  update(){

  }
  export() {
    this.ExportService.exportExcel(this.records,'Danh sách yêu cầu bảo dưỡng');
  }
}
