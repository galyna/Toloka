using Core.Data.Entities;
using FluentNHibernate.Mapping;

namespace Core.Data.Mappings
{
    public class RoleMap : ComponentMap<Role>
    {
        public RoleMap()
        {
           // Id(x => x.Id);
            Map(x => x.IsAuthor);
            Map(x => x.IsAdmin);
        }
    }
}