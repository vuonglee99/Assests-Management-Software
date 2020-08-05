using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group14.AbpZeroTemplate.Web.Core
{
    public class Group14WebCoreModule : AbpModule
    {
        public Group14WebCoreModule() { }

        public override void Initialize()
        {
            
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group14WebCoreModule).GetAssembly());
        }
    }
}
