import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {  LOAI_XE_DTO, LoaiXeServiceProxy } from "@shared/service-proxies/service-proxies";
import { Router, ActivatedRoute } from "@angular/router";
import { XeService } from "../../services/xe.service";
import {FormBuilder,FormGroup,Validators, ReactiveFormsModule} from '@angular/forms';
import { getDate } from "ngx-bootstrap/chronos/utils/date-getters";


@Component({
    templateUrl: './loai-xe-add.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./loai-xe-add.component.css']
})
export class LoaiXeAddComponent extends AppComponentBase implements OnInit {
    /**
     *
     */
    editable:boolean=false;
    public formUser:FormGroup;
    inputModel: any= {
        lX_ID:'',
        lX_TEN:'',
        lX_MO_TA:'',
        lX_CODE:'',
        lX_HANGXE:'',
        lX_LOAINGUYENLIEU:'',
        lX_DINHMUCNL:0,
        lX_NAMSX:2000,
        recorD_STATUS:'1',
        makeR_ID :'',
        creatE_DT: new Date().toJSON(),
        autH_STATUS :'U',
        checkeR_ID :'',
        approvE_DT:''
    };
    //inputModel: LOAI_XE_DTO = new LOAI_XE_DTO();
    saveInputModel: LOAI_XE_DTO = new LOAI_XE_DTO();
    editPageState: string;
    maxLengthName:boolean=false;
    maxLengthDesc:boolean=false;
    isSpecialName:boolean=false;
    isSpecialDesc:boolean=false;

    maxLengthHangXe: boolean = false;
    isSpecialHangXe: boolean = false;
    maxLengthLoaiNguyenLieu: boolean = false;
    isSpecialLoaiNguyenLieu: boolean = false;
    maxLengthCode: boolean = false;
    isSpecialCode: boolean = false;
    canAdd:boolean=false;

    constructor(injector: Injector,private loaiXeServiceProxy: LoaiXeServiceProxy) {
        super(injector);

    }
    ngOnInit(): void {
        //
    }


    onAdd(){
        this.loaiXeServiceProxy.loaiXeInsert(this.inputModel).subscribe(response=>{
            //this.message.success("Thêm mới thành công");
            if (response.RESULT == 0) {
                this.message.success(response.ID);
            }
            else {
                //this.canAdd = true;
                this.message.error(response.ERROR_DESC);
            }
        });
    }

    checkSpecialName(){
        var format = /[&'"\\<>\/`]+/;
        if(format.test(this.inputModel.lX_TEN) ||
            this.inputModel.lX_TEN.indexOf("`")!=-1||
            this.inputModel.lX_TEN.indexOf("~")!=-1){
                this.isSpecialName=true;
                this.canAdd=false;
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
                this.canAdd=false;
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
            this.canAdd = false;
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
            this.canAdd = false;
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
            this.canAdd = false;
        }
        else {
            this.isSpecialCode = false;
        }
    }
    checkValidInput(){
        this.canAdd=true;
        
        if(this.inputModel.lX_TEN.length>100){
            this.maxLengthName=true;
            this.canAdd=false;
        }
        else{
            this.maxLengthName=false;
        }
        if(this.inputModel.lX_MO_TA.length>1000){
            this.maxLengthDesc=true;
            this.canAdd=false;
        }
        else{
            this.maxLengthDesc=false;
        }

        if (this.inputModel.lX_HANGXE.length > 20) {
            this.maxLengthHangXe = true;
            this.canAdd = false;
        }
        else {
            this.maxLengthHangXe = false;
        }
        if (this.inputModel.lX_LOAINGUYENLIEU.length > 20) {
            this.maxLengthLoaiNguyenLieu = true;
            this.canAdd = false;
        }
        else {
            this.maxLengthLoaiNguyenLieu = false;
        }

        if (this.inputModel.lX_CODE.length > 20) {
            this.maxLengthCode = true;
            this.canAdd = false;
        }
        else {
            this.maxLengthCode = false;
        }
        console.log('do input:' + this.canAdd);
        this.checkSpecialName();
        this.checkSpecialDesc();
        this.checkSpeciaHangXe();
        this.checkSpecialCode();
        this.checkSpeciaLoaiNguyenLieu();
        console.log('check:' + this.canAdd);
        if(this.inputModel.lX_CODE == null ||
           this.inputModel.lX_TEN==null ||
            this.inputModel.lX_CODE == '' ||
           this.inputModel.lX_TEN=='' ||
           this.inputModel.lX_TEN.length>100||
           this.inputModel.lX_MO_TA.length>1000||
            this.inputModel.lX_HANGXE.length > 20 ||
            this.inputModel.lX_LOAINGUYENLIEU.length > 20){
            console.log(this.inputModel);
            this.canAdd = false;
            console.log('if dai:' + this.canAdd);
        }
        
    }

    onExit(){
        this.navigate(['/app/admin/loai-xe']);
    }
}
