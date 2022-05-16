using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.IEComRepo.Catalog;
using DbAccessLayer.SPMecanism;
using DbModelsCore.DB;
using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;

namespace DbAccessLayer.Repo.EComRepo.Catalog
{
    public class CategoryRepo : GenericRepo<DBProductCategory>, ICategoryRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public CategoryRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }

        public IEnumerable<VMCategoryList> GetAllCategories(long userId)
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_CategoryList]");
                _storedProcedure.AddInputParameter("userId", userId, SqlDbType.BigInt);

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMCategoryList> listOfData = new List<VMCategoryList>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfData.Add(new VMCategoryList(dr));
                }

                return listOfData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
