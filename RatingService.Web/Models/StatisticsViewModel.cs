using System.Collections.Generic;

namespace RatingService.Web.Models
{
	public class StatisticsViewModel
	{
		public IEnumerable<RatingViewModel> Ratings { get; set; }

		public string RatingType { get; set; }

		public IEnumerable<string> RaitngTypes { get; set; }
	}
}