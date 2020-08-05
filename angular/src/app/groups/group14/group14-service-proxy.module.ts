import { NgModule } from "@angular/core";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { AbpHttpInterceptor } from "abp-ng2-module/dist/src/abpHttpInterceptor";
import * as ApiServiceProxies from '../../../shared/service-proxies/service-proxies'

@NgModule({
    providers: [
        ApiServiceProxies.NhanVienServiceProxy,
       { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },
       ApiServiceProxies.NhaCungUngServiceProxy,
       { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },
       ApiServiceProxies.ThietBiVatTuServiceProxy,
       { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },
        ApiServiceProxies.PhieuCapPhatTBVTServiceProxy,
        {provide:HTTP_INTERCEPTORS, useClass:AbpHttpInterceptor,multi:true}
    ]
})
export class Group14ServiceProxyModule { 

}
