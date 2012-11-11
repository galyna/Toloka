using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TolokaStudio.Models
{
    public class ProductDetail
    {
         [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

         [AllowHtml]
         public string HtmlDetail { get; set; }
    }
}