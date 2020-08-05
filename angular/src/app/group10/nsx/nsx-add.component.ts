import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {
    NSXServiceProxy,
    CM_NSX_DTO,
} from "@shared/service-proxies/service-proxies";

@Component({
    templateUrl: "./nsx-add.component.html",
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class NsxAddComponent extends AppComponentBase {
    /**
     *
     */

    constructor(injector: Injector, private NsxService: NSXServiceProxy) {
        super(injector);
        this.inputModel.nsX_ID = this.getRouteParam("id");
        this.editPageState = this.getRouteData("editPageState");
    }
    initData() {
        if (this.editPageState != "add") {
        }
    }
    editPageState: string;
    inputModel: CM_NSX_DTO = new CM_NSX_DTO();

    back() {
        this.navigate(["/app/group10/nha-san-xuat"]);
    }
    save() {
        if (
            this.inputModel.nsX_CODE == null ||
            this.inputModel.nsX_NAME == null ||
            this.inputModel.nsX_FROM == null
        ) {
            this.message.error("Vui lòng nhập đủ thông tin.");
        } else if (
            this.inputModel.nsX_CODE.trim() == "" ||
            this.inputModel.nsX_NAME.trim() == "" ||
            this.inputModel.nsX_FROM.trim() == ""
        ) {
            this.message.error("Vui lòng nhập thông tin hợp lệ.");
        } else {
            this.NsxService.cM_NSX_Insert(this.inputModel).subscribe(
                (response) => {
                    if (response["RESULT"] == "0") {
                        this.message.success("Thêm thành công.");
                        this.navigate(["/app/group10/nha-san-xuat"]);
                    } else {
                        this.message.error(response["ERROR_DESC"]);
                    }
                }
            );
        }
    }
}
