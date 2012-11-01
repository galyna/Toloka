using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data.Entities;

namespace TolokaStudio.Models
{
    public class BascetModel
    {
        public IList<Order> Orders { get; set; }
        public string Comments { get; set; }
        public User User { get; set; }
    }
   
}