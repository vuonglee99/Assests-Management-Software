import { NgModule } from '@angular/core';
import { BranchesListComponent } from "./branches/branchesList.component";
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { BuildingsListView } from './buidling-views/buildings-list.view';
import { BuildingDetailView } from './buidling-views/building-detail.view';
import { FloorDetailView } from './floor-views/floor-detail.view';
import { ApartmentDetailView } from './apartment-views/apartment-detail.view';
import { ResidentsListView } from './resident-views/residents-list.view';
import { ResidentDetailView } from './resident-views/resident-detail.view';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    // Branches
                    { path: 'branches', component: BranchesListComponent, data: { permission: 'Pages.Group3.Branches' } },
                    // Buildings
                    { path: 'buildings', component: BuildingsListView, data: { permission: "Pages.Group03.Buildings" } },
                    { path: 'building-create', component: BuildingDetailView, data: { permission: "Pages.Group03.Buildings.Create", pageState: 1 } },
                    { path: 'building-detail/:buildingId', component: BuildingDetailView, data: { permission: "Pages.Group03.Buildings", pageState: 0 } },
                    { path: 'building-modify/:buildingId', component: BuildingDetailView, data: { permission: "Pages.Group03.Buildings.Update", pageState: 2 } },
                    // Floors
                    { path: 'buildings/:buildingId/floor-create', component: FloorDetailView, data: { permission: "Pages.Group03.Floors.Create", pageState: 1 } },
                    { path: 'buildings/:buildingId/floor-detail/:floorId', component: FloorDetailView, data: { permission: "Pages.Group03.Floors", pageState: 0 } },
                    { path: 'buildings/:buildingId/floor-modify/:floorId', component: FloorDetailView, data: { permission: "Pages.Group03.Floors.Update", pageState: 2 } },
                    // Apartments
                    { path: 'buildings/:buildingId/:floorId/apartment-create', component: ApartmentDetailView, data: { permission: "Pages.Group03.Apartments.Create", pageState: 1 } },
                    { path: 'buildings/:buildingId/:floorId/apartment-detail/:apartmentId', component: ApartmentDetailView, data: { permission: "Pages.Group03.Apartments", pageState: 0 } },
                    { path: 'buildings/:buildingId/:floorId/apartment-modify/:apartmentId', component: ApartmentDetailView, data: { permission: "Pages.Group03.Apartments.Update", pageState: 2 } },
                    // Residents
                    { path: 'residents', component: ResidentsListView, data: { permission: "Pages.Group03.Residents" } },
                    { path: 'resident-create', component: ResidentDetailView, data: { permission: "Pages.Group03.Residents.Create", pageState: 1 } },
                    { path: 'resident-detail/:residentId', component: ResidentDetailView, data: { permission: "Pages.Group03.Residents", pageState: 0 } },
                    { path: 'resident-modify/:residentId', component: ResidentDetailView, data: { permission: "Pages.Group03.Residents.Update", pageState: 2 } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group3RoutingModule {
    constructor(
        private router: Router
    ) {
        router.events.subscribe((event) => {
            this.hideOpenDataTableDropdownMenus();

            if (event instanceof NavigationEnd) {
                window.scroll(0, 0);
            }
        });
    }

    hideOpenDataTableDropdownMenus(): void {
        let $dropdownMenus = $('.dropdown-menu.tether-element');
        $dropdownMenus.css({
            'display': 'none'
        });
    }
}
