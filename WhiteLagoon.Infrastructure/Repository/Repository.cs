using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WhiteLagoon.Application.Common.Interface;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region CTOR
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = db.Set<T>();
        }
        #endregion

        #region Create 
        public void Create(T entity)
        {
            _db.Add(entity);
            save();
        }
        #endregion

        #region Get ALL 

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> quary = dbSet;
            if (filter != null)
            {
                quary = quary.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                //villa , villanumber --case sensitive
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quary = quary.Include(property);
                }
            }
                return quary.ToList();
        }
        #endregion

        #region Get 1 
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> quary = dbSet;
            if (filter != null)
            {
                quary = quary.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                //villa , villanumber --case sensitive
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quary = quary.Include(property);
                }
            }
            return quary.FirstOrDefault();

        }
        #endregion

        #region Any
        public bool Any(Expression<Func<T, bool>>? filter)
        {
            return dbSet.Any(filter);
        }


        #endregion

        #region save 
        public void save()
        {
            _db.SaveChanges();
        }
        #endregion
    }
}
