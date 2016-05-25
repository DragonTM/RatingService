using System;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security.DataHandler;
using RatingService.Auth.Implementation;
using RatingService.Auth.Utils;

namespace RatingService.Auth
{
	public class AuthModule : IHttpModule
	{
		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.PostAuthenticateRequest += ReplacePrincipal;
		}

		private static void ReplacePrincipal(object sender, EventArgs args)
		{
			var cookie = HttpContext.Current.Request.Cookies[AuthService.CookiesIdentifier];

			if (cookie == null)
			{
				return;
			}

			var ticketFormat = new TicketDataFormat(new DataProtector(AuthService.ProtectionPurpose));
			var ticket = ticketFormat.Unprotect(cookie.Value);

			if (ticket != null)
			{
				var principal = new ClaimsPrincipal(ticket.Identity);
				HttpContext.Current.User = principal;
			}
		}
	}
}
