using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data.Entities;
using System.ComponentModel.DataAnnotations;
using ModelRes.Product;
using ViewRes.Home;
using System.Web.Mvc;



namespace TolokaStudio.Models
{
    public class ProductCreateModel
    {
        [Required(ErrorMessageResourceName = "Required_Name",
                ErrorMessageResourceType = typeof(ProductCreate))]
        [Display(Name = "Name", ResourceType = typeof(ProductCreate))]
        public string Name { get; set; }
        public string HtmlBanner { get; set; }
        [Required(ErrorMessageResourceName = "Required_Price",
            ErrorMessageResourceType = typeof(ProductCreate))]
        [Display(Name = "Price", ResourceType = typeof(ProductCreate))]
        public double Price { get; set; }
        [Required(ErrorMessageResourceName = "Required_Image",
        ErrorMessageResourceType = typeof(Home))]
        public virtual string ImagePath { get; set; }
        public int StoreID { get; set; }
        public string HtmlDetail { get; set; }
        public int EmployeeId { get; set; }
    }
}