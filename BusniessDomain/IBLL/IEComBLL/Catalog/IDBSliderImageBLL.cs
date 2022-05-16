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
    public interface IDBSliderImageBLL
    {
        public Task<DBSliderImage> GetById(int id);
        public IEnumerable<VMSliderImageInfo> GetSliderImageInfo();
        public Task<ProcessResponse> Insert(DBSliderImage model);
        public Task<ProcessResponse> Update(DBSliderImage model);
        public Task<IEnumerable<DBSliderImage>> GetAllActiveSliders();
    }
}
