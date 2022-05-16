using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Product
{
    public class DBProductAttributeItemMap
    {
        public long ID { get; set; }
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual DBProduct Product { get; set; }
        public long AttributeId { get; set; }
        //public virtual DBProductAttribute ProductAttribute { get; set; }
        public long ItemAttributeId { get; set; }
        [ForeignKey("ItemAttributeId")]
        public virtual DBProductAttributeItems AttributeItem { get; set; }
    }
}
