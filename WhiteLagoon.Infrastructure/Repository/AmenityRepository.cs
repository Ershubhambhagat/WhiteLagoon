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
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        #region CTOR
        private readonly ApplicationDbContext _db;
        public AmenityRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        #endregion

        public void Remove(Amenity entity)
        {
            _db.Amenities.Remove(entity);
            save();
        }
        public void Update(Amenity entity)
        {
           _db.Amenities.Update(entity);
            save();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
