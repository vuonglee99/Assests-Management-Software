import { ViewEncapsulation, Component, Injector, OnInit, Self } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {  LOAI_XE_DTO, LoaiXeServiceProxy } from "@shared/service-proxies/service-proxies";
import { Router, ActivatedRoute,Params } from "@angular/router";
import { XeService } from "../../services/xe.service";
import {FormBuilder,FormGroup,Validators, ReactiveFormsModule} from '@angular/forms';


@Component({
    templateUrl: './loai-xe-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./loai-xe-detail.component.css']
})
export class LoaiXeDetailComponent extends AppComponentBase implements OnInit {
    /**
     *
     */
    editable:boolean=false;
    public formUser:FormGroup;
    inputModel: LOAI_XE_DTO = new LOAI_XE_DTO();
    saveInputModel: LOAI_XE_DTO = new LOAI_XE_DTO();
    editPageState: string;
    public static confirmDelete:boolean=false;
    isNotFound:boolean=false;
    maxLengthName:boolean=false;
    maxLengthDesc:boolean=false;
    isSpecialName:boolean=false;
    isSpecialDesc:boolean=false;

    maxLengthHangXe: boolean=false;
    isSpecialHangXe: boolean = false;
    maxLengthLoaiNguyenLieu: boolean = false;
    isSpecialLoaiNguyenLieu: boolean = false;
    maxLengthCode: boolean = false;
    isSpecialCode: boolean = false;
    canSave:boolean=true;
    trangThai:string;

    constructor(injector: Injector,
        private loaiXeServiceProxy: LoaiXeServiceProxy,
        public activatedRouteService:ActivatedRoute) {
        super(injector);

    }
    ngOnInit(): void {
        this.isNotFound=false;
        this.activatedRouteService.params.subscribe((data:Params)=>{
            let id=data['id'];
            this.loaiXeServiceProxy.loaiXeById(id).subscribe(response=>{
                console.log(response);
                console.log(response.autH_STATUS);
                if(response.lX_ID!=null && !(response.recorD_STATUS=="0" && response.autH_STATUS=='A')){
                    this.inputModel=response;
                    this.getTrangThai();
                    console.log(this.trangThai);
                }
                else{
                    this.isNotFound=true;
                }

            });
        });
        //this.inputModel.loaI_XE_ID='loai_xe0000000000006';

    }
    onEdit(){
        this.saveInputModel.lX_ID=this.inputModel.lX_ID;
        this.saveInputModel.lX_TEN=this.inputModel.lX_TEN;
        this.saveInputModel.lX_MO_TA=this.inputModel.lX_MO_TA;
        this.saveInputModel.lX_CODE=this.inputModel.lX_CODE;
        this.saveInputModel.lX_HANGXE = this.inputModel.lX_HANGXE;
        this.saveInputModel.lX_LOAINGUYENLIEU = this.inputModel.lX_LOAINGUYENLIEU;
        this.saveInputModel.lX_NAMSX = this.inputModel.lX_NAMSX;
        this.saveInputModel.lX_DINHMUCNL = this.inputModel.lX_DINHMUCNL;
        this.editable=true;
    }

    onSaveEdit(){
        this.editable=false;
        this.loaiXeServiceProxy.loaiXeUpdate(this.inputModel).subscribe(response=>{
            if(response['0']['RESULT']=='0'){
                this.message.success(this.l("G2SuaThanhCong"));
            }
            else{
                this.editable=true;
                this.message.error(this.l("G2MaLoaiXe") + " " + this.l("G2KhongHopLe") +": "+ response['ERROR_DESC']);
            }
        });
    }

    onCancelEdit(){
        this.editable=false;
        this.inputModel.lX_ID=this.saveInputModel.lX_ID;
        this.inputModel.lX_CODE=this.saveInputModel.lX_CODE;
        this.inputModel.lX_TEN=this.saveInputModel.lX_TEN;
        this.inputModel.lX_MO_TA=this.saveInputModel.lX_MO_TA;
        this.inputModel.lX_HANGXE=this.saveInputModel.lX_HANGXE;
        this.inputModel.lX_LOAINGUYENLIEU=this.saveInputModel.lX_LOAINGUYENLIEU;
        this.inputModel.lX_NAMSX=this.saveInputModel.lX_NAMSX;
        this.inputModel.lX_DINHMUCNL=this.saveInputModel.lX_DINHMUCNL;
        this.maxLengthName=false;
        this.maxLengthDesc=false;
        this.isSpecialName=false;
        this.isSpecialDesc=false;
        this.maxLengthHangXe = false;
        this.isSpecialHangXe = false;
        this.maxLengthCode = false;
        this.isSpecialCode = false;
        this.maxLengthLoaiNguyenLieu = false;
        this.isSpecialLoaiNguyenLieu = false;

        this.canSave=true;
    }

