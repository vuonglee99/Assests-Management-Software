import { ViewEncapsulation, Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/common/app-component-base";
import { Table } from 'primeng/components/table/table';

import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { BranchServiceProxy, ApartmentDTO, ApartmentServiceProxy, ApSearchDTO, ApartmentTypeDTO, ApartmentTypeServiceProxy, BuildingsServiceProxy, FloorServiceProxy, Floor_DTO, BuildingTableDTO, CustomApartmentDTO, SessionServiceProxy, } from "@shared/service-proxies/service-proxies";
import { a } from "@angular/core/src/render3";
import { stringify } from "querystring";
import { DragDropModule, DropdownModule, Dropdown } from "primeng/primeng";


@Component({
    templateUrl: './apartment-list.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['../group4-style.css']
})
export class ApartmentListComponent extends AppComponentBase implements OnInit {

    @ViewChild('dt') table: Table;
    listAllBuildings = [];
    listAllFloor = [];
    listAllTypes = [];
    apTypes = [];
    selectedApType = new ApartmentTypeDTO();
    defaultType = new ApartmentTypeDTO();
    apBuildings = [];
    selectedBuilding = new BuildingTableDTO();
    defaultBuilding = new BuildingTableDTO();
    apFloors = [];
    selectedFloor = new Floor_DTO();
    defaultFloor = new Floor_DTO();
    apStatus = [];
    selectedStatus = { value: '', text: this.l('g4SeleteAll') };
    apAuthStatus = [];
    selectedAuthStatus = { value: '', text: this.l('g4SeleteAll') };
    filterCodeText = '';
    filterNameText = '';
    filterMaxTenant: number;
    records = [];
    selectedApartment;
    loading = null;
    permissionViewChanges; // 0: only view before changes, 1 


    constructor(injector: Injector, private apartmentService: ApartmentServiceProxy, private apartmentTypeService: ApartmentTypeServiceProxy, private buildingService: BuildingsServiceProxy, private floorService: FloorServiceProxy, private sessionService: SessionServiceProxy) {
        super(injector);
        if (this.isGrantedAny('Pages.Group4.Apartment.Add', 'Pages.Group4.Apartment.Update', 'Pages.Group4.Apartment.Delete', 'Pages.Group4.Apartment.Approve'))
            this.permissionViewChanges = '1';
        else this.permissionViewChanges = '0';
        this.setDefaultAllListFilter();
    }

    ngOnInit() {
    }



    // event Toolbar
    add() {
        this.navigate(['/app/admin/apartment-add']);
    }
    viewDetail() {
        if (this.selectedApartment != undefined) {
            this.navigate([`/app/admin/apartment-view/${this.selectedApartment.apartmenT_ID}`]);
        } else {
            this.message.warn(this.l('g4UnseletedApartmentWarning'));
        }
    }
    update() {
        if (this.selectedApartment != undefined) {
            this.navigate([`/app/admin/apartment-edit/${this.selectedApartment.apartmenT_ID}`]);
        } else {
            this.message.warn(this.l('g4UnseletedApartmentWarning'));
        }
    }
    delete() {
        if (this.selectedApartment != undefined) {
            this.message.confirm('', this.l('g4DeleteWarningMessage', this.selectedApartment.apartmenT_NAME), (isConfirmed) => {
                if (isConfirmed) {
                    this.apartmentService.apartmentDelete(this.selectedApartment.apartmenT_ID)
                        .subscribe((response) => {
                            if (response[0]['RESULT'] == '0') {
                                this.message.info(this.l(this.l('g4WaitingApprove')));
                                this.search();
                            } else {
                                this.message.error(response[0]['ERROR_DESC']);
                            }
                        });
                }
            });
        } else {
            this.message.warn(this.l('g4UnseletedApartmentWarning'));
        }
    }

    search() {
        let maxTenant = this.filterMaxTenant == null ? 0 : this.filterMaxTenant;
        this.apartmentService.apartmentSearch(this.filterCodeText, this.filterNameText, this.selectedApType.apartmenT_TYPE_ID, this.selectedBuilding.buildinG_ID, this.selectedFloor.floor_ID, this.selectedStatus.value, this.selectedAuthStatus.value, maxTenant, this.permissionViewChanges).subscribe(res => {
            this.records = res;
        })
    }

    exportExcel() {
        let excel = this.records.map(pt => this.coverCustomAp(pt));
        this.apartmentService.apartment_ExportExcel(excel).subscribe(res => {
            if (res) this.message.success(this.l('g4ExportSuccess'));
            else this.message.error(this.l('g4ExportFalled'));
        });

    }

    coverCustomAp(pt) {
        let x = new CustomApartmentDTO();
        x.apartmenT_CODE = pt.apartmenT_CODE;
        x.apartmenT_NAME = pt.apartmenT_NAME;
        x.apartmenT_TYPE_NAME = pt.apartmenT_TYPE_NAME;
        x.buildinG_NAME = pt.buildinG_NAME;
        x.floor_NAME = pt.floor_NAME;
        x.numbeR_OF_PEOPLE = `${pt.residenT_QUANTITY}/${pt.apartmenT_MAX_TENANT}`;
        x.apartmenT_PRICE = pt.apartmenT_PRICE;
        x.apartmenT_STATUS = `${pt.apartmenT_STATUS == '0' ? this.l('g4Disable') : this.l('g4Enable')}`;
        x.autH_STATUS = pt.autH_STATUS_NAME;
        return x;
    }

    setDefaultAllListFilter() {
        this.apAuthStatus = [
            { value: '', text: this.l('g4SeleteAll') },
            { value: 'B', text: this.l('g4WaitingStatus') },
            { value: 'A', text: this.l('g4ApprovedStatus') }];
        this.apStatus = [
            { value: '', text: this.l('g4SeleteAll') },
            { value: '0', text: this.l('g4Disable') },
            { value: '1', text: this.l('g4Enable') }];
        this.defaultType.apartmenT_TYPE_ID = '';
        this.defaultType.apartmenT_TYPE_NAME = this.l('g4SeleteAll');
        this.defaultBuilding.buildinG_ID = '';
        this.defaultBuilding.buildinG_NAME = this.l('g4SeleteAll');
        this.defaultFloor.floor_ID = '';
        this.defaultFloor.floor_NAME = this.l('g4SeleteAll');
        this.apartmentTypeService.apartmentTypeGetAll().subscribe(res => {
            this.listAllTypes = res;
            this.apTypes = this.apTypes.concat(this.defaultType, this.listAllTypes);
        });

        this.buildingService.pagingSearch('', '', '1', '', '', 0, 1000).subscribe(res => {
            this.listAllBuildings = res.items;
            // load list building to filter
            this.apBuildings = this.apBuildings.concat(this.defaultBuilding, this.listAllBuildings);
            this.apBuildings.forEach(element => {
                if (element.buildinG_ID == this.getRouteParam('buildingID')) {
                    this.selectedBuilding = element;
                }
            })
        });
        // load Floor list
        this.floorService.getByBuildingId(this.getRouteParam('buildingID')).subscribe(res => {
            this.apFloors = this.apFloors.concat(this.defaultFloor, res);
            this.apFloors.forEach(element => {
                if (element.floor_ID == this.getRouteParam('floorID')) {
                    this.selectedFloor = element;
                }
            });
            this.floorService.getAll().subscribe(res5 => {
                this.listAllFloor = res5;
                this.search();
            });
        })
    }


    // event 
    onChangeBuildings() {
        this.floorService.getByBuildingId(this.selectedBuilding.buildinG_ID).subscribe(res => {
            this.apFloors = [];
            this.apFloors = this.apFloors.concat(this.defaultFloor, res);
            this.selectedFloor = this.apFloors[0];
        })
    }


    dblclick(row) {
        this.navigate([`/app/admin/apartment-view/${row.apartmenT_ID}`]);
    }
}