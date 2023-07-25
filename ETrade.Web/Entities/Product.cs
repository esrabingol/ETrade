using ETrade.Abstract;

namespace ETrade.Entities
{
	public class Product : IEntity
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ImageUrl { get; set; }
		public decimal ProductPrice { get; set; }
		public List<ProductCategory> productCategories { get; set; }
		public int UserId { get; set; }
	}
}
