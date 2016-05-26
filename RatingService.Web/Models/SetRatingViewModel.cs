using System.Collections.Generic;

namespace RatingService.Web.Models
{
	public class SetRatingViewModel
	{
		public IEnumerable<string> RaitngTypes { get; set; }

		public string RatingType { get; set; }

		public IEnumerable<QuestionViewModel> Questions { get; set; }
	}
}