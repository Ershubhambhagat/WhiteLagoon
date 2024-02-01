using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WhiteLagoon.Application.Common.Interface;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaNumberRepository
    {
        #region CTOR
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
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
