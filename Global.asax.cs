using Barker.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Barker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
        //    {
        //        Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"]
        //    + HttpContext.Current.Request.RawUrl);
        //    }
        //}
        protected void Application_Start()
        {
            Database.SetInitializer<BarkerData>(null);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
      
    }
}
