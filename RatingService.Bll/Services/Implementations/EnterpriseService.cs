using RaitngService.Bll.Services.Interfaces;
using RatingService.Dal.Interfaces;
using RatingService.Domain.Entities;

namespace RaitngService.Bll.Services.Implementations
{
	public class EnterpriseService : IEnterpriseService
	{
		private readonly IUnitOfWork _uow;
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<Enterprise> _enterpriseRepository;
		private readonly IProtectingService _protectingService;

		public EnterpriseService(IUnitOfWork uow, IRepository<User> userRepository, IRepository<Enterprise> enterpriseRepository, IProtectingService protectingService)
		{
			_uow = uow;
			_userRepository = userRepository;
			_enterpriseRepository = enterpriseRepository;
			_protectingService = protectingService;
		}

		public void Register(string userName, string password, Enterprise enterprise)
		{
			var user = new User
			{
				Login = userName,
				Password = _protectingService.Encrypt(password)
			};

			enterprise.User = user;

			_userRepository.Create(user);
			_enterpriseRepository.Create(enterprise);

			_uow.SaveChanges();
		}
	}
}
