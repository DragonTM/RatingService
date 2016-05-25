using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RatingService.Dal.Interfaces
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> Get();

		IEnumerable<T> Get(Expression<Func<T, bool>> expression, int? skip, int? take);

		T Get(int id);

		T FirstOrDefault(Expression<Func<T, bool>> expression);

		void Create(T model);

		void Update(T model);

		void Delete(int id);
	}
}
