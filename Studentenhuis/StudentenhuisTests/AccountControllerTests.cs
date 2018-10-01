using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Studentenhuis.Controllers;
using Studentenhuis.Models;
using Studentenhuis.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StudentenhuisTests
{
	public class AccountControllerTests
	{
		[Fact]
		public void ShouldReturnStudentFromIndexViewCorrectly()
		{
			// Arrange
			Mock<IStudentRepository> mock = new Mock<IStudentRepository>();
			AccountController controller = new AccountController(null, null, mock.Object);

			Student student = new Student()
			{
				FirstName = "Ginni",
				LastName = "Wassell"
			};

			mock.Setup(m => m.Students).Returns(new List<Student>()
			{
				student,
				new Student() { FirstName = "Brita", LastName = "Blais" },
				new Student() { FirstName = "Aida", LastName = "Selwyn" },
				new Student() { FirstName = "Kass", LastName = "Huzzey" },
				new Student() { FirstName = "Padraig ", LastName = "Berrigan" },
				new Student() { FirstName = "Morgan", LastName = "Parsall" },
				new Student() { FirstName = "Byrann", LastName = "Rubin" },
				new Student() { FirstName = "Keene", LastName = "Jukubczak" },
				new Student() { FirstName = "Opaline ", LastName = "Rowthorne" },
				new Student() { FirstName = "Elisha", LastName = "Brosini" },
			});

			// Act
			List<Student> model = (controller.Index() as ViewResult)?.ViewData.Model as List<Student>;

			// Assert
			Assert.Equal(10, model.Count);
			Assert.Equal("Ginni", model[0].FirstName);
			Assert.Equal("Wassell", model[0].LastName);
		}

		[Theory]
		[InlineData("Ginni", "1")]
		public async Task ShouldReturnInvalidLogin(string username, string password)
		{
			// Arrange
			Mock<IUserStore<Student>> userStore = new Mock<IUserStore<Student>>();
			userStore.As<IUserPasswordStore<Student>>().Setup(x => x.FindByNameAsync(username, CancellationToken.None)).ReturnsAsync((Student)null);

			Mock<FakeUserManager> userManager = new Mock<FakeUserManager>();
			userManager.Setup(x => x.Users).Returns((IQueryable<Student>)null);
			userManager.Setup(x => x.DeleteAsync(It.IsAny<Student>())).ReturnsAsync(IdentityResult.Success);
			userManager.Setup(x => x.CreateAsync(It.IsAny<Student>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
			userManager.Setup(x => x.UpdateAsync(It.IsAny<Student>())).ReturnsAsync(IdentityResult.Success);

			AccountController controller = new AccountController(userManager.Object, null, null);

			LoginViewModel viewModel = new LoginViewModel()
			{
				Username = username,
				Password = password
			};

			// Act
			ViewResult viewResult = (await controller.Login(viewModel)) as ViewResult;

			// Assert
			Assert.False(viewResult.ViewData.ModelState.IsValid);
		}

		[Theory]
		[InlineData("Ginni", "1")]
		public async Task ShouldReturnValidLogin(string username, string password)
		{
			// Arrange
			RegisterViewModel registerViewModel = new RegisterViewModel()
			{
				Username = username,
				Password = password
			};

			Student student = new Student
			{
				UserName = registerViewModel.Username,
				Email = registerViewModel.Email,
				FirstName = registerViewModel.FirstName,
				MiddleName = registerViewModel.MiddleName,
				LastName = registerViewModel.LastName,
				PhoneNumber = registerViewModel.PhoneNumber
			};

			IQueryable<Student> students = new List<Student> { student }.AsQueryable();

			Mock<IUserStore<Student>> userStore = new Mock<IUserStore<Student>>();
			userStore.As<IUserPasswordStore<Student>>().Setup(x => x.FindByNameAsync(username, CancellationToken.None)).ReturnsAsync(student);

			Mock<FakeUserManager> userManager = new Mock<FakeUserManager>();
			userManager.Setup(x => x.Users).Returns(students);
			userManager.Setup(x => x.CreateAsync(It.IsAny<Student>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

			Mock<FakeSignInManager> signInManager = new Mock<FakeSignInManager>();
			signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<Student>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

			AccountController controller = new AccountController(userManager.Object, signInManager.Object, null);

			LoginViewModel loginViewModel = new LoginViewModel()
			{
				Username = student.UserName,
				Password = password
			};

			// Act
			RedirectResult redirectResult = (await controller.Login(loginViewModel)) as RedirectResult;

			// Assert
			Assert.Equal("/", redirectResult?.Url);
		}

		[Theory]
		[InlineData("Ginni", "12345678")]
		public async Task ShouldRegisterWithMinimumPasswordLengthCorrectly(string username, string password)
		{
			// Arrange
			RegisterViewModel registerViewModel = new RegisterViewModel()
			{
				Username = username,
				Password = password
			};

			Student student = new Student
			{
				UserName = registerViewModel.Username,
				Email = registerViewModel.Email,
				FirstName = registerViewModel.FirstName,
				MiddleName = registerViewModel.MiddleName,
				LastName = registerViewModel.LastName,
				PhoneNumber = registerViewModel.PhoneNumber
			};

			IQueryable<Student> students = new List<Student> { student }.AsQueryable();

			Mock<IUserStore<Student>> userStore = new Mock<IUserStore<Student>>();
			userStore.As<IUserPasswordStore<Student>>().Setup(x => x.FindByNameAsync(username, CancellationToken.None)).ReturnsAsync(student);

			Mock<FakeUserManager> userManager = new Mock<FakeUserManager>();
			userManager.Setup(x => x.Users).Returns(students);

			if (password.Length >= 8)
			{
				userManager.Setup(x => x.CreateAsync(It.IsAny<Student>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
			}
			else
			{
				IdentityError[] erorrs = { new IdentityError() { Description = "Password must be 8 characters long and cannot be longer than 128 characters" } };
				userManager.Setup(x => x.CreateAsync(It.IsAny<Student>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(erorrs));
			}

			AccountController controller = new AccountController(userManager.Object, null, null);

			// Act
			RedirectResult redirectResult = (await controller.Register(registerViewModel)) as RedirectResult;

			// Assert
			Assert.Equal("/", redirectResult?.Url);
		}

		[Theory]
		[InlineData("Ginni", "123")]
		public async Task CannotRegisterWithWrongPasswordLength(string username, string password)
		{
			// Arrange
			RegisterViewModel registerViewModel = new RegisterViewModel()
			{
				Username = username,
				Password = password
			};

			Student student = new Student
			{
				UserName = registerViewModel.Username,
				Email = registerViewModel.Email,
				FirstName = registerViewModel.FirstName,
				MiddleName = registerViewModel.MiddleName,
				LastName = registerViewModel.LastName,
				PhoneNumber = registerViewModel.PhoneNumber
			};

			IQueryable<Student> students = new List<Student> { student }.AsQueryable();

			Mock<IUserStore<Student>> userStore = new Mock<IUserStore<Student>>();
			userStore.As<IUserPasswordStore<Student>>().Setup(x => x.FindByNameAsync(username, CancellationToken.None)).ReturnsAsync(student);

			Mock<FakeUserManager> userManager = new Mock<FakeUserManager>();
			userManager.Setup(x => x.Users).Returns(students);

			if (password.Length >= 8)
			{
				userManager.Setup(x => x.CreateAsync(It.IsAny<Student>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
			}
			else
			{
				IdentityError[] erorrs = { new IdentityError() { Description = "Password must be 8 characters long and cannot be longer than 128 characters" } };
				userManager.Setup(x => x.CreateAsync(It.IsAny<Student>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(erorrs));
			}

			AccountController controller = new AccountController(userManager.Object, null, null);

			// Act
			ViewResult viewResult = (await controller.Register(registerViewModel)) as ViewResult;

			// Assert
			Assert.False(viewResult.ViewData.ModelState.IsValid);
		}
	}
}