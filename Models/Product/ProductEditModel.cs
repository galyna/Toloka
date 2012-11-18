using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ModelRes.Product;
using ViewRes.Home;

namespace TolokaStudio.Models
{
    public class ProductEditModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required_Name",
                ErrorMessageResourceType = typeof(ProductCreate))]
        [Display(Name = "Name", ResourceType = typeof(ProductCreate))]
        public string Name { get; set; }

        [AllowHtml]
        public string HtmlBannerEdit { get; set; }

        [AllowHtml]
        public string HtmlBanner { get; set; }

        [AllowHtml]
        public string HtmlDetail { get; set; }

        [AllowHtml]
        public string HtmlSmallBanner { get; set; }


        [Required(ErrorMessageResourceName = "Required_Price",
            ErrorMessageResourceType = typeof(ProductCreate))]
        [Display(Name = "Price", ResourceType = typeof(ProductCreate))]
        public double Price { get; set; }


        public virtual string ImageMediumPath { get; set; }

        public virtual string ImageSmallPath { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int StoreID { get; set; }


        [HiddenInput(DisplayValue = false)]
        public int EmployeeId { get; set; }
    }
}