    checkSpecialName(){
        var format = /[&'"\\<>\/`]+/;
        if(format.test(this.inputModel.lX_TEN) ||
            this.inputModel.lX_TEN.indexOf("`")!=-1||
            this.inputModel.lX_TEN.indexOf("~")!=-1){
                this.isSpecialName=true;
                this.canSave=false;
        }
        else{
            this.isSpecialName=false;
        }

    }

    checkSpecialDesc(){
        var format = /[&'"\\<>\/`]+/;
        if(format.test(this.inputModel.lX_MO_TA) ||
            this.inputModel.lX_MO_TA.indexOf("`")!=-1||
            this.inputModel.lX_MO_TA.indexOf("~")!=-1){
                this.isSpecialDesc=true;
                this.canSave=false;
        }
        else{
            this.isSpecialDesc=false;
        }
    }

    checkSpeciaLoaiNguyenLieu() {
        var format = /[&'"\\<>\/`]+/;
        if (format.test(this.inputModel.lX_LOAINGUYENLIEU) ||
            this.inputModel.lX_LOAINGUYENLIEU.indexOf("`") != -1 ||
            this.inputModel.lX_LOAINGUYENLIEU.indexOf("~") != -1) {
            this.isSpecialLoaiNguyenLieu = true;
            this.canSave = false;
        }
        else {
            this.isSpecialLoaiNguyenLieu = false;
        }
    }

    checkSpeciaHangXe() {
        var format = /[&'"\\<>\/`]+/;
        if (format.test(this.inputModel.lX_HANGXE) ||
            this.inputModel.lX_HANGXE.indexOf("`") != -1 ||
            this.inputModel.lX_HANGXE.indexOf("~") != -1) {
            this.isSpecialHangXe = true;
            this.canSave = false;
        }
        else {
            this.isSpecialHangXe = false;
        }
    }

    checkSpecialCode() {
        var format = /[&'"\\<>\/`]+/;
        if (format.test(this.inputModel.lX_CODE) ||
            this.inputModel.lX_CODE.indexOf("`") != -1 ||
            this.inputModel.lX_CODE.indexOf("~") != -1) {
            this.isSpecialCode = true;
            this.canSave = false;
        }
        else {
            this.isSpecialCode = false;
        }
    }

    //OK
    checkValidInput(){
        this.canSave=true;

        if(this.inputModel.lX_TEN.length>100){
            this.maxLengthName=true;
            this.canSave=false;
        }
        else{
            this.maxLengthName=false;
        }
        if(this.inputModel.lX_MO_TA.length>1000){
            this.maxLengthDesc=true;
            this.canSave=false;
        }
        else{
            this.maxLengthDesc=false;
        }

        if (this.inputModel.lX_HANGXE.length > 20) {
            this.maxLengthHangXe = true;
            this.canSave = false;
        }
        else {
            this.maxLengthHangXe = false;
        }
        if (this.inputModel.lX_LOAINGUYENLIEU.length > 20) {
            this.maxLengthLoaiNguyenLieu = true;
            this.canSave = false;
        }
        else {
            this.maxLengthLoaiNguyenLieu = false;
        }
        
        if (this.inputModel.lX_CODE.length > 20) {
            this.maxLengthCode = true;
            this.canSave = false;
        }
        else {
            this.maxLengthCode = false;
        }

        this.checkSpecialName();
        this.checkSpecialDesc();
        this.checkSpeciaHangXe();
        this.checkSpecialCode();
        this.checkSpeciaLoaiNguyenLieu();
        if(this.inputModel.lX_ID==null ||
           this.inputModel.lX_TEN==null ||
           this.inputModel.lX_ID=='' ||
           this.inputModel.lX_TEN=='' ||
           this.inputModel.lX_TEN.length>100||
           this.inputModel.lX_MO_TA.length>1000||
           this.isSpecialName==true||
           this.isSpecialDesc==true||
           this.isSpecialCode==true||
           this.isSpecialHangXe==true||
           this.isSpecialLoaiNguyenLieu==true||
           this.inputModel.lX_HANGXE.length > 20||
           this.inputModel.lX_LOAINGUYENLIEU.length > 20)
           this.canSave=false;
    }



    onRouterAdd()
    {
        this.navigate(['/app/admin/loai-xe-add']);
    }

    onDelete(){
        this.message.confirm(
            this.l("G2BanChacChanMuonXoaLoaiXe")+ ` "${this.inputModel.lX_ID}" ?`,
            this.l("G2XacNhan"),
            (isTrue)=> {
                if(isTrue){
                    this.loaiXeServiceProxy.loaiXeDelete(this.inputModel.lX_ID).subscribe(respoen=>{
                        this.message.success(this.l("G2XoaThanhCong"));
                        this.navigate(['/app/admin/loai-xe']);
                    })
                }else{
                    console.log("no")
                }
            }

        )


    }

    onExit(){
        this.navigate(['/app/admin/loai-xe']);
    }

    onApprove(){
        console.log("LOAIXECODE:" + this.inputModel.lX_CODE);
        this.loaiXeServiceProxy.loaiXe_Approve(this.inputModel.lX_CODE).subscribe(respoen => {
            this.message.success(this.l("G2DuyetThanhCong"));
            this.navigate(['/app/admin/loai-xe']);
        });
    }
    onUnApprove() {
        this.loaiXeServiceProxy.loaiXe_UnApprove(this.inputModel.lX_CODE).subscribe(respoen => {
            this.message.success(this.l("G2KhongDuyetThanhCong"));
            this.navigate(['/app/admin/loai-xe']);
        });
    }

    getTrangThai(){
        if(this.inputModel.autH_STATUS=='U'&&this.inputModel.approvE_DT==null && this.inputModel.recorD_STATUS!='0'){
            this.trangThai='Chờ duyệt thêm';
        }
        else if (this.inputModel.autH_STATUS == 'U' && this.inputModel.approvE_DT != null && this.inputModel.recorD_STATUS == '0') {
            this.trangThai = 'Chờ duyệt xóa';
        }
        else if (this.inputModel.autH_STATUS == 'U' && this.inputModel.approvE_DT != null && this.inputModel.recorD_STATUS != '0') {
            this.trangThai = 'Chờ duyệt sửa';
        }
        else{
            this.trangThai = 'NON'
        }
    }
}
