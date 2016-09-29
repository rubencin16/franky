using Owin;
using System.Web.Http;

namespace SFContent
{
    public static class Startup
    {
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            
            config.Routes.MapHttpRoute(
                name: "MallAPI",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            appBuilder.UseWebApi(config);
        }
    }
}
