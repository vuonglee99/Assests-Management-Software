// import { ViewEncapsulation, Component, Injector, OnInit } from "@angular/core";
// import { appModuleAnimation } from "@shared/animations/routerTransition";
// import { AppComponentBase } from "@shared/common/app-component-base";
// import { XeServiceProxy, CM_XE_DTO } from "@shared/service-proxies/service-proxies";


// @Component({
//     templateUrl: './xe-list.component.html',
//     encapsulation: ViewEncapsulation.None,
//     animations: [appModuleAnimation()]
// })
// export class XeListComponent extends AppComponentBase implements OnInit {

//     /**
//      *
//      */
//     constructor(injector: Injector,
//         private xeService: XeServiceProxy) {
//         super(injector);
//     }

//     records: CM_XE_DTO[] = [];

//     filterInput: CM_XE_DTO = new CM_XE_DTO();

//     ngOnInit(): void {
//         this.search();
//     }

//     addNew() {
//         this.navigate(['/app/admin/xe-add']);
//     }

//     update() {

//     }

//     delete() {

//     }

//     search() {
//         this.xeService.cM_XE_Search(this.filterInput).subscribe(response => {
//             this.records = response;
//         })
//     }

//     viewDetail() {

//     }
// }