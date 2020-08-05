using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group15.AbpZeroTemplate.Web.Core
{
    public class Group15WebCoreModule : AbpModule
    {
        public Group15WebCoreModule() { }

        public override void Initialize()
        {
            
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group15WebCoreModule).GetAssembly());
        }
    }
}
