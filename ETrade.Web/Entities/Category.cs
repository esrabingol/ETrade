using ETrade.Abstract;

namespace ETrade.Entities
{
    public class Category:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductCategory> productCategories { get; set; }
        public string CategoryName { get; set; }
        public string URL { get; set; }
    }
}
