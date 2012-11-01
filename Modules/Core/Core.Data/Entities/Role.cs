namespace Core.Data.Entities
{
    public class Role
    {
        public virtual int Id { get; protected set; }
        public virtual bool IsAdmin { get; set; }
        public virtual bool IsAuthor { get; set; }
    }
}