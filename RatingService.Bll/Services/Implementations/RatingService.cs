﻿using System;
using System.Collections.Generic;
using System.Linq;
using RatingService.Bll.Services.Interfaces;
using RatingService.Bll.Strategies.Implementatios;
using RatingService.Bll.Strategies.Interfaces;
using RatingService.Dal.Interfaces;
using RatingService.Domain.Entities;

namespace RatingService.Bll.Services.Implementations
{
	public class RatingService : IRatingService
	{
		private readonly IRepository<Enterprise> _enterpriseRepository;
		private readonly IRepository<Question> _questionsRepository;
		private readonly IRepository<Answer> _answersRepository;
		private readonly IRepository<Rating> _ratingsRepository;
		private readonly IRepository<Suggestion> _suggestionsRepository;
		private readonly IUnitOfWork _uow;

		public RatingService(IRepository<Question> questionsRepository, IRepository<Answer> answersRepository, IRepository<Rating> ratingsRepository, IRepository<Suggestion> suggestionsRepository, IRepository<Enterprise> enterpriseRepository, IUnitOfWork uow)
		{
			_questionsRepository = questionsRepository;
			_answersRepository = answersRepository;
			_ratingsRepository = ratingsRepository;
			_suggestionsRepository = suggestionsRepository;
			_enterpriseRepository = enterpriseRepository;
			_uow = uow;
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
				answer.Question = _questionsRepository.Get(answer.QuestionId);

				var entity = _answersRepository.FirstOrDefault(a => a.QuestionId == answer.QuestionId && a.EnterpriseId == enterpriseId);

				if (entity == null)
				{
					_answersRepository.Create(answer);
				}
				else
				{
					entity.Result = answer.Result;

					_answersRepository.Update(entity);
				}
			}

			var enterprise = _enterpriseRepository.Get(enterpriseId);

			var ratingStrategy = ResolveStrategy(ratingType);

			var ratingPoints = ratingStrategy.EvaluateRating(enterprise);

			_ratingsRepository.Create(new Rating
			{
				EnterpriseId = enterpriseId,
				Points = (int)Math.Round(ratingPoints),
				RatingType = ratingType
			});

			_uow.SaveChanges();

			return _suggestionsRepository.FirstOrDefault(s => s.RatingType == ratingType && Math.Abs(s.CriticalValue - ratingPoints) < 10);
		}

		public IEnumerable<Rating> GetResults(RatingType ratingType, int? take = null)
		{
			var ratings = _ratingsRepository.Get(r => r.RatingType == ratingType).OrderByDescending(r => r.Points);

			if (take.HasValue)
			{
				return ratings.Take(take.Value);
			}

			return ratings;
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
