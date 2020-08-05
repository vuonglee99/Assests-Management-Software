import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
    selector: 'app-search-bar',
    templateUrl: './search-bar.component.html',
    styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

    @Input() statusOptions: Array<any>;
    @Input() searchFieldOptions: Array<any>;
    @Input() statusLabel: string;

    @Output() search = new EventEmitter();

    public searchValue: string;
    public filterInput: any;
    public searchField: string;

    constructor() {
    }

    ngOnInit() {
        this.filterInput = {autH_STATUS: '_'};
        console.log(this.statusOptions, this.searchFieldOptions);
    }

    handleSearch() {
        if (this.searchField) {
            this.filterInput[this.searchField] = this.searchValue;
        }
        this.search.emit(this.filterInput);
    }

    handleChangeSearchField(event: any) {
        delete this.filterInput[this.searchField];
        this.searchField = event.value;
    }
}
