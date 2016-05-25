namespace RatingService.Domain.Entities
{
	public class Rating
	{
		public int Id { get; set; }

		public RatingType RaitngType { get; set; }

		public int Points { get; set; }

		public int EnterpriseId { get; set; }

		public virtual Enterprise Enterprise { get; set; }
	}
}
