using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Core.Data.Entities;
using ModelRes.Account;

namespace TolokaStudio.Models
{
    public class VKAuthorization
    {
       public string Code { get; set; }
       public string Test { get; set; }
       public string AccesToken { get; set; }
       public string ExpiresIn { get; set; }
       public string UserId { get; set; }
       public string Error { get; set; }
       public string ErrorDescription { get; set; }
    }
    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceName = "Required_current_password",
                ErrorMessageResourceType = typeof(Account))]
        [DataType(DataType.Password)]
        [Display(Name = "Current_password", ResourceType = typeof(Account))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required_new_password",
                ErrorMessageResourceType = typeof(Account))]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "New_password", ResourceType = typeof(Account))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm_new_password", ResourceType = typeof(Account))]
        [Compare("NewPassword", ErrorMessageResourceName = "The_new_password_and_confirmation_password_do_not_match",
                ErrorMessageResourceType = typeof(Account))]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessageResourceName = "Required_user_name",
                ErrorMessageResourceType = typeof(Account))]
        [Display(Name = "User_name", ResourceType = typeof(Account))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Required_password",
                ErrorMessageResourceType = typeof(Account))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Account))]
        public string Password { get; set; }

      
        public bool Email { get; set; }

        [Display(Name = "Remember_me", ResourceType = typeof(Account))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceName = "Required_user_name",
                ErrorMessageResourceType = typeof(Account))]
        [Display(Name = "User_name", ResourceType = typeof(Account))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Required_email_address",
                ErrorMessageResourceType = typeof(Account))]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email_address", ResourceType = typeof(Account))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required_password",
                ErrorMessageResourceType = typeof(Account))]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Account))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm_password", ResourceType = typeof(Account))]
        [Compare("Password", ErrorMessageResourceName = "The_new_password_and_confirmation_password_do_not_match",
                ErrorMessageResourceType = typeof(Account))]
        public string ConfirmPassword { get; set; }
    }

    public class UsersModel
    {
        public IList<User> Users { get; set; }
    }
}
