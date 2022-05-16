using DbModelsCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;

namespace ViewModelsCore.Admin
{
    public class VMLoginResponse: ProcessResponse
    {
        public DBUser User { get; set; }
        public string Role { get; set; }

    }
}
