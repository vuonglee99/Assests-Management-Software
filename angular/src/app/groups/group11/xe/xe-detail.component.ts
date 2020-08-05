import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { XeServiceProxy, XE_DTO, NguoiThueXe_DTO, NTXServiceProxy } from "@shared/service-proxies/service-proxies";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";
import { ExcelExportService} from '../../group10/service/excelexport.service';
import * as moment from "moment";
@Component({
    templateUrl: './xe-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class XeDetailComponent extends AppComponentBase implements OnInit {
    /**
     *
     */
    constructor(injector: Injector, private formBuilder: FormBuilder, private xeService: XeServiceProxy, private ntxService: NTXServiceProxy, private exportService: ExcelExportService) {
        super(injector);
        this.show = false;
        this.inputModel.xE_CODE = this.getRouteParam('id');
        this.editPageState = this.getRouteData('editPageState');
    }


    ngOnInit() {
        this.inputModel.xE_TOTAL_KM = this.inputModel.xE_TOTAL_KM ? this.inputModel.xE_TOTAL_KM : 0;
        this.inputModel.xE_LAST_TOTAL_KM = this.inputModel.xE_LAST_TOTAL_KM ? this.inputModel.xE_LAST_TOTAL_KM : 0;
        this.xeForm = this.formBuilder.group({
            code: [{value: this.inputModel.xE_CODE}, [Validators.required, Validators.pattern(/^[a-zA-Z0-9^_]+$/)]],
            name: [this.inputModel.xE_NAME, [Validators.required, Validators.pattern(/^[a-zA-Z0-9\s]{3,50}$/)]],
            model: [this.inputModel.xE_MODEL, [Validators.required]],
            license_plate: [this.inputModel.xE_LICENSE_PLATE, [Validators.required]],
            color: [this.inputModel.xE_COLOR, [Validators.required]],
            seats: [this.inputModel.xE_SEATS, [Validators.required, Validators.pattern('^[0-9]+$')]],
            manufacturer: [this.inputModel.xE_MANUFACTURER, [Validators.required]],
            max_speed: [this.inputModel.xE_MAX_SPEED, [Validators.required, Validators.pattern('^[0-9]+$')]],
            consumption: [this.inputModel.xE_CONSUMPTION, [Validators.required, Validators.pattern('^[0-9]+$')]],
            manufacture_year: [this.inputModel.xE_MANUFACTURE_YEAR, [Validators.required, Validators.pattern('^[0-9]+$')]],
            price: [this.inputModel.xE_PRICE, [Validators.required, Validators.pattern('^[0-9]+$')]],
            status: [this.inputModel.xE_STATUS, [Validators.required]],
            notes: [this.inputModel.xE_NOTES, []],
            ntx_name: [this.currentNTX.ntX_NAME,[]],
            ntx_code: [this.currentNTX.ntX_CODE,[]],
            last_distance: [{value:  this.inputModel.xE_LAST_TOTAL_KM, disabled: true}, []],
            total_distance: [{value:  this.inputModel.xE_TOTAL_KM, disabled: this.editPageState === "add" ? true : false}, [Validators.required]],

        });

        if (this.editPageState === "view") {
            this.loadData();
        } else {
            this.show = true
            this.inputModel.xE_STATUS = 'Tốt';
        }
        // ntx
        this.loadNTX();
        this.cols = [
            { field: "ntX_ID", name: "STT", header: this.l('GROUP10_NUMBER_ORDER') },
            { field: "ntX_NAME", name: "ntX_NAME", header: this.l('Group10_NTX_NAME') },
            { field: "ntX_ADDRESS", name: "ntX_ADDRESS", header: this.l('Group10_NTX_ADDRESS') },
            { field: "ntX_CODE", name: "ntX_CODE", header: this.l('Group10_NTX_CODE') },
            { field: "ntX_GENDER", name: "ntX_GENDER", header: this.l('Group10_NTX_GENDER') },
            {
                field: "ntX_BIRTHDAY",
                name: "ntX_BIRTHDAY",
                header: this.l('Group10_NTX_BIRTHDAY'),
            },
            { field: "ntX_ID_CARD", name: "ntX_ID_CARD", header: this.l('Group10_NTX_ID_CARD') },
            { field: "ntX_LICENSE", name: "ntX_LICENSE", header:  this.l('Group10_NTX_LICENSE') },
        ];
    }
    show: boolean = false;
    title: string = "Chi tiết Xe";
    inputModel: XE_DTO = new XE_DTO();
    editPageState: string;
    xeForm: FormGroup;
    submitted = false;
    //ntx
    cols: any[];
    currentNTX: NguoiThueXe_DTO = new NguoiThueXe_DTO();

    loadData() {
        this.xeService.xE_ByCode(this.inputModel.xE_CODE).subscribe(response => {
            this.inputModel = response[0];
            this.show = true;
        })
    }
    save() {
        this.submitted = true;
        if (this.xeForm.invalid) {
            this.message.error('Bạn vui lòng nhập lại đầy đủ các thông tin!');
            return;
        }
        this.show = false;

        this.inputModel.xE_CODE = this.xeForm.getRawValue().code;
        this.inputModel.xE_NAME = this.xeForm.value.name;
        this.inputModel.xE_PRICE = this.xeForm.value.price;
        this.inputModel.xE_MAX_SPEED = this.xeForm.value.max_speed;
        this.inputModel.xE_LICENSE_PLATE = this.xeForm.value.license_plate;
        this.inputModel.xE_MANUFACTURER = this.xeForm.value.manufacturer;
        this.inputModel.xE_MANUFACTURE_YEAR = this.xeForm.value.manufacture_year;
        this.inputModel.xE_CONSUMPTION = this.xeForm.value.consumption;
        this.inputModel.xE_SEATS = this.xeForm.value.seats;
        this.inputModel.xE_COLOR = this.xeForm.value.color;
        this.inputModel.xE_MODEL = this.xeForm.value.model;
        this.inputModel.xE_STATUS = this.xeForm.value.status;
        this.inputModel.xE_LAST_TOTAL_KM = this.xeForm.getRawValue().last_distance;
        this.inputModel.xE_TOTAL_KM = this.xeForm.getRawValue().total_distance;

        if (this.editPageState == 'add') {
            console.log(this.inputModel);
            this.xeService.xE_Insert(this.inputModel).subscribe(response => {
                if (response.length === 0) {
                    this.message.success('Thêm mới thành công');
                    this.navigate(['/app/admin/xe-group11']);
                }
                else if (response['RESULT'] === '-1') {
                    this.message.error(response['ERROR_DESC']);
                }
                else {
                    this.message.error('Mã xe đã tồn tại');
                }
                this.show = true;
            })
        } else if (this.editPageState == 'view') {
            this.xeService.xE_Update(this.inputModel).subscribe(response => {
                if (response.length === 0) {
                    this.message.success('Đã lưu thành công');
                }
                else if (response['RESULT'] === '-1') {
                    alert("failed");
                    this.message.error(response['ERROR_DESC']);
                }
                else {
                    alert("failure");
                }
                this.xeService.xE_ByCode(this.inputModel.xE_CODE).subscribe(response => {
                    this.inputModel = response[0];
                    this.show = true;
                })
            })

            this.show = true;
        }

    }

    delete() {
        this.message.confirm(
            this.l('CarDeleteWarningMessage', this.inputModel.xE_CODE),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.show = false;
                    this.xeService.xE_Delete(
                        this.inputModel.xE_ID
                    ).subscribe((response) => {
                        if (response.length === 0) {
                            this.message.success("Xóa thành công.");
                            window.history.back();
                        } else {
                            this.message.error(response["ERROR_DESC"]);
                        }
                    });
                }
            }
        );
    }

    numberOnly(event): boolean {
        const charCode = (event.which) ? event.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;

    }
    get f() { return this.xeForm.controls; }
    loadNTX() {

        this.xeService.xE_ByCode(this.inputModel.xE_CODE).subscribe(response => {

            this.ntxService.nguoiThueXe_ByXeId_HienTai(response[0].xE_ID).subscribe(result =>{
                this.currentNTX.ntX_NAME = result.ntX_NAME?result.ntX_NAME:'Trống';
                this.currentNTX.ntX_CODE = result.ntX_CODE?result.ntX_CODE: 'Trống';

            })
            this.ntxService
            .nguoiThueXe_ByXeID(response[0].xE_ID)
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });


        })


       // console.log(this.records);
    }
    //gr10
    exportList: any[];
    export()
    {
        //    const header = ['stt', 'mantx', 'hoten', 'gioitinh', 'diachi', 'ngaysinh', 'cmnd', 'gplx'];
        // this.inputModel.ntX_ADDRESS = null;
        // this.inputModel.ntX_BIRTHDAY = null;
        // this.inputModel.ntX_CODE = null;
        // this.inputModel.ntX_GENDER = null;
        // this.inputModel.ntX_ID = null;
        // this.inputModel.ntX_ID_CARD = null;
        // this.inputModel.ntX_LICENSE = null;
        // this.inputModel.ntX_NAME = null;
        let i = 1;
        this.exportList = []
        this.primengTableHelper.records.forEach(element => {
            var a = {
                stt: i.toString(),
                mantx: element.ntX_CODE,
                hoten: element.ntX_NAME,
                gioitinh: element.ntX_GENDER = "1"? "Nam":"Nữ",
                diachi: element.ntX_ADDRESS,
                ngaysinh: moment(element.ntX_BIRTHDAY).format("DD/MM/YYYY"),
               cmnd: element.ntX_ID_CARD,
               gplx: element.ntX_LICENSE

            }
            this.exportList.push(a);
        });
        console.log("exportlist here: ");
        console.log(this.primengTableHelper.records);

        if (this.exportList.length == 0) {
            this.message.error(this.l('Group10_EXCEL_FAIL'));
        }
        else {
            this.message.confirm(
                this.l('Group10_EXCEL_REQUEST'),
                this.l('Group10_EXCEL_COMPLETE'),

                (isConfirmed) => {
                    if (isConfirmed) {
                        this.exportService.generateExcelLichSuDaTHue(this.exportList,
                            this.inputModel.xE_CODE,
                            this.inputModel.xE_NAME, this.inputModel.xE_COLOR, this.inputModel.xE_LICENSE_PLATE);

                    }
                }
            );


        }

    }
}
