using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Studentenhuis.Models.ViewModels
{
	/// <summary>
	/// Represents a strongly typed schedule view model.
	/// </summary>
	public class ScheduleViewModel
	{
		/// <summary>
		/// Provides the APIs for managing students in a persistence store.
		/// </summary>
		public UserManager<Student> UserManager;

		/// <summary>
		/// Gets or sets a <see cref="Meal"/> object for the <see cref="ScheduleViewModel"/>.
		/// </summary>
		public Meal Meal { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="Student"/> object for the <see cref="ScheduleViewModel"/>.
		/// </summary>
		public Student Student { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ScheduleViewModel"/> class.
		/// </summary>
		/// <param name="meal">The meal object.</param>
		/// <param name="student">The student object.</param>
		public ScheduleViewModel(IMealRepository meal, Student student)
		{
			if (student != null)
			{
				Student = student;
			}
			try
			{
				Meal = meal.Meals.Where(m => m.Date.Date.CompareTo(DateTime.Today.Date) == 0).First();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}