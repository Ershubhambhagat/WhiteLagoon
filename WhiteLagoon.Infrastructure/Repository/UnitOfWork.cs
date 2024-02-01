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
    public class UnitOfWork : IUnitOfWork
    {
        public IVillaNumberRepository VillaNumber { get; private set; }
        public IVillaRepository Villa { get; private set; }


        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            VillaNumber = new VillaNumberRepository(_db);
            Villa = new VillaRepository(_db);
        }
    }
}
