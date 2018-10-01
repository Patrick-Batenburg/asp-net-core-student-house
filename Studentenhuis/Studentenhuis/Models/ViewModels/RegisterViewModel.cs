using System.ComponentModel.DataAnnotations;

namespace Studentenhuis.Models.ViewModels
{
	/// <summary>
	/// Represents a strongly typed register view model.
	/// </summary>
	public class RegisterViewModel
	{
		/// <summary>
		/// The first name of the student.
		/// </summary>
		[Required]
		[Display(Name = "First Name")]
		[StringLength(64, MinimumLength = 1, ErrorMessage = "First name cannot be longer than 64 characters")]
		public string FirstName { get; set; }

		/// <summary>
		/// The middle name of the student.
		/// </summary>
		[Display(Name = "Middle Name")]
		public string MiddleName { get; set; }

		/// <summary>
		/// The last name of the student.
		/// </summary>
		[Required]
		[Display(Name = "Last name")]
		[StringLength(64, MinimumLength = 1, ErrorMessage = "Last name cannot be longer than 64 characters")]
		public string LastName { get; set; }

		/// <summary>
		/// The username of the student account.
		/// </summary>
		[Required]
		[Display(Name = "Username")]
		[StringLength(64, ErrorMessage = "Username cannot be longer than 64 characters", MinimumLength = 1)]
		public string Username { get; set; }

		/// <summary>
		/// The email address of the student account.
		/// </summary>
		[Required]
		[Display(Name = "Email Address")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		/// <summary>
		/// The phone number of the student.
		/// </summary>
		[Required]
		[Display(Name = "Phone Number")]
		[Phone]
		[RegularExpression("^[0-9]{8}$", ErrorMessage = "Not a valid 8-digit NL phone number (must not include spaces or special characters)")]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// The password of the student account.
		/// </summary>
		[Required]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		[StringLength(128, MinimumLength = 8, ErrorMessage = "Password must be 8 characters long and cannot be longer than 128 characters")]
		public string Password { get; set; }

		/// <summary>
		/// The confirmation password of the student account.
		/// </summary>
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password does not match the confirmation password")]
		public string ConfirmPassword { get; set; }
	}
}