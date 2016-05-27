using System;
using System.Collections.Generic;
using RaitngService.Bll.Services.Interfaces;
using RaitngService.Bll.Strategies.Implementatios;
using RatingService.Bll.Strategies.Implementatios;
using RatingService.Bll.Strategies.Interfaces;
using RatingService.Dal.Interfaces;
using RatingService.Domain.Entities;

namespace RaitngService.Bll.Services.Implementations
{
	public class RatingService : IRatingService
	{
		private readonly IRepository<Enterprise> _enterpriseRepository;
		private readonly IRepository<Question> _questionsRepository;
		private readonly IRepository<Answer> _answersRepository;
		private readonly IRepository<Rating> _ratingsRepository;
		private readonly IRepository<Suggestion> _suggestionsRepository;

		public RatingService(IRepository<Question> questionsRepository, IRepository<Answer> answersRepository, IRepository<Rating> ratingsRepository, IRepository<Suggestion> suggestionsRepository, IRepository<Enterprise> enterpriseRepository)
		{
			_questionsRepository = questionsRepository;
			_answersRepository = answersRepository;
			_ratingsRepository = ratingsRepository;
			_suggestionsRepository = suggestionsRepository;
			_enterpriseRepository = enterpriseRepository;
		}

		public IEnumerable<Question> GetQuestions(RatingType ratingType)
		{
			return _questionsRepository.Get(q => q.RatingType == ratingType);
		}

		public Suggestion SaveAnswers(int enterpriseId, RatingType ratingType, IEnumerable<Answer> answers)
		{
			var enterprise = _enterpriseRepository.Get(enterpriseId);

			foreach (var answer in answers)
			{
				answer.EnterpriseId = enterpriseId;
				_answersRepository.Create(answer);
			}

			var ratingStrategy = ResolveStrategy(ratingType);

			var ratingPoints = ratingStrategy.EvaluateRating(enterprise);

			_ratingsRepository.Create(new Rating
			{
				EnterpriseId = enterpriseId,
				Points = (int)Math.Round(ratingPoints),
				RatingType = ratingType
			});

			return _suggestionsRepository.FirstOrDefault(s => s.RatingType == ratingType && Math.Abs(s.CriticalValue - ratingPoints) < 10);
		}

		private IRatingStrategy ResolveStrategy(RatingType ratingType)
		{
			IRatingStrategy strategy;

			switch (ratingType)
			{
				case RatingType.Test:
					strategy = new TestRatingStrategy();
					break;
				case RatingType.Universal:
					strategy = new UniversalRatingStrategy();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(ratingType), ratingType, null);
			}

			return strategy;
		}
	}
}
