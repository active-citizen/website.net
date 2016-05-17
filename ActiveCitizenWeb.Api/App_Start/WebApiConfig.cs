using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Converters;
using System.Globalization;
using ActiveCitizenWeb.Api.App_Start;
using Autofac.Integration.WebApi;

namespace ActiveCitizenWeb.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = IocConfig.RegisterDependencies();

            //GlobalConfiguration.Configuration.DependencyResolver = ;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            // JSON serialization
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
            };

            // Forcing datetime conversion to UTC format
            var isoDateTimeConverter = new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AdjustToUniversal };
            serializerSettings.Converters.Add(isoDateTimeConverter);

            // Forcing 'application/json' response content type
            var jsonMediaTypeFormatter = new JsonMediaTypeFormatter { SerializerSettings = serializerSettings };
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonMediaTypeFormatter));

            // Forcing camel case json serialization + ignoring nulls
            config.Formatters.JsonFormatter.SerializerSettings = serializerSettings;
        }

        private class JsonContentNegotiator : IContentNegotiator
        {
            private readonly MediaTypeFormatter jsonFormatter;

            public JsonContentNegotiator(MediaTypeFormatter formatter)
            {
                jsonFormatter = formatter;
            }

            public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
            {
                var result = new ContentNegotiationResult(jsonFormatter, new MediaTypeHeaderValue("application/json"));
                return result;
            }
        }
    }
}
