import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {
    NSXServiceProxy,
    CM_NSX_DTO,
} from "@shared/service-proxies/service-proxies";

@Component({
    templateUrl: "./nsx-delete.component.html",
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class NsxDeleteComponent extends AppComponentBase {
    /**
     *
     */

    constructor(injector: Injector, private NsxService: NSXServiceProxy) {
        super(injector);
        // this.inputModel.nsX_ID = this.getRouteParam("id");
        this.editPageState = this.getRouteData("editPageState");
    }
    initData() {
        if (this.editPageState != "delete") {
        }
    }
    editPageState: string;
    inputModel: CM_NSX_DTO = new CM_NSX_DTO();

    back() {
        this.navigate(["/app/group10/nha-san-xuat"]);
    }
    delete() {
        this.message.confirm(
            "Xác nhận xóa",
            "Bạn có chắc chắn muốn xóa dữ liệu này không?",
            (isConfirmed) => {
                if (isConfirmed) {
                    this.NsxService.cM_NSX_Delete(
                        this.inputModel.nsX_ID
                    ).subscribe((response) => {
                        if (response["RESULT"] == "0") {
                            this.message.success("Xóa thành công.");
                        } else {
                            this.message.error(response["ERROR_DESC"]);
                        }
                    });
                }
            }
        );
    }
}
