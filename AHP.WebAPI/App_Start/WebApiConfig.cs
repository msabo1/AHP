using AHP.Model;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
namespace AHP.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Registers all modules from referenced assemblies and all modules in WebAPI
            var builder = new ContainerBuilder();


            builder.Register(ctx => new MapperConfiguration(cfg =>
            { cfg.AddProfile(new ModelMapperProfile()); }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();

            //var path = AppDomain.CurrentDomain.BaseDirectory;
            //Assembly[] assemblies = Directory.GetFiles(path, "AHP.*.dll").Select(Assembly.LoadFrom).ToArray();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.ToString().StartsWith("AHP."));
            builder.RegisterAssemblyModules(assemblies.ToArray());

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}