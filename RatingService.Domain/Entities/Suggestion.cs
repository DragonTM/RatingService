namespace RatingService.Domain.Entities
{
	public class Suggestion
	{
		public int Id { get; set; }

		public RatingType RatingType { get; set; }

		public int CriticalValue { get; set; }

		public string Text { get; set; }
	}
}
