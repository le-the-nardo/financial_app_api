using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financial_app_api.Shared.Models
{
    public class CommandResult<T>
    {
        public bool error { get; set; }
        public string message { get; set; }
        public T result { get; set; }
    }
}
