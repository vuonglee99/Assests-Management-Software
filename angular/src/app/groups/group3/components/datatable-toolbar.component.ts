import { Component, Input, Injector, Output, EventEmitter } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";

@Component({
    selector: 'datatable-toolbar',
    template:
        `
        <div class="g3-toolbar">
            <button *ngIf="!isHidenList[0]" [disabled]="isDisabledList[0]" class="btn btn-primary"
                (click)="createItem.emit($event)"
            >
                <i class="fa fa-plus-square"></i>
            </button>
            <button *ngIf="!isHidenList[1]" [disabled]="isDisabledList[1]" class="btn btn-primary"
                (click)="detailItem.emit($event)" 
            >
                <i class="fa fa-info"></i>
            </button>
            <button *ngIf="!isHidenList[2]" [disabled]="isDisabledList[2]" class="btn btn-primary"
                (click)="modifyItem.emit($event)" 
            >
                <i class="fa fa-pencil-square"></i>
            </button>
            <button *ngIf="!isHidenList[3]" [disabled]="isDisabledList[3]" class="btn btn-primary"
                (click)="deleteItem.emit($event)" 
            >
                <i class="fa fa-minus-square"></i>
            </button>
            <button *ngIf="!isHidenList[4]" [disabled]="isDisabledList[4]" class="btn btn-primary"
                (click)="exportItems.emit($event)" 
            >
                <i class="fa fa-file-text "></i>
            </button>
        </div>
        `,
    styleUrls: ['../styles.css'],
})
export class DatatableToolbarComponent extends AppComponentBase {
    @Input() isHidenList: boolean[] = [false, false, false, false, false];
    @Input() isDisabledList: boolean[] = [false, false, false, false, false];

    @Output() createItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() detailItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() modifyItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() deleteItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() exportItems: EventEmitter<any> = new EventEmitter<any>();

    constructor(injector: Injector) {
        super(injector);
    }
}