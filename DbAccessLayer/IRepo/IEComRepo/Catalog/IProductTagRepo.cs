using DbAccessLayer.GenericRepo;
using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;

namespace DbAccessLayer.IRepo.IEComRepo.Catalog
{
    public interface IProductTagRepo : IGenericRepo<DBProductTag>
    {
        public IEnumerable<VMProductTag> GetAllProductTags();
    }
}
