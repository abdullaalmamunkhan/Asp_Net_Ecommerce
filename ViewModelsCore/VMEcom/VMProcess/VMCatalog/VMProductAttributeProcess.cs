using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsCore.Common;

namespace ViewModelsCore.VMEcom.VMProcess.VMCatalog
{
    public class VMProductAttributeProcess
    {
        public List<VMCheckBoxList> SelectedCategories { get; set; }
        public DBProductAttribute ProductAttribute { get; set; }
    }
}
