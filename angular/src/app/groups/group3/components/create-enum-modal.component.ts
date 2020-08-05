import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Injector, Output, ViewChild, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateOrganizationUnitInput, OrganizationUnitDto, OrganizationUnitServiceProxy, UpdateOrganizationUnitInput } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';

export interface IOrganizationUnitOnEdit {
    id?: number;
    parentId?: number;
    displayName?: string;
}

@Component({
    selector: 'create-enum-modal',
    templateUrl: './create-enum-modal.component.html'
})
export class CreateEnumModalComponent extends AppComponentBase {
    @ViewChild('modal') modal: ModalDirective;

    value = '';
    @Input() title: string = '';
    @Input() fieldName: string = '';
    @Output() save: EventEmitter<any> = new EventEmitter<any>();

    constructor(
        injector: Injector,
    ) {
        super(injector);
    }

    show(): void { this.modal.show(); }
    close(): void { this.modal.hide(); }
}
