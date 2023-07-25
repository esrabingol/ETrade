using ETrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETrade.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        List<Product> GetAll();
        List<Product> GetPopularProducts();

        void Create(Product entity);
        void Update(Product entit);
        void Delete(Product entity);

    }
}
