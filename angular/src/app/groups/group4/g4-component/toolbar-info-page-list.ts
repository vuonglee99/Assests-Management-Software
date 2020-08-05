import { Component, ElementRef, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';



@Component({
    selector: 'toolbar-info-page-list',
    template:
    
        `
        <div>
        <ul style="background-color: #58a3dc;font-size: 15px;padding-left:10px;margin:0;color:white">
            <li *ngIf="isGranted(isGrantedAdd)" style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="add.emit()"><i class="fa fa-plus"></i></li>
            <li *ngIf="isGranted(isGrantedDelete)" style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="delete.emit()"><i class="fa fa-trash"></i></li>
            <li style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="viewDetail.emit()"><i class="fa fa-eye"></i></li>
            <li style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="search.emit()"><i class="fa fa-search"></i></li>
            <li style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="exportExcel.emit()"><i class="fa fa-file-excel-o"></i></li>
        </ul>
    </div>


    <!-- info -->
    <!--<div style="background-color: #58a3dc; padding: 10px 30px; margin-top: 15px; color:white; display: flex; align-items: center;">
        <h1 class="m-0">{{l('g4List')}} {{namePage}}</h1>
    </div>-->
    <!-- Heading -->
    <div class="m-subheader">
      <div class="d-flex align-items-center">
        <div class="mr-auto col-xs-6">
          <h3 class="m-subheader__title m-subheader__title--separator">
            <span>{{l('g4List')}} {{namePage}}</span>
          </h3>
          <span class="m-section__sub">{{l('g4Manage')}} {{namePage}}</span>
        </div>
      </div>
    </div>

        `
})

export class toolbarList extends AppComponentBase implements OnInit {

    @Input() namePage: string = '';
    @Input() isGrantedAdd: string = '';
    @Input() isGrantedDelete: string = '';
    @Output() add: EventEmitter<any> = new EventEmitter<any>();
    @Output() delete: EventEmitter<any> = new EventEmitter<any>();
    @Output() viewDetail: EventEmitter<any> = new EventEmitter<any>();
    @Output() search: EventEmitter<any> = new EventEmitter<any>();
    @Output() exportExcel: EventEmitter<any> = new EventEmitter<any>();
    
    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
    }
}
