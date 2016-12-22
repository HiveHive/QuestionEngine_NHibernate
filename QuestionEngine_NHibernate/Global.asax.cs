using System.Web.Mvc;
using System.Web.Routing;
using QuestionEngine_NHibernate.Models.DataAccess;

namespace QuestionEngine_NHibernate
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DatabaseManager.Instance.Initialize(new DefaultDatabaseInitializer());
        }
    }
}
