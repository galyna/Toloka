using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data.Entities;

namespace TolokaStudio.Models
{
    public class StoreIndexModel
    {
        public IList<User> Users { get; set; }
        public IList<Store> Stores { get; set; }
        public IList<Employee> Employees { get; set; }
    }
}