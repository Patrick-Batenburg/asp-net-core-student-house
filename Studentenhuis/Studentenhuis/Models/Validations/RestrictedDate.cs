using System;
using System.ComponentModel.DataAnnotations;

namespace Studentenhuis.Models.Validations
{
	/// <summary>
	/// Specifies the minimum date of a <see cref="DateTime"/> object allowed in a property.
	/// </summary>
	public class RestrictedDate : ValidationAttribute
	{
		/// <summary>
		/// Determines whether a specified object is valid.
		/// </summary>
		/// <param name="value">The object to validate.</param>
		/// <returns><see langword="True"/> if the value is greater than <see cref="DateTime.Now"/>; otherwise, <see langword="False"/>.</returns>
		public override bool IsValid(object value)
		{
			DateTime date = (DateTime)value;
			return date > DateTime.Now;
		}
	}
}