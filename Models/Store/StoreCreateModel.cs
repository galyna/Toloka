using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data.Entities;
using System.ComponentModel.DataAnnotations;
using ViewRes.Store;

using System.Web.Mvc;
using ModelRes.Store;
using ViewRes.Home;

namespace TolokaStudio.Models
{
    public class StoreCreateModel
    {
        [Required(ErrorMessageResourceName = "Required_Name",
         ErrorMessageResourceType = typeof(Create))]
        [Display(Name = "Name", ResourceType = typeof(Create))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required_Description",
         ErrorMessageResourceType = typeof(Create))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Required_Image",
         ErrorMessageResourceType = typeof(Home))]
        [HiddenInput(DisplayValue = false)]
        public virtual string ImagePath { get; set; }
         [AllowHtml]
        public string HtmlBanner { get; set; }
    }
}