using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Studentenhuis.Models;

namespace StudentenhuisTests
{
	public class FakeSignInManager : SignInManager<Student>
	{
		public FakeSignInManager()
				: base(new Mock<FakeUserManager>().Object,
					 new Mock<IHttpContextAccessor>().Object,
					 new Mock<IUserClaimsPrincipalFactory<Student>>().Object,
					 new Mock<IOptions<IdentityOptions>>().Object,
					 new Mock<ILogger<SignInManager<Student>>>().Object,
					 new Mock<IAuthenticationSchemeProvider>().Object)
		{ }
	}
}
