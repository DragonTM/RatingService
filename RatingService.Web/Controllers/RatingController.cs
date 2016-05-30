using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using RatingService.Bll.Services.Interfaces;
using RatingService.Domain.Entities;
using RatingService.Web.Models;

namespace RatingService.Web.Controllers
{
	public class RatingController : Controller
	{
		private readonly IRatingService _ratingService;
		private readonly IMapper _mapper;

		public RatingController(IRatingService ratingService, IMapper mapper)
		{
			_ratingService = ratingService;
			_mapper = mapper;
		}

		// GET: Rating
		public ActionResult Index(RatingType ratingType = RatingType.Universal, int? top = null)
		{
			var ratingsStatistic = _ratingService.GetResults(ratingType, top);

			var statisticViewModel = new StatisticsViewModel
			{
				RatingType = ratingType.ToString(),
				Ratings = _mapper.Map<IEnumerable<RatingViewModel>>(ratingsStatistic),
				RaitngTypes = GetRatings()
			};

			return View(statisticViewModel);
		}

		public ActionResult Calculate(RatingType ratingType = RatingType.Universal)
		{
			var setRatingViewModel = new SetRatingViewModel
			{
				Questions = _mapper.Map<IEnumerable<QuestionViewModel>>(_ratingService.GetQuestions(ratingType)),
				RatingType = ratingType.ToString(),
				RaitngTypes = GetRatings()
			};

			return View("Create", setRatingViewModel);
		}

		[HttpPost]
		public ActionResult Calculate(CalculateViewModel model)
		{
			var enterpriseId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);

			_ratingService.SaveAnswers(enterpriseId, model.RatingType, _mapper.Map<IEnumerable<Answer>>(model.Answers));

			return RedirectToAction("Index", new { ratingType = model.RatingType });
		}

		private IEnumerable<string> GetRatings()
		{
			return new List<string> { RatingType.Universal.ToString(), RatingType.Test.ToString() };
		}
	}
}