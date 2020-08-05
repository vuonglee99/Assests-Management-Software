import { Component, ViewChild, ElementRef, Input, Output, EventEmitter, Injector, OnInit, OnChanges, SimpleChanges, AfterViewInit } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";

export class SearchableSelectComponentItem {
    value: string; label: string

    constructor(value: string, label: string) {
        this.value = value;
        this.label = label;
    }
}

@Component({
    selector: 'searchable-select',
    template:
        `
        <div class="input-group">
            <div *ngIf="editable" class="input-group-prepend">
                <button class="btn btn-primary"
                    (click)="createItem.emit($event)"
                >
                    <i class="fa fa-plus-square"></i>
                </button>
            </div>
            <select #SearchableSelect
                class="form-control"
                [(ngModel)]="selectedValue"
                (ngModelChange)="selectedValueChange.emit($event)"
                [attr.data-live-search]="true"
                jq-plugin="selectpicker"
            >
                <option value="">{{emptyText}}</option>
                <option *ngFor="let item of items" [value]="item.value">{{item.label}}</option>
            </select>
            <div *ngIf="editable" class="input-group-append">
                <button class="btn btn-primary" [disabled]="!selectedValue"
                    (click)="onDeleteItemClick($event)"
                >
                    <i class="fa fa-minus-square"></i>
                </button>
            </div>
        </div>
        `
})
export class SearchableSelectComponent extends AppComponentBase implements OnChanges {
    @ViewChild('SearchableSelect') elementRef: ElementRef;

    @Input() disabled: boolean = false;
    @Input() editable: boolean = false;
    @Input() items: SearchableSelectComponentItem[] = [];
    @Input() emptyText: string = '';
    @Input() selectedValue: string = undefined;
    @Output() selectedValueChange: EventEmitter<string> = new EventEmitter<string>();
    @Output() createItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() deleteItem: EventEmitter<number> = new EventEmitter<number>();

    constructor(injector: Injector) {
        super(injector);
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.disabled) {
            $(this.elementRef.nativeElement).prop(
                'disabled', changes.disabled.currentValue);
            setTimeout(
                () => $(this.elementRef.nativeElement).selectpicker('refresh'),
                0
            );
        }
        if (changes.items) {
            setTimeout(
                () => $(this.elementRef.nativeElement).selectpicker('refresh'),
                0
            );
            this.selectedValue = undefined;
        }
        if (changes.selectedValue)
            setTimeout(
                () => $(this.elementRef.nativeElement).selectpicker(
                    'val', changes.selectedValue.currentValue),
                0
            );
    }

    onDeleteItemClick(event?: any) {
        const index = this.items.findIndex(x => x.value === this.selectedValue);
        this.deleteItem.emit(index);
    }

}