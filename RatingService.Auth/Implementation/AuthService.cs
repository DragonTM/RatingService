using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using RaitngService.Bll.Services.Interfaces;
using RatingService.Auth.Interfaces;
using RatingService.Auth.Utils;
using RatingService.Dal.Interfaces;
using RatingService.Domain.Entities;

namespace RatingService.Auth.Implementation
{
	public class AuthService : IAuthService
	{
		public const string CookiesIdentifier = "RatingServiceCookies";
		public const string ProtectionPurpose = "Authorization token";

		private IRepository<User> _userRepository;
		private IProtectingService _protectingService;

		public AuthService(IRepository<User> useRepository, IProtectingService protectingService)
		{
			_userRepository = useRepository;
			_protectingService = protectingService;
		}

		public bool Login(string userName, string password)
		{
			var user = _userRepository.FirstOrDefault(u => u.Login == userName);

			if (user == null)
			{
				return false;
			}

			if (_protectingService.Decrypt(user.Password) != password)
			{
				return false;
			}

			var identity = new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }, "RatingService auth", ClaimTypes.Name, ClaimTypes.Role);

			var principal = new ClaimsPrincipal(identity);

			AddCookieFromIdentity(identity);

			HttpContext.Current.User = principal;

			return true;
		}

		public void Logout()
		{
			HttpContext.Current.Response.Cookies[CookiesIdentifier].Value = string.Empty;
		}

		private void AddCookieFromIdentity(ClaimsIdentity identity, DateTime? expirationTime = null)
		{
			var authProperties = new AuthenticationProperties()
			{
				IsPersistent = false,
				ExpiresUtc = expirationTime ?? DateTime.UtcNow.AddHours(24),
				IssuedUtc = DateTime.UtcNow
			};

			var ticket = new AuthenticationTicket(identity, authProperties);

			var ticketFormat = new TicketDataFormat(new DataProtector(ProtectionPurpose));
			var serializedTicked = ticketFormat.Protect(ticket);

			var cookie = new HttpCookie(CookiesIdentifier, serializedTicked);

			HttpContext.Current.Response.Cookies.Add(cookie);
		}
	}
}
