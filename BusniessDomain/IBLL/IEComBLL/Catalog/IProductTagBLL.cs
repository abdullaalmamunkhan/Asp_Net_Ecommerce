using DbAccessLayer.GenericRepo;
using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;

namespace BusniessDomain.IBLL.IEComBLL.Catalog
{
    public interface IProductTagBLL
    {
        public IEnumerable<VMProductTag> GetAllProductTags();
        public Task<IEnumerable<VMDropdownModel>> GetTagDropdown();
        public Task<IEnumerable<DBProductTag>> GetAll();
        public Task<ProcessResponse> Insert(DBProductTag model);
        public Task<DBProductTag> GetById(int id);
        public Task<ProcessResponse> Update(DBProductTag model);
        public Task<ProcessResponse> Delete(int id);
    }
}
