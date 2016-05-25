using System.Data.Entity;
using RatingService.Dal.Interfaces;

namespace RatingService.Dal.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;

		public UnitOfWork(DbContext context)
		{
			_context = context;
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
