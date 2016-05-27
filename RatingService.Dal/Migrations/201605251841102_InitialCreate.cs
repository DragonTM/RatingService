using System.Data.Entity.Migrations;

namespace RatingService.Dal.Migrations
{
	public partial class InitialCreate : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Answer",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					QuestionId = c.Int(nullable: false),
					Result = c.Single(nullable: false),
					EnterpriseId = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Enterprise", t => t.EnterpriseId, cascadeDelete: true)
				.ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
				.Index(t => t.QuestionId)
				.Index(t => t.EnterpriseId);

			CreateTable(
				"dbo.Enterprise",
				c => new
				{
					Id = c.Int(nullable: false),
					Name = c.String(),
					Director = c.String(),
					KeyIdentifier = c.String(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.User", t => t.Id)
				.Index(t => t.Id);

			CreateTable(
				"dbo.Rating",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					RaitngType = c.Int(nullable: false),
					Points = c.Int(nullable: false),
					EnterpriseId = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Enterprise", t => t.EnterpriseId, cascadeDelete: true)
				.Index(t => t.EnterpriseId);

			CreateTable(
				"dbo.User",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Login = c.String(),
					Password = c.String(),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.Question",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					RaitngType = c.Int(nullable: false),
					Text = c.String(),
					Value = c.Single(nullable: false),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.Suggestion",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					RaitngType = c.Int(nullable: false),
					CriticalValue = c.Int(nullable: false),
					Text = c.String(),
				})
				.PrimaryKey(t => t.Id);

		}

		public override void Down()
		{
			DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
			DropForeignKey("dbo.Enterprise", "Id", "dbo.User");
			DropForeignKey("dbo.Rating", "EnterpriseId", "dbo.Enterprise");
			DropForeignKey("dbo.Answer", "EnterpriseId", "dbo.Enterprise");
			DropIndex("dbo.Rating", new[] { "EnterpriseId" });
			DropIndex("dbo.Enterprise", new[] { "Id" });
			DropIndex("dbo.Answer", new[] { "EnterpriseId" });
			DropIndex("dbo.Answer", new[] { "QuestionId" });
			DropTable("dbo.Suggestion");
			DropTable("dbo.Question");
			DropTable("dbo.User");
			DropTable("dbo.Rating");
			DropTable("dbo.Enterprise");
			DropTable("dbo.Answer");
		}
	}
}
