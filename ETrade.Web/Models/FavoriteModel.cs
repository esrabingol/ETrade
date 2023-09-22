namespace ETrade.Web.Models
{
    public class FavoriteModel
    {
        public int FavoriteId { get; set; }
        public List<FavoriteItemModel> favoriteItems { get; set; }
    }

    public class FavoriteItemModel
    {
        public int FavoriteItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

    }
}
