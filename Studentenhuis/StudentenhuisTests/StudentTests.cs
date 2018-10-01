using Studentenhuis.Models;
using Xunit;

namespace StudentenhuisTests
{
	public class StudentTests
	{
		[Fact]
		public void ShouldPrintFullNameCorrectly()
		{
			Student model = new Student()
			{
				FirstName = "Patrick",
				MiddleName = "van",
				LastName = "Batenburg"
			};

			Assert.Equal("Patrick van Batenburg", model.FullName());
		}
	}
}
