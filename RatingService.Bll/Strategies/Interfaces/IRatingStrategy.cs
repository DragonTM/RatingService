using RatingService.Domain.Entities;

namespace RatingService.Bll.Strategies.Interfaces
{
	internal interface IRatingStrategy
	{
		float EvaluateRating(Enterprise enterprise);
	}
}
