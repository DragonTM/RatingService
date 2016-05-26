using System.Web.Mvc;
using AutoMapper;
using RaitngService.Bll.Services.Interfaces;
using RatingService.Auth.Interfaces;
using RatingService.Domain.Entities;
using RatingService.Web.Models;

namespace RatingService.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IEnterpriseService _enterpriseService;
		private readonly IAuthService _authService;

		public AccountController(IEnterpriseService enterpriseService, IMapper mapper, IAuthService authService)
		{
			_enterpriseService = enterpriseService;
			_mapper = mapper;
			_authService = authService;
		}

		public ActionResult Login()
		{
			return View();
		}


		[HttpPost]
		public ActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Password = string.Empty;

				return View(model);
			}

			if (!_authService.Login(model.UserName, model.Password))
			{
				ModelState.AddModelError("", "Wrong credentials");

				model.Password = string.Empty;

				return View(model);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegistrationViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var enterprise = _mapper.Map<Enterprise>(model);

			_enterpriseService.Register(model.UserName, model.Password, enterprise);

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Logout()
		{
			_authService.Logout();

			return RedirectToAction("Index", "Home");
		}
	}
}