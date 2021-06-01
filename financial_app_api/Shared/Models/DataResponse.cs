using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financial_app_api.Shared.Models
{
    public class DataResponse
    {
        public string payload_data { get; set; }
        public bool error { get; set; }
        public string info { get; set; }
    }
    public class DataResponse<T>
    {
        public static DataResponse<T> Error(string info)
        {
            var dr = new DataResponse<T>();
            dr.error = true;
            dr.info = info;
            return dr;
        }
        public DataResponse() { }
        public DataResponse(T data) { payload_data = data; }
        public T payload_data { get; set; }
        public bool error { get; set; }
        public string info { get; set; }
    }
}
