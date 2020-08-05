using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group13.AbpZeroTemplate.Application
{
    public class Group13ApplicationModule : AbpModule
    {
        public Group13ApplicationModule() { }

        public override void Initialize()
        {
            Configuration.Authorization.Providers.Add<Group13AuthorizationProvider>();
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group13ApplicationModule).GetAssembly());
        }
    }
}
