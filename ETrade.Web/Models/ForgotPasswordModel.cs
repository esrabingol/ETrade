//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ETrade.Web.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Mailadres alanı boş geçilemez!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Yeni şifrenizi giriniz!")]
        [MinLength(4,ErrorMessage ="Şifreniz en az 4 karakter uzunluğunda olmalıdır.")]
        [RegularExpression("^[0-9]+$", ErrorMessage ="Şifreniz sadece rakamlardan oluşmalıdır!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifrenizi tekrar giriniz!")]
        [Compare("NewPassword", ErrorMessage ="Yeni şifreler uyuşmuyor!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
