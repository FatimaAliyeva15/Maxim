using System.ComponentModel.DataAnnotations;

namespace Maxim.ViewModel
{
	public class RegisterVM
	{
		[Required]
		[MinLength(3)]
		[MaxLength(20)]
		public string Name { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(20)]
		public string Surname { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(20)]
		public string UserName { get; set; }
		[Required]
		[MinLength(7)]
		[MaxLength(50)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[MinLength(7)]
		[MaxLength(100)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password), Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
