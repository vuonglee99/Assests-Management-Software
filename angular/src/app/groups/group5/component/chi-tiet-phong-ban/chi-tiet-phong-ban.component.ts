import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";

// import { XeServiceProxy, CM_XE_DTO } from "@shared/service-proxies/service-proxies";



@Component({
    templateUrl: './chi-tiet-phong-ban.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./chi-tiet-phong-ban.component.css']
})
export class ChiTietPhongBanComponent extends AppComponentBase implements OnInit {

    /**
     *
     */
    constructor(injector: Injector,
        // private xeService: XeServiceProxy
    ) {
        super(injector);
        this.id = this.getRouteParam('id');
    }



    
   id="";

    ngOnInit(): void {
        // this.search();
    }
    //disabled các thẻ input
    disabledInput(){
        $(":disabled").prop('disabled',false);
    }

}   