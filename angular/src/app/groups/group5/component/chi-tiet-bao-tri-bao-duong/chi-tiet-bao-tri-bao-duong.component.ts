import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
@Component({
  templateUrl: './chi-tiet-bao-tri-bao-duong.component.html',
  styleUrls: ['./chi-tiet-bao-tri-bao-duong.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()],
})
export class ChiTietBaoTriBaoDuongComponent extends AppComponentBase implements OnInit{

  constructor(injector: Injector) {
    super(injector);
  }

 ngOnInit(): void {

  }


  date3 : Date;
  date7 : Date;
  disabledInput(){
    $(":disabled").prop('disabled',false);
}
}
