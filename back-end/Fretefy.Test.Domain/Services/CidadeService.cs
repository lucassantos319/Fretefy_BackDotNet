using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fretefy.Test.Domain.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public Cidade Get(Guid id)
        {
            return _cidadeRepository.List().FirstOrDefault(f => f.Id == id);
        }

        private void ErrorHandler(ref ResultResponse<Cidade> result,string msg)
        {
            result.success = false;
            result.error = msg;
        }
        
        public ResultResponse<Cidade> ValidateCities(IEnumerable<Cidade> regiaoCidades)
        {
            var validCitiesResponse = new ResultResponse<Cidade>()
            {
                message = "Criado região com sucesso",
                success = true,
            };
            
            if (regiaoCidades == null || !regiaoCidades.Any() )
                ErrorHandler(ref validCitiesResponse,"Região não possui nenhum cidade !");

            var existedCities = List().Where(x => regiaoCidades.Any(y => y.Nome.Equals(x.Nome))).ToList();
            if (existedCities.Count() != regiaoCidades.Count())
                ErrorHandler(ref validCitiesResponse, "Cidade indicada não existe no cadastro !");

            var sameCities = regiaoCidades.GroupBy(x => x.Nome)
                .Select(x => new { total = x.Count() })
                .FirstOrDefault(x => x.total > 1);
            
            if (sameCities != null )
                ErrorHandler(ref validCitiesResponse, "A mesma cidade indicada mais de uma vez !");

            if (validCitiesResponse.success)
                validCitiesResponse.data = existedCities;
        
            return validCitiesResponse; 
        }
        
        public IEnumerable<Cidade> List()
        {
            return _cidadeRepository.List();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _cidadeRepository.ListByUf(uf);
        }

        public IEnumerable<Cidade> Query(string terms)
        {
            return _cidadeRepository.Query(terms);
        }
    }
}
