using System;
using Fretefy.Test.Domain.Enums;

namespace Fretefy.Test.Domain.Entities
{
    public class RegiaoCidade : IEntity
    {
        public RegiaoCidade(){}
        
        public Guid Id { get; set; }
        public EStatus Status { get; set; }
        public Guid CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
        public Guid RegiaoId { get; set; }
        public virtual Regiao Regiao { get; set; }
    }
}