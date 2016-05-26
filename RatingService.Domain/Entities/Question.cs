using System.Collections.Generic;

namespace RatingService.Domain.Entities
{
	public class Question
	{
		public int Id { get; set; }

		public RatingType RatingType { get; set; }

		public string Text { get; set; }

		public float Value { get; set; }

		public virtual ICollection<Answer> Answers { get; set; }
	}
}
