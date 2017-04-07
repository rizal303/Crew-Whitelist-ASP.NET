using System.Web.Optimization;
using System.Web.Routing;

namespace CrewWhitelistApps
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BudleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
