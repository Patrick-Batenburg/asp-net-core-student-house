using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Studentenhuis.Models;
using System;
using System.Threading.Tasks;

namespace StudentenhuisTests
{
	public class FakeUserManager : UserManager<Student>
	{
		public FakeUserManager()
			: base(new Mock<IUserStore<Student>>().Object,
			  new Mock<IOptions<IdentityOptions>>().Object,
			  new Mock<IPasswordHasher<Student>>().Object,
			  new IUserValidator<Student>[0],
			  new IPasswordValidator<Student>[0],
			  new Mock<ILookupNormalizer>().Object,
			  new Mock<IdentityErrorDescriber>().Object,
			  new Mock<IServiceProvider>().Object,
			  new Mock<ILogger<UserManager<Student>>>().Object)
		{ }

		public override Task<IdentityResult> CreateAsync(Student user, string password)
		{
			return Task.FromResult(IdentityResult.Success);
		}

		public override Task<IdentityResult> AddToRoleAsync(Student user, string role)
		{
			return Task.FromResult(IdentityResult.Success);
		}

		public override Task<string> GenerateEmailConfirmationTokenAsync(Student user)
		{
			return Task.FromResult(Guid.NewGuid().ToString());
		}

	}
}
