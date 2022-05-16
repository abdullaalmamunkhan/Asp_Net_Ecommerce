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
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;

namespace DbAccessLayer.Repo.EComRepo.Catalog
{
    public class ProductAttributeRepo:GenericRepo<DBProductAttribute>, IProductAttributeRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public ProductAttributeRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }

        public IEnumerable<VMDropdownModel> GetProductAttributeListsByCategory(long catId)
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_Product_Attributes_By_Category]");
                _storedProcedure.AddInputParameter("catId", catId, SqlDbType.BigInt);

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMDropdownModel> listOfData = new List<VMDropdownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfData.Add(new VMDropdownModel(dr));
                }

                return listOfData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<VMProductAttributeList> GetProductAttributeList()
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_ProductAttributes]");

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMProductAttributeList> listOfData = new List<VMProductAttributeList>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfData.Add(new VMProductAttributeList(dr));
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
