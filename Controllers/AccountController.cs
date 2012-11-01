using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TolokaStudio.Models;
using Core.Data.Entities;
using Core.Data.Repository;
using Core.Data.Repository.Interfaces;
using System.Security.Cryptography;
using System.Text;
using TolokaStudio.Common;
using System.Security.Principal;

namespace TolokaStudio.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> UserRepository;
        private readonly IRepository<Employee> EmployeeRepository;
        private const string DefaulAuthorCabinet = "\\Employee\\Cabinet";
        private const string DefaulAdminCabinet = "\\Store\\Index";

        public AccountController()
        {
            UserRepository = new Repository<User>();
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult CreateUser(UserCreate model)
        {
            User userdb = UserRepository.Get(u => u.UserName == model.Email).SingleOrDefault();
            if (userdb == null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false/* createPersistentCookie */);
                User user = new User()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Password = EncodePassword(model.Password)
                };
                UserRepository.SaveOrUpdate(user);
                if (model.Email == "galynavistovska@gmail.com")
                {
                    user.Role.IsAdmin = true;
                    UserRepository.SaveOrUpdate(user);
                    return Json(DefaulAdminCabinet);
                }
            }
            else if (CheckPassword(userdb.Password, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Email, false/* createPersistentCookie */);
                userdb.Orders = null;
                UserRepository.SaveOrUpdate(userdb);
                if (userdb.Role.IsAdmin)
                {
                    return Json(DefaulAdminCabinet);
                }
                if (userdb.Role.IsAuthor)
                {
                    return Json(DefaulAuthorCabinet);
                }

            }
            return Json("\\");
        }
        [HttpGet]
        public ActionResult VKAuthorization(string code = "")
        {
            if (code != "")
            {
                string VKAuthURL = String.Format("https://oauth.vk.com/access_token?client_id=3204168&client_secret=Fjtxbc3pUBKGIEPt8mVY&code={0}&redirect_uri=http://tolokastudio.pp.ua/Account/VKAuthorization&", code);
                return Redirect(VKAuthURL);
            }
            return null;
        }
        [HttpPost]
        public ActionResult VKAuthorization( VKAuthorizationModel model)
        {
                return View(model);
        }



        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        private User GetUser(string username, string password)
        {
            User user = UserRepository.Get(u => u.UserName == username && u.Password == EncodePassword(password)).SingleOrDefault();

            return user;

        }
        /// <summary>
        /// Check the password 
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="dbpassword"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;

            pass2 = EncodePassword(dbpassword);

            if (pass1 == pass2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Encode password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>Encoded password.</returns>
        private string EncodePassword(string password)
        {
            string passwordEncoded = password;

            HMACSHA1 hash = new HMACSHA1();
            hash.Key = HexToByte("12");
            passwordEncoded =
                Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));

            return passwordEncoded;
        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        #endregion
    }
}
