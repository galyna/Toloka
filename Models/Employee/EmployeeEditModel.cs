using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ModelRes.Employee;
using ViewRes.Home;
using Core.Data.Entities;

namespace TolokaStudio.Models
{
    public class EmployeeEditModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required_FirstName",
               ErrorMessageResourceType = typeof(EmployeeCreate))]
        [Display(Name = "FirstName", ResourceType = typeof(EmployeeCreate))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required_LastName",
               ErrorMessageResourceType = typeof(EmployeeCreate))]
        [Display(Name = "LastName", ResourceType = typeof(EmployeeCreate))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "Required_Image",
       ErrorMessageResourceType = typeof(Home))]

        [UIHint("ProfileImage")]
        public virtual string ImagePath { get; set; }

        [Required(ErrorMessageResourceName = "Required_Email",
               ErrorMessageResourceType = typeof(EmployeeCreate))]
        [Display(Name = "Email", ResourceType = typeof(EmployeeCreate))]
        public virtual string Email { get; set; }
        [HiddenInput(DisplayValue = false)]

        [AllowHtml]
        public string HtmlBannerEdit { get; set; }
        [AllowHtml]
        public string HtmlBanner { get; set; }

        [Display(Name = "HtmlDetail", ResourceType = typeof(EmployeeCreate))]
        public string HtmlDetail { get; set; }

        public List<Product> Products { get; set; }

    
    }
}