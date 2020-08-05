import { ViewEncapsulation, Component, Injector, OnInit, ViewChild, ElementRef } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { ApartmentDTO, ApartmentServiceProxy, ApartmentTypeServiceProxy, ApartmentTypeDTO, BuildingsServiceProxy, FloorServiceProxy, ApartmentResidentServiceProxy, ResidentDTO, BuildingTableDTO, SessionServiceProxy, ApSearchDTO } from "@shared/service-proxies/service-proxies";
import { g4TenantComponent } from "./g4-tenant.component";
import { Table } from "primeng/table";
import { isUndefined, parseInt } from "lodash";
import { FormControl, NgForm } from "@angular/forms";
import { CompareApComponent } from "../g4-component/compare-ap";



@Component({
    templateUrl: './apartment-detail.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../group4-style.css'],

})
export class ApartmentDetailComponent extends AppComponentBase {

    @ViewChild('g4Tenant') g4Tenant: g4TenantComponent;
    @ViewChild('compare') compare: CompareApComponent;
    @ViewChild('formRef1') formAp: NgForm;
    @ViewChild(Table) tb: Table;

    inputModel = new ApartmentDTO();
    apOld = new ApartmentDTO();
    editPageState: string;
    apTypes = [];
    apBuildings = [];
    apFloors = [];
    selectedApType;
    selectedBuilding;
    selectedFloor;
    selectedTenant;
    listTenant = [];
    listFreeTenant = []
    userID;
    autH_STATUS_VALUE;
    apartmentNewID;
    apOldName;
    apNewName;

    //status

    constructor(injector: Injector,
        private apartmentService: ApartmentServiceProxy,
        private apartmentTypeService: ApartmentTypeServiceProxy,
        private buildingService: BuildingsServiceProxy,
        private floorService: FloorServiceProxy,
        private residentService: ApartmentResidentServiceProxy,
        private sessionService: SessionServiceProxy,

    ) {
        super(injector);
        this.sessionService.getCurrentLoginInformations().subscribe(res => { this.userID = res.user.id.toString() })
        this.apartmentService.apartment_GetAuthStatusName(this.getRouteParam('id')).subscribe(res => this.autH_STATUS_VALUE = res[0]['RESULT']);
        this.refeshPage();
    }
    ngOnInit() {
    }

    refeshPage() {
        this.inputModel.apartmenT_ID = this.getRouteParam('id');
        this.editPageState = this.getRouteData('editPageState');
        this.apartmentService.apartmentByID(this.getRouteParam('id')).subscribe(res => {
            this.inputModel = res;
            this.findApOld();
            this.refeshBuilding();
            this.refeshFloor();
            this.refeshType();
            this.refeshTenant();
            this.refeshFreeTenant();
            if (this.editPageState == 'add') {
                this.inputModel.apartmenT_STATUS = '0';
                this.inputModel.makeR_ID = this.userID;
            }
        })

    }

    //  combobox + table 
    refeshBuilding() {
        this.buildingService.pagingSearch('', '', '1', '', '', 0, 1000).subscribe(res => {
            this.apBuildings = res.items;

            this.apBuildings.forEach(e => {
                if (e.buildinG_ID == this.inputModel.buildinG_ID)
                    this.selectedBuilding = e;
            });
        })
    }
    refeshFloor() {
        this.floorService.getByBuildingId(this.inputModel.buildinG_ID).subscribe(res => {
            this.apFloors = res;

            this.apFloors.forEach(e => {
                if (e.floor_ID == this.inputModel.floor_ID)
                    this.selectedFloor = e;
            });
        })
    }
    refeshType() {
        this.apartmentTypeService.apartmentTypeGetAll().subscribe(res1 => {
            this.apTypes = res1;

            this.apTypes.forEach(e => {
                if (e.apartmenT_TYPE_ID == this.inputModel.apartmenT_TYPE_ID)
                    this.selectedApType = e;
            });
        })
    }

    refeshTenant() {
        this.residentService.apartmentResident_ResidentByApartment(this.getRouteParam('id')).subscribe(res => {
            this.listTenant = res;
        })
    }
    refeshFreeTenant() {
        this.residentService.apartmentResident_FreeResidentByApartment(this.getRouteParam('id')).subscribe(res => {
            this.listFreeTenant = res;
        })
    }

    findApOld() {
        this.apartmentService.apartmentByID(this.inputModel.apartmenT_OLD_ID).subscribe(res => {
            this.apOld = res;
        });
    }


    // toolbar
    add() {

    }
    back() {
        this.navigate([`/app/admin/apartment`]);

    }
    update() {
        this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/${this.inputModel.floor_ID}/apartment-edit/${this.inputModel.apartmenT_ID}`]);
    }
    view() {
        this.navigate([`/app/admin/buildings/${this.inputModel.buildinG_ID}/${this.inputModel.floor_ID}/apartment-view/${this.inputModel.apartmenT_ID}`]);
    }
    delete() {
        this.message.confirm('', this.l('g4DeleteWarningMessage', this.inputModel.apartmenT_NAME), (isConfirmed) => {
            if (isConfirmed) {

                this.apartmentService.apartmentDelete(this.inputModel.apartmenT_ID)
                    .subscribe((response) => {
                        if (response[0]['RESULT'] == '0') {
                            this.message.info(this.l(this.l('g4WaitingApprove')));
                            this.refresh();
                        } else {
                            this.message.error(response[0]['ERROR_DESC']);
                        }
                    });
            }
        });
    }
    approve() {
        this.apartmentService.apartmentApprove(this.inputModel.apartmenT_ID, this.userID).subscribe(res => {
            if (res[0]['RESULT'] == '0') {
                this.message.success(this.l('g4SuccessfullyApproved'));
                switch (this.autH_STATUS_VALUE) {

                    case '1':
                        this.refresh();
                        break;
                    case '2':
                        this.refreshApproveUpdate();
                        break;
                    case '3':
                        this.back();
                        break;

                    default:
                        break;
                }
            }
            else if (res[0]['RESULT'] == '-1') {
                this.message.error(res[0]['ERROR_DESC']);
            }
        })
    }

    deny() {
        this.apartmentService.apartmentDeny(this.inputModel.apartmenT_ID, this.userID).subscribe(res => {
            if (res[0]['RESULT'] == '0') {
                this.message.success(this.l('g4SuccessfullyDenied'));
                switch (this.autH_STATUS_VALUE) {

                    case '1':
                        this.back();
                        break;
                    case '2':
                        this.refreshDenyUpdate();
                        break;
                    case '3':
                        this.refresh();
                        break;

                    default:
                        break;
                }
            }
            else if (res[0]['RESULT'] == '-1') {
                this.message.error(res[0]['ERROR_DESC']);
            }
        })
    }
    save(formRef1) {
        this.touchedForm();
        if (formRef1.valid) {
            this.setValue();
            if (this.editPageState == 'add') this.saveAdd();
            if (this.editPageState == 'edit') this.saveEdit();
        }
        else {
            this.message.error(this.l('g4SaveErrorMessage'));
        }
    }
    setValue() {
        this.inputModel.apartmenT_TYPE_ID = this.selectedApType.apartmenT_TYPE_ID;
        this.inputModel.buildinG_ID = this.selectedBuilding.buildinG_ID;
        this.inputModel.floor_ID = this.selectedFloor.floor_ID;
    }
    saveAdd() {
        this.apartmentService.apartmentInsert(this.inputModel).subscribe(response => {
            if (response[0]['RESULT'] == '0') {
                this.message.info(this.l('g4WaitingApprove'));
            }
            else if (response[0]['RESULT'] == '-1') {
                this.message.error(response[0]['ERROR_DESC']);
            }
        })
    }
    saveEdit() {
        this.apartmentService.apartmentUpdate(this.inputModel).subscribe(response => {
            if (response[0]['RESULT'] == '0') {
                this.message.info(this.l('g4WaitingApprove'));
                this.back();
            }
            else if (response[0]['RESULT'] == '-1') {
                this.message.error(response[0]['ERROR_DESC']);
            }
        })
    }
    touchedForm() {
        this.formAp.form.controls['ap_code'].markAsTouched();
        this.formAp.form.controls['apartmenT_NAME'].markAsTouched();
        this.formAp.form.controls['apartmenT_MAX_TENANT'].markAsTouched();
        this.formAp.form.controls['ddApBuilding'].markAsTouched();
        this.formAp.form.controls['ddApFloor'].markAsTouched();
        this.formAp.form.controls['ddApType'].markAsTouched();

    }

    // navigate
    gotoPageView() {
        this.navigate(['/app/admin/apartment-view/', this.inputModel.apartmenT_ID]);
        console.log('view');
    }
    gotoPageEdit() {
        this.navigate(['/app/admin/apartment-edit/', this.inputModel.apartmenT_ID]);
    }

    refresh() {
        this.router.navigateByUrl(`/app/admin/apartment-edit/${this.inputModel.apartmenT_ID}`, { skipLocationChange: true }).then(() => {
            this.router.navigate([`/app/admin/apartment-view/${this.inputModel.apartmenT_ID}`]);
        });
    }

    refreshSaveUpdate() {
        // this.apartmentService.apartment_FindNewID(this.getRouteParam('id')).subscribe(res => {
        //     this.router.navigate([`/app/admin/apartment-view/${res[0].APARTMENT_ID}`]);
        // });
    }
    refreshApproveUpdate() {
        this.router.navigateByUrl(`/app/admin/apartment-edit/${this.inputModel.apartmenT_OLD_ID}`, { skipLocationChange: true }).then(() => {
            this.router.navigate([`/app/admin/apartment-view/${this.inputModel.apartmenT_OLD_ID}`]);
        });
    }
    refreshDenyUpdate() {
        this.router.navigateByUrl(`/app/admin/apartment-edit/${this.inputModel.apartmenT_OLD_ID}`, { skipLocationChange: true }).then(() => {
            this.router.navigate([`/app/admin/apartment-view/${this.inputModel.apartmenT_OLD_ID}`]);
        });
    }

    // tenant
    deleteTenant() {
        if (!isUndefined(this.selectedTenant)) {
            this.message.confirm('', this.l('g4DeleteWarningMessage', this.selectedTenant.residenT_NAME), (isConfirmed) => {
                if (isConfirmed) {
                    this.residentService.apartmentResident_Delete(this.getRouteParam('id'), this.selectedTenant.residenT_ID).subscribe(res => {
                        if (res[0]['RESULT'] == '0') {
                            this.message.success(this.l('g4SuccessfullyDeleted'));
                            this.refeshTenant();
                        }
                        else if (res[0]['RESULT'] == '-1') {
                            this.message.error(res[0]['ERROR_DESC']);
                        }
                    })
                }
            });
        } else
            this.message.warn(this.l('g4WarningSelectedTenant'));
    }
    ShowAddTenant() {
        if (this.listTenant.length >= this.inputModel.apartmenT_MAX_TENANT) {
            this.message.error(this.l('g4MaxTenantMessage'));
            return;
        }
        this.refeshFreeTenant();
        this.g4Tenant.show();
    }
    addTenant(event) {
        this.residentService.apartmentResident_Insert(this.inputModel.apartmenT_ID, event.residenT_ID, this.userID).subscribe(res => {
            if (res[0]['RESULT'] == '0') {
                this.message.success(this.l('g4SuccessfullyAdded'));
                this.refeshTenant();
                this.refeshFreeTenant();
            }
            else if (res[0]['RESULT'] == '-1') {
                this.message.error(res[0]['ERROR_DESC']);
            }
        })
    }
    searchTenant(event) {
        this.residentService.apartmentResident_SearchFreeResident(this.inputModel.apartmenT_ID, event).subscribe(res => {
            this.listFreeTenant = res;
        })
    }

    // event 
    onChangeBuildings() {
        this.floorService.getByBuildingId(this.selectedBuilding.buildinG_ID).subscribe(res => {
            this.apFloors = res;
        })
    }

    async viewChange() {
        await this.apartmentTypeService.apartmentTypeByID(this.apOld.apartmenT_TYPE_ID).subscribe(r =>  this.compare.apOldName = r.apartmenT_TYPE_NAME);
        await this.apartmentTypeService.apartmentTypeByID(this.inputModel.apartmenT_TYPE_ID).subscribe(r =>  this.compare.apNewName = r.apartmenT_TYPE_NAME);
        this.compare.show();
    }
}
