// import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
// import { appModuleAnimation } from "@shared/animations/routerTransition";
// import { AppComponentBase } from "@shared/common/app-component-base";
// import { CM_XE_DTO, XeServiceProxy } from "@shared/service-proxies/service-proxies";

// @Component({
//     templateUrl: './xe-detail.component.html',
//     encapsulation: ViewEncapsulation.None,
//     animations: [appModuleAnimation()]
// })
// export class XeDetailComponent extends AppComponentBase {
//     /**
//      *
//      */
//     constructor(injector: Injector, private xeService: XeServiceProxy) {
//         super(injector);
//         this.inputModel.xE_ID = this.getRouteParam('id');
//         this.editPageState = this.getRouteData('editPageState');
//     }

//     inputModel: CM_XE_DTO = new CM_XE_DTO();

//     editPageState: string;

//     save() {
//         if (this.editPageState == 'add') {
//             this.xeService.cM_XE_Insert(this.inputModel).subscribe(response => {
//                 if (response['RESULT'] == '0') {
//                     this.message.success('Thêm mới thành công');
//                 }
//                 else if(response['RESULT'] == '-1'){
//                     this.message.error(response['ERROR_DESC']);
//                 }
//             })
//         }
//     }
// }