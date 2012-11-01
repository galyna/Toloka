using Core.Data.Entities;
using FluentNHibernate.Mapping;

namespace Core.Data.Mappings
{
    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.IsPublished);
            Map(x => x.HtmlBanner).CustomSqlType("nvarchar(max)");
            Map(x => x.ImagePath);
            HasMany(x => x.Products)
                .Cascade.All()
                .Inverse();
        }
    }
}