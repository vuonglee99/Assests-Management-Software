import { Component, Input, Injector, Output, EventEmitter } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";

@Component({
    selector: 'approve-status',
    template:
        `
        <div class="d-flex justify-content-between align-items-center mb-2">
            <div class="form-group" style="margin-bottom: 0px">
                <i class="fa fa-check-circle m--font-success" 
                    *ngIf="approveStatus==='1'" ></i>
                <i class="fa fa-times-circle m--font-danger" 
                    *ngIf="approveStatus==='2'"></i>
                <i class="fa fa-question-circle m--font-warning" 
                    *ngIf="approveStatus==='0'"></i>
                <label class="ml-2 m--font-boldest m--font-success" 
                    *ngIf="approveStatus==='1'">{{l('Group3Approved')}}</label>
                <label class="ml-2 m--font-boldest m--font-danger" 
                    *ngIf="approveStatus==='2'">{{l('Group3Denied')}}</label>
                <label class="ml-2 m--font-boldest m--font-warning" 
                    *ngIf="approveStatus==='0'&&recordStatus==='1'">{{l('Group3Waiting')}} ({{l('Group3Create')}}/{{l('Group3Update')}})</label>
                <label class="ml-2 m--font-boldest m--font-warning" 
                    *ngIf="approveStatus==='0'&&recordStatus==='0'">{{l('Group3Waiting')}} ({{l('Group3Delete')}})</label>
            </div>
            <div *ngIf="approveStatus==='0' && permission.isGranted('Pages.Group03.Buildings.Approve')"
                class="d-flex justify-content-between"
            >
                <button class="btn btn-danger btn-sm" (click)="disapprove.emit($event)">
                    <i class="fa fa-times-circle"></i> {{l('Group3Denied')}}
                </button>
                <button class="btn btn-success btn-sm" (click)="approve.emit($event)">
                    <i class="fa fa-check-circle"></i> {{l('Group3Approve')}}
                </button>
            </div>
        </div>
        `
})
export class ApproveStatusComponent extends AppComponentBase {
    @Input() approveStatus = '';
    @Input() recordStatus = '';

    @Output() approve: EventEmitter<any> = new EventEmitter<any>();
    @Output() disapprove: EventEmitter<any> = new EventEmitter<any>();
    
    constructor(injector: Injector) {
        super(injector);
    }
}