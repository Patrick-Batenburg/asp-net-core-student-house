using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Studentenhuis.Controllers
{
	/// <summary>
	/// A class for an MVC controller with view support for status codes.
	/// </summary>
	public class StatusCodeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="StatusCodeController"/> class.
		/// </summary>
		/// <param name="logger">When overridden in a derived form, provides functionality for loggers that handle events raised by the url that the request came through.</param>
		public StatusCodeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Represents an <see cref="ActionResult"/> that renders the the <see cref="Index"/> view to the response.
		/// </summary>
		/// <returns>A view of the <see cref="Index"/> action method.</returns>
		[HttpGet("/StatusCode/{statusCode}")]
		public IActionResult Index(int statusCode)
		{
			IStatusCodeReExecuteFeature reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
			_logger.LogInformation($"Unexpected Status Code: {statusCode}, OriginalPath: {reExecute.OriginalPath}");
			return View(statusCode);
		}
	}
}