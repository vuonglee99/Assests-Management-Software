import { Component, ElementRef, EventEmitter, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';



@Component({
  selector: 'toolbar-info-page-detail',
  template:
    `
    <div>
        <ul style="background-color: #58a3dc;font-size: 15px;padding-left:10px;margin:0;color:white">
        <li *ngIf="editPageState != 'edit'" style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="back.emit()"><i class="fa fa-arrow-left"></i></li>
            <li *ngIf="editPageState != 'view'" style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="save.emit()">
                <i class="fa fa-floppy-o"></i></li>
            <li *ngIf="isGranted(isGrantedUpdate) && autH_STATUS_VALUE == '0' && editPageState === 'view'" style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="update.emit()">
                <i class="fa fa-pencil"></i></li>
            <li *ngIf="editPageState === 'edit'" style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="view.emit()">
                <i class="fa fa-times"></i></li>
            <li *ngIf="isGranted(isGrantedDelete) && autH_STATUS_VALUE == '0' && editPageState === 'view'" style="display: inline-block;padding:10px;padding-left: 20px;cursor: pointer;" (click)="delete.emit()"><i class="fa fa-trash"></i></li>
           
        </ul>
    </div>
    
    <!-- Heading -->
    <div class="m-subheader">
      <div class="d-flex align-items-center">
        <div class="mr-auto col-xs-6">

          <h3 *ngIf="editPageState=='add'" class="m-subheader__title m-subheader__title--separator">
            <span>{{l('g4AddPageName')}} {{namePage}}</span>
          </h3>
          <h3 *ngIf="editPageState=='edit'" class="m-subheader__title m-subheader__title--separator">
          <span>{{l('g4EditPageName')}} {{namePage}}</span>
        </h3>
        <h3 *ngIf="editPageState=='view'" class="m-subheader__title m-subheader__title--separator">
        <span>{{l('g4ViewPageName')}} {{namePage}}</span>
      </h3>

          
        </div>
      </div>
    </div>


    

        `
})
export class toolbarDetail extends AppComponentBase implements OnInit {

  @Input() autH_STATUS_VALUE: string = '';
  @Input() namePage: string = '';
  @Input() editPageState: string = '';
  @Input() isGrantedUpdate: string = '';
  @Input() isGrantedDelete: string = '';

  @Output() save: EventEmitter<any> = new EventEmitter<any>();
  @Output() update: EventEmitter<any> = new EventEmitter<any>();
  @Output() delete: EventEmitter<any> = new EventEmitter<any>();
  @Output() view: EventEmitter<any> = new EventEmitter<any>();
  @Output() back: EventEmitter<any> = new EventEmitter<any>();

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {
  }
}
