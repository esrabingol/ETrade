using ETrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace ETrade.Abstract
{
    public interface ICategoryService
    {
        List<Category> categories();
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);


    }
}
