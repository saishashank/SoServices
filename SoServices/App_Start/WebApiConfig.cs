using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SoServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ValidateUserAPI",
                routeTemplate: "api/User/{action}/{login}/{password}",
                defaults: new
                {
                    controller = "UserDetails",
                    //action = "ValidateUser",
                    login = RouteParameter.Optional,
                    password = RouteParameter.Optional
                }
            );

            //config.Routes.MapHttpRoute(
            //    name: "ValidateUserAPI",
            //    routeTemplate: "api/User/{userDetail}",
            //    defaults: new
            //    {
            //        controller = "UserDetails",
            //        action = "RegisterUser",
            //        userDetail = RouteParameter.Optional
            //    }
            //);
        }
    }
}
