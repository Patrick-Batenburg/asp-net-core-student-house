using System.ComponentModel.DataAnnotations.Schema;

namespace Studentenhuis.Models
{
	/// <summary>
	/// Represents a strongly typed guest object.
	/// </summary>
	public class Guest
	{
		/// <summary>
		/// Gets or sets a <see cref="Student"/> object as guest.
		/// </summary>
		public Student Student { get; set; }

		/// <summary>
		/// Gets or sets the student ID of the guest.
		/// </summary>
		public string StudentId { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="Meal"/> object of the guest.
		/// </summary>
		public Meal Meal { get; set; }

		/// <summary>
		/// Gets or sets the meal ID of the guest.
		/// </summary>
		public int MealId { get; set; }
	}
}