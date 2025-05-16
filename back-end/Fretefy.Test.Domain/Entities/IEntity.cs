using System;
using Fretefy.Test.Domain.Enums;

namespace Fretefy.Test.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        EStatus Status { get; set; }
        
    }
}
