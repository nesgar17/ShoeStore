﻿namespace ShoeStore
{
    using ShoeStore.Classes;
    using System.Data.Entity;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            checkRolesAndSuperUser();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.DataContext, Migrations.Configuration>());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void checkRolesAndSuperUser()
        {
            UsersHelper.CheckRole("Admin");
            UsersHelper.CheckRole("Owner");            
            UsersHelper.CheckRole("Cliente");
            UsersHelper.CheckRole("Directivo");
            UsersHelper.CheckRole("Gerente");
            UsersHelper.CheckRole("Zapateria");
            UsersHelper.CheckSuperUser();
        }
    }
}
