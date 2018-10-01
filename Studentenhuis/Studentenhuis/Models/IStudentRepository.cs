using System.Collections.Generic;

namespace Studentenhuis.Models
{
	/// <summary>
	/// Defines specific data operations on the students.
	/// </summary>
	public interface IStudentRepository
	{
		/// <summary>
		/// Gets a <see cref="IEnumerable{T}"/> that represents all students.
		/// </summary>
		IEnumerable<Student> Students { get; }
	}
}