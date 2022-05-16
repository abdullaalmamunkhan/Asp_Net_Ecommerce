using CommonProperties.Encription;
using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.IAdminRepo;
using DbAccessLayer.SPMecanism;
using DbModelsCore.DB;
using DbModelsCore.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;

namespace DbAccessLayer.Repo.AdminRepo
{
    public class UserRepo: GenericRepo<DBUser>, IUserRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public UserRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }

        public IEnumerable<VMUserList> GetAllUserList(int roleId)
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_UserListByRoleId]");
                _storedProcedure.AddInputParameter("roleId", roleId, SqlDbType.Int);

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMUserList> listOfUsers = new List<VMUserList>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfUsers.Add(new VMUserList(dr));
                }

                return listOfUsers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UpdateUserPassword(long userId,string userPassword)
        {
            try
            {
                userPassword=(!string.IsNullOrEmpty(userPassword)) ? SimpleCryptService.Factory().Encrypt(userPassword) : userPassword;
                _storedProcedure.ProcedureName = ("[dbo].[Get_UserListByRoleId]");
                _storedProcedure.AddInputParameter("userId", userId, SqlDbType.BigInt);
                _storedProcedure.AddInputParameter("password", userPassword, SqlDbType.NVarChar);

                _storedProcedure.ExecuteNonQuery();
               
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
