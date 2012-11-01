using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Entities;
using FluentNHibernate.Mapping;

namespace Core.Data.Mappings
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.Id);
            Map(x => x.Email);
            Map(x => x.Comments).CustomSqlType("nvarchar(max)");
            Map(x => x.ProcessDateTime);
            References(x => x.Product);
            References(x => x.User).Cascade.Merge();
            Component(x => x.OrderStatus);

        }
    }
}
