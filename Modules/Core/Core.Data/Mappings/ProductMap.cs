using Core.Data.Entities;
using FluentNHibernate.Mapping;

namespace Core.Data.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Price);
            Map(x => x.IsPublished);
            References(x => x.Store);
            References(x => x.Employee); 
            Map(x => x.HtmlBanner).CustomSqlType("nvarchar(max)");
            Map(x => x.HtmlDetail).CustomSqlType("ntext");
            Map(x => x.ImagePath);
            Map(x => x.HtmlBannerOrdered).CustomSqlType("nvarchar(max)"); 
            Map(x => x.HtmlBannerOrderedNot).CustomSqlType("nvarchar(max)");
            Map(x => x.HtmlBannerEdit).CustomSqlType("nvarchar(max)");
    

        }
    }
}