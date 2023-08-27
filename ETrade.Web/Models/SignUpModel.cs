using System.ComponentModel.DataAnnotations;

namespace ETrade.Models
{
	public class SignUpModel 
	{  //kullanıcıdan alınacak bilgiler

		[Required(ErrorMessage ="İsim alanı boş geçilemez")]
		public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş geçilemez")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Mailadres alanı boş geçilemez")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Numarası alanı boş geçilemez")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
