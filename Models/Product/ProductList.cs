using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data.Entities;

namespace TolokaStudio.Models
{
    public class ProductList
    {
      
        public  int[] ids { get; set; }
        public  List<Product> Products { get; set; }
    }
}
