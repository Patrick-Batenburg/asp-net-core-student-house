using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Studentenhuis.Models;
using Studentenhuis.Models.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Studentenhuis.Components
{
	/// <summary>
	/// A class for view component that renders the schedule for todays meals.
	/// </summary>
	public class ScheduleViewComponent : ViewComponent
	{
		private readonly IMealRepository _mealRepository;
		private UserManager<Student> _userManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScheduleViewComponent"/> class.
		/// </summary>
		/// <param name="mealRepository">Provides specific data operations on the meals.</param>
		/// <param name="userManager">Provides the APIs for managing students in a persistence store.</param>
		public ScheduleViewComponent(IMealRepository mealRepository, UserManager<Student> userManager)
		{
			_mealRepository = mealRepository;
			_userManager = userManager;
		}

		/// <summary>
		/// Executes a result type of <see cref="ViewComponent"/> to render asynchronously.
		/// </summary>
		/// <returns>A view of the <see cref="ViewComponent"/>.</returns>
		public async Task<IViewComponentResult> InvokeAsync()
		{
			Student student = null;
			ClaimsPrincipal identity = HttpContext.User;
			string studentId = identity.FindFirstValue(ClaimTypes.NameIdentifier);

			if (studentId != null)
			{
				student = await _userManager.FindByIdAsync(studentId);
			}

			return View(new ScheduleViewModel(_mealRepository, student));
		}
	}
}