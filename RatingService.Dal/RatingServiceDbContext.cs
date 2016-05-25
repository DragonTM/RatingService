using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RatingService.Domain.Entities;

namespace RatingService.Dal
{
	public class RatingServiceDbContext : DbContext
	{
		public RatingServiceDbContext()
			: base("name=RatingServiceDbContext")
		{
		}

		public virtual DbSet<User> Users { get; set; }

		public virtual DbSet<Enterprise> Enterprises { get; set; }

		public virtual DbSet<Rating> Ratings { get; set; }

		public virtual DbSet<Suggestion> Suggestions { get; set; }

		public virtual DbSet<Question> Questions { get; set; }

		public virtual DbSet<Answer> Answers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<User>();

			modelBuilder.Entity<Enterprise>()
				.HasRequired(e => e.User)
				.WithOptional(u => u.Enterprise);

			modelBuilder.Entity<Enterprise>()
				.HasMany(e => e.Ratings)
				.WithRequired(r => r.Enterprise)
				.HasForeignKey(r => r.EnterpriseId);

			modelBuilder.Entity<Enterprise>()
				.HasMany(e => e.Answers)
				.WithRequired(a => a.Enterprise)
				.HasForeignKey(a => a.EnterpriseId);

			modelBuilder.Entity<Question>()
				.HasMany(q => q.Answers)
				.WithRequired(a => a.Question)
				.HasForeignKey(a => a.QuestionId);

			modelBuilder.Entity<Suggestion>();

			base.OnModelCreating(modelBuilder);
		}
	}
}
