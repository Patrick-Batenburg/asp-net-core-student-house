using Microsoft.AspNetCore.Mvc;
using Studentenhuis.Models.ViewModels;
using System.Diagnostics;

namespace Studentenhuis.Controllers
{
	/// <summary>
	/// A class for an MVC controller with view support for home.
	/// </summary>
	public class HomeController : Controller
	{
		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Index"/> view to the response.
		/// </summary>
		/// <returns>A view of the <see cref="Index"/> action method.</returns>
		public IActionResult Index()
		{
			if (!ModelState.IsValid)
			{
				return View("Error");
			}

			return View();
		}
	}
}