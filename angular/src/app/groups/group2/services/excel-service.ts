import { saveAs } from 'file-saver';
import * as XLSX from 'xlsx'
import { Injectable } from '@angular/core';
import * as moment from 'moment';
@Injectable({
  providedIn: 'root'
})
export class ExcelService {
constructor() { }
public exportAsExcelFile(json: any[],bankk:any ,excelFileName: string): void {
	let TBKK:ThietBiKiemKe[]=[];
		//console.log(json);
		let tb:any[]=[];

	for (var i = 0; i < json.length; ++i) {
		// code...

		TBKK[i]=new ThietBiKiemKe(json[i].ctbkK_MA_TB,json[i].ctbkK_TEN_TB,json[i].ctbkK_DV_QL,json[i].ctbkK_TT_SAU
			,json[i].ctbkK_TT,json[i].ctbkK_THOI_GIAN);
		//console.log(json[i]);

	}
	//console.log(TBKK);
	//console.log(TBKK[i].ctbkK_MA_TB);
	 //const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(ba);
  const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(TBKK);
  const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data']};
  const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
  this.saveAsExcelFile(excelBuffer, excelFileName);
}
private saveAsExcelFile(buffer: any, fileName: string): void {
  const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
  const EXCEL_EXTENSION = '.xlsx'
   const data: Blob = new Blob([buffer], {type: EXCEL_TYPE});
   saveAs.saveAs(data, fileName + '_export_' + new  Date().getTime() + EXCEL_EXTENSION);
}
}

@Injectable({
  providedIn: 'root'
})
 class ThietBiKiemKe  {
   public Ma_Thiet_Bi: string;
   public Ten_Thiet_Bi: string ;
   public Don_Vi_Quan_Li: string;
   public Tinh_Trang: string ; 
  public  Tinh_Trang_Sau_KiemKe: string; 
   
  public  Thoi_Gian:string;
  constructor(ma:string,ten:string,dv:string,ttsau:string,tt:string,tg:string)

  {
  	this.Ma_Thiet_Bi=ma;this.Ten_Thiet_Bi=ten;this.Don_Vi_Quan_Li=dv;this.Tinh_Trang_Sau_KiemKe=ttsau;
  	this.Tinh_Trang=tt;this.Thoi_Gian=tg;
  }
   // public init(data?: any) {
   //      if (data) {
           
            
   //          this.ctbkK_MA_TB = data["ctbkK_MA_TB"];
   //          this.ctbkK_TT = data["ctbkK_TT"];
   //          this.ctbkK_TT_SAU = data["ctbkK_TT_SAU"];
   //          this.ctbkK_TEN_TB = data["ctbkK_TEN_TB"];
   //          this.ctbkK_DV_QL = data["ctbkK_DV_QL"];
   //          this.ctbkK_THOI_GIAN = data["ctbkK_THOI_GIAN"] ? moment(data["ctbkK_THOI_GIAN"].toString()) : <any>undefined;
   //          ;
    
   //      }
   //  }
}