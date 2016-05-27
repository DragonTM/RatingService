using System.Data.Entity;
using Microsoft.Practices.Unity;
using RatingService.Auth.Implementation;
using RatingService.Auth.Interfaces;
using RatingService.Bll.Services.Implementations;
using RatingService.Bll.Services.Interfaces;
using RatingService.Dal;
using RatingService.Dal.Implementation;
using RatingService.Dal.Interfaces;

namespace RatingService.Profile.IocProfiles
{
	public class ConfigureUnity
	{
		public static void Init(IUnityContainer container)
		{
			InitDal(container);
			InitBll(container);
			InitAuth(container);
		}

		private static void InitDal(IUnityContainer container)
		{
			container.RegisterType<DbContext, RatingServiceDbContext>(new PerResolveLifetimeManager());
			container.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>));
			container.RegisterType<IUnitOfWork, UnitOfWork>();
		}

		private static void InitBll(IUnityContainer container)
		{
			container.RegisterType<IProtectingService, ProtectingService>();
			container.RegisterType<IEnterpriseService, EnterpriseService>();
			container.RegisterType<IRatingService, Bll.Services.Implementations.RatingService>();
		}

		private static void InitAuth(IUnityContainer container)
		{
			container.RegisterType<IAuthService, AuthService>();
		}
	}
}
