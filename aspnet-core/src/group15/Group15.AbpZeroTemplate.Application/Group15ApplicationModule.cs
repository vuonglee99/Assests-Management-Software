using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group15.AbpZeroTemplate.Application
{
    public class Group15ApplicationModule : AbpModule
    {
        public Group15ApplicationModule() { }

        public override void Initialize()
        {
            Configuration.Authorization.Providers.Add<Group15AuthorizationProvider>();
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group15ApplicationModule).GetAssembly());
        }
    }
}
