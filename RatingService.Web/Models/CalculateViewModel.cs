using System.Collections.Generic;
using RatingService.Domain.Entities;

namespace RatingService.Web.Models
{
	public class CalculateViewModel
	{
		public RatingType RatingType { get; set; }

		public IEnumerable<Answer> Answers { get; set; }
	}
}