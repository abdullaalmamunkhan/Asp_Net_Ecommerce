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
    public interface IAttributeItemsBLL
    {
        public IEnumerable<VMAttributeItemList> GetAllAttributeItemList();
        public Task<IEnumerable<VMDropdownModel>> GetAttributeItemDropdown();
        public Task<IEnumerable<DBProductAttributeItems>> GetAll();
        public Task<ProcessResponse> Insert(DBProductAttributeItems model);
        public Task<DBProductAttributeItems> GetById(long id);
        public Task<ProcessResponse> Update(DBProductAttributeItems model);
        public Task<ProcessResponse> Delete(int id);
        public Task<IEnumerable<VMDropdownModel>> GetAttributeItemsByAttributeId(long attributeId);
    }
}
