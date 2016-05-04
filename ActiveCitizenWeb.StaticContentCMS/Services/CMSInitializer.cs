﻿using Autofac;
using AutoMapper;
using ActiveCitizen.Model.StaticContent.Faq;
using ActiveCitizenWeb.StaticContentCMS.ViewModel.Faq;
using ActiveCitizenWeb.StaticContentCMS.Configuration;
using ActiveCitizen.LDAP.IdentityProvider;

namespace ActiveCitizenWeb.StaticContentCMS.Services
{
    public static class CMSInitializer
    {
        public static void Initialize(ContainerBuilder builder)
        {
            builder.RegisterInstance(InitializeMapping()).SingleInstance();
            builder.Register(ctx => new LdapConnectionSettingsInitializer().Initialize()).As<ILdapConnectionSettings>().SingleInstance();
            builder.RegisterType<AppSettings>().AsImplementedInterfaces().SingleInstance();
        }

        static IMapper InitializeMapping()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FaqListItem, QuestionVM>();//.ForMember(vm => vm.CategoryId, li => li.MapFrom(vm => vm.Category.Id));
                    cfg.CreateMap<QuestionVM, FaqListItem>();//.ForMember(li => li.Category, conf => conf.ResolveUsing(vm => new FaqListCategory { Id = vm.CategoryId }));
                });
            return config.CreateMapper();
        }
        
    }
}