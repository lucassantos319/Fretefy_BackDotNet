using System.Collections.Generic;
using System.Linq;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoRepository : IRegiaoRepository
    {
        private readonly DbContext _dbContext;
        private DbSet<Regiao> _dbSet;
        
        public RegiaoRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Regiao>();
            _dbContext = dbContext;
        }
        public IQueryable<Regiao> List()
        {
            return _dbSet
                .Include(x=>x.RegiaoCidades)
                .ThenInclude(x=>x.Cidade)
                .AsQueryable();
        }

        public bool Create(Regiao regiao)
        {
            if (_dbSet.Add(regiao).State == EntityState.Added)
            {
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Update(Regiao regiao)
        {
            if (_dbSet.Update(regiao).State == EntityState.Modified)
            {
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<Regiao> Query(string terms)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.Nome, $"%{terms}%"));
        }
    }
}