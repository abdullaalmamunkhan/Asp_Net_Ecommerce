using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;
using ViewModelsCore.VMEcom.VMProcess.VMProduct;

namespace BusniessDomain.IBLL.IEComBLL.Product
{
    public interface IProdcutBLL
    {
        public  Task<ProcessResponse> Insert(VMProcessProduct model);
        public IEnumerable<VMProductList> GetAllProductList(long userId);
        public VMProcessProduct GetProductById(long id);
        public Task<ProcessResponse> Update(VMProcessProduct model);
        public IEnumerable<VMProductFilterList> GetProductFilterLists(ProductFilterParam param);
        public IEnumerable<VMProductFilterList> GetHomePageProducts();
    }
}
