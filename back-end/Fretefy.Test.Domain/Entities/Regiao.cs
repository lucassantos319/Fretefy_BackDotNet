using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Fretefy.Test.Domain.Enums;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao : IEntity
    {
        public Regiao()
        {
        }
        
        public Regiao(string nome)
        {
            Id = new Guid();
            Nome = nome; 
        }

        public Regiao(string nome, IEnumerable<RegiaoCidade> regiaoCidades)
        {
            Id = new Guid();
            Nome = nome;
            regiaoCidades = regiaoCidades;
        }
        
        public Guid Id { get; set; }
        public EStatus Status { get; set; }

        [Required]
        public string Nome { get; set; }
        
        [JsonIgnore]
        public virtual IEnumerable<RegiaoCidade> RegiaoCidades { get; set; }
        
        public IEnumerable<Cidade> Cidades { get; set; } 
       
        
    }
}