import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { ModelXeServiceProxy, MODELXE_DTO } from "@shared/service-proxies/service-proxies";
import { SortEvent } from 'primeng/api';
import { saveAs } from 'file-saver';

@Component({
    templateUrl: './model-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ModelListComponent extends AppComponentBase implements OnInit {

    /**
     *
     */
    constructor(injector: Injector,
        private modelService: ModelXeServiceProxy) {
        super(injector);
    }

    records: MODELXE_DTO[] = [];
    listSelectedRow: MODELXE_DTO[] = [];
    filterInput: MODELXE_DTO = new MODELXE_DTO();
    cols: any[];
    clonedModels: { [s: string]: MODELXE_DTO; } = {};
    editing:boolean=false;
    loading=null;
    ngOnInit(): void {
        this.filterInput.recorD_STATUS = "";
        this.search();
        this.filterInput.modeL_TYPE = "";
        this.filterInput.modeL_HSX = "";
    }

    addNew() {
        this.navigate(['/app/admin/model-add']);
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
                            this.listSelectedRow[i].autH_STATUS='0';
                            this.listSelectedRow[i].recorD_STATUS='0';
                            this.modelService.modelXe_Update(this.listSelectedRow[i]).subscribe(res => {
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
        this.records=[];
        this.primengTableHelper.records=[];
        this.modelService.modelXe_GetAll(this.filterInput).subscribe(response => {
            for(var i=0;i<response.length;i++){
                if(!(response[i].recorD_STATUS ==='0' && response[i].autH_STATUS ==='1'))//chua xoa
                    if(response[i].autH_STATUS =='0')//chua duoc duyet
                    {
                        if(this.isGranted('Pages.Group12.Model.Delete'))//la nguoi co quyen duyet
                        this.records.push(response[i]);
                    }
                    else this.records.push(response[i]);

            }
            this.primengTableHelper.totalRecordsCount = this.records.length;
            this.primengTableHelper.records = this.records;
            this.primengTableHelper.hideLoadingIndicator();
        })
    }

    viewDetail() {
        if (this.listSelectedRow.length == 0)
            this.message.info(this.l('Group12_ChooseAModel'), this.l('Group12_Notification'));
        else if (this.listSelectedRow.length > 1)
            this.message.warn(this.l('Group12_ChooseMaxModel'), this.l('Group12_Notification'));
        else this.navigate(['app/admin/model-view', this.listSelectedRow[0].modeL_ID]);
    }

    // onRowSelect(event){
    //     console.log(this.listSelectedRow);
    // }

    // onRowUnselect(event){
    //     console.log(this.listSelectedRow);
    // }

    onRowDblClick(event, model: MODELXE_DTO) {
        this.navigate(['app/admin/model-view', model.modeL_ID]);
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
        let modelList:MODELXE_DTO[]=[];
        for(var i=0;i<this.primengTableHelper.records.length;i++){
            if(this.primengTableHelper.records[i].autH_STATUS=="1")
                modelList.push(this.primengTableHelper.records[i]);
        }
        import("xlsx").then(xlsx => {
            const worksheet = xlsx.utils.json_to_sheet(modelList);
            const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
            const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
            this.saveAsExcelFile(excelBuffer, "primengTable");
        });
    }
}