using RatingService.Domain.Entities;

namespace RaitingService.Dal.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<RatingService.Dal.RatingServiceDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			ContextKey = "RatingService.Dal.RatingServiceDbContext";
		}

		protected override void Seed(RatingService.Dal.RatingServiceDbContext context)
		{
			context.Questions.AddOrUpdate(new Question
			{
				Id = 1,
				RatingType = RatingType.Test,
				Text = "What do you think about this site?",
				Value = 1
			});

			context.Questions.AddOrUpdate(new Question
			{
				Id = 2,
				RatingType = RatingType.Test,
				Text = "What do you think about this site?2",
				Value = 1
			});

			context.SaveChanges();
		}
	}
}
