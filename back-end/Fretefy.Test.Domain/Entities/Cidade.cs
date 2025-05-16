using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Fretefy.Test.Domain.Enums;

namespace Fretefy.Test.Domain.Entities
{
    public class Cidade : IEntity
    {
        public Cidade()
        {
        
        }

        public Cidade(string nome, string uf)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            UF = uf;
            Status = EStatus.Ativo;
        }

        public Guid Id { get; set; }
        public EStatus Status { get; set; }

        public string Nome { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<RegiaoCidade> RegiaoCidades { get; set; }
        [JsonIgnore]
        public virtual Regiao Regiao { get; set; }
        
        public string UF { get; set; }
    }
}
