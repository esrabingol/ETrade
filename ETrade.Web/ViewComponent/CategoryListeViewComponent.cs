using Microsoft.AspNetCore.Mvc;

namespace ETrade.Entities
{
	public class CategoryListeViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
	{
		public IViewComponentResult Invoke(string categoryName)
		{
			List<Category> categories = new List<Category>
			{
				new Category(){CategoryName="LivingRoom",URL= "LivingRoom"},
				new Category(){CategoryName="BathRoom", URL="BathRoom"},
				new Category(){CategoryName="BedRoom",URL="BedRoom"},
				new Category(){CategoryName="Kitchen", URL = "Kitchen"}

			};
			return View(categories);
		}
	}
}

