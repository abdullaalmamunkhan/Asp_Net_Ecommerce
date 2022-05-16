using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.Common
{
    public class ProcessResponse
    {
        public bool IsInvalidCredential { get; set; }
        public bool IsExist { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public int Id { get; set; }
    }
}
