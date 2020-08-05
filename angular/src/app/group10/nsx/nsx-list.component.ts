import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import {
    CM_NSX_DTO,
    NSXServiceProxy,
} from "@shared/service-proxies/service-proxies";

@Component({
    templateUrl: "./nsx-list.component.html",
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class NsxListComponent extends AppComponentBase implements OnInit {
    /**
     *
     */
    constructor(injector: Injector, private NsxService: NSXServiceProxy) {
        super(injector);
    }
    records: CM_NSX_DTO[] = [];

    filterInput: CM_NSX_DTO = new CM_NSX_DTO();

    ngOnInit(): void {
        throw new Error("Method not implemented.");
    }

    show() {
        // this.NsxService.cM_NSX_Show(this.filterInput).subscribe((response) => {
        //     this.records = response;
        // });
    }

    update() {
        this.navigate(["/app/group10/nsx-edit"]);
    }

    addNew() {
        this.navigate(["/app/group10/nsx-add"]);
    }
    delete() {
        this.navigate(["/app/group10/nsx-delete"]);
    }
    viewDetail() {
        this.navigate(["/app/group10/nsx-detail"]);
    }
    search() {

    }

}
