using System.ComponentModel.DataAnnotations;

namespace ETrade.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "İsim alanı boş geçilemez")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        
        public string Password { get; set; }


    }
}
