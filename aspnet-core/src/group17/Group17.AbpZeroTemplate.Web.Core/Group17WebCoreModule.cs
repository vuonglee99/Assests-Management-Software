using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Group17.AbpZeroTemplate.Web.Core
{
    public class Group17WebCoreModule : AbpModule
    {
        public Group17WebCoreModule() { }

        public override void Initialize()
        {
            
        }

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(Group17WebCoreModule).GetAssembly());
        }
    }
}
