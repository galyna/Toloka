using System.Collections.Generic;
namespace Core.Data.Entities
{
    public class Employee
    {
        public virtual int Id { get; protected set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string HtmlBanner { get; set; }
        public virtual string HtmlDetail { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string HtmlBannerEdit { get; set; }
        public virtual bool IsPublished { get; set; }
        public Employee()
        {
            HtmlDetail = "";
            HtmlBanner = "";
            HtmlBannerEdit = "";
          
        }
    }
}