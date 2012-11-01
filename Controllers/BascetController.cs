using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TolokaStudio.Common;
using Core.Data.Repository.Interfaces;
using Core.Data.Entities;
using Core.Data.Repository;
using TolokaStudio.Models;
using System.Web.Security;

namespace TolokaStudio.Controllers
{

    public class BascetController : Controller
    {
        private readonly IRepository<User> UserRepository;

        public BascetController()
        {
            UserRepository = new Repository<User>();
        }

        public ActionResult Index()
        {
            BascetModel bascetModel = new BascetModel();
            var currentUser = base.ControllerContext.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(currentUser))
            {

                User user = UserRepository.Get(u => u.UserName == currentUser).SingleOrDefault();

                if (user == null)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {

                    bascetModel.Orders = user.Orders;
                    bascetModel.User = user;
                    bascetModel.Comments = user.Orders.Any()?user.Orders.LastOrDefault().Comments:"";  
                    return View(bascetModel);
                }             
            }

            return RedirectToAction("Index", "Product");
        }



    }
}
