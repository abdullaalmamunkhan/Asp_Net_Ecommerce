using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.IEComRepo.Product;
using DbAccessLayer.SPMecanism;
using DbModelsCore.DB;
using DbModelsCore.Models.Ecommerce.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.Repo.EComRepo.Product
{
    public class ProductTagMapRepo : GenericRepo<DBProductTagMap>, IProductTagMapRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public ProductTagMapRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }
    }
}
