using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Core.Data.Entities;

namespace Core.Data.Mappings
{
   public class OrderStatusMap: ComponentMap<OrderStatus>
    {
       public OrderStatusMap()
        {
           // Id(x => x.Id);
            Map(x => x.IsDone);
            Map(x => x.IsInProgress);
        }
    }
}
