using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TolokaStudio.Models
{
    public class ProductEditModel : ProductCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public override string ImagePath { get; set; }
    }
}