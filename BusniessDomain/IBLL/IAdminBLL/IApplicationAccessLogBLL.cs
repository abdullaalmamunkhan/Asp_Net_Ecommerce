using DbModelsCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;

namespace BusniessDomain.IBLL.IAdminBLL
{
    public interface IApplicationAccessLogBLL
    {
        public Task<ProcessResponse> Insert(DBApplicationAccessLog model);
    }
}
