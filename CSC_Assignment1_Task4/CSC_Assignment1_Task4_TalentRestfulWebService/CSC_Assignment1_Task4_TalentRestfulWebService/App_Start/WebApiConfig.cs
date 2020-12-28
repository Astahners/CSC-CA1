using CSC_Assignment1_Task4_TalentRestfulWebService.Controllers;
using CSC_Assignment1_Task4_TalentRestfulWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace CSC_Assignment1_Task4_TalentRestfulWebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

            //var container = new UnityContainer();
            //container.RegisterType<ITalentRepository, TalentRepository>(new HierarchicalLifetimeManager());
            //config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
