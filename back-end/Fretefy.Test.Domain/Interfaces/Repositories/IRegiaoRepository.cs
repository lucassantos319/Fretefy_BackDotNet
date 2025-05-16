using System.Collections.Generic;
using System.Linq;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        IQueryable<Regiao> List();
        bool Create(Regiao regiao);
        bool Update(Regiao regiao);
        
        IEnumerable<Regiao> Query(string terms);
    }
}