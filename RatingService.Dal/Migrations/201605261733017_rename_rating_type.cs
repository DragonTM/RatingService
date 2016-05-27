using System.Data.Entity.Migrations;

namespace RatingService.Dal.Migrations
{
	public partial class rename_rating_type : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Rating", "RatingType", c => c.Int(nullable: false));
			AddColumn("dbo.Question", "RatingType", c => c.Int(nullable: false));
			AddColumn("dbo.Suggestion", "RatingType", c => c.Int(nullable: false));
			DropColumn("dbo.Rating", "RaitngType");
			DropColumn("dbo.Question", "RaitngType");
			DropColumn("dbo.Suggestion", "RaitngType");
		}

		public override void Down()
		{
			AddColumn("dbo.Suggestion", "RaitngType", c => c.Int(nullable: false));
			AddColumn("dbo.Question", "RaitngType", c => c.Int(nullable: false));
			AddColumn("dbo.Rating", "RaitngType", c => c.Int(nullable: false));
			DropColumn("dbo.Suggestion", "RatingType");
			DropColumn("dbo.Question", "RatingType");
			DropColumn("dbo.Rating", "RatingType");
		}
	}
}
