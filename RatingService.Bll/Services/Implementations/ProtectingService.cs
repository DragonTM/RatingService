using System;
using System.Security.Cryptography;
using System.Text;
using RaitngService.Bll.Services.Interfaces;

namespace RaitngService.Bll.Services.Implementations
{
	public class ProtectingService : IProtectingService
	{
		public string Encrypt(string origin)
		{
			var data = Encoding.Unicode.GetBytes(origin);
			var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

			return Convert.ToBase64String(encrypted);
		}

		public string Decrypt(string encrypted)
		{
			var data = Convert.FromBase64String(encrypted);
			var decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);

			return Encoding.Unicode.GetString(decrypted);
		}
	}
}
