import { Component, ElementRef, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

class Items { id: string; value: string; }

@Component({
    selector: 'branch-status-combo',
    template:
        `<select #BranchStatusCombobox
            class="form-control"
            [(ngModel)]="selectedItem"
            (ngModelChange)="selectedItemChange.emit($event)"
            [attr.data-live-search]="true"
            jq-plugin="selectpicker">
                <option value="">{{emptyText}}</option>
                <option *ngFor="let status of statuses" [value]="status.id">{{status.value}}</option>
        </select>`
})
export class BranchStatusComboComponent extends AppComponentBase implements OnInit {

    @ViewChild('BranchStatusCombobox') BranchStatusComboboxElement: ElementRef;

    statuses: Items[] = [
        { id: '1', value: 'Đang hoạt động' },
        { id: '0', value: 'Đã ngừng hoạt động' }
    ];

    @Input() selectedItem: string = undefined;
    @Output() selectedItemChange: EventEmitter<string> = new EventEmitter<string>();

    @Input() emptyText = '';

    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
        setTimeout(() => {
            $(this.BranchStatusComboboxElement.nativeElement).selectpicker('refresh');
        }, 0);
    }
}
