using System;
using System.Collections.Generic;
using System.Linq;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoCidadeRepository : IRegiaoCidadeRepository
    {
        private readonly DbContext _dbContext;
        private DbSet<RegiaoCidade> _dbSet;
        
        public RegiaoCidadeRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<RegiaoCidade>();
            _dbContext = dbContext;
        }
        public IQueryable<RegiaoCidade> List()
        {
            return _dbSet.AsQueryable();
        }

        public bool Create(RegiaoCidade regiao)
        {
            if (_dbSet.Add(regiao).State == EntityState.Added)
            {
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Update(RegiaoCidade regiao)
        {
            if (_dbSet.Update(regiao).State == EntityState.Modified)
            {
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<RegiaoCidade> GetByRegionId(Guid regionId)
        {
            return _dbSet
                .Where(x => x.RegiaoId == regionId);
        }
    }
}