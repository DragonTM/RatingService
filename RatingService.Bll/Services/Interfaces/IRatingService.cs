using System.Collections.Generic;
using RatingService.Domain.Entities;

namespace RaitngService.Bll.Services.Interfaces
{
	public interface IRatingService
	{
		IEnumerable<Question> GetQuestions(RatingType ratingType);

		Suggestion SaveAnswers(int enterpriseId, RatingType ratingType, IEnumerable<Answer> answers);
	}
}
