import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { XeListComponent } from './xe/xe-list.component';
import { XeDetailComponent } from './xe/xe-detail.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'xe', component: XeListComponent, data: { permission: 'Pages.Group13.Xe' } },
                    { path: 'xe-add', component: XeDetailComponent, data: { permission: 'Pages.Group13.Xe.Add', editPageState: "add" } },
                    { path: 'xe-edit', component: XeDetailComponent, data: { permission: 'Pages.Group13.Xe.Edit', editPageState: "edit" } },
                    { path: 'xe-view', component: XeDetailComponent, data: { permission: 'Pages.Group13.Xe.View', editPageState: "view" } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class Group13RoutingModule { }
