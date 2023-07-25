using ETrade.Abstract;
using ETrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace ETrade.Concrete.Memory
{
    public class MemoryProductRespository : IProductRepository
    {
        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            var products = new List<Product>()
            {
                new Product() {ProductId=1, ProductName= "Hasır Sepet",ImageUrl="sepet.jpg",ProductPrice=150},
                new Product() {ProductId=2, ProductName= "Dekoratif Masa Süsü",ImageUrl="minyat.jpg",ProductPrice=2500},
                new Product() {ProductId=3, ProductName= "Halı ",ImageUrl="hali.jpg",ProductPrice=4500}

            };
            return products;

        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetOne(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetPopulerProducts()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
