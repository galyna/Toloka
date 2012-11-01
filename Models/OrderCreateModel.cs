using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Core.Data.Entities;

namespace SumkaWeb.Models
{
    public class OrderCreateModel
    {
        public virtual int Id { get; protected set; }
        public virtual Product Product { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int EmployeeId { get; set; }
        public virtual string ContactEmail { get; set; }
 
    }
}