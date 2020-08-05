import { Component, ElementRef, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';



@Component({
    selector: 'approve-detail',
    templateUrl:'./approve-detail.html'
})

export class approveDetail extends AppComponentBase implements OnInit {

    @Input() editPageState: string = '';
    @Input() autH_STATUS_VALUE: string = '';
    @Input() isGrantedApprove: string = '';

    @Output() approve: EventEmitter<any> = new EventEmitter<any>();
    @Output() deny: EventEmitter<any> = new EventEmitter<any>();
    @Output() viewChange: EventEmitter<any> = new EventEmitter<any>();


    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
    }
}
