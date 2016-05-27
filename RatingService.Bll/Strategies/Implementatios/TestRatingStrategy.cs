using System.Linq;
using RatingService.Bll.Strategies.Interfaces;
using RatingService.Domain.Entities;

namespace RaitngService.Bll.Strategies.Implementatios
{
	public class TestRatingStrategy : IRatingStrategy
	{
		public float EvaluateRating(Enterprise enterprise)
		{
			return enterprise.Answers.Where(a => a.Question.RatingType == RatingType.Test).Sum(a => a.Result * a.Question.Value) / enterprise.Answers.Count(a => a.Question.RatingType == RatingType.Test);
		}
	}
}
