using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data.Repository.Interfaces;
using Core.Data.Entities;
using Core.Data.Repository;

namespace TolokaStudio.Common
{
    public class TolokaAuthorizeAsAuthorAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (isAuthorized)
            {

                var currentUser = httpContext.User.Identity.Name;
                if (!CheckUser(currentUser))
                {
                    return false;
                }
            }
            return isAuthorized;
        }

        private bool CheckUser(string name)
        {
            IRepository<User> UserRepository = new Repository<User>();
            User user = UserRepository.Get(u => u.UserName == name).SingleOrDefault();
            if (user != null && user.Role.IsAuthor)
            {
                return true;
            }
            // If we got this far, something failed, redisplay form
            return false;
        }
    }

    public class TolokaAuthorizeAsAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (isAuthorized)
            {
                var currentUser = httpContext.User.Identity.Name;
                if (!CheckUser(currentUser))
                {
                    return false;
                }
            }
            return isAuthorized;
        }

        private bool CheckUser(string name)
        {
            IRepository<User> UserRepository = new Repository<User>();
            User user = UserRepository.Get(u => u.UserName == name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin)
            {
                return true;
            }
            // If we got this far, something failed, redisplay form
            return false;
        }
    }
    public class TolokaAuthorizeAsSimpleUserAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (isAuthorized)
            {
                var currentUser = httpContext.User.Identity.Name;
                if (!CheckUser(currentUser))
                {
                    return false;
                }
            }
            return isAuthorized;
        }

        private bool CheckUser(string name)
        {
            IRepository<User> UserRepository = new Repository<User>();
            User user = UserRepository.Get(u => u.UserName == name).SingleOrDefault();
            if (user != null)
            {
                return true;
            }
            // If we got this far, something failed, redisplay form
            return false;
        }
    }
}

