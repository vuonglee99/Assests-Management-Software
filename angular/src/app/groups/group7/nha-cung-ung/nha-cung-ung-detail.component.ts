import {ViewEncapsulation, Component, Injector, OnInit, ViewChild} from '@angular/core';
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {
    NhaCungUngServiceProxy,
    NHA_CUNG_UNG_DTO,
} from "@shared/service-proxies/service-proxies";
import { Response } from "@angular/http";
import { NgModel, FormGroup, FormControl, Validators } from "@angular/forms";

@Component({
    templateUrl: "./nha-cung-ung-detail.component.html",
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ["./nha-cung-ung-detail.component.css"],
})
export class NhaCungUngDetailComponent extends AppComponentBase {
    /**
     *
     */
    constructor(
        injector: Injector,
        private nhaCungUngService: NhaCungUngServiceProxy
    ) {
        super(injector);
        //Default value
        this.editPageState = this.getRouteData("editPageState");
    }

    @ViewChild('formRef1') formRef1;
    inputModel: NHA_CUNG_UNG_DTO = new NHA_CUNG_UNG_DTO();
    editPageState: string;
    hasData: boolean = false;
    submitted: boolean = false;
    newAdding: boolean = false;

    ngOnInit() {
        if (this.editPageState == "view" || this.editPageState == "edit") {
            this.nhaCungUngService
                .nhaCungUngById(this.getRouteParam("id"))
                .toPromise()
                .then((res) => {
                    if (
                        res.recorD_STATUS !== undefined &&
                        res.recorD_STATUS != "0"
                    ) {
                        this.inputModel = res;
                        this.hasData = true;
                    }
                });
        }
        if (this.editPageState == "add") {
            this.hasData = true;
        }
    }

    backToListPage() {
        this.navigate(["/app/admin/nhacungung"]);
    }

    onAdd() {
        this.navigate(["/app/admin/nha-cung-ung-add"]);
    }
    onEdit() {
        this.navigate([
            "/app/admin/nha-cung-ung-edit/" + this.inputModel.ncU_MA_NCU,
        ]);
    }
    onSave(formRef1) {
        if (this.formRef1.valid) {
            if (this.editPageState == "add") {
                try {
                    this.nhaCungUngService
                        .nhaCungUngInsert(this.inputModel)
                        .subscribe((response) => {
                            this.notify.success(this.l("Thêm mới thành công"));
                            this.inputModel = new NHA_CUNG_UNG_DTO();
                            this.submitted = false;
                            this.newAdding = true;
                            // console.log(response)
                            // if (response[0]["RESULT"] == "0") {
                            // } else if (response[0]["RESULT"] == "-1") {
                            //     this.message.error(response[0]["ERROR_DESC"]);
                            // }
                        });
                } catch {
                    this.notify.error(
                        this.l(
                            "Thêm mới không thành công. Có thể là trùng mã NCU"
                        )
                    );
                }
            } else if (this.editPageState == "edit") {
                try {
                    this.nhaCungUngService
                        .nhaCungUngUpdate(this.inputModel)
                        .subscribe(() => {
                            this.notify.success(this.l("Chỉnh sửa thành công"));
                            this.onBack();
                        });
                } catch {
                    this.notify.error(this.l("Chỉnh sửa không thành công"));
                }
            }
        } else {
            this.submitted = true;
            this.message.error("Vui lòng nhập thông tin đầy đủ và chính xác");
        }
    }
    onDelete() {
        // this.message.confirm('Xác nhận xóa', 'Bạn có chắc muốn xóa dữ liệu này ?', (isConfirmed) => {
        //     if (isConfirmed) {
        //         this.branchService.branchDelete(this.inputModel.brancH_ID).toPromise().then();
        //         this.message.success('Xóa thành công');
        //         this.back();
        //     }
        // });
        // Sửa thông báo cho đồng nhất, nếu không thích thì xóa
        this.message.confirm(
            `Bạn có chắc muốn xóa nhà cung ứng ${this.inputModel.ncU_TEN}?`,
            this.l("Are You Sure"),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.nhaCungUngService
                        .nhaCungUngDelete(this.inputModel.ncU_MA_NCU)
                        .subscribe(() => {
                            this.notify.success(
                                this.l(
                                    "Xóa thành công nhà cung ứng " +
                                        this.inputModel.ncU_TEN
                                )
                            );
                            this.backToListPage();
                        });
                }
            }
        );
    }
    onBack() {
        if (this.editPageState == "edit") {
            this.navigate([
                "/app/admin/nha-cung-ung-view/" + this.inputModel.ncU_MA_NCU,
            ]);
        } else {
            this.backToListPage();
            // window.history.back();
        }
    }
}
