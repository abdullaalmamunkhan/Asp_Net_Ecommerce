using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.IEComRepo.Product;
using DbAccessLayer.SPMecanism;
using DbModelsCore.DB;
using DbModelsCore.Models.Ecommerce.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;

namespace DbAccessLayer.Repo.EComRepo.Product
{
    public class ProductRepo : GenericRepo<DBProduct>, IProductRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public ProductRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }

        public IEnumerable<VMProductFilterList> GetHomePageProducts()
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_Home_Page_Products]");

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMProductFilterList> listOfProducts = new List<VMProductFilterList>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfProducts.Add(new VMProductFilterList(dr));
                }

                return listOfProducts;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<VMProductFilterList> GetProductFilterLists(ProductFilterParam param)
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_Product_By_SearchParam]");
                _storedProcedure.AddInputParameter("catId", param.catId, SqlDbType.BigInt);
                _storedProcedure.AddInputParameter("proId", param.proId, SqlDbType.NVarChar);
                _storedProcedure.AddInputParameter("attId", param.attId, SqlDbType.NVarChar);
                _storedProcedure.AddInputParameter("itemId", param.itemId, SqlDbType.NVarChar);
                _storedProcedure.AddInputParameter("min", param.minValue, SqlDbType.NVarChar);
                _storedProcedure.AddInputParameter("max", param.maxValue, SqlDbType.NVarChar);
                _storedProcedure.AddInputParameter("searchstring", param.searchstring, SqlDbType.NVarChar);

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMProductFilterList> listOfProducts = new List<VMProductFilterList>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfProducts.Add(new VMProductFilterList(dr));
                }

                return listOfProducts;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IEnumerable<VMProductList> GetAllProductList(long userId)
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_ProductListByUserId]");
                _storedProcedure.AddInputParameter("userId", userId, SqlDbType.BigInt);

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMProductList> listOfProducts = new List<VMProductList>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfProducts.Add(new VMProductList(dr));
                }

                return listOfProducts;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
