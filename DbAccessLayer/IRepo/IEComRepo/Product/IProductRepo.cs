using DbAccessLayer.GenericRepo;
using DbModelsCore.Models.Ecommerce.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.VMEcom.VMList;

namespace DbAccessLayer.IRepo.IEComRepo.Product
{
    public interface IProductRepo : IGenericRepo<DBProduct>
    {
        public IEnumerable<VMProductList> GetAllProductList(long userId);
        public IEnumerable<VMProductFilterList> GetProductFilterLists(ProductFilterParam param);
        public IEnumerable<VMProductFilterList> GetHomePageProducts();
    }
}
