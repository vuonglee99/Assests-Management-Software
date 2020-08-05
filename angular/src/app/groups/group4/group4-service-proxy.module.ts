import { NgModule } from "@angular/core";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { AbpHttpInterceptor } from "abp-ng2-module/dist/src/abpHttpInterceptor";
import * as ApiServiceProxies from '../../../shared/service-proxies/service-proxies'

@NgModule({
    providers: [
        ApiServiceProxies.BranchServiceProxy,ApiServiceProxies.ApartmentTypeServiceProxy,ApiServiceProxies.ApartmentServiceProxy,
        ApiServiceProxies.BuildingsServiceProxy,ApiServiceProxies.FloorServiceProxy,ApiServiceProxies.ApartmentResidentServiceProxy,
       { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
    ]
})
export class Group4ServiceProxyModule { 

}
