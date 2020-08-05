import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuClientComponent } from '@app/gwebsite/menu-client/menu-client.component';
import { DemoModelsComponent } from './demoModels/demoModels.component';
import { StaffListComponent } from '../groups/group8/staff/staff-list.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: "",
                children: [
                    {
                        path: "menu-client",
                        component: MenuClientComponent,
                        data: { permission: "Pages.Administration.MenuClient" },
                    },
                    {
                        path: 'demo-model', component: DemoModelsComponent,
                        data: { permission: 'Pages.Administration.DemoModel' }
                    }
                ]
            }
        ])
    ],
    exports: [RouterModule],
})
export class GWebsiteRoutingModule {}
