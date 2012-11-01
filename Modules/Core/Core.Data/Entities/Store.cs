using System.Collections.Generic;

namespace Core.Data.Entities
{
    public class Store
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual string HtmlBanner { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual bool IsPublished { get; set; }



        public Store()
        {
            Products = new List<Product>();
        }

        public virtual Product AddProduct(Product product)
        {
            product.Store = this;
            Products.Add(product);
            return product;
        }
        public virtual IList<Product> DeleteProduct(Product product)
        {
            product.Store = null;
            Products.Remove(product);
            return Products;
        }
        public virtual IList<Product> GetProducts()
        {
            return Products;
        }

    }
}