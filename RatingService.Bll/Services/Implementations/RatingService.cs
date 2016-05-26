using System;
using System.Collections.Generic;
using System.Linq;
using RaitngService.Bll.Services.Interfaces;
using RatingService.Dal.Interfaces;
using RatingService.Domain.Entities;

namespace RaitngService.Bll.Services.Implementations
{
	public class RatingService : IRatingService
	{
		private readonly IRepository<Question> _questionsRepository;
		private readonly IRepository<Answer> _answersRepository;
		private readonly IRepository<Rating> _ratingsRepository;
		private readonly IRepository<Suggestion> _suggestionsRepository;

		public RatingService(IRepository<Question> questionsRepository, IRepository<Answer> answersRepository, IRepository<Rating> ratingsRepository, IRepository<Suggestion> suggestionsRepository)
		{
			_questionsRepository = questionsRepository;
			_answersRepository = answersRepository;
			_ratingsRepository = ratingsRepository;
			_suggestionsRepository = suggestionsRepository;
		}

		public IEnumerable<Question> GetQuestions(RatingType ratingType)
		{
			return _questionsRepository.Get(q => q.RatingType == ratingType);
		}

		public Suggestion SaveAnswers(int enterpriseId, RatingType ratingType, IEnumerable<Answer> answers)
		{
			foreach (var answer in answers)
			{
				answer.EnterpriseId = enterpriseId;
				_answersRepository.Create(answer);
			}

			var ratingPoints = _answersRepository
								.Get(a => a.EnterpriseId == enterpriseId && a.Question.RatingType == ratingType)
								.Sum(a => a.Result * a.Question.Value);

			_ratingsRepository.Create(new Rating
			{
				EnterpriseId = enterpriseId,
				Points = (int)Math.Round(ratingPoints),
				RatingType = ratingType
			});

			return _suggestionsRepository.FirstOrDefault(s => s.RatingType == ratingType && Math.Abs(s.CriticalValue - ratingPoints) < 10);
		}
	}
}
