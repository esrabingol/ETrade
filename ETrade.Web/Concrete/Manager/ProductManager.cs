using ETrade.Abstract;
using ETrade.Concrete.EntityFraemwork;
using ETrade.Context;
using ETrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ETrade.Concrete.Manager
{
    
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;
      public  ProductManager (IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Create(Product entity)
        {
            //ürün oluştur
            _productRepository.Create(entity);
        }

        public void Delete(Product entity)
        {
            //ürün sil
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            //tüm ürünleri al
           return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetPopularProducts()
        {
            return _productRepository.GetAll(p => p.ProductPrice > 2000); 
        }
        public void Update(Product entity)
        {
             _productRepository.Update(entity);
        }

  
    }
}
