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
    public interface ICategoryBLL
    {
        public IEnumerable<VMCategoryList> GetAllCategories(long userId);
        public Task<IEnumerable<DBProductCategory>> GetAll();
        public Task<IEnumerable<VMDropdownModel>> GetCategoryDropdown();
        public Task<ProcessResponse> Insert(DBProductCategory model);
        public Task<DBProductCategory> GetById(int id);
        public Task<ProcessResponse> Update(DBProductCategory model);
        public Task<ProcessResponse> Delete(int id);
        public IEnumerable<VMCheckBoxList> GetCategoryCheckBoxListForAdmin();
        public IEnumerable<DBProductCategory> GetCategoryMenuList();
        public Task<IEnumerable<DBProductCategory>> GetTopTweentyFourCategoryList();
    }
}
