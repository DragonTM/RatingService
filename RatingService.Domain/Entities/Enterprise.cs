using System.Collections.Generic;

namespace RatingService.Domain.Entities
{
	public class Enterprise
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Director { get; set; }

		public string KeyIdentifier { get; set; }

		public virtual User User { get; set; }

		public virtual ICollection<Rating> Ratings { get; set; }

		public virtual ICollection<Answer> Answers { get; set; }
	}
}
