using Studentenhuis.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studentenhuis.Models
{
	/// <summary>
	/// Represents a strongly typed meal object.
	/// </summary>
	public class Meal
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Meal"/> class.
		/// </summary>
		public Meal()
		{
			Guests = new HashSet<Guest>();
		}

		/// <summary>
		/// Gets or sets the ID of the meal.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets the title of the meal.
		/// </summary>
		[Required(ErrorMessage = "Cannot be blank")]
		[MaxLength(128)]
		public string Title { get; set; }

		/// <summary>
		/// Gets the description of the meal.
		/// </summary>
		[MaxLength(2000, ErrorMessage = "Cannot exceed the maximum length of 2000 characters")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		/// <summary>
		/// Gets the date of the meal.
		/// </summary>
		[Required(AllowEmptyStrings = false, ErrorMessage = "Enter a date in the dd/mm/yyyy hh:mm format")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
		[RestrictedDate(ErrorMessage = "Date occurs in the past")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="Student"/> object that represents the cook of the meal.
		/// </summary>
		public Student Cook { get; set; }

		/// <summary>
		/// Gets the max amount of guests of the meal.
		/// </summary>
		[Display(Name = "Max amount of guests")]
		[Range(1, int.MaxValue, ErrorMessage = "Cannot be 0")]
		public int MaxGuests { get; set; }

		/// <summary>
		/// Gets the price of the meal of the meal.
		/// </summary>
		[Required(ErrorMessage = "Enter a price")]
		[Range(0, int.MaxValue, ErrorMessage = "Cannot be a negative value")]
		public double Price { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="ICollection{T}"/> that represents the guests at the meal.
		/// </summary>
		public virtual ICollection<Guest> Guests { get; set; }
	}
}