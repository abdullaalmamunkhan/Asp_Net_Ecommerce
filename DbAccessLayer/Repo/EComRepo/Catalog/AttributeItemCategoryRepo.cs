using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.IEComRepo.Catalog;
using DbAccessLayer.SPMecanism;
using DbModelsCore.DB;
using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.Repo.EComRepo.Catalog
{
    public class AttributeItemCategoryRepo:GenericRepo<DBAttributeItemCategoryMap>, IAttributeItemCategoryRepo
    {
        private readonly ApplicationDbContext _context;
        protected readonly IStoredProcedure _storedProcedure;
        public AttributeItemCategoryRepo(ApplicationDbContext context, IStoredProcedure storedProcedure) : base(context)
        {
            _context = context;
            _storedProcedure = storedProcedure;
        }
    }
}
