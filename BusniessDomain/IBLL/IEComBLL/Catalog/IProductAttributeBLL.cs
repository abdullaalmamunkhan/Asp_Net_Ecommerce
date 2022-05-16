using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;
using ViewModelsCore.VMEcom.VMList;
using ViewModelsCore.VMEcom.VMProcess.VMCatalog;

namespace BusniessDomain.IBLL.IEComBLL.Catalog
{
    public interface IProductAttributeBLL
    {
        public IEnumerable<VMProductAttributeList> GetProductAttributeList();
        public Task<IEnumerable<VMDropdownModel>> GetProductAttributeDropdown();
        public Task<IEnumerable<DBProductAttribute>> GetAll();
        public Task<ProcessResponse> Insert(VMProductAttributeProcess model);
        public Task<VMProductAttributeProcess> GetById(long id);
        public Task<ProcessResponse> Update(VMProductAttributeProcess model);
        public Task<ProcessResponse> Delete(int id);
        public Task<IEnumerable<VMDropdownModel>> GetProdcutAttributeCategoriesByAttributeId(long attributeId);
        public IEnumerable<VMDropdownModel> GetAllAttributeListByCategory(int categoryId);
        public Task<IEnumerable<DBProductAttribute>> GetProductAttributesList(long categoryId);
    }
}
