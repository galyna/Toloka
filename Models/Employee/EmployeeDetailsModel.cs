using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ModelRes.Employee;
using System.Web.Mvc;

namespace TolokaStudio.Models
{
    public class DetailsModel
    {
        [Display(Name = "HtmlDetail", ResourceType = typeof(EmployeeCreate))]
        [AllowHtml]
        public string HtmlDetail { get; set; }
        public int Id { get; set; }
    }
}