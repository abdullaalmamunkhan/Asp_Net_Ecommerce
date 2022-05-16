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
    public class ProductTagRepo : GenericRepo<DBProductTag>, IProductTagRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public ProductTagRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }

        public IEnumerable<VMProductTag> GetAllProductTags()
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_TagList]");

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMProductTag> listOfData = new List<VMProductTag>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfData.Add(new VMProductTag(dr));
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
