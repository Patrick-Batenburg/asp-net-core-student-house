using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Studentenhuis.Models
{
	/// <summary>
	/// A default implementation of <see cref="IStudentRepository"/> that provides specific data operations on the students.
	/// </summary>
	public class StudentRepository : IStudentRepository
	{
		private ApplicationDbContext _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="StudentRepository"/> class.
		/// </summary>
		/// <param name="db">A <see cref="DbContext"/> class for the Entity Framework database context used for identity.</param>
		public StudentRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// Gets a <see cref="IEnumerable{T}"/> that represents all students.
		/// </summary>
		IEnumerable<Student> IStudentRepository.Students => _db.Students.Include("CookAtMeals").Include("GuestAtMeals.Meal").ToList();
	}
}