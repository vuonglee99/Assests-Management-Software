import { Component, EventEmitter, Injector, Output, ViewChild, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ApartmentResidentServiceProxy } from '@shared/service-proxies/service-proxies';
import { isUndefined } from 'lodash';

@Component({
    selector: 'g4Tenant',
    templateUrl: './g4-tenant.component.html',
    styleUrls:['../group4-style.css']
})
export class g4TenantComponent extends AppComponentBase {

    @Input() listFreeTenant;
    @ViewChild('createModal') modal: ModalDirective;
    @Output() g4TenantAdd: EventEmitter<any> = new EventEmitter<any>();
    @Output() g4TenantSearch: EventEmitter<any> = new EventEmitter<any>();

    constructor(injector: Injector, private residentService: ApartmentResidentServiceProxy) {
        super(injector);
    }


    show() {
        this.modal.show();
    }

    close() {
        this.modal.hide();
    }

    selectedTenant;
    addTenant() {
        console.log(this.selectedTenant);
        if (isUndefined(this.selectedTenant)) {
            this.message.warn("Vui lòng chọn 1 người thuê");
        } else {
            this.g4TenantAdd.emit(this.selectedTenant);
        }
    }

    filter = '';
    search() {
        this.g4TenantSearch.emit(this.filter);
    }
}
