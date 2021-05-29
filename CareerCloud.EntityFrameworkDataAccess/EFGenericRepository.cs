using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.DataAccessLayer;
using System.Data.Entity;

namespace CareerCloud.EntityFrameworkDataAccess
{
	public class EFGenericRepository<T> : IDataRepository<T> where T : class

	{
		private CareerCloudContext _mycontext;
		public EFGenericRepository()
		{
			_mycontext = new CareerCloudContext();
			
		}
		public void Add(params T[] items)
		{
			foreach (var item in items)
			{
				_mycontext.Entry(item).State = System.Data.Entity.EntityState.Added;
			}
			_mycontext.SaveChanges();
		}

		public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
		{
			throw new NotImplementedException();
		}

		public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
		{
			IQueryable<T> dbQuery = _mycontext.Set<T>();
			foreach (var navigationProperty in navigationProperties)
			{
				dbQuery = dbQuery.Include<T, object>(navigationProperty);
			}
			return dbQuery.ToList<T>();
		}

		public IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
		{
			IQueryable<T> dbQuery = _mycontext.Set<T>();
			foreach (Expression<Func<T, object>> navigationproperty in navigationProperties)
			{
				dbQuery = dbQuery.Include<T, object>(navigationproperty);
			}
			return dbQuery.AsNoTracking().Where(where).ToList<T>();
		}

		public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
		{
			IQueryable<T> dbQuery = _mycontext.Set<T>();
			foreach (Expression<Func<T, object>> navigationproperty in navigationProperties)
			{
				dbQuery = dbQuery.Include<T, object>(navigationproperty);
			}
			return dbQuery.FirstOrDefault(where);

		}

		public void Remove(params T[] items)
		{
			foreach (T item in items)
			{
				_mycontext.Entry(item).State = EntityState.Deleted;
			}
			_mycontext.SaveChanges();
		}
	

		public void Update(params T[] items)
		{
			foreach (T item in items)
			{
				_mycontext.Entry(item).State = EntityState.Modified;
			}
			_mycontext.SaveChanges();
		}
	}
}
