using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Studentenhuis.Models
{
	/// <summary>
	/// A class that provides initial data to populate a database.
	/// </summary>
	public class SeedData
	{
		/// <summary>
		/// Provides initial data to populate a database.
		/// </summary>
		/// <param name="serviceProvider">Defines a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</param>
		public static void EnsurePopulated(IServiceProvider serviceProvider)
		{
			ApplicationDbContext db = serviceProvider.GetRequiredService<ApplicationDbContext>();
			UserManager<Student> userManager = serviceProvider.GetRequiredService<UserManager<Student>>();

			if (!db.Students.Any())
			{
				Student student1 = new Student { Id = "1", FirstName = "Ginni", LastName = "Wassell", Email = "ginni@email.com", UserName = "Ginni", PhoneNumber = "12345678" };
				Student student2 = new Student { Id = "2", FirstName = "Brita", LastName = "Blais", Email = "brita@email.com", UserName = "Brita", PhoneNumber = "12345678" };
				Student student3 = new Student { Id = "3", FirstName = "Aida", LastName = "Selwyn", Email = "aida@email.com", UserName = "Aida", PhoneNumber = "12345678" };
				Student student4 = new Student { Id = "4", FirstName = "Kass", LastName = "Huzzey", Email = "kass@email.com", UserName = "Kass", PhoneNumber = "12345678" };
				Student student5 = new Student { Id = "5", FirstName = "Padraig", LastName = "Berrigan", Email = "padraig@email.com", UserName = "Padraig", PhoneNumber = "12345678" };
				Student student6 = new Student { Id = "6", FirstName = "Morgan", LastName = "Parsall", Email = "morgan@email.com", UserName = "Morgan", PhoneNumber = "12345678" };
				Student student7 = new Student { Id = "7", FirstName = "Byrann", LastName = "Rubin", Email = "byrann@email.com", UserName = "Byrann", PhoneNumber = "12345678" };
				Student student8 = new Student { Id = "8", FirstName = "Keene", LastName = "Jukubczak", Email = "keene@email.com", UserName = "Keene", PhoneNumber = "12345678" };
				Student student9 = new Student { Id = "9", FirstName = "Opaline", LastName = "Rowthorne", Email = "opaline@email.com", UserName = "Opaline", PhoneNumber = "12345678" };
				Student student10 = new Student { Id = "10", FirstName = "Elisha", LastName = "Brosini", Email = "elisha@email.com", UserName = "Elisha", PhoneNumber = "12345678" };
				db.Students.AddRange(student1, student2, student3, student4, student5, student6, student7, student8, student9, student10);

				if (!db.Meals.Any())
				{
					Meal meal1 = new Meal() { Title = "A title1", Description = "A plain description1", Price = 1.00, Cook = student1, Date = DateTime.Now.AddDays(1), MaxGuests = 1 };
					Meal meal2 = new Meal() { Title = "A title2", Description = "A plain description2", Price = 2.00, Cook = student2, Date = DateTime.Now.AddDays(2), MaxGuests = 2 };
					Meal meal3 = new Meal() { Title = "A title3", Description = "A plain description3", Price = 3.00, Cook = student3, Date = DateTime.Now.AddDays(3), MaxGuests = 3 };
					Meal meal4 = new Meal() { Title = "A title4", Description = "A plain description4", Price = 4.00, Cook = student4, Date = DateTime.Now.AddDays(4), MaxGuests = 4 };
					Meal meal5 = new Meal() { Title = "A title5", Description = "A plain description5", Price = 5.00, Cook = student5, Date = DateTime.Now.AddDays(5), MaxGuests = 5 };
					Meal meal6 = new Meal() { Title = "A title6", Description = "A plain description6", Price = 6.00, Cook = student6, Date = DateTime.Now.AddDays(6), MaxGuests = 6 };
					Meal meal7 = new Meal() { Title = "A title7", Description = "A plain description7", Price = 7.00, Cook = student7, Date = DateTime.Now.AddDays(7), MaxGuests = 7 };
					Meal meal8 = new Meal() { Title = "A title8", Description = "A plain description8", Price = 8.00, Cook = student8, Date = DateTime.Now.AddDays(8), MaxGuests = 8 };
					Meal meal9 = new Meal() { Title = "A title9", Description = "A plain description9", Price = 9.00, Cook = student9, Date = DateTime.Now.AddDays(9), MaxGuests = 9 };
					Meal meal10 = new Meal() { Title = "A title10", Description = "A plain description10", Price = 9.99, Cook = student10, Date = DateTime.Now.AddDays(10), MaxGuests = 10 };

					meal1.Guests.Add(new Guest() { Student = student2, StudentId = student2.Id, Meal = meal1, MealId = meal1.Id });

					meal2.Guests.Add(new Guest() { Student = student3, StudentId = student3.Id, Meal = meal2, MealId = meal2.Id });
					meal2.Guests.Add(new Guest() { Student = student4, StudentId = student4.Id, Meal = meal2, MealId = meal2.Id });

					meal3.Guests.Add(new Guest() { Student = student4, StudentId = student4.Id, Meal = meal3, MealId = meal3.Id });
					meal3.Guests.Add(new Guest() { Student = student5, StudentId = student5.Id, Meal = meal3, MealId = meal3.Id });
					meal3.Guests.Add(new Guest() { Student = student6, StudentId = student6.Id, Meal = meal3, MealId = meal3.Id });

					meal4.Guests.Add(new Guest() { Student = student5, StudentId = student5.Id, Meal = meal4, MealId = meal4.Id });
					meal4.Guests.Add(new Guest() { Student = student6, StudentId = student6.Id, Meal = meal4, MealId = meal4.Id });
					meal4.Guests.Add(new Guest() { Student = student7, StudentId = student7.Id, Meal = meal4, MealId = meal4.Id });
					meal4.Guests.Add(new Guest() { Student = student8, StudentId = student8.Id, Meal = meal4, MealId = meal4.Id });

					meal5.Guests.Add(new Guest() { Student = student6, StudentId = student6.Id, Meal = meal5, MealId = meal5.Id });
					meal5.Guests.Add(new Guest() { Student = student7, StudentId = student7.Id, Meal = meal5, MealId = meal5.Id });
					meal5.Guests.Add(new Guest() { Student = student8, StudentId = student8.Id, Meal = meal5, MealId = meal5.Id });
					meal5.Guests.Add(new Guest() { Student = student9, StudentId = student9.Id, Meal = meal5, MealId = meal5.Id });
					meal5.Guests.Add(new Guest() { Student = student10, StudentId = student10.Id, Meal = meal5, MealId = meal5.Id });

					meal6.Guests.Add(new Guest() { Student = student7, StudentId = student7.Id, Meal = meal6, MealId = meal6.Id });
					meal7.Guests.Add(new Guest() { Student = student8, StudentId = student8.Id, Meal = meal7, MealId = meal7.Id });
					meal8.Guests.Add(new Guest() { Student = student9, StudentId = student9.Id, Meal = meal8, MealId = meal8.Id });
					meal9.Guests.Add(new Guest() { Student = student10, StudentId = student10.Id, Meal = meal9, MealId = meal9.Id });
					meal10.Guests.Add(new Guest() { Student = student1, StudentId = student1.Id, Meal = meal10, MealId = meal10.Id });

					db.Meals.AddRange(meal1, meal2, meal3, meal4, meal5, meal6, meal7, meal8, meal9, meal10);
				}

				db.SaveChanges();
			}
		}
	}
}