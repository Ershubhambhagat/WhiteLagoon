using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WhiteLagoon.Application.Common.Interface;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class VillaRepository : IVillaRepository
    {
        #region CTOR
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Create 
        public void Create(Villa entity)
        {
            _db.Add(entity);
        }
        #endregion

        #region Get ALL 

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> quary = _db.Set<Villa>();
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
        public Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Villa> quary = _db.Set<Villa>();
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

        #region Update  
        public void Update(Villa entity)
        {
            _db.Villas.Update(entity);
            Save();
        }
        #endregion

        #region Remove 
        public void Remove(Villa entity)
        {
            _db.Remove(entity);
            Save();
        }

        #endregion

        #region Save 
        public void Save()
        {
            _db.SaveChanges();
        }
        #endregion
    }
}
