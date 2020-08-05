using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group17.AbpZeroTemplate.Application
{
    public class Group17ApplicationModule : AbpModule
    {
        public Group17ApplicationModule() { }

        public override void Initialize()
        {
            Configuration.Authorization.Providers.Add<Group17AuthorizationProvider>();
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group17ApplicationModule).GetAssembly());
        }
    }
}
