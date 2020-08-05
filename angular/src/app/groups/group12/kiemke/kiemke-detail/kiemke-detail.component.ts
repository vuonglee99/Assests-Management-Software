import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { KiemKeServiceProxy, KIEMKE_DTO } from "@shared/service-proxies/service-proxies";
import { SelectItem } from 'primeng/api';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import * as moment from "moment";


@Component({
    templateUrl: './kiemke-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class KiemKeDetailComponent extends AppComponentBase implements OnInit {

    kiemKeForm: FormGroup;
    kiemKeList: KIEMKE_DTO[] = [];
    kiemKe: KIEMKE_DTO = new KIEMKE_DTO();
    currrentId: any;
    dateCreatedInput: any = null;
    dateClosingInput: any = null;
    isEdit: boolean = false;
    isEditable:boolean=false;
    isUnApproved: boolean = false;
    submitted: boolean = false;
    nhanvienList: string[] = [];
    editPageState:string;
    kK_CODE=null;
    kK_MADONVI=null;
    kK_TENDONVI=null;
    kK_TONGTB_DUOCKIEMKE=null;
    constructor(
        injector: Injector,
        private kiemKeService: KiemKeServiceProxy,
        private formBuider: FormBuilder
    ) {

        super(injector);
        this.kiemKeForm = this.formBuider.group({
            'kK_CODE': new FormControl('', [Validators.required]),
            'kK_MADONVI': new FormControl('', [Validators.required]),
            'kK_NGAYCHOT': new FormControl('', [Validators.required]),
            'kK_NGAYTAO': new FormControl('', [Validators.required]),
            'kK_TENDONVI': new FormControl('', [Validators.required]),
            'kK_TONGTB_DUOCKIEMKE': new FormControl(Number, [Validators.required]),
        });

    }


    ngOnInit(): void {
        this.editPageState = this.getRouteData('editPageState');
        this.currrentId = this.getRouteParam('kK_ID');
        this.getKiemKe();
        this.getAllNhanVien();
    }

    getKiemKe() {
        this.kiemKeService.kiemKe_GetAll(this.kiemKe).subscribe(res => {
            this.kiemKeList = res;
            
            for (var i = 0; i < this.kiemKeList.length; i++) {
                if (this.kiemKeList[i].kK_ID == this.currrentId) {
                    this.kiemKe = this.kiemKeList[i];
                }
            }
            if (this.kiemKe.autH_STATUS != '1')
                this.isUnApproved = true;
            else this.isUnApproved = false;

            //g2
            this.kiemKe.kK_NGAYTAO = this.kiemKe.kK_NGAYTAO.replace('/', '-').replace('/', '-');
            this.kiemKe.kK_NGAYCHOT = this.kiemKe.kK_NGAYCHOT.replace('/', '-').replace('/', '-');
            
            if(this.kiemKe.autH_STATUS=='0') this.isEditable=false;
            else this.isEditable=true;
        })
    }


    save() {
        this.submitted = true;
        if (this.kiemKeForm.valid) {
            if(this.editPageState=="update"){
                this.kiemKe.approvE_DT=this.getDate(new Date());
                this.kiemKe.kK_NGAYTAO = this.kiemKe.kK_NGAYTAO.replace('-', '/').replace('-', '/');
                this.kiemKe.kK_NGAYCHOT = this.kiemKe.kK_NGAYCHOT.replace('-', '/').replace('-', '/');
                this.kiemKe.autH_STATUS='0';
                this.isEdit=false;
                this.kiemKeService.kiemKe_Update(this.kiemKe).subscribe(response => {
                    if (response[0].RESULT == '0') {
                        this.message.success('Chỉnh sửa thành công');
                    }
                    else if (response[0].RESULT == '-1') {
                        this.message.error(response[0].ERROR_DESC);
                    }
                })
            }
        } else {
            this.message.error('Bạn phải hoàn thành các mục dữ liệu yêu cầu!');
        }
    }


    getAllNhanVien() {
        this.nhanvienList.push();
        this.nhanvienList.push("Hà Thị Anh");
        this.nhanvienList.push("Lê Bá Vương");
        this.nhanvienList.push("Phạm Tuấn Anh");
    }

    delete() {
        if (this.kiemKe.kK_TRANGTHAI == "đóng") {
            this.kiemKeService.kiemKe_Delete(this.kiemKe.kK_ID).subscribe(res => {
                if (res[0].RESULT == '0') {
                    this.message.success('Xóa thành công');
                    this.router.navigate(['/app/admin/kiemke']);
                }
                else if (res[0].RESULT == '-1') {
                    this.message.error(res[0].ERROR_DESC);
                }
            });
        } else this.message.error("Không thể xóa bản kiểm kê đang mở!");

    }

    strToDate(dateInput: string): Date {
        let arr = dateInput.split("/", 3);
        var dd = parseInt(arr[0]);
        var mm = parseInt(arr[1]);
        var yyyy = parseInt(arr[2]);

        let date = new Date(yyyy, mm - 1, dd);
        return date;
    }

    approve(isApprove: boolean) {
        isApprove ? (this.kiemKe.autH_STATUS = '1', this.isUnApproved = false) : this.kiemKe.autH_STATUS = '-1';

        this.kiemKeService.kiemKe_Update(this.kiemKe).subscribe(response => {
            if (response[0].RESULT == '0') {
                if(isApprove)
                    this.message.success(this.l('Group12_CheckSuccess'));
                else this.message.success(this.l('Group12_DenyApproveSuccessfully'));
                if(this.kiemKe.recorD_STATUS=='0' && isApprove)
                    this.navigate(['app/admin/kiemke']);

            }
            else if (response[0].RESULT == '-1') {
                this.message.error(response[0].ERROR_DESC);
            }
        })
    }
    createDate() {
        console.log(typeof this.dateCreatedInput);
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
}