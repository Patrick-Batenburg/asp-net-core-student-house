using Microsoft.AspNetCore.Mvc;
using Studentenhuis.Models;
using Studentenhuis.Models.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;

namespace Studentenhuis.Components
{
	/// <summary>
	/// A class for view component that renders the buttons on the meal details view.
	/// </summary>
	public class MealButtonViewComponent : ViewComponent
	{
		/// <summary>
		/// Executes a result type of <see cref="ViewComponent"/> to render.
		/// </summary>
		/// <param name="meal">The meal object.</param>
		/// <returns>A view of the <see cref="ViewComponent"/>.</returns>
		public IViewComponentResult Invoke(Meal meal)
		{
			ClaimsPrincipal identity = HttpContext.User;
			string studentId = identity.FindFirstValue(ClaimTypes.NameIdentifier);

			if (studentId == null)
			{
				return View("Default", new MealButtonViewModel() { Meal = meal, Status = Status.LoginError });
			}

			if (meal.Cook.Id == studentId && meal.Guests.Count == 0)
			{
				return View("Default", new MealButtonViewModel() { Meal = meal, Status = Status.NoGuests });
			}

			if (meal.Cook.Id == studentId)
			{
				return View("Default", new MealButtonViewModel() { Meal = meal, Status = Status.IsCook });
			}

			if (meal.Guests.Select(g => g.StudentId).Contains(studentId))
			{
				return View("Default", new MealButtonViewModel() { Meal = meal, Status = Status.IsGuest });
			}

			if (meal.MaxGuests <= meal.Guests.Count())
			{
				return View("Default", new MealButtonViewModel() { Meal = meal, Status = Status.Full });
			}

			return View("Default", new MealButtonViewModel() { Meal = meal, Status = Status.Default, MealId = meal.Id });
		}
	}
}