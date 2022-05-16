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
   public class AttributeItemsRepo:GenericRepo<DBProductAttributeItems>, IAttributeItemsRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public AttributeItemsRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }

        public IEnumerable<VMAttributeItemList> GetAllAttributeItemList()
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_AttributeItemList]");

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMAttributeItemList> listOfData = new List<VMAttributeItemList>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfData.Add(new VMAttributeItemList(dr));
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
