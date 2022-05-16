using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.Common
{
    public class APIResponseMessage
    {
        public object data { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public bool isSuccess { get; set; }
    }
}
