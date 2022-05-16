using DbAccessLayer.GenericRepo;
using DbAccessLayer.IRepo.IEComRepo.Catalog;
using DbModelsCore.DB;
using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.Repo.EComRepo.Catalog
{
    public class DBCategoryAttributeMaperRepo : GenericRepo<DBCategoryAttributeMaper>, IDBCategoryAttributeMaperRepo
    {
        private readonly ApplicationDbContext _context;
        public DBCategoryAttributeMaperRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
