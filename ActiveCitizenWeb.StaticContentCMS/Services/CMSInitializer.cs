using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using ActiveCitizenWeb.StaticContentCMS.Controllers;
using AutoMapper;
using ActiveCitizen.Model.StaticContent.FAQ;
using ActiveCitizenWeb.StaticContentCMS.ViewModel.FAQ;

namespace ActiveCitizenWeb.StaticContentCMS.Services
{
    public static class CMSInitializer
    {
        public static void Initialize(ContainerBuilder builder)
        {
            builder.RegisterType<FAQController>().AsSelf();
            builder.RegisterType<FaqListItemsController>().AsSelf();
            builder.RegisterInstance(InitializeMapping()).SingleInstance();
        }

        static IMapper InitializeMapping()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FaqListItem, QuestionVM>();
                    cfg.CreateMap<QuestionVM, FaqListItem>();
                });
            return config.CreateMapper();
        }
        
    }
}