using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Studentenhuis.Controllers;
using Studentenhuis.Models;
using Studentenhuis.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace StudentenhuisTests
{
	public class MealControllerTests
	{
		[Fact]
		public void ShouldReturnMealFromIndexViewCorrectly()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Cook = student,
				Date = DateTime.Now.AddDays(6)
			};

			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal,
				new Meal() { Id = 1, Title = "Chicken - White Meat With Tender", Description = "éƒ¨è½æ ¼", Price = 1.00, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 2, Title = "Amarula Cream", Description = "1.00E+02", Price = 1.00, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 3, Title = "Cake - Cheese Cake 9 Inch", Description = "<>?:\"{}|_+", Price = 1.00, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 4, Title = "Crawfish", Description = "ð œŽð œ±ð ¹ð ±“ð ±¸ð ²–ð ³", Price = 1.00, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 5, Title = "Ham - Proscuitto", Description = "ã€€", Price = 1.00, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 6, Title = "Soup - Campbells Bean Medley", Description = "1.00E+02", Price = 1.00, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 7, Title = "Beef - Kobe Striploin", Description = "NULL", Price = 1.00, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 8, Title = "Muffin Batt - Choc Chk", Description = "ç¤¾æœƒç§‘å­¸é™¢èªžå­¸ç ”ç©¶æ‰€", Price = 1.50, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 9, Title = "Bread - Italian Roll With Herbs", Description = "ï¼‘ï¼’ï¼“", Price = 1.00, Date = DateTime.Now.AddDays(1)}
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			List<Meal> model = (controller.Index() as ViewResult)?.ViewData.Model as List<Meal>;

			// Assert
			Assert.Equal(10, model.Count);
			Assert.Equal("Ginni", model[0].Cook.FirstName);
			Assert.Equal("Wassell", model[0].Cook.LastName);
			Assert.Equal(1, model[0].Cook.CookAtMeals.Count);
			Assert.Equal("Chicken - White Meat With Tender", model[1].Title);
			Assert.Equal("éƒ¨è½æ ¼", model[1].Description);
			Assert.NotEqual("Muffin Batt - Choc Chk", model[2].Title);
			Assert.NotEqual("ç¤¾æœƒç§‘å­¸é™¢èªžå­¸ç ”ç©¶æ‰€", model[2].Description);
			Assert.Equal(1.50, model[8].Price);
			Assert.Equal(1.00, model[9].Price);
		}

		[Fact]
		public void ShouldReturnScheduleForTwoWeeks()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				new Meal() { Id = 100, Title = "Compound - Strawberry", Description = "ã€€", Price = 1.00, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 1, Title = "Chicken - White Meat With Tender", Description = "éƒ¨è½æ ¼", Price = 1.00, Date = DateTime.Now.AddDays(2)},
				new Meal() { Id = 2, Title = "Amarula Cream", Description = "1.00E+02", Price = 1.00, Date = DateTime.Now.AddDays(4)},
				new Meal() { Id = 3, Title = "Cake - Cheese Cake 9 Inch", Description = "<>?:\"{}|_+", Price = 1.00, Date = DateTime.Now.AddDays(8)},
				new Meal() { Id = 4, Title = "Crawfish", Description = "ð œŽð œ±ð ¹ð ±“ð ±¸ð ²–ð ³", Price = 1.00, Date = DateTime.Now.AddDays(16)},
				new Meal() { Id = 5, Title = "Ham - Proscuitto", Description = "ã€€", Price = 1.00, Date = DateTime.Now.AddDays(32)},
				new Meal() { Id = 6, Title = "Soup - Campbells Bean Medley", Description = "1.00E+02", Price = 1.00, Date = DateTime.Now.AddDays(-1)},
				new Meal() { Id = 7, Title = "Beef - Kobe Striploin", Description = "NULL", Price = 1.00, Date = DateTime.Now.AddDays(-4)},
				new Meal() { Id = 8, Title = "Muffin Batt - Choc Chk", Description = "ç¤¾æœƒç§‘å­¸é™¢èªžå­¸ç ”ç©¶æ‰€", Price = 1.50, Date = DateTime.Now.AddDays(-8)},
				new Meal() { Id = 9, Title = "Bread - Italian Roll With Herbs", Description = "ï¼‘ï¼’ï¼“", Price = 1.00, Date = DateTime.Now.AddDays(-16)}
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			List<Meal> model = (controller.Index() as ViewResult)?.ViewData.Model as List<Meal>;

			// Assert
			Assert.Equal(4, model.Count);
		}

		[Fact]
		public void ShouldRenderDetailViewCorrectly()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				new Meal() { Id = 100, Title = "Compound - Strawberry", Description = "ã€€", Price = 1.50, Date = DateTime.Now.AddDays(1)},
				new Meal() { Id = 1, Title = "Chicken - White Meat With Tender", Description = "éƒ¨è½æ ¼", Price = 1.00, Date = DateTime.Now.AddDays(2)},
				new Meal() { Id = 2, Title = "Amarula Cream", Description = "1.00E+02", Price = 1.00, Date = DateTime.Now.AddDays(4)},
				new Meal() { Id = 3, Title = "Cake - Cheese Cake 9 Inch", Description = "<>?:\"{}|_+", Price = 1.00, Date = DateTime.Now.AddDays(8)},
				new Meal() { Id = 4, Title = "Crawfish", Description = "ð œŽð œ±ð ¹ð ±“ð ±¸ð ²–ð ³", Price = 1.00, Date = DateTime.Now.AddDays(16)},
				new Meal() { Id = 5, Title = "Ham - Proscuitto", Description = "ã€€", Price = 1.00, Date = DateTime.Now.AddDays(32)},
				new Meal() { Id = 6, Title = "Soup - Campbells Bean Medley", Description = "1.00E+02", Price = 1.00, Date = DateTime.Now.AddDays(-1)},
				new Meal() { Id = 7, Title = "Beef - Kobe Striploin", Description = "NULL", Price = 1.00, Date = DateTime.Now.AddDays(-4)},
				new Meal() { Id = 8, Title = "Muffin Batt - Choc Chk", Description = "ç¤¾æœƒç§‘å­¸é™¢èªžå­¸ç ”ç©¶æ‰€", Price = 1.50, Date = DateTime.Now.AddDays(-8)},
				new Meal() { Id = 9, Title = "Bread - Italian Roll With Herbs", Description = "ï¼‘ï¼’ï¼“", Price = 1.00, Date = DateTime.Now.AddDays(-16)}
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			Meal model = (controller.Detail(100) as ViewResult)?.ViewData.Model as Meal;

			// Assert
			Assert.Equal("Compound - Strawberry", model.Title);
			Assert.Equal("ã€€", model.Description);
			Assert.Equal(1.50, model.Price);
		}

		[Fact]
		public void CannotCreateMealWithDateInThePast()
		{
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(-1),
				MaxGuests = 1
			};

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Create(It.IsAny<Meal>())).Returns((Meal model) =>
			{
				if (meal.Date > DateTime.Now.Date)
				{
					return true;
				}

				return null;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			ViewResult viewResult = controller.Create(meal) as ViewResult;

			// Assert
			Assert.False(viewResult.ViewData.ModelState.IsValid);
		}

		[Fact]
		public void CannotCreateMealWithNegativePrice()
		{
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = -1.50,
				Date = DateTime.Now.AddDays(-1),
				MaxGuests = 1
			};

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Create(It.IsAny<Meal>())).Returns((Meal model) =>
			{
				if (meal.Title != null && meal.MaxGuests > 0 && meal.Price >= 0)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			ViewResult viewResult = controller.Create(meal) as ViewResult;

			// Assert
			Assert.Null(viewResult.ViewName);
		}

		[Fact]
		public void CannotCreateMealWithNoTitle()
		{
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Meal meal = new Meal()
			{
				Id = 100,
				Title = null,
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(-1),
				MaxGuests = 1
			};

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Create(It.IsAny<Meal>())).Returns((Meal value) =>
			{
				if (meal.Title != null && meal.MaxGuests > 0 && meal.Price >= 0)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			ViewResult viewResult = controller.Create(meal) as ViewResult;

			// Assert
			Assert.Null(viewResult.ViewName);
		}

		[Fact]
		public void CannotCreateMealWithZeroMaxGuests()
		{
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(-1),
				MaxGuests = 0
			};

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Create(It.IsAny<Meal>())).Returns((Meal model) =>
			{
				if (meal.Date > DateTime.Now)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			ViewResult viewResult = controller.Create(meal) as ViewResult;

			// Assert
			Assert.Null(viewResult.ViewName);
		}

		[Fact]
		public void CannotEditMealWithNoTitle()
		{
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Meal meal = new Meal()
			{
				Id = 100,
				Title = null,
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(-1),
				MaxGuests = 1
			};

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Create(It.IsAny<Meal>())).Returns((Meal model) =>
			{
				if (meal.Date > DateTime.Now)
				{
					return true;
				}

				return false;
			});

			mealMock.Setup(m => m.Update(It.IsAny<Meal>())).Returns((Meal model) =>
			{
				if (meal.Title != null && meal.MaxGuests > 0 && meal.Price >= 0)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			controller.Create(meal);
			meal.Title = null;
			ViewResult viewResult = controller.Edit(meal) as ViewResult;

			// Assert
			Assert.Null(viewResult.ViewName);
		}

		[Fact]
		public void CannotEditMealWithZeroMaxGuests()
		{
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(-1),
				MaxGuests = 0
			};

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Create(It.IsAny<Meal>())).Returns((Meal model) =>
			{
				if (meal.Date > DateTime.Now)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			controller.Create(meal);
			meal.MaxGuests = 0;
			ViewResult viewResult = controller.Edit(meal) as ViewResult;

			// Assert
			Assert.Null(viewResult.ViewName);
		}

		[Fact]
		public void ShouldDeleteMealCorrectly()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				Id = "101",
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1
			};

			ClaimsPrincipal identity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, student.Id) }));

			meal.Cook = student;
			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Delete(It.IsAny<int>())).Returns((int mealId) =>
			{
				if (meal.Guests.Count == 0)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object)
			{
				ControllerContext = new ControllerContext()
				{
					HttpContext = new DefaultHttpContext() { User = identity }
				}
			};

			// Act
			RedirectResult redirectResult = controller.Delete(new MealButtonViewModel() { MealId = 100 }) as RedirectResult;

			// Assert
			Assert.Equal("/Meal", redirectResult?.Url);
		}

		[Fact]
		public void ShouldCreateMealCorrectly()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				Id = "101",
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			ClaimsPrincipal identity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, student.Id) }));

			meal.Cook = student;
			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Create(It.IsAny<Meal>())).Returns((Meal model) =>
			{
				if (meal.Title != null && meal.MaxGuests > 0 && meal.Price >= 0)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object)
			{
				ControllerContext = new ControllerContext()
				{
					HttpContext = new DefaultHttpContext() { User = identity }
				}
			};

			// Act
			RedirectResult redirectResult = controller.Create(meal) as RedirectResult;

			// Assert
			Assert.Equal("/Meal", redirectResult?.Url);
		}

		[Fact]
		public void ShouldEditMealCorrectly()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				Id = "101",
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			ClaimsPrincipal identity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, student.Id) }));

			meal.Cook = student;
			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Update(It.IsAny<Meal>())).Returns((int mealId) =>
			{
				if (meal.Cook.Id == student.Id && meal.Title != null && meal.MaxGuests > 0 && meal.Price >= 0)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object)
			{
				ControllerContext = new ControllerContext()
				{
					HttpContext = new DefaultHttpContext() { User = identity }
				}
			};

			// Act
			ViewResult viewResult = controller.Edit(meal.Id) as ViewResult;

			// Assert
			Assert.True(viewResult.ViewData.ModelState.IsValid);
		}

		[Fact]
		public void CannotEditSomeoneElseMeal()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				Id = "101",
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			ClaimsPrincipal identity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
			{
				 new Claim(ClaimTypes.NameIdentifier, student.Id)
			}));

			meal.Cook = student;
			student.CookAtMeals.Add(meal);

			studentMock.Setup(s => s.Students).Returns(new List<Student>()
			{
				student
			});

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Update(It.IsAny<Meal>())).Returns((int mealId) =>
			{
				if (meal.Cook.Id == student.Id)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object)
			{
				ControllerContext = new ControllerContext()
				{
					HttpContext = new DefaultHttpContext() { User = identity }
				}
			};

			// Act
			meal.Cook = new Student() { Id = "abc" };
			ViewResult viewResult = controller.Edit(meal.Id) as ViewResult;

			// Assert
			Assert.Equal("Error", viewResult?.ViewName);
		}

		[Fact]
		public void CannotDeleteSomeoneElseMeal()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				Id = "101",
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			ClaimsPrincipal identity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
			{
				 new Claim(ClaimTypes.NameIdentifier, student.Id)
			}));

			meal.Cook = student;
			student.CookAtMeals.Add(meal);

			studentMock.Setup(s => s.Students).Returns(new List<Student>()
			{
				student
			});

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Delete(It.IsAny<int>())).Returns((int mealId) =>
			{
				if (meal.Cook.Id == student.Id)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object)
			{
				ControllerContext = new ControllerContext()
				{
					HttpContext = new DefaultHttpContext() { User = identity }
				}
			};

			// Act
			meal.Cook = new Student() { Id = "abc" };
			ViewResult viewResult = controller.Delete(new MealButtonViewModel() { MealId = 100 }) as ViewResult;

			// Assert
			Assert.Equal("Error", viewResult?.ViewName);
		}

		[Fact]
		public void ShouldJoinWhenMaxGuestsIsNotReached()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Cook = student,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Join(It.IsAny<int>(), It.IsAny<string>())).Returns((int mealId, string studentId) =>
			{
				if (meal.Guests.Count < meal.MaxGuests)
				{
					meal.Guests.Add(new Guest());
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			RedirectResult redirectResult = controller.Join(new MealButtonViewModel() { MealId = 100 }) as RedirectResult;

			// Assert
			Assert.Equal("/Meal", redirectResult?.Url);
		}

		[Fact]
		public void CannotJoinWhenMaxGuestsIsReached()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Cook = student,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Join(It.IsAny<int>(), It.IsAny<string>())).Returns((int mealId, string studentId) =>
			{
				if (meal.Guests.Count < meal.MaxGuests)
				{
					meal.Guests.Add(new Guest());
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			controller.Join(new MealButtonViewModel() { MealId = 100 });
			ViewResult viewResult = controller.Join(new MealButtonViewModel() { MealId = 100 }) as ViewResult;

			// Assert
			Assert.Equal("Error", viewResult?.ViewName);
		}

		[Fact]
		public void ShouldLeaveMealCorrectly()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Cook = student,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			Guest item = new Guest();

			mealMock.Setup(m => m.Join(It.IsAny<int>(), It.IsAny<string>())).Returns((int mealId, string studentId) =>
			{
				if (meal.Guests.Count < meal.MaxGuests)
				{
					item.MealId = mealId;
					item.StudentId = studentId;
					meal.Guests.Add(item);
					return true;
				}

				return false;
			});

			mealMock.Setup(m => m.Leave(It.IsAny<int>(), It.IsAny<string>())).Returns((int mealId, string studentId) =>
			{
				meal.Guests.Remove(item);

				if (meal.Guests.Count == 0)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			controller.Join(new MealButtonViewModel() { MealId = 100 });
			RedirectResult redirectResult = controller.Leave(new MealButtonViewModel() { MealId = 100 }) as RedirectResult;

			// Assert
			Assert.Equal("/Meal", redirectResult?.Url);
		}

		[Fact]
		public void ShouldJoinMealCorrectly()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Cook = student,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Join(It.IsAny<int>(), It.IsAny<string>())).Returns((int mealId, string studentId) =>
			{
				if (meal.Guests.Count < meal.MaxGuests)
				{
					meal.Guests.Add(new Guest());
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			RedirectResult redirectResult = controller.Join(new MealButtonViewModel() { MealId = 100 }) as RedirectResult;

			// Assert
			Assert.Equal("/Meal", redirectResult?.Url);
		}

		[Fact]
		public void CannotDeleteMealWithGuests()
		{
			// Arrange
			Mock<IMealRepository> mealMock = new Mock<IMealRepository>();
			Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();

			Student student = new Student()
			{
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			Meal meal = new Meal()
			{
				Id = 100,
				Cook = student,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6),
				MaxGuests = 1,
			};

			student.CookAtMeals.Add(meal);

			mealMock.Setup(m => m.Meals).Returns(new List<Meal>()
			{
				meal
			});

			mealMock.Setup(m => m.Join(It.IsAny<int>(), It.IsAny<string>())).Returns((int mealId, string user) =>
			{
				if (meal.Guests.Count < meal.MaxGuests)
				{
					meal.Guests.Add(new Guest());
					return true;
				}

				return false;
			});

			mealMock.Setup(m => m.Delete(It.IsAny<int>())).Returns((int mealId) =>
			{
				if (meal.Guests.Count == 0)
				{
					return true;
				}

				return false;
			});

			MealController controller = new MealController(mealMock.Object, studentMock.Object);

			// Act
			RedirectResult redirectResult = controller.Delete(new MealButtonViewModel() { MealId = 100 }) as RedirectResult;
			controller.Join(new MealButtonViewModel() { MealId = 100 });
			ViewResult viewResult = controller.Delete(new MealButtonViewModel() { MealId = 100 }) as ViewResult;

			// Assert
			Assert.Equal("Error", viewResult?.ViewName);
		}
	}
}