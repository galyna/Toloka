using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data.Entities;
using System.Web.Mvc;

namespace TolokaStudio.Models
{
    public class StoreEditModel : StoreCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public IList<Product> Products { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public override string ImagePath { get; set; }
    }
}