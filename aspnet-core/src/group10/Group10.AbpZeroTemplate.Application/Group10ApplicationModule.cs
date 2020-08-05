using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group10.AbpZeroTemplate.Application
{
    public class Group10ApplicationModule : AbpModule
    {
        public Group10ApplicationModule() { }

        public override void Initialize()
        {
            Configuration.Authorization.Providers.Add<Group10AuthorizationProvider>();
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group10ApplicationModule).GetAssembly());
        }
    }
}
