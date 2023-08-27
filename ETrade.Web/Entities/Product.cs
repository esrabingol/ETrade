using ETrade.Abstract;
using System.Collections.Generic;

namespace ETrade.Entities
{
	public class Product : IEntity
	{
		
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }
		public decimal? ProductPrice { get; set; }
		public string ProductCategory { get; set; }

		public int UserId { get; set; }
		public List<ProductCategory> productCategories { get; set; }
		
	}
}
