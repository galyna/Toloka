using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data.Entities;

namespace TolokaStudio.Models
{
    public class HomeModel
    {

        public IList<Store> Stores { get; set; }

    }
}