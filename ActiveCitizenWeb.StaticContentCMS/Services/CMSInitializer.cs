using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using ActiveCitizenWeb.StaticContentCMS.Controllers;
using AutoMapper;
using ActiveCitizen.Model.StaticContent.Faq;
using ActiveCitizenWeb.StaticContentCMS.ViewModel.Faq;

namespace ActiveCitizenWeb.StaticContentCMS.Services
{
    public static class CMSInitializer
    {
        public static void Initialize(ContainerBuilder builder)
        {
            builder.RegisterType<FaqListController>().AsSelf();
            builder.RegisterInstance(InitializeMapping()).SingleInstance();
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