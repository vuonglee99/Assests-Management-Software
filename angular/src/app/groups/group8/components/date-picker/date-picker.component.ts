import { Component, Injector, ViewEncapsulation, Input, Output, EventEmitter } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";

@Component({
    selector: 'group8-date-picker',
    templateUrl: './date-picker.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class Group8DatePickerComponent extends AppComponentBase {
    ngModelValue: any;  

    @Output() ngModelChange: EventEmitter<any> = new EventEmitter<any>();

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
    }


    constructor(injector: Injector) {
        super(injector);
    }

    onChange() {
        this.ngModelChange.emit(this.ngModelValue);
    }

    ngOnInit() {
        if(this.disabled) {
            this.placeholder = '';
        }
    }

}