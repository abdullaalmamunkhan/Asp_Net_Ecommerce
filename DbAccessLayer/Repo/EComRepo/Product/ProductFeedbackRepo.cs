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
    public class ProductFeedbackRepo:GenericRepo<DBProductFeedback>, IProductFeedbackRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public ProductFeedbackRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }
    }
}
