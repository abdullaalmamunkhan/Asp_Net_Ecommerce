using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.ITestRepo;
using DbAccessLayer.SPMecanism;
using DbModelsCore.DB;
using DbModelsCore.Models.TestModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Test;

namespace DbAccessLayer.Repo.TestRepo
{
    public class TestTableModelRepo : GenericRepo<DBTestTableModel>, ITestTableModelRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public TestTableModelRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }


        public List<VMTestTableList> GetAllFromSP(int id)
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_All_Test_Table_Data]");
                _storedProcedure.AddInputParameter("id", id, SqlDbType.Int);

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMTestTableList> listOfTestData = new List<VMTestTableList>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfTestData.Add(new VMTestTableList(dr));
                }

                return listOfTestData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
