using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studentenhuis.Models;
using Studentenhuis.Models.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;

namespace Studentenhuis.Controllers
{
	/// <summary>
	/// A class for an MVC controller with view support for meals.
	/// </summary>
	public class MealController : Controller
	{
		private IMealRepository _mealRepository;
		private IStudentRepository _studentRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="MealController"/> class.
		/// </summary>
		/// <param name="mealRepository">Provides specific data operations for the <see cref="Meal"/> model.</param>
		/// <param name="studentRepository">Provides specific data operations for the <see cref="Student"/> model.</param>
		public MealController(IMealRepository mealRepository, IStudentRepository studentRepository)
		{
			_mealRepository = mealRepository;
			_studentRepository = studentRepository;
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Index"/> view to the response.
		/// </summary>
		/// <returns>A view of the <see cref="Index"/> action method.</returns>
		public IActionResult Index()
		{
			return View(_mealRepository.Meals.Where(m => m.Date.Date.CompareTo(DateTime.Now.Date) >= 0).Where(m => m.Date.Date.CompareTo(DateTime.Now.Date.AddDays(14)) < 0).ToList());
		}


		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Detail(int)"/> view to the response.
		/// </summary>
		/// <param name="id">The ID of the meal.</param>
		/// <returns>A view of the <see cref="Detail(int)"/> action method.</returns>
		[Authorize]
		public IActionResult Detail(int id)
		{
			return View(_mealRepository.Meals.Where(m => m.Id == id).FirstOrDefault());
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Create"/> view to the response.
		/// </summary>
		/// <returns>A view of the <see cref="Create"/> action method.</returns>
		[Authorize]
		public IActionResult Create()
		{
			return View(new Meal());
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Create(Meal)"/> view to the response.
		/// </summary>
		/// <param name="meal">The meal to create.</param>
		/// <returns>A view of the <see cref="Create(Meal)"/> action method.</returns>
		[HttpPost]
		[Authorize]
		public IActionResult Create(Meal meal)
		{
			meal.Cook = _studentRepository.Students.Where(e => e.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
			IActionResult actionResult = View(meal);
			bool? result = _mealRepository.Create(meal);

			if (result == true)
			{
				actionResult = Redirect("/Meal");
			}
			else if(result == null)
			{
				ModelState.AddModelError("Date", "A meal is already planned on this day");
			}

			return actionResult;
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Edit(int)"/> view to the response.
		/// </summary>
		/// <param name="id">The ID of the meal to edit.</param>
		/// <returns>A view of the <see cref="Edit(int)"/> action method.</returns>
		[Authorize]
		public IActionResult Edit(int id)
		{
			Meal meal = _mealRepository.Meals.Where(m => m.Id == id).FirstOrDefault();
			IActionResult actionResult = View();

			if (!ModelState.IsValid || (!meal?.Cook?.Id.Equals(User?.FindFirstValue(ClaimTypes.NameIdentifier))) == true)
			{
				actionResult = View("Error");
			}
			else
			{
				actionResult = View(_mealRepository.Meals.Where(m => m.Id == id).FirstOrDefault());
			}

			return actionResult;
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Edit(Meal)"/> view to the response.
		/// </summary>
		/// <param name="meal">The meal to edit.</param>
		/// <returns>A view of the <see cref="Edit(Meal)"/> action method.</returns>
		[HttpPost]
		[Authorize]
		public IActionResult Edit(Meal meal)
		{
			IActionResult actionResult = View(meal);

			if (meal?.Guests?.Count != 0)
			{
				actionResult = View("Error");
			}
			else if (_mealRepository.Update(meal))
			{
				actionResult = Redirect($"/Meal/Detail/{meal.Id}");
			}
			else
			{
				meal.Title = _mealRepository.Meals.Where(m => m.Id == meal.Id).FirstOrDefault().Title;
			}

			return actionResult;
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Delete(MealButtonViewModel)"/> view to the response.
		/// </summary>
		/// <param name="mealButtonViewModel">Represents the button as a part of the meal detail view.</param>
		/// <returns>A view of the <see cref="Delete(MealButtonViewModel)"/> action method.</returns>
		[HttpPost]
		[Authorize]
		public IActionResult Delete(MealButtonViewModel mealButtonViewModel)
		{
			Meal meal = _mealRepository.Meals.Where(m => m.Id == mealButtonViewModel.MealId).FirstOrDefault();
			IActionResult actionResult = View(mealButtonViewModel);

			if (!ModelState.IsValid || meal?.Guests?.Count != 0 || (!meal?.Cook?.Id.Equals(User?.FindFirstValue(ClaimTypes.NameIdentifier))) == true)
			{
				actionResult = View("Error");
			}
			else
			{
				_mealRepository.Delete(mealButtonViewModel.MealId);
				actionResult = Redirect("/Meal");
			}

			return actionResult;
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Join(MealButtonViewModel)"/> view to the response.
		/// </summary>
		/// <param name="mealButtonViewModel">Represents the button as a part of the meal detail view.</param>
		/// <returns>A view of the <see cref="Join(MealButtonViewModel)"/> action method.</returns>
		[HttpPost]
		[Authorize]
		public IActionResult Join(MealButtonViewModel mealButtonViewModel)
		{
			Meal meal = _mealRepository.Meals.Where(e => e.Id == mealButtonViewModel.MealId).FirstOrDefault();
			IActionResult actionResult = View();

			if (meal?.Guests?.Count >= meal.MaxGuests)
			{
				actionResult = View("Error");
			}
			else if (_mealRepository.Join(mealButtonViewModel.MealId, User?.FindFirstValue(ClaimTypes.NameIdentifier)))
			{
				actionResult = Redirect("/Meal");
			}
			else
			{
				actionResult = View("Error");
			}

			return actionResult;
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Leave(MealButtonViewModel)"/> view to the response.
		/// </summary>
		/// <param name="mealButtonViewModel">Represents the button as a part of the meal detail view.</param>
		/// <returns>A view of the <see cref="Leave(MealButtonViewModel)"/> action method.</returns>
		[HttpPost]
		[Authorize]
		public IActionResult Leave(MealButtonViewModel mealButtonViewModel)
		{
			IActionResult actionResult = View();

			if (_mealRepository.Leave(mealButtonViewModel.MealId, User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""))
			{
				actionResult = Redirect("/Meal");
			}
			else
			{
				actionResult = View("Error");
			}

			return actionResult;
		}
	}
}