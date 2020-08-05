import {DUT_DTO} from '@shared/service-proxies/service-proxies';

export default class viewDUT extends DUT_DTO {
    constructor(data?: any) {
        super(data);
        if (data) {
            this.RowNo = data.RowNo;
        }
    }

    RowNo: number;
}
