import { Component, EventEmitter, Injector, Output, ViewChild, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { ApartmentResidentServiceProxy, ApartmentDTO, ApartmentTypeServiceProxy } from '@shared/service-proxies/service-proxies';
import { isUndefined } from 'lodash';

@Component({
    selector: 'compare-ap',
    templateUrl: './compare-ap.html',
    styleUrls: ['../group4-style.css']
})
export class CompareApComponent extends AppComponentBase {

    @Input() apOld: ApartmentDTO;
    @Input() apNew: ApartmentDTO;
    @ViewChild('createModal') modal: ModalDirective;
    apOldName='';
    apNewName='';
    constructor(injector: Injector) {
        super(injector);
    }


    show() {
        this.modal.show();
        console.log(this.apNewName);
        console.log(this.apOldName);
    }

    close() {
        this.modal.hide();
    }

}
