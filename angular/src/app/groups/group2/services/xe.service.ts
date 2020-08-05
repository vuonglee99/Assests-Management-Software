import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { CM_XE_DTO } from '@shared/service-proxies/service-proxies';
import { element } from '@angular/core/src/render3/instructions';

@Injectable({
  providedIn: 'root'
})
export class XeService {

    InitData: any[] = [
        {
            xE_CODE:'1',
            xE_ID:'1',
            xE_NAME:'Xe 1',
            xE_COLOR:'Black',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'2',
            xE_ID:'2',
            xE_NAME:'Xe 2',
            xE_COLOR:'Blue',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'3',
            xE_ID:'3',
            xE_NAME:'Xe 3',
            xE_COLOR:'Red',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'4',
            xE_ID:'4',
            xE_NAME:'Xe 4',
            xE_COLOR:'Green',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'5',
            xE_ID:'5',
            xE_NAME:'Xe 5',
            xE_COLOR:'Yellow',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'6',
            xE_ID:'6',
            xE_NAME:'Xe 6',
            xE_COLOR:'Purple',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'7',
            xE_ID:'7',
            xE_NAME:'Xe 7',
            xE_COLOR:'Pink',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'8',
            xE_ID:'8',
            xE_NAME:'Xe 8',
            xE_COLOR:'Gray',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'9',
            xE_ID:'9',
            xE_NAME:'Xe 9',
            xE_COLOR:'Orange',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'10',
            xE_ID:'10',
            xE_NAME:'Xe 10',
            xE_COLOR:'White',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'11',
            xE_ID:'11',
            xE_NAME:'Xe 11',
            xE_COLOR:'Accent',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
        {
            xE_CODE:'12',
            xE_ID:'12',
            xE_NAME:'Xe 12',
            xE_COLOR:'Cyan',
            notes:'',
            recorD_STATUS:'',
            makeR_ID:'',
            creatE_DT:null,
            autH_STATUS:'',
            checkeR_ID:'',
            approvE_DT:null,
            init:null,
            toJSON:null,
        },
    ];

    IdNavigate:string='-1';
  constructor(public http:HttpClient) { }

  getAllXe(){
  	return this.InitData;
  }

  setIdNavigate(value){
      this.IdNavigate=value;
  }

  getIdNavigate(){
      return this.IdNavigate;
  }

  deleteById(id){
      if(id==null || id =="") return;
      this.InitData.splice(this.getIndexById(id),1);
  }

  getIndexById(id):any{
    let index=0;
    let save=0;
    this.InitData.forEach(element => {
        if(element.xE_ID==id) {
            save=index;
        }
        index++;
    });
    return save;
  }

  getXeById(id):any{
    let save=null;
    this.InitData.forEach(element => {
        if(element.xE_ID==id) {
            save=element;
        }
    });
    return save;
  }

//   updateItem(item:CM_XE_DTO){
//     this.InitData.forEach(element=>{
//         if(element.xE_ID==item.xE_ID)
//         {
//             element.xE_CODE=item.xE_CODE;
//             element.xE_COLOR=item.xE_COLOR;
//             element.xE_NAME=item.xE_NAME;
//         }
//     });
//   }

//   addItem(item:CM_XE_DTO){

//       item.xE_ID=Math.random().toString(36).substr(2, 9).toString();
//       this.InitData.push(item);
//       console.log('new id: ',item.xE_ID);
//       console.log(this.InitData);
//   }


}
