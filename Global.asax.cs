using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using FluentNHibernate.Automapping;
using System.Web.Security;
using System.Security.Principal;
using Core.Data.Repository.Interfaces;
using Core.Data.Repository;
using Core.Data.Entities;


namespace TolokaStudio
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(Object sender,
EventArgs e)
{
  if (HttpContext.Current.User != null)
  {
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
     if (HttpContext.Current.User.Identity is FormsIdentity)
     {
        FormsIdentity id =
            (FormsIdentity)HttpContext.Current.User.Identity;
        FormsAuthenticationTicket ticket = id.Ticket;

        // Get the stored user-data, in this case, our roles
        IRepository<User> UserRepository = new Repository<User>();
        User user = UserRepository.Get(u => u.UserName == HttpContext.Current.User.Identity.Name).SingleOrDefault();
       
        string userData = "";
        if (user != null && user.Role.IsAuthor)
        {
            userData = "Author";
        }
        if (user != null && user.Role.IsAdmin)
        {
            userData = "Admin";
        }
        string[] roles = userData.Split(',');
        HttpContext.Current.User = new GenericPrincipal(id, roles);
     }
    }
  }
}
    }
}
