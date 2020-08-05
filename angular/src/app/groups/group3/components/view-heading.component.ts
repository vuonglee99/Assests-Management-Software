import { Component, Input, Injector } from "@angular/core";
import { AppComponentBase } from "@shared/common/app-component-base";

@Component({
    selector: 'view-heading',
    template:
        `
        <div class="m-subheader ">
            <div class="d-flex align-items-center">
                <div class="mr-auto col-xs-6">
                    <h3 class="m-subheader__title" [ngClass]="{'m-subheader__title--separator': subtitle !== ''}">
                        <span>{{title}}</span>
                    </h3>
                    <span class="m-section__sub" *ngIf="subtitle">{{subtitle}}</span>
                </div>
            </div>
        </div>
        `
})
export class ViewHeadingComponent extends AppComponentBase {
    @Input() title = '';
    @Input() subtitle = '';

    constructor(injector: Injector) {
        super(injector);
    }
}