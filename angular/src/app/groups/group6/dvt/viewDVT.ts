import { DVT_DTO } from "@shared/service-proxies/service-proxies";

export default class viewDVT extends DVT_DTO {
    constructor(data?: any) {
        super(data);
        if (data) this.RowNo = data.RowNo;
    }
    RowNo: number;
}
