import { Component, AfterViewInit, Injector, ViewChild, ElementRef, Input, Output, EventEmitter, OnChanges, SimpleChanges } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";
import * as moment from "moment";

@Component({
    selector: 'date-picker',
    template:
        `<input type="datetime" #DatePicker class="form-control" placeholder="dd/mm/yyyy">`
})
export class DatePickerComponent extends AppComponentBase implements AfterViewInit, OnChanges {
    @ViewChild('DatePicker') datePicker: ElementRef;

    @Input() moment: moment.Moment = moment();
    @Output() momentChange: EventEmitter<moment.Moment> = new EventEmitter<moment.Moment>();

    constructor(injector: Injector) {
        super(injector);
    }

    ngAfterViewInit(): void {
        $(this.datePicker.nativeElement).datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L',
            defaultDate: this.moment.format('YYYY-MM-DD')
        });
        $(this.datePicker.nativeElement).datetimepicker()
            .on('dp.change', () => this.onValueChange(
                $(this.datePicker.nativeElement).data('DateTimePicker').date()
            ));
    }

    onValueChange(curTime: moment.Moment) {
        this.moment = curTime;
        this.momentChange.emit(this.moment);
    }

    ngOnChanges(changes: SimpleChanges): void {
        const datePicker = $(this.datePicker.nativeElement).data('DateTimePicker');
        if (changes.moment && datePicker)
            datePicker.date(this.moment);
    }

}