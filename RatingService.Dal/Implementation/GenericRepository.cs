using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RatingService.Dal.Interfaces;

namespace RatingService.Dal.Implementation
{
	public class GenericRepository<T> : IRepository<T> where T : class
	{
		protected readonly DbContext _context;
		protected readonly DbSet<T> _set;

		public GenericRepository(DbContext context)
		{
			_context = context;
			_set = context.Set<T>();
		}

		public IEnumerable<T> Get()
		{
			return _set;
		}

		public IEnumerable<T> Get(Expression<Func<T, bool>> expression, int? skip, int? take)
		{
			var entities = _set.Where(expression);

			if (skip.HasValue)
			{
				entities = entities.Skip(skip.Value);
			}

			if (take.HasValue)
			{
				entities = entities.Take(take.Value);
			}

			return entities;
		}

		public T Get(int id)
		{
			return _set.Find(id);
		}

		public T FirstOrDefault(Expression<Func<T, bool>> expression)
		{
			return _set.FirstOrDefault(expression);
		}

		public void Create(T model)
		{
			_set.Add(model);
		}

		public void Update(T model)
		{
			_context.Entry(model).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			var item = _set.Find(id);

			_set.Remove(item);
		}
	}
}
