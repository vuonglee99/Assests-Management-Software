using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Group0.AbpZeroTemplate.Application;
using Group10.AbpZeroTemplate.Application;
using Group11.AbpZeroTemplate.Application;
using Group12.AbpZeroTemplate.Application;
using Group13.AbpZeroTemplate.Application;
using Group14.AbpZeroTemplate.Application;
using Group15.AbpZeroTemplate.Application;
using Group17.AbpZeroTemplate.Application;
using Group2.AbpZeroTemplate.Application;
using Group3.AbpZeroTemplate.Application;
using Group4.AbpZeroTemplate.Application;
using Group5.AbpZeroTemplate.Application;
using Group6.AbpZeroTemplate.Application;
using Group7.AbpZeroTemplate.Application;
using Group8.AbpZeroTemplate.Application;
using Group9.AbpZeroTemplate.Application;
using GSoft.AbpZeroTemplate.Authorization;
using GWebsite.AbpZeroTemplate.Application;

namespace GSoft.AbpZeroTemplate
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(AbpZeroTemplateCoreModule),
        typeof(Group0ApplicationModule),
        typeof(Group2ApplicationModule),
        typeof(Group3ApplicationModule),
        typeof(Group4ApplicationModule),
        typeof(Group5ApplicationModule),
        typeof(Group6ApplicationModule),
        typeof(Group7ApplicationModule),
        typeof(Group8ApplicationModule),
        typeof(Group9ApplicationModule),
        typeof(Group10ApplicationModule),
        typeof(Group11ApplicationModule),
        typeof(Group12ApplicationModule),
        typeof(Group13ApplicationModule),
        typeof(Group14ApplicationModule),
        typeof(Group15ApplicationModule),
        typeof(Group17ApplicationModule),
        typeof(GWebsiteApplicationModule)
        )]
    public class AbpZeroTemplateApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpZeroTemplateApplicationModule).GetAssembly());
        }
    }
}