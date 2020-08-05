﻿using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace GSoft.AbpZeroTemplate.Localization
{
    public static class AbpZeroTemplateLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    AbpZeroTemplateConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AbpZeroTemplateLocalizationConfigurer).GetAssembly(),
                        "GSoft.AbpZeroTemplate.Localization.AbpZeroTemplate"
                    )
                )
            );

            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "UIT_AMS",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                         typeof(AbpZeroTemplateLocalizationConfigurer).GetAssembly(),
                        "GSoft.AbpZeroTemplate.Localization.UIT_AMS"
                    )
                )
            );
        }
    }
}