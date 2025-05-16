using System;
using System.Collections.Generic;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface IRegiaoService
    {
        Regiao Get(Guid id);
        IEnumerable<Regiao> List();
        ResultResponse<Regiao> Create(Regiao regiao,IEnumerable<Cidade> cidades);
        ResultResponse<Regiao> Update(Regiao regiao, IEnumerable<Cidade> cidades);
        IEnumerable<Regiao> Query(string terms);
    }
}