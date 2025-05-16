using System;
using System.Collections.Generic;
using System.Linq;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoCidadeRepository
    {
        IQueryable<RegiaoCidade> List();
        bool Create(RegiaoCidade regiao);
        bool Update(RegiaoCidade regiao);
        IEnumerable<RegiaoCidade> GetByRegionId(Guid regionId);
    }
}