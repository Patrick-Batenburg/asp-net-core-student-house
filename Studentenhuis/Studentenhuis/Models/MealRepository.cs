using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Studentenhuis.Models
{
	/// <summary>
	/// A default implementation of <see cref="IMealRepository"/> that provides specific data operations on the meals.
	/// </summary>
	public class MealRepository : IMealRepository
	{
		private ApplicationDbContext _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="MealRepository"/> class.
		/// </summary>
		/// <param name="db">A <see cref="DbContext"/> class for the Entity Framework database context used for identity.</param>
		public MealRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// Gets a <see cref="IEnumerable{T}"/> that represents all meals.
		/// </summary>
		IEnumerable<Meal> IMealRepository.Meals => _db.Meals.Include("Cook").Include("Guests.Student").OrderBy(m => m.Date).ToList();

		/// <summary>
		/// Creates a new meal in the database.
		/// </summary>
		/// <param name="meal">The meal to create.</param>
		/// <returns><see langword="True"/> if it succeeded to create an new meal in the database; otherwise <see langword="False"/>.</returns>
		public bool? Create(Meal meal)
		{
			bool? result = null;

			if (_db.Meals.Where(m => m.Date.Date.Equals(meal.Date.Date)).Count() < 1)
			{
				if (meal.Price >= 0 && !string.IsNullOrEmpty(meal?.Title) && meal.Date > DateTime.Now)
				{
					if (string.IsNullOrEmpty(meal?.Description))
					{
						meal.Description = "None.";
					}

					_db.Meals.Add(meal);
					result = _db.SaveChanges() == 1;
				}
				else
				{
					result = false;
				}
			}

			return result;
		}

		/// <summary>
		/// Updates an existing meal in the database.
		/// </summary>
		/// <param name="meal">The meal to update.</param>
		/// <returns><see langword="True"/> if it succeeded to update an existing meal in the database; otherwise <see langword="False"/>.</returns>
		public bool Update(Meal meal)
		{
			bool result = false;
			Meal model = _db.Meals.Where(m => m.Id == meal.Id).FirstOrDefault();

			if (!string.IsNullOrEmpty(meal?.Title) && model?.Guests?.Count() < 1 && meal.Price >= 0)
			{
				model.Title = meal.Title;
				model.MaxGuests = meal.MaxGuests;
				model.Price = meal.Price;

				if (!string.IsNullOrEmpty(meal?.Description))
				{
					model.Description = meal.Description;
				}
				else
				{
					model.Description = "None.";
				}

				result = _db.SaveChanges() == 1;
			}

			return result;
		}

		/// <summary>
		/// Deletes an existing meal in the database.
		/// </summary>
		/// <param name="mealId">The ID of the meal to delete.</param>
		/// <returns><see langword="True"/> if it succeeded to delete an existing meal in the database; otherwise <see langword="False"/>.</returns>
		public bool Delete(int mealId)
		{
			bool result = false;
			Meal meal = _db.Meals.Where(m => m.Id == mealId).FirstOrDefault();

			if (meal != null && meal?.Guests?.Count() < 1)
			{
				_db.Remove(meal);
				result = _db.SaveChanges() == 1;
			}

			return result;
		}

		/// <summary>
		/// Joins an existing meal as guest in the database.
		/// </summary>
		/// <param name="mealId">The ID of the meal to join.</param>
		/// <param name="studentId">The ID of the student.</param>
		/// <returns><see langword="True"/> if it succeeded to join an existing meal as guest in the database; otherwise <see langword="False"/>.</returns>
		public bool Join(int mealId, string studentId)
		{
			bool result = false;
			Meal meal = _db.Meals.Where(m => m.Id == mealId).FirstOrDefault();

			if (meal?.Guests?.Count() < meal?.MaxGuests)
			{
				_db.Guests.Add(new Guest()
				{
					StudentId = studentId,
					MealId = mealId
				});

				result = _db.SaveChanges() == 1;
			}

			return result;
		}

		/// <summary>
		/// Leaves an existing meal as guest in the database.
		/// </summary>
		/// <param name="mealId">The ID of the meal to leave.</param>
		/// <param name="studentId">The ID of the student.</param>
		/// <returns><see langword="True"/> if it succeeded to leave an existing meal as guest in the database; otherwise <see langword="False"/>.</returns>
		public bool Leave(int mealId, string studentId)
		{
			bool result = false;
			Guest guest = _db.Guests.Where(s => s.StudentId == studentId).Where(m => m.MealId == mealId).FirstOrDefault();

			if (guest != null)
			{
				_db.Guests.Remove(guest);
				result = _db.SaveChanges() == 1;
			}

			return result;
		}
	}
}