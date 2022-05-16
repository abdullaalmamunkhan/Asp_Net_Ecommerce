using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Product
{
    public class DBProductAttributeMap
    {
        public long ID { get; set; }
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual DBProduct Product { get; set; }
        public long ProductAttributeId { get; set; }
        [ForeignKey("ProductAttributeId")]
        public virtual DBProductAttribute ProductAttribute { get; set; }
    }
}
