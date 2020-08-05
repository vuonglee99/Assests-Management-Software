import { NgModule } from "@angular/core";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { AbpHttpInterceptor } from "abp-ng2-module/dist/src/abpHttpInterceptor";
import * as ApiServiceProxies from '../../../shared/service-proxies/service-proxies'

@NgModule({
    providers: [
        ApiServiceProxies.DonViTinhServiceProxy,   ApiServiceProxies.FloorTypeServiceProxy,
        ApiServiceProxies.FloorServiceProxy,
       { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
    ]
})
export class Group15ServiceProxyModule { 
    
}

export interface IStatus_DTO {
    status_ID: string | undefined;
    status_NAME: string | undefined;
}

export class Status_DTO implements IStatus_DTO {
    status_ID!: string | undefined;
    status_NAME!: string | undefined;

    constructor(data?: IStatus_DTO) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.status_ID = data["status_ID"];
            this.status_NAME = data["status_NAME"];
        }
    }

    static fromJS(data: any): IStatus_DTO {
        data = typeof data === 'object' ? data : {};
        let result = new Status_DTO();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["status_ID"] = this.status_ID;
        data["status_NAME"] = this.status_NAME;
        return data; 
    }
}
