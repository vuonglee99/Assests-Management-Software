import { Component, Input, Output, EventEmitter, Injector } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";

@Component({
    selector: 'detailview-toolbar',
    template:
    `
    <div class="g3-toolbar">
        <button *ngIf="!isHidenList[0]" [disabled]="isDisabledList[0]" class="btn btn-primary"
            (click)="navigateBack.emit($event)" 
        >
            <i class="fa fa-arrow-left"></i>
        </button>
        <button *ngIf="!isHidenList[1]" [disabled]="isDisabledList[1]" class="btn btn-primary"
            (click)="cancelItem.emit($event)"
        >
            <i class="fa fa-times"></i>
        </button>
        <button *ngIf="!isHidenList[2]" [disabled]="isDisabledList[2]" class="btn btn-primary"
            (click)="createItem.emit($event)"
        >
            <i class="fa fa-floppy-o"></i>
        </button>
        <button *ngIf="!isHidenList[3]" [disabled]="isDisabledList[3]" class="btn btn-primary"
            (click)="modifyItem.emit($event)" 
        >
            <i class="fa fa-pencil-square"></i>
        </button>
        <button *ngIf="!isHidenList[4]" [disabled]="isDisabledList[4]" class="btn btn-primary"
            (click)="deleteItem.emit($event)" 
        >
            <i class="fa fa-minus-square"></i>
        </button>
        <button *ngIf="!isHidenList[5]" [disabled]="isDisabledList[5]" class="btn btn-primary"
            (click)="updateItem.emit($event)" 
        >
            <i class="fa fa-floppy-o"></i>
        </button>
    </div>
    `
})
export class DetailViewToolbarComponent extends AppComponentBase {
    @Input() isHidenList: boolean[] = [false, false, false, false, false, false];
    @Input() isDisabledList: boolean[] = [false, false, false, false, false, false];

    @Output() navigateBack: EventEmitter<any> = new EventEmitter<any>();
    @Output() cancelItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() createItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() modifyItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() deleteItem: EventEmitter<any> = new EventEmitter<any>();
    @Output() updateItem: EventEmitter<any> = new EventEmitter<any>();

    constructor(injector: Injector) {
        super(injector);
    }
}