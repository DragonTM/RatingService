using System.Linq;
using RatingService.Bll.Strategies.Interfaces;
using RatingService.Domain.Entities;

namespace RatingService.Bll.Strategies.Implementatios
{
	internal class UniversalRatingStrategy : IRatingStrategy
	{
		public float EvaluateRating(Enterprise enterprise)
		{
			return enterprise.Answers.Where(a => a.Question.RaitngType == RatingType.Universal).Sum(a => a.Result * a.Question.Value);
		}
	}
}
