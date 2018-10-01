using System;

namespace Studentenhuis.Models.ViewModels
{
	/// <summary>
	/// Represents a strongly typed meal button view model on the details view.
	/// </summary>
	public class MealButtonViewModel
	{
		/// <summary>
		/// Gets or sets a <see cref="Meal"/> object of the <see cref="MealButtonViewModel"/>.
		/// </summary>
		public Meal Meal { get; set; }

		/// <summary>
		/// Gets or sets the meal ID of the <see cref="MealButtonViewModel"/>.
		/// </summary>
		public int MealId { get; set; }

		/// <summary>
		/// Gets or sets the flag that determines the status of the <see cref="MealButtonViewModel"/>.
		/// </summary>
		public Status Status { get; set; }
	}

	/// <summary>
	/// Flags that determines the status of the <see cref="MealButtonViewModel"/> object.
	/// </summary>
	[Flags]
	public enum Status
	{
		Default = 0,
		LoginError = 1,
		IsCook = 2,
		IsGuest = 3,
		NoGuests = 4,
		Full = 5
	}
}