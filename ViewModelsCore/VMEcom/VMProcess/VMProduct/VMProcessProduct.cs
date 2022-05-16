using DbModelsCore.Models.Ecommerce.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.VMEcom.VMProcess.VMProduct
{
    public class VMProcessProduct
    {
        public DBProduct Product { get; set; }
        public IList<DBProductAttributeItemMap> ProductAttributeItemMaps { get; set; }
        public string SelectedTags { get; set; }
        public IList<ImageGalleryList> ImageGalleryLists { get; set; }
    }


    public class ImageGalleryList
    {
        public bool IsGallery { get; set; }
        public bool IsFetured { get; set; }
        public string ImageURL { get; set; }
        public string ImageName { get; set; }
    }
}
