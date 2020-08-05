import { ViewEncapsulation, Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { Table } from 'primeng/components/table/table';
import { AppComponentBase } from "@shared/common/app-component-base";
import { BranchServiceProxy, Group03BranchDTO } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';

@Component({
    templateUrl: './branchesList.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../styles.css']
})
export class BranchesListComponent extends AppComponentBase implements OnInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    //Filters
    filterText = '';
    filterCodeText = '';
    filterNameText = '';
    filterStatusText = '';
    advancedFiltersAreShown = false;
    selectedBranch = undefined;

    constructor(injector: Injector, private service: BranchServiceProxy) {
        super(injector);
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    ngOnInit(): void {
    }

    getBranches(event?: LazyLoadEvent): void {
        console.log(this.filterStatusText);
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this.service.branchSearch(
            this.filterText,
            this.filterCodeText,
            this.filterNameText,
            this.filterStatusText,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event) + 1
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });

    }

    deleteBranch(branch: Group03BranchDTO): void {
        this.message.confirm(
            `Branch ${branch.brancH_NAME} will be deleted`,
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.service.branchDelete(branch.brancH_ID)
                        .subscribe((result) => {
                            this.reloadPage();
                            if (undefined === result.ERROR_DESC) {
                                this.notify.success(this.l('SuccessfullyDeleted'));
                            } else {
                                this.notify.error(result.ERROR_DESC);
                            }
                        });
                }
            }
        );
    }

    addBranch() {
        this.navigate(["/app/admin/branch-add"])
    }

    editBranch(branch: Group03BranchDTO): void {
        this.navigate([`/app/admin/branch-edit/${branch.brancH_ID}`])
    }

    viewBranch(branch: Group03BranchDTO): void {
        this.navigate([`/app/admin/branch-view/${branch.brancH_ID}`])
    }

    itemClick(branch: Group03BranchDTO): void {
        if (this.selectedBranch && this.selectedBranch.brancH_ID === branch.brancH_ID)
            this.selectedBranch = undefined;
        else
            this.selectedBranch = branch;
    }
}