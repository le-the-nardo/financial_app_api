using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financial_app_api.Shared.Models
{
    public class ListResponse<T>
    {
        public List<T> list { get; set; }
        public bool error { get; set; }
        public string info { get; set; }
    }
}

