using Studentenhuis.Models;
using System;
using Xunit;

namespace StudentenhuisTests
{
	public class MealTests
	{
		[Fact]
		public void ShouldGetPropertyValuesCorrectly()
		{
			Meal model = new Meal()
			{
				Id = 100,
				Title = "Compound - Strawberry",
				Description = "ã€€",
				Price = 1.50,
				Date = DateTime.Now.AddDays(6).Date,
				MaxGuests = 2,
			};

			Assert.Equal(100, model.Id);
			Assert.Equal(2, model.MaxGuests);
			Assert.Equal("Compound - Strawberry", model.Title);
			Assert.Equal("ã€€", model.Description);
			Assert.Equal(1.50, model.Price);
			Assert.Equal(DateTime.Now.AddDays(6).Date, model.Date);
		}
	}
}
