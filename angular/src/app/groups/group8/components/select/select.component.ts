import { Component, Injector, ViewEncapsulation, Input, Output, EventEmitter, forwardRef } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from "@angular/forms";

@Component({
    selector: 'group8-select',
    templateUrl: './select.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    exportAs: 'ngModel',
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => Group8SelectComponent),
        multi: true,
    }]
})
export class Group8SelectComponent implements ControlValueAccessor {
    ngModelValue: string;

    @Output() ngModelChange: EventEmitter<string> = new EventEmitter<string>();
    @Output() onchange: EventEmitter<any> = new EventEmitter<any>();

    @Input("id") id: string;
    @Input("label") label: string;
    @Input("name") name: string;
    @Input("class") class: string;
    @Input("placeholder") placeholder: string = "Enter Value";
    @Input("required") required: boolean;
    @Input("disabled") disabled: boolean;
    @Input("dataSources") dataSources: any[];
    @Input("dataValue") dataValue: string | undefined = 'Value';
    @Input("dataDisplay") dataDisplay: string | undefined = 'Name';

    @Input()
    get ngModel() {
        return this.ngModelValue
    };
    set ngModel(val) {
        this.ngModelValue = val;
        this.ngModelChange.emit(this.ngModelValue);
        this.onchange.emit(val)
    }

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


    onChange: (_: any) => void = (_: any) => {  };


    ngOnInit() {
        if (this.disabled) {
            this.placeholder = '';
        }
    }

}