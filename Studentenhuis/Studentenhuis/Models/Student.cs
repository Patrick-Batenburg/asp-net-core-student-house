using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Studentenhuis.Models
{
	/// <summary>
	/// Represents a strongly typed student object.
	/// </summary>
	public class Student : IdentityUser
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Student"/> class.
		/// </summary>
		public Student()
		{
			GuestAtMeals = new HashSet<Guest>();
			CookAtMeals = new HashSet<Meal>();
		}

		/// <summary>
		/// Gets or sets the first name of the student.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the middle name of the student.
		/// </summary>
		public string MiddleName { get; set; }

		/// <summary>
		/// Gets or sets the last name of the student.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="ICollection{T}"/> that represents the meals a student is a guest at.
		/// </summary>
		public virtual ICollection<Guest> GuestAtMeals { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="ICollection{T}"/> that represents the meals a student is a cook at.
		/// </summary>
		public virtual ICollection<Meal> CookAtMeals { get; set; }

		/// <summary>
		/// Gets the full name of the student.
		/// </summary>
		/// <returns>The full name of the student.</returns>
		public string FullName()
		{
			return $"{FirstName} {MiddleName} {LastName}";
		}
	}
}