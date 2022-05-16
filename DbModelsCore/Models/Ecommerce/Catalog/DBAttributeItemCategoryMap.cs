using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Catalog
{
    public class DBAttributeItemCategoryMap
    {
        public long ID { get; set; }
        public long ItemAttributeId { get; set; }
        [ForeignKey("ItemAttributeId")]
        public virtual DBProductAttributeItems AttributeItem { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual DBProductCategory Category { get; set; }
    }
}
