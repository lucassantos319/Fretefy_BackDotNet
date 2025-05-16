using System;
using System.Collections.Generic;
using System.Linq;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Enums;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;

namespace Fretefy.Test.Domain.Services
{
    public class RegiaoCidadeService : IRegiaoCidadeService
    {
        private readonly IRegiaoCidadeRepository _repository;

        public RegiaoCidadeService(IRegiaoCidadeRepository repository)
        {
            _repository = repository;
        }
        
        public RegiaoCidade Get(Guid id)
        {
            return _repository.List().FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<RegiaoCidade> List()
        {
            return _repository.List();
        }

        public void Create(Regiao regiao, IEnumerable<Cidade> cidades)
        {
            var registers = GetByRegionId(regiao.Id); 
            if (registers != null && !registers.Any(x=>cidades.Any(y=>y.Id == x.CidadeId)))
            {
                foreach (var cidade in cidades)
                    _repository.Create(new RegiaoCidade() 
                    {
                        Id = new Guid(),
                        Status = EStatus.Ativo,
                        RegiaoId = regiao.Id,
                        CidadeId = cidade.Id,
                    });
            }
        }

        public IEnumerable<RegiaoCidade> GetByRegionId(Guid regionId)
        {
            return _repository.GetByRegionId(regionId);
        }
        
        public ResultResponse<bool> Update(RegiaoCidade regiao)
        {
            var result = _repository.Update(regiao);
            return new ResultResponse<bool>
            {
                success = result
            };
        }
    }
}