namespace RaitngService.Bll.Services.Interfaces
{
	public interface IProtectingService
	{
		string Encrypt(string origin);

		string Decrypt(string encrypted);
	}
}
