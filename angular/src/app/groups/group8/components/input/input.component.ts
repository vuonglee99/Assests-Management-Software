import { Component, Injector, forwardRef, ViewEncapsulation, Input, Output, EventEmitter } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from "@angular/forms";

@Component({
    selector: 'group8-input',
    templateUrl: './input.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => Group8InputComponent),
        multi: true
    }]
})
export class Group8InputComponent implements ControlValueAccessor {
    ngModelValue: any;

    @Output() ngModelChange: EventEmitter<any> = new EventEmitter<any>();
    @Output() keypress: EventEmitter<any> = new EventEmitter<any>();

    @Input("id") id: string;
    @Input("label") label: string;
    @Input("name") name: string;
    @Input("class") class: string;
    @Input("placeholder") placeholder: string = "Enter Value";
    @Input("required") required: boolean;
    @Input("type") type: string;
    @Input("pattern") pattern: string;
    @Input("disabled") disabled: boolean;
    @Input("formControlName") formControlName: any;

    @Input()
    get ngModel() {
        return this.ngModelValue
    };
    set ngModel(val) {
        this.ngModelValue = val;
        this.ngModelChange.emit(this.ngModelValue);
    }


    constructor() { }

    /**
  * Method that is invoked on an update of a model.
  */
    updateChanges() {
        this.onChange(this.ngModelValue);
    }

    writeValue(obj: any): void {
        this.ngModelValue = obj;
        this.updateChanges();
    }
    registerOnChange(fn: any): void {
        this.onChange = fn;
    }
    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }
    setDisabledState?(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

    onTouched: () => void = () => { };


    onChange: (_: any) => void = (_: any) => {};

    ngOnInit() {
        if (this.disabled) {
            this.placeholder = '';
        }
    }

}