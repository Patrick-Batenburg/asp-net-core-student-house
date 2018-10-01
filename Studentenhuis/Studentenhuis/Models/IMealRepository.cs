using System.Collections.Generic;

namespace Studentenhuis.Models
{
	/// <summary>
	/// Defines specific data operations on the meals.
	/// </summary>
	public interface IMealRepository
	{
		/// <summary>
		/// Gets a <see cref="IEnumerable{T}"/> that represents all meals.
		/// </summary>
		IEnumerable<Meal> Meals { get; }

		/// <summary>
		/// Creates a new meal in the database.
		/// </summary>
		/// <param name="meal">The meal to create.</param>
		/// <returns><see langword="True"/> if it succeeded to create an new meal in the database; otherwise <see langword="False"/>.</returns>
		bool? Create(Meal meal);

		/// <summary>
		/// Updates an existing meal in the database.
		/// </summary>
		/// <param name="meal">The meal to update.</param>
		/// <returns><see langword="True"/> if it succeeded to update an existing meal in the database; otherwise <see langword="False"/>.</returns>
		bool Update(Meal meal);

		/// <summary>
		/// Deletes an existing meal in the database.
		/// </summary>
		/// <param name="mealId">The ID of the meal to delete.</param>
		/// <returns><see langword="True"/> if it succeeded to delete an existing meal in the database; otherwise <see langword="False"/>.</returns>
		bool Delete(int mealId);

		/// <summary>
		/// Joins an existing meal as guest in the database.
		/// </summary>
		/// <param name="mealId">The ID of the meal to join.</param>
		/// <param name="studentId">The ID of the student.</param>
		/// <returns><see langword="True"/> if it succeeded to join an existing meal as guest in the database; otherwise <see langword="False"/>.</returns>
		bool Join(int mealId, string studentId);

		/// <summary>
		/// Leaves an existing meal as guest in the database.
		/// </summary>
		/// <param name="mealId">The ID of the meal to leave.</param>
		/// <param name="studentId">The ID of the student.</param>
		/// <returns><see langword="True"/> if it succeeded to leave an existing meal as guest in the database; otherwise <see langword="False"/>.</returns>
		bool Leave(int mealId, string studentId);
	}
}