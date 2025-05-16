using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Enums;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;

namespace Fretefy.Test.Domain.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _repository;
        private readonly IRegiaoCidadeService _regiaoCidadeService;
        private readonly ICidadeService _cidadeService;
        
        public RegiaoService(IRegiaoRepository repository, ICidadeService cidadeService,IRegiaoCidadeService regiaoCidadeService)
        {
            _repository = repository;
            _cidadeService = cidadeService;
            _regiaoCidadeService = regiaoCidadeService;
        }
        
        public Regiao Get(Guid id)
        {
            return _repository.List().FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Regiao> List()
        { 
            var regions = _repository.List();
            foreach (var region in regions)
            {
                var citiesList = new List<Cidade>(); 
                citiesList.AddRange(region.RegiaoCidades.Select(x=>x.Cidade));

                region.Cidades = citiesList;
            }
            
            return regions;
        }

        private void ErrorHandler(ref ResultResponse<Regiao> result,string msg)
        {
            result.success = false;
            result.error = result.message = msg;
        }
        
        private ResultResponse<Regiao> ValidateRegion(Regiao regiao)
        {
            var validRegionResponse = new ResultResponse<Regiao>()
            {
                message = "Criado região com sucesso",
                success = true,
            };

            var existedRegions = List().FirstOrDefault(f => f.Nome == regiao.Nome);
            if (existedRegions != null)
                ErrorHandler(ref validRegionResponse,"Região já cadastrada !");

            return validRegionResponse;
        }

        public ResultResponse<Regiao> Create(Regiao regiao,IEnumerable<Cidade> cidades)
        {
            var validRegiao = ValidateRegion(regiao);
            if (!validRegiao.success)
                return validRegiao;

            var validateCities = _cidadeService.ValidateCities(cidades);
            if (!validateCities.success)
            {
               ErrorHandler(ref validRegiao,validateCities.error);
               return validRegiao;
            }

            regiao.Status = EStatus.Ativo;
            var createdRegiao = _repository.Create(regiao);
            if (!createdRegiao)
                ErrorHandler(ref validRegiao,"Não foi possivel criar região !");

            SetRelationsRegister(regiao,validateCities);
            if ( validRegiao.success)
                validRegiao.data = new []{ regiao };
            
            return validRegiao;
        }

        private void SetRelationsRegister(Regiao createdRegiao, ResultResponse<Cidade> validateCities)
        {
            var listCities = new List<Cidade>();
            _regiaoCidadeService.Create(createdRegiao, validateCities.data);
            foreach (var regionCity in createdRegiao.RegiaoCidades)
            {
                var city = validateCities.data.FirstOrDefault(f => f.Nome.Equals(regionCity.Cidade.Nome));
                if ( city != null)
                    listCities.Add(city);
            }
            
            createdRegiao.Cidades = listCities.ToArray();
        }
        
        private ResultResponse<bool> UpdateSetRelationsRegister(Regiao regiao, IEnumerable<Cidade> cidades)
        {
            var updateResult = new ResultResponse<bool>()
            {
                success = true,
                message = "Atualizado registros com sucesso !"
            };
            
            if (regiao == null && regiao.Id == null)
            {
                updateResult.success = false;
                updateResult.message = "Região não foi informada corretamente, precisa ser informado o Id";
                return updateResult;
            }

            var cities = _cidadeService.List();
            var updatedRegistes = _regiaoCidadeService.GetByRegionId(regiao.Id);
            foreach (var regionCity in updatedRegistes)
            {
                var existRelation = cidades.Any(x=>x.Id == regionCity.CidadeId || x.Nome.Equals(regionCity.Cidade.Nome));
                if (existRelation)
                {
                    var updateRegister = cidades.FirstOrDefault(x => x.Id == regionCity.CidadeId || x.Nome.Equals(regionCity.Cidade.Nome));
                    if (updateRegister != null && updateRegister.Status != regionCity.Status)
                    {
                       var result =  _regiaoCidadeService.Update(regionCity);
                       if (!result.success)
                       {
                           updateResult.error = $"Erro ao realizar a atualização relação com a cidade {updateRegister.Nome}";
                           updateResult.success = false;
                       }
                    }
                }
                else
                    _regiaoCidadeService.Create(regiao,cidades.Where(x=>x.Id == regionCity.CidadeId));
            }

            return updateResult;
        }
        
        public ResultResponse<Regiao> Update(Regiao regiao, IEnumerable<Cidade> cidades)
        {
            var validRegiao = new ResultResponse<Regiao>()
            {
                success = true
            };

            var existedRegiao = _repository.List().FirstOrDefault(x => x.Id == regiao.Id || x.Nome.Equals(regiao.Nome));
            if (existedRegiao == null)
            {
                validRegiao.error = "Região não existente !"; 
                validRegiao.success = false;
                return validRegiao;
            }

            regiao = existedRegiao;
            var updateRegion = _repository.Update(regiao);
            if (!updateRegion)
                ErrorHandler(ref validRegiao,"Não foi possivel realizar a atualização da região !");
            
            var updateRelations = UpdateSetRelationsRegister(regiao, cidades);
            
            if ( updateRelations.success)
                updateRelations.data = new []{ true };
            else
                validRegiao.error = updateRelations.error;
            
            return validRegiao;
        }

        public IEnumerable<Regiao> Query(string terms)
        {
            return _repository.Query(terms);
        }
        
    }
}