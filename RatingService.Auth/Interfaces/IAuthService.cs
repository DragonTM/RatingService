namespace RatingService.Auth.Interfaces
{
	public interface IAuthService
	{
		bool Login(string userName, string password);

		void Logout();
	}
}
