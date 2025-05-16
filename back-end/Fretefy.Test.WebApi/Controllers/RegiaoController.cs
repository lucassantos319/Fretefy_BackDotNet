using System;
using System.Collections.Generic;
using System.Text.Json;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.WebApi.Models.RequestsBodys;
using Microsoft.AspNetCore.Mvc;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _service;
        
        public RegiaoController(IRegiaoService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IActionResult List([FromQuery] string terms)
        {
            IEnumerable<Regiao> regioes;

            if (!string.IsNullOrEmpty(terms))
                regioes = _service.Query(terms);
            else
                regioes = _service.List();

            return Ok(regioes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var cidades = _service.Get(id);
            return Ok(cidades);
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegiaoRequest requestBody)
        {
            try
            {
                var region = requestBody.regiao;
                var cities = requestBody.cidades;
                
                var createdRegion = _service.Create(region,cities);
                if (createdRegion.success)
                    return Ok(createdRegion.data);
                else
                    return Problem(detail:createdRegion.error);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] RegiaoRequest requestBody)
        {
            try
            {
                var region = requestBody.regiao;
                var cities = requestBody.cidades;
                
                var updatedRegion = _service.Update(region,cities);
                if (updatedRegion.success)
                    return Ok();
                else
                    return Problem(detail:updatedRegion.error);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
    }
}

