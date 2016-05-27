using RatingService.Domain.Entities;

namespace RatingService.Bll.Services.Interfaces
{
	public interface IEnterpriseService
	{
		void Register(string userName, string password, Enterprise enterprise);
	}
}
