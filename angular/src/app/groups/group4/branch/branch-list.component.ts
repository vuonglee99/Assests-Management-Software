// import { Component, Injector, ViewEncapsulation } from "@angular/core";
// import { AppComponentBase } from "@shared/common/app-component-base";
// import { appModuleAnimation } from "@shared/animations/routerTransition";
// import { CM_BRANCH_DTO, BranchServiceProxy } from "@shared/service-proxies/service-proxies";

// @Component({
//     templateUrl: './branch-list.component.html',
//     animations: [appModuleAnimation()],
//     encapsulation: ViewEncapsulation.None,
//     styleUrls: ['./branch-detail.component.css']
// })

// export class BranchListComponent extends AppComponentBase {
//     /**
// *
// */
//     constructor(injector: Injector,
//         private branchService: BranchServiceProxy) {
//         super(injector);
//     }

//     records: CM_BRANCH_DTO[] = [];
//     filterInput: CM_BRANCH_DTO = new CM_BRANCH_DTO();
//     selectedBranch: CM_BRANCH_DTO = new CM_BRANCH_DTO();
//     ngOnInit(): void {
//         this.search();
//     }

//     addNew() {
//         this.navigate(['/app/admin/branch-add']);
//     }
    
//     //---------------TESTING
//     // update() {
//     //     if (this.selectedBranch.brancH_ID != null) {
//     //         this.navigate(['/app/admin/branch-edit', this.selectedBranch.brancH_ID]);
//     //     } else {
//     //         this.message.warn('Vui lòng chọn một đơn vị');
//     //     }
//     // }
//     // delete() {
//     //     if (this.selectedBranch.brancH_ID != null) {
//     //         this.message.confirm('Xác nhận xóa', 'Bạn có chắc muốn xóa dữ liệu này ?', (isConfirmed) => {
//     //             if (isConfirmed) {
//     //                 this.branchService.cM_BRANCH_Delete(this.selectedBranch.brancH_ID).toPromise().then(() => this.ngOnInit());
//     //                 this.message.success('Xóa thành công');
//     //                 this.selectedBranch = new CM_BRANCH_DTO();
//     //             }
//     //         });
//     //     } else {
//     //         this.message.warn('Vui lòng chọn đơn vị cần xóa');
//     //     }
//     // }
//     //-----------------TESTING

//     search() {
//         // this.branchService.branchSearch(this.filterInput).subscribe(response => {
//         //     this.records = response;
//         // })
//     }


//     viewDetail() {
//         if (this.selectedBranch.brancH_ID != null) {
//             this.navigate(['/app/admin/branch-view', this.selectedBranch.brancH_ID]);
//         } else {
//             this.message.warn('Vui lòng chọn một đơn vị');
//         }
//     }

//     selectedRow(branch: CM_BRANCH_DTO) {
//         this.selectedBranch = branch;
//     }
// }