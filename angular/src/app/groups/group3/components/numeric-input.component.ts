import { Component, Input, Injector, Output, EventEmitter } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";

@Component({
    selector: 'numeric-input',
    template:
        `
        <div class="input-group">
            <input [id]="id" class="form-control m-input text-right" type="text" 
                [disabled]="disabled"
                [value]="value | number:'1.0-5':'en'"
                (keypress)="onKeyPress($event)"
                [placeholder]="l(placeholder)" >
            <div *ngIf="isCurrency" class="input-group-append">
                <span class="input-group-text">{{l('Group3CurrencyUnit')}}</span>
            </div>
        </div>
        `
})
export class NumericInputComponent extends AppComponentBase {
    @Input() id: string;
    @Input() disabled: boolean = false;
    @Input() placeholder: string = '';
    @Input() isCurrency: boolean = false;
    @Input() value: number;
    @Output() valueChange: EventEmitter<number> = new EventEmitter<number>();

    constructor(injector: Injector) {
        super(injector);
    }

    onKeyPress(event?: KeyboardEvent) {
        if (!/^\.$/.test(event.key))
            event.preventDefault();
        let showValue = $('#' + this.id).val() + event.key;
        if (!/^(\,|\d)+(\.\d{0,5})?$/.test(showValue))
        return;
        showValue = showValue.replace(/\,/g, '');
        this.value = parseFloat(showValue);
        this.valueChange.emit(this.value);
    }
}