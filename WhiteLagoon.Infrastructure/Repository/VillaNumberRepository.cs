using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interface;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        #region CTOR
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        #endregion

        public void Remove(VillaNumber entity)
        {
            _db.VillaNumber.Remove(entity);
            save();
        }
        public void Update(VillaNumber entity)
        {
           _db.Update(entity);
            save();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
