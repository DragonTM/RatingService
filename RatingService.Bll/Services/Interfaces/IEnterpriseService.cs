using RatingService.Domain.Entities;

namespace RaitngService.Bll.Services.Interfaces
{
	public interface IEnterpriseService
	{
		void Register(string userName, string password, Enterprise enterprise);
	}
}
