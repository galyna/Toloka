using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Core.Data.Entities
{
    public class User
    {
        public virtual int Id { get; protected set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Role Role { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual IList<Order> Orders { get; set; }
        public virtual IList<Order> OrdersHistory { get; set; }
        public virtual IList<Product> Products { get; set; }
        public User()
        {
            Role = new Role();
            Orders= new List<Order>();
            OrdersHistory = new List<Order>();
        }
    
        public virtual Order AddOrder(Order order)
        {
          Orders.Add(order);
          order.User = this;
            return order;
        }
        public virtual Order AddOrdersHistory(Order order)
        {
            OrdersHistory.Add(order);
            return order;
        }
        public virtual void CleanOrders()
        {
            Orders.Clear();
        }
        public virtual Order DeleteOrder(Order order)
        {
     
            order.User = null;
            Orders.Remove(order);
            return order;
        }
    }
}
