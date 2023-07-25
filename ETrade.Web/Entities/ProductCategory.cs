using Microsoft.EntityFrameworkCore;

namespace ETrade.Entities
{
    
    public class ProductCategory
    {

        public int CategoryId { get; set; }
        public Category category { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }

    }
}
