using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Admin;
using ViewModelsCore.Common;

namespace BusniessDomain.IBLL.IAdminBLL
{
    public interface IAccountsBLL
    {
        public Task<VMLoginResponse> Register(VMRegistrationProcess model);
        public Task<VMLoginResponse> Login(VMLoginProcess model);
    }
}
