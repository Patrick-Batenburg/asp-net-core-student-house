using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Studentenhuis.Models
{
	/// <summary>
	/// A DbContext instance represents a session with the database and can be used to 
	/// query and save instances of your entities. DbContext is a combination of the 
	/// Unit Of Work and Repository patterns.
	/// </summary>
	public class ApplicationDbContext : IdentityDbContext<Student>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
		/// </summary>
		/// <param name="options">The options used by a <see cref="DbContext"/>.</param>
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		/// <summary>
		/// A <see cref="DbSet{TEntity}"/> can be used to query and save instances
		/// of students. LINQ queries against a <see cref="DbSet{TEntity}"/> will
		/// be translated into queries against the database.
		/// </summary>
		public DbSet<Student> Students { get; set; }

		/// <summary>
		/// A <see cref="DbSet{TEntity}"/> can be used to query and save instances
		/// of meals. LINQ queries against a <see cref="DbSet{TEntity}"/> will
		/// be translated into queries against the database.
		/// </summary>
		public DbSet<Meal> Meals { get; set; }

		/// <summary>
		/// A <see cref="DbSet{TEntity}"/> can be used to query and save instances
		/// of guests. LINQ queries against a <see cref="DbSet{TEntity}"/> will
		/// be translated into queries against the database.
		/// </summary>
		public DbSet<Guest> Guests { get; set; }

		/// <summary>
		/// Configures the schema needed for the identity framework.
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Guest>().HasKey(g => new { g.MealId, g.StudentId });
			modelBuilder.Entity<Student>().HasMany(s => s.GuestAtMeals).WithOne(g => g.Student);
			modelBuilder.Entity<Meal>().HasOne(m => m.Cook).WithMany(s => s.CookAtMeals);
			modelBuilder.Entity<Meal>().HasMany(m => m.Guests).WithOne(g => g.Meal);
			modelBuilder.Entity<Meal>().HasIndex(m => m.Date).IsUnique();
		}

		/// <summary>
		/// Provides the APIs for managing students in a persistence store.
		/// </summary>
		protected UserManager<Student> UserManager { get; set; }
	}
}