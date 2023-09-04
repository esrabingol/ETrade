using ETrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ETrade.Abstract
{
    //IRepository içindeki tüm özelliklere sahip
    public interface IProductRepository :IRepository<Product>
    {

        IEnumerable<Product> GetPopulerProducts();
      

    }
}
