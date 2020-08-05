using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group14.AbpZeroTemplate.Application
{
    public class Group14ApplicationModule : AbpModule
    {
        public Group14ApplicationModule() { }

        public override void Initialize()
        {
            Configuration.Authorization.Providers.Add<Group14AuthorizationProvider>();
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group14ApplicationModule).GetAssembly());
        }
    }
}
