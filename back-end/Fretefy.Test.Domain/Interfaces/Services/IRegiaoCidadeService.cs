using System;
using System.Collections.Generic;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface IRegiaoCidadeService
    {
        RegiaoCidade Get(Guid id);
        IEnumerable<RegiaoCidade> List();
        void Create(Regiao regiao,IEnumerable<Cidade> cidades);
        ResultResponse<bool> Update(RegiaoCidade regiao);
        IEnumerable<RegiaoCidade> GetByRegionId(Guid regionId);
    }
}