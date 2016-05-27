namespace RatingService.Bll.Services.Interfaces
{
	public interface IProtectingService
	{
		string Encrypt(string origin);

		string Decrypt(string encrypted);
	}
}
