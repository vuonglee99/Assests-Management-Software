import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
// import { BranchListComponent } from './branch/branch-list.component';
import { BranchDetailComponent } from './branch/branch-detail.component';
import { ApartmentTypeListComponent } from './apartment-type/apartment-type-list';
import { ApartmentListComponent } from './apartment/apartment-list.component';
import { ApartmentDetailComponent } from './apartment/apartment-detail.component';
import { ApartmentTypeDetailComponent } from './apartment-type/apartment-type-detail';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    // { path: 'branch', component: BranchListComponent, data: { permission: 'Pages.Group4.Branch' } },
                    { path: 'branch-add', component: BranchDetailComponent, data: { permission: 'Pages.Group4.Branch.Add', editPageState: "add" } },
                    { path: 'branch-edit/:id', component: BranchDetailComponent, data: { permission: 'Pages.Group4.Branch.Update', editPageState: "edit" } },
                    { path: 'branch-view/:id', component: BranchDetailComponent, data: { permission: 'Pages.Group4.Branch.View', editPageState: "view" } },

                    { path: 'apartment-type', component: ApartmentTypeListComponent, data: { permission: 'Pages.Group4.ApartmentType' } },
                    { path: 'apartment-type-add', component: ApartmentTypeDetailComponent, data: { permission: 'Pages.Group4.ApartmentType.Add', editPageState: "add" } },
                    { path: 'apartment-type-edit/:id', component: ApartmentTypeDetailComponent, data: { permission: 'Pages.Group4.ApartmentType.Update', editPageState: "edit" } },
                    { path: 'apartment-type-view/:id', component: ApartmentTypeDetailComponent, data: { permission: 'Pages.Group4.ApartmentType.View', editPageState: "view" } },

                    { path: 'buildings/:buildingID/:floorID/apartment', component: ApartmentListComponent, data: { permission: 'Pages.Group4.Apartment' } },
                    { path: 'buildings/:buildingID/:floorID/apartment-add', component: ApartmentDetailComponent, data: { permission: 'Pages.Group4.Apartment.Add', editPageState: "add" } },
                    { path: 'buildings/:buildingID/:floorID/apartment-edit/:id', component: ApartmentDetailComponent, data: { permission: 'Pages.Group4.Apartment.Update', editPageState: "edit" } },
                    { path: 'buildings/:buildingID/:floorID/apartment-view/:id', component: ApartmentDetailComponent, data: { permission: 'Pages.Group4.Apartment.View', editPageState: "view" } },
                    
                    { path: 'apartment', component: ApartmentListComponent, data: { permission: 'Pages.Group4.Apartment' } },
                    { path: 'apartment-add', component: ApartmentDetailComponent, data: { permission: 'Pages.Group4.Apartment.Add', editPageState: "add" } },
                    { path: 'apartment-edit/:id', component: ApartmentDetailComponent, data: { permission: 'Pages.Group4.Apartment.Update', editPageState: "edit" } },
                    { path: 'apartment-view/:id', component: ApartmentDetailComponent, data: { permission: 'Pages.Group4.Apartment.View', editPageState: "view" } },

                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group4RoutingModule { }
