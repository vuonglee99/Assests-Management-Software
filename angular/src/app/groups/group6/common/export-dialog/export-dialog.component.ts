import {Component, Injector, Input, OnInit} from '@angular/core';
import {AppComponentBase} from '@shared/common/app-component-base';

@Component({
    selector: 'app-export-dialog',
    templateUrl: './export-dialog.component.html',
    styleUrls: ['./export-dialog.component.css']
})
export class ExportDialogComponent extends AppComponentBase implements OnInit {
    @Input() modalId: string;

    private labelId: string;

    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit() {
        this.labelId = Math.random().toString();
    }

}
