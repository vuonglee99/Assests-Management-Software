import {Component, Input, OnInit} from '@angular/core';

@Component({
    selector: 'app-ant-tag',
    templateUrl: './ant-tag.component.html',
    styleUrls: ['./ant-tag.component.css']
})
export class AntTagComponent implements OnInit {

    @Input() color: string;
    @Input() label: string;

    public tagClass: string;

    constructor() {
    }

    ngOnInit() {
        this.tagClass = '';
        this.tagClass += `ant-tag ant-tag-${this.color.toLowerCase()}`;
    }

}
