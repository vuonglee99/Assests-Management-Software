import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { KiemKeServiceProxy, KIEMKE_DTO, ChiTietBanKiemKeServiceProxy } from "@shared/service-proxies/service-proxies";
import { SelectItem } from 'primeng/api';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';



@Component({
    templateUrl: './kiemke-add.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class KiemKeAddComponent extends AppComponentBase implements OnInit {

    kiemKeForm:FormGroup;
    kiemKeInput: KIEMKE_DTO = new KIEMKE_DTO();
    cols: any[];
    nhanvienList:string[]=[];
    dateInput:any=null;
    editPageState: string;
    submitted = null; kK_CODE = null; kK_MADONVI = null; kK_TENDONVI = null; kK_TONGTB_DUOCKIEMKE=null; 
    constructor(
        injector: Injector,
        private kiemKeService: KiemKeServiceProxy,
        private formBuider: FormBuilder,
        private ctKiemKeService: ChiTietBanKiemKeServiceProxy,
    ) {
       
        super(injector);
        this.editPageState = this.getRouteData('editPageState');
        this.kiemKeForm=this.formBuider.group({
            'kK_CODE':new FormControl('',[Validators.required]),
            'kK_MADONVI':new FormControl('',[Validators.required]),
             'kK_NGAYCHOT':new FormControl('',[Validators.required]),
            'kK_TENDONVI':new FormControl('',[Validators.required]),
            'kK_TONGTB_DUOCKIEMKE':new FormControl(Number,[Validators.required]),
        });
    }


    ngOnInit(): void {
        
        this.kiemKeInput.kK_NGUOITAO="Hà Thị Anh";
        this.kiemKeInput.kK_TRANGTHAI="đóng";
        this.kiemKeInput.kK_TONGTB_DUSOVOISAOKE=this.kiemKeInput.kK_TONGTB_THIEUSOVOISAOKE=this.kiemKeInput.kK_TONGTB_THUASOVOISAOKE=0;
        this.kiemKeInput.kK_TONGTB_DUOCKIEMKE=0;
        this.getAllNhanVien();
    }

    save() {
        if (this.kiemKeForm.valid && this.editPageState == 'create') {            
            var today=this.getDate(new Date());
            
            if (this.strToDate(this.kiemKeInput.kK_NGAYCHOT)<this.strToDate(today)) {
                this.message.error('Ngày chốt không thể bé hơn ngày hiện tại!');
            }else{
                this.kiemKeInput.recorD_STATUS = "1";
                this.kiemKeInput.kK_NGAYTAO=this.getDate(new Date());
                this.kiemKeInput.autH_STATUS='0';

                this.kiemKeInput.kK_NGAYTAO = this.kiemKeInput.kK_NGAYTAO.replace('-', '/').replace('-', '/');
                this.kiemKeInput.kK_NGAYCHOT = this.kiemKeInput.kK_NGAYCHOT.replace('-', '/').replace('-', '/');

                this.kiemKeService.kiemKe_Insert(this.kiemKeInput).subscribe(response => {
                    if (response[0].RESULT == '0') {
                        this.message.success('Thêm mới thành công');
                        if (response[0].ID!=null){
                            this.ctKiemKeService.taoChiTietKiemKe(response[0].ID,this.kiemKeInput.kK_MADONVI).subscribe(res=>{});
                        }
                    }
                    else if (response[0].RESULT == '-1') {
                        this.message.error(response[0].ERROR_DESC);
                    }
                })
            }
        }else{
            this.message.error('Bạn phải hoàn thành các mục dữ liệu yêu cầu!');
        }
    }

    

   
     getDate(dateInput: any): string {
        if (dateInput != null) {
            let d = new Date(Date.parse(dateInput));
            var dd = (d.getDate() < 10) ? dd = '0' + d.getDate() : dd = d.getDate();
            var mm = ((d.getMonth() + 1) < 10) ? mm = '0' + (d.getMonth() + 1) : mm = (d.getMonth() + 1);
            var yyyy = d.getFullYear();
            let myDate = yyyy + "/" + mm + "/" + dd;
            return String(myDate);
        } else return "";
    }

    strToDate(dateInput: string): Date {
        let arr = dateInput.split("/", 3);
        var dd = parseInt(arr[0]);
        var mm = parseInt(arr[1]);
        var yyyy = parseInt(arr[2]);

        let date = new Date(yyyy, mm - 1, dd);
        return date;
    }
    
    getAllNhanVien() {
        this.nhanvienList.push();
        this.nhanvienList.push( "Hà Thị Anh");
        this.nhanvienList.push( "Lê Bá Vương");
        this.nhanvienList.push("Phạm Tuấn Anh");
    }
    
}