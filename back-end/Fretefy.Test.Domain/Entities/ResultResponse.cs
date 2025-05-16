using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities
{
    public class ResultResponse<T> 
    {
        public string message { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
        public IEnumerable<T> data { get; set; }
        
    }
}