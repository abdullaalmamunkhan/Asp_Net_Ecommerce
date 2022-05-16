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
    class DBSliderImageRepo : GenericRepo<DBSliderImage>, IDBSliderImageRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public DBSliderImageRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }
        public IEnumerable<VMSliderImageInfo> GetSliderImageInfo()
        {
            try
            {
                _storedProcedure.ProcedureName = ("[dbo].[Get_SliderImage_List]");

                DataTable dt = _storedProcedure.ExecuteQueryToDataTable();
                List<VMSliderImageInfo> listOfData = new List<VMSliderImageInfo>();
                foreach (DataRow dr in dt.Rows)
                {
                    listOfData.Add(new VMSliderImageInfo(dr));
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
