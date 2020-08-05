using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group13.AbpZeroTemplate.Web.Core
{
    public class Group13WebCoreModule : AbpModule
    {
        public Group13WebCoreModule() { }

        public override void Initialize()
        {
            
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group13WebCoreModule).GetAssembly());
        }
    }
}
