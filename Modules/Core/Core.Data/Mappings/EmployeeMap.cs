using Core.Data.Entities;
using FluentNHibernate.Mapping;

namespace Core.Data.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Email);
            Map(x => x.HtmlBanner).CustomSqlType("nvarchar(max)");
            Map(x => x.HtmlDetail).CustomSqlType("ntext");
            Map(x => x.ImagePath);
            Map(x => x.HtmlBannerEdit).CustomSqlType("nvarchar(max)");
            Map(x => x.IsPublished);
        }
    }
}