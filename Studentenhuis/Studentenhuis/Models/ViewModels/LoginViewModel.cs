using System.ComponentModel.DataAnnotations;

namespace Studentenhuis.Models.ViewModels
{
	/// <summary>
	/// Represents a strongly typed login view model.
	/// </summary>
	public class LoginViewModel
	{
		/// <summary>
		/// The username of the student account.
		/// </summary>
		[Required]
		[UIHint("Username")]
		public string Username { get; set; }

		/// <summary>
		/// The password of the student account.
		/// </summary>
		[Required]
		[UIHint("Password")]
		public string Password { get; set; }

		/// <summary>
		/// The URL of the view the student came from post-login.
		/// </summary>
		public string ReturnUrl { get; set; } = "/";
	}
}