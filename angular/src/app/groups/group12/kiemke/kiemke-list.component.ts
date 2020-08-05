import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { KiemKeServiceProxy, KIEMKE_DTO } from "@shared/service-proxies/service-proxies";
import { SortEvent } from 'primeng/api';
import { SelectItem } from 'primeng/api';
import { saveAs } from 'file-saver';

@Component({
    templateUrl: './kiemke-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class KiemKeListComponent extends AppComponentBase implements OnInit {

    kiemKeInput: KIEMKE_DTO = new KIEMKE_DTO();
    records: KIEMKE_DTO[] = [];
    selectedKiemKe: KIEMKE_DTO;
    cols: any[];
    nhanvienList: string[]=[];
    listSelectedRow: KIEMKE_DTO[] = [];
    loading=null;
    dateInput: any = null;
    constructor(
        injector: Injector,
        private kiemKeService: KiemKeServiceProxy
    ) {
        super(injector);
    }


    ngOnInit(): void {
        this.kiemKeInput.kK_NGUOITAO="";
        this.kiemKeInput.recorD_STATUS="";
        this.search();
        this.getAllNhanVien();
    }

    addNew() {
        this.router.navigate(['/app/admin/kiemke/kiemke-add']);
    }


    delete() {
        if (this.listSelectedRow.length == 0)
            this.message.info(this.l('Group12_ChooseAModel'), this.l('Group12_Notification'));

        else {
            this.message.confirm(
                this.l('Group12_Notification'),
                this.l('Group12_DeleteModelWaringMessage'),
                (isConfirned) => {
                    if (isConfirned) {
                        for (var i = 0; i < this.listSelectedRow.length; i++) {
                            this.listSelectedRow[i].autH_STATUS = '0';
                            this.listSelectedRow[i].recorD_STATUS = '0';
                            this.kiemKeService.kiemKe_Update(this.listSelectedRow[i]).subscribe(res => {
                                if (res[0].RESULT != '0') {
                                    this.message.error(res[0].ERROR_DESC);
                                    return;
                                }
                            });
                        }
                        this.message.success(this.l('Group12_DeleteSuccess'), this.l('Group12_Notification'));
                        this.search();
                    }
                }
            )
        }
    }

    search() {
        this.records = [];
        this.primengTableHelper.records = [];
        console.log(this.kiemKeInput);

        this.kiemKeService.kiemKe_GetAll(this.kiemKeInput).subscribe(response => {
            for (var i = 0; i < response.length; i++) {
                if (!(response[i].recorD_STATUS === '0' && response[i].autH_STATUS === '1'))//chua xoa
                    if (response[i].autH_STATUS == '0')//chua duoc duyet
                    {
                        if (this.isGrantedAny('Pages.Group12.KiemKe.Create','Pages.Group12.KiemKe.Approve'))//la nguoi co quyen duyet
                            this.records.push(response[i]);
                    }
                    else this.records.push(response[i]);
            }
            this.primengTableHelper.totalRecordsCount = this.records.length;
            this.primengTableHelper.records = this.records;
            console.log(this.records);
            this.primengTableHelper.hideLoadingIndicator();
        })
    }

    viewDetail() {
        if (this.listSelectedRow.length == 0)
            this.message.info(this.l('Group12_ChooseAModel'), this.l('Group12_Notification'));
        else if (this.listSelectedRow.length > 1)
            this.message.warn(this.l('Group12_ChooseMaxModel'), this.l('Group12_Notification'));
        else this.navigate(['app/admin/kiemke/kiemke-view', this.listSelectedRow[0].kK_ID]);
    }

    onRowDblClick(event, kiemke: KIEMKE_DTO) {
        this.navigate(['app/admin/kiemke/kiemke-view', kiemke.kK_ID]);
    }

    getAllNhanVien() {
        this.nhanvienList.push();
        this.nhanvienList.push( "Hà Thị Anh");
        this.nhanvienList.push( "Lê Bá Vương");
        this.nhanvienList.push("Phạm Tuấn Anh");
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

    convertDate(date:any){
        return new Date(date).toLocaleDateString();
    }

    xemChiTiet(){
        if (this.listSelectedRow.length == 0)
            this.message.info(this.l('Group12_ChooseAModel'), this.l('Group12_Notification'));
        else if (this.listSelectedRow.length > 1)
            this.message.warn(this.l('Group12_ChooseMaxModel'), this.l('Group12_Notification'));
        else this.navigate(['/app/admin/kiemke-mobile', this.listSelectedRow[0].kK_CODE]);
    }
   
    saveAsExcelFile(buffer: any, fileName: string): void {
        import("file-saver").then(FileSaver => {
            let EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
            let EXCEL_EXTENSION = '.xlsx';
            const data: Blob = new Blob([buffer], {
                type: EXCEL_TYPE
            });
           saveAs(data, fileName + 'export' + new Date().getTime() + EXCEL_EXTENSION);
        });
    }
    exportExcel() {
        let kiemkeList:KIEMKE_DTO[]=[];
        for(var i=0;i<this.primengTableHelper.records.length;i++){
            if(this.primengTableHelper.records[i].autH_STATUS=="1")
                kiemkeList.push(this.primengTableHelper.records[i]);
        }
        import("xlsx").then(xlsx => {
            const worksheet = xlsx.utils.json_to_sheet(kiemkeList);
            const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
            const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
            this.saveAsExcelFile(excelBuffer, "primengTable");
        });
    }
}