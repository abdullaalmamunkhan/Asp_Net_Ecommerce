using DbAccessLayer.GenericRepo;
using DbModelsCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;

namespace DbAccessLayer.IRepo.IAdminRepo
{
    public interface IUserRepo: IGenericRepo<DBUser>
    {
        public IEnumerable<VMUserList> GetAllUserList(int roleId);
        public bool UpdateUserPassword(long userId, string userPassword);
    }
}
