using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Admin;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;

namespace BusniessDomain.IBLL.IAdminBLL
{
    public interface IUserBLL
    {
        public VMProcessUser GetUserById(long id);
        public Task<ProcessResponse> Insert(VMProcessUser model);
        public Task<ProcessResponse> Update(VMProcessUser model);
        public IEnumerable<VMUserList> GetAllUserList(int roleId);
        public bool UpdateUserPassword(long userId, string userPassword);
        public Task<ProcessResponse> EditProfile(VMProcessUser model);
    }
}
