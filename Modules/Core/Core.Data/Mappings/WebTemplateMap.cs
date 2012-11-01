using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Mapping;

namespace Core.Data.Entities
{
   public class WebTemplateMap: ClassMap<WebTemplate>
    {
       public WebTemplateMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Html).CustomSqlType("nvarchar(max)");
        }
    }
}
