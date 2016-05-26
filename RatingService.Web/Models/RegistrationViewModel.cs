using System.ComponentModel.DataAnnotations;

namespace RatingService.Web.Models
{
	public class RegistrationViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Compare("Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Director { get; set; }

		[Required]
		[MaxLength(7)]
		[MinLength(7)]
		public string KeyIdentifier { get; set; }
	}
}