namespace RatingService.Domain.Entities
{
	public class Answer
	{
		public int Id { get; set; }

		public int QuestionId { get; set; }

		public float Result { get; set; }

		public int EnterpriseId { get; set; }

		public virtual Enterprise Enterprise { get; set; }

		public virtual Question Question { get; set; }
	}
}
