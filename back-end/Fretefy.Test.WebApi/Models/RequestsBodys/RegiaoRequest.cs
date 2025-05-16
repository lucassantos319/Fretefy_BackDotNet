using System.Collections.Generic;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.WebApi.Models.RequestsBodys
{
    public class RegiaoRequest
    {
        public Regiao regiao { get; set; }
        public IEnumerable<Cidade> cidades { get; set; }
    }
}