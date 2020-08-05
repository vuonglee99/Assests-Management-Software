import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {DUT} from '../dut/dut.component';
import {DUTDetailComponent} from '../dut/dut-detail.component';
import {TTYCSC} from '@app/groups/group6/TTYCSC/TTYCSC.component';
import {TTYCSCDetailComponent} from '@app/groups/group6/TTYCSC/TTYCSC-detail.component';
import {DVT} from '@app/groups/group6/dvt/dvt.component';
import {DVT_DetailComponent} from '@app/groups/group6/dvt/dvt-detail.component';
import {GR6_Dashboard} from '@app/groups/group6/TTYCSC/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    {
                        path: 'donvitinh',
                        component: DVT,
                        // data: {
                        //     permission: null,
                        //     editPageState: "search",
                        // },
                    },
                ],
            },
            {
                path: '',
                children: [
                    {
                        path: 'donvitinh/:id',
                        component: DVT_DetailComponent,
                        data: {permission: null, editPageState: 'detail'},
                    },
                ],
            },
            {
                path: '',
                children: [
                    {
                        path: 'douutien',
                        component: DUT,
                    },
                ],
            },
            {
                path: '',
                children: [
                    {
                        path: 'douutien/:id',
                        component: DUTDetailComponent,
                        data: {permission: null, editPageState: 'detail'},
                    },
                ],
            },
            {
                path: '',
                children: [
                    {
                        path: 'douutien/:id/edit',
                        component: DUTDetailComponent,
                        data: {permission: null, editPageState: 'EDIT'},
                    },
                ],
            },

            {
                path: '',
                children: [
                    {
                        path: 'trangthaiyeucausuachua',
                        component: TTYCSC,
                    },
                ],
            },
            {
                path: '',
                children: [
                    {
                        path: 'trangthaiyeucausuachua/:id',
                        component: TTYCSCDetailComponent,
                        data: {permission: null, editPageState: 'detail'},
                    },
                ],
            },
            {
                path: '',
                children: [
                    {
                        path: 'trangthaiyeucausuachua/:id/edit',
                        component: TTYCSCDetailComponent,
                        data: {permission: null, editPageState: 'EDIT'},
                    },
                ],
            },
            {
                path: '',
                children: [
                    {
                        path: 'statistics/yeu-cau-sua-chua',
                        component: GR6_Dashboard,
                        data: {permission: null, editPageState: 'EDIT'},
                    },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class Group6RoutingModule {
}
