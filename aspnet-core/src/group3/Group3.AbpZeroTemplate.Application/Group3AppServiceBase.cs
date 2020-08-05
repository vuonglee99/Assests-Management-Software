using Abp.Application.Services;
using Abp.Dependency;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using GSoft.AbpZeroTemplate;
using GSoft.AbpZeroTemplate.Authorization.Users;
using GSoft.AbpZeroTemplate.Helpers;
using GSoft.AbpZeroTemplate.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class Group3AppServiceBase : ApplicationService
    {
        protected IProcedureHelper procedureHelper;
        public Group3AppServiceBase()
        {
            this.procedureHelper = IocManager.Instance.Resolve<IProcedureHelper>();
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}