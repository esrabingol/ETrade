using System.ComponentModel.DataAnnotations;

namespace ETrade.Web.Models
{
	public class ProductModel
	{
		public int ProductId { get; set; }

		[Required(ErrorMessage = "Ürün İsmi Giriniz.")]
		[StringLength(40,MinimumLength =2)]
		public string ProductName { get; set; }

		[Required(ErrorMessage = "Ürün Resmi Seçiniz.")]
		public string ImageUrl { get; set; }

		[Required(ErrorMessage = "Ürün Açıklaması Giriniz.")]
		public string Description { get; set; }


        [Required(ErrorMessage = "Ürün Kategorisini Giriniz.")]
        public string ProductCategory { get; set; }

        [Required(ErrorMessage ="Ürün Fiyatını 1-100000 Aralığında Belirtiniz.")]
		[Range(1,100000)] //fiyat değer aralığı
		public decimal? ProductPrice { get; set; }

		[Required(ErrorMessage ="Personel No Giriniz.")]
		public int UserId { get; set; }

		

	}
}
