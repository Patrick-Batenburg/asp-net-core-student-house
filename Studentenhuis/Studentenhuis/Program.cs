using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Studentenhuis.Models;
using System;

namespace Studentenhuis
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IWebHost host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;

				try
				{
					SeedData.EnsurePopulated(services);
				}
				catch (Exception ex)
				{
					ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occured seeding the database.");
				}
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
