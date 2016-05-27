using System.Collections.Generic;
using RatingService.Domain.Entities;

namespace RatingService.Bll.Services.Interfaces
{
	public interface IRatingService
	{
		IEnumerable<Question> GetQuestions(RatingType ratingType);

		Suggestion SaveAnswers(int enterpriseId, RatingType ratingType, IEnumerable<Answer> answers);
	}
}
