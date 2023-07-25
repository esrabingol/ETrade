using System.ComponentModel.DataAnnotations;

namespace ETrade.Models
{
	public class Person
	{
		[Required(ErrorMessage = "Name can't be empty!")]
		public string Name { get; set; }
		[Required(ErrorMessage = "can't be empty!")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "can't be empty!")]
		public string Password { get; set; }
		[Required(ErrorMessage = "can't be empty!")]

		[Compare("Password")]
		[DataType(DataType.Password)]
		public string Repassword { get; set; }
		[Range(typeof(bool), "true", "true", ErrorMessage = "Have to confirm assegment")]
		public bool StatementConfirm { get; set; }
	}
}
