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
    public class ProdutImageMapRepo : GenericRepo<DBProdutImageMap>, IProdutImageMapRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public ProdutImageMapRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }
    }
}
