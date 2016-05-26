using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using RaitngService.Bll.Services.Interfaces;
using RatingService.Domain.Entities;
using RatingService.Web.Models;

namespace RatingService.Web.Controllers
{
	public class RatingController : Controller
	{
		private readonly RaitngService.Bll.Services.Interfaces.IRatingService _ratingService;
		private readonly IMapper _mapper;

		public RatingController(IRatingService ratingService, IMapper mapper)
		{
			_ratingService = ratingService;
			_mapper = mapper;
		}

		// GET: Rating
		public ActionResult Index()
		{
			return View();
		}

		[ActionName("fill")]
		public ActionResult Create(RatingType ratingType = RatingType.Universal)
		{
			var setRatingViewModel = new SetRatingViewModel
			{
				Questions = _mapper.Map<IEnumerable<QuestionViewModel>>(_ratingService.GetQuestions(ratingType)),
				RatingType = ratingType.ToString(),
				RaitngTypes = new List<string> { RatingType.Universal.ToString(), RatingType.Test.ToString() }
			};

			return View("Create", setRatingViewModel);
		}

		public ActionResult Calculate(CalculateViewModel model)
		{
			return RedirectToAction("Index", "Home");
		}
	}
}