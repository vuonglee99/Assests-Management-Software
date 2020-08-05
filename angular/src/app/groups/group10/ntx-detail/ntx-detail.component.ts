import {
    ViewEncapsulation,
    Component,
    Injector,
    TemplateRef,
} from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {
    NSXServiceProxy,
    NTXServiceProxy,
    CM_NSX_DTO,
    NguoiThueXe_DTO,
    XE_DTO,
    XeServiceProxy
} from "@shared/service-proxies/service-proxies";
import { ModalModule } from "ngx-bootstrap/modal";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import {
    FormBuilder,
    FormGroup,
    Validators,
    ValidationErrors,
} from "@angular/forms";
import * as moment from "moment";
import { ExcelExportService } from "../service/excelexport.service";

@Component({
    templateUrl: "./ntx-detail.component.html",
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ["./ntx-detail.component.css"],
})
export class NtxDetailComponent extends AppComponentBase {
    /**
     *
     */
    flagBirthday: boolean= false;
    isUpdate: boolean = false;
    title: string;
    modalRef: BsModalRef;
    registerForm: FormGroup;
    submitted = false;
    currentId: string;

    selectedGender: string = "1";
    selectedGenderChange(event: any) {
        this.selectedGender = event.target.value;
    }
    records: NguoiThueXe_DTO[] = [];

    inputModel: NguoiThueXe_DTO = new NguoiThueXe_DTO();

    selectedNtx: NguoiThueXe_DTO = new NguoiThueXe_DTO();

    editPageState: string;

    constructor(
        injector: Injector,

        private formBuilder: FormBuilder,
        private ntxService: NTXServiceProxy,
        private modalService: BsModalService,
        private xeService: XeServiceProxy,
        private exportService: ExcelExportService
    ) {
        super(injector);
        this.title = "Chi tiết người thuê xe ";
        this.currentId = this.getRouteParam("id");
        this.editPageState = this.getRouteData("editPageState");
        if (this.editPageState == "view") {
            this.initData();
            this.search();
        } else if (this.editPageState == "edit" ) {
            this.initData();
            this.search();
            this.update();
        }
    }
    initData() {
        this.ntxService
            .nguoiThueXe_ById(this.currentId)
            .subscribe((response) => {
                this.inputModel = response;
                if (this.inputModel.ntX_NAME == null) {
                    this.message.error(
                       this.l('Group10_ID_NOT_FOUND')
                    );
                    this.navigate(["/app/admin/ntx-list"]);
                }
                this.ngOnInit();
            });
    }
    ngOnInit(): void {
        // this.show();

        this.registerForm = this.formBuilder.group({
            name: [
                this.inputModel.ntX_NAME,
                [Validators.required, Validators.maxLength(50)],
            ],
            address: [
                this.inputModel.ntX_ADDRESS,
                [Validators.required, Validators.maxLength(200)],
            ],
            code: [
                this.inputModel.ntX_CODE,
                [Validators.required, Validators.maxLength(20)],
            ],

            birthday: [
                moment(this.inputModel.ntX_BIRTHDAY).format("YYYY-MM-DD"),
            ],

            card: [
                this.inputModel.ntX_ID_CARD,
                [
                    Validators.required,
                    Validators.maxLength(9),
                    Validators.minLength(9),
                    Validators.pattern("^[0-9]*$"),
                ],
            ],
            license: [
                this.inputModel.ntX_LICENSE,
                [
                    Validators.required,
                    Validators.maxLength(20),
                    Validators.pattern("^[0-9]*$"),
                ],
            ],
        });
        if (this.editPageState == "add") {
            this.disabledInput();
            this.title = "Thêm mới người thuê xe";
        } else if (this.editPageState == "edit") this.disabledInput();
    }
    get f() {
        return this.registerForm.controls;
    }

    unDisabledInput() {
        $(":disabled").prop("disabled", false);
    }
    disabledInput() {
        $(":disabled").prop("disabled", false);
    }

    onSubmit() {
        Object.keys(this.registerForm.controls).forEach((key) => {
            const controlErrors: ValidationErrors = this.registerForm.get(key)
                .errors;
            if (controlErrors != null) {
                this.message.error(
                    this.l('Group10_ERROR_INPUT')
                );
            }
        });
        this.submitted = true;
       if(moment(
        this.registerForm.value.birthday
    ).add('days',1).isAfter((new Date()))){
       this.flagBirthday = true;
       return;
    }else
    this.flagBirthday = false;
        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }

        this.inputModel.ntX_NAME = this.registerForm.value.name;
        this.inputModel.ntX_CODE = this.registerForm.value.code;
        this.inputModel.ntX_GENDER = this.selectedGender;
        this.inputModel.ntX_LICENSE = this.registerForm.value.license;
        this.inputModel.ntX_ID_CARD = this.registerForm.value.card;
        this.inputModel.ntX_BIRTHDAY = moment(
            this.registerForm.value.birthday
        ).add(1, "days");

        // alert(this.registerForm.value.birthday);
        // alert(moment(this.registerForm.value.birthday));
        this.inputModel.ntX_ADDRESS = this.registerForm.value.address;

        if (this.isUpdate) {
            this.ntxService
                .nguoiThueXe_Update(this.inputModel)
                .subscribe((response) => {
                    if (response["RESULT"] == "0") {
                        this.message.success(  this.l('Group10_UPDATE_COMPLETE'));
                        //this.ngOnInit();
                    } else if (response["RESULT"] == "-1") {
                        this.message.error(response["ERROR_DESC"]);
                    }
                });
        } else if (this.editPageState == "add") {
            this.inputModel.ntX_ID = null;
            this.ntxService
                .nguoiThueXe_Insert(this.inputModel)
                .subscribe((response) => {
                    if (response["RESULT"] == "0") {
                        this.message.success(  this.l('Group10_ADD_COMPLETE'));

                        //this.ngOnInit();
                    } else if (response["RESULT"] == "-1") {
                        this.message.error(response["ERROR_DESC"]);
                    }
                });
        }
        this.back();
    }

    update() {
        this.disabledInput();
        this.title = "Cập nhật người thuê xe";
        this.isUpdate = true;

    }

    delete() {
        this.message.confirm(
            this.l('AreYouSure'),
            this.l('Group10_DELETE_WARNING'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.ntxService
                        .nguoiThueXe_Delete(this.inputModel.ntX_ID)
                        .subscribe((response) => {
                            if (response["RESULT"] == "0") {
                                this.message.success(this.l('Group10_DELETE_COMPLETE'));
                                this.navigate(["/app/admin/ntx-list"]);
                            } else {
                                this.message.error(response["ERROR_DESC"]);
                            }
                        });
                }
            }
        );
    }
    back() {
        this.isUpdate = false;
        this.navigate(["/app/admin/ntx-list"]);
    }
    insert() {
        this.disabledInput();
        this.title = "Thêm mới nhà sản xuất";
        this.isUpdate = false;

        this.inputModel.ntX_ADDRESS = null;
        this.inputModel.ntX_BIRTHDAY = null;
        this.inputModel.ntX_CODE = null;
        this.inputModel.ntX_GENDER = null;
        this.inputModel.ntX_ID = null;
        this.inputModel.ntX_ID_CARD = null;
        this.inputModel.ntX_LICENSE = null;
        this.inputModel.ntX_NAME = null;

        this.ngOnInit();
    }
    search() {
        this.xeService.xE_ByNtxID(this.currentId).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        })
    }
    exportList: any[];
    export() {

        let i = 1;
        this.exportList = []
        this.primengTableHelper.records.forEach(element => {
            var a = {
                stt: i.toString(),
                maxe: element.xE_CODE,
                tenxe: element.xE_NAME,
                mauxe: element.xE_COLOR,
                hangsx: element.xE_MANUFACTURER,
                bienso: element.xE_LICENSE_PLATE

            }
            this.exportList.push(a);
            i=i+1;
        });


        if (this.exportList.length == 0) {
            this.message.error( this.l('Group10_EXCEL_FAIL'));
        }
        else {
            this.message.confirm(
                this.l('Group10_EXCEL_REQUEST'),
                this.l('Group10_EXCEL_COMPLETE'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this.exportService.generateExcelNtxDetail(this.exportList,
                            this.inputModel.ntX_ID,
                            this.inputModel.ntX_NAME, this.inputModel.ntX_ID_CARD, this.inputModel.ntX_LICENSE);

                    }
                }
            );


        }

    }
}
