using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Catalog
{
    public class DBProductAttributeItems:BaseClassInfo
    {
        [StringLength(200)]
        public string Name { get; set; }
        public long ProductAttributeId { get; set; }
        [ForeignKey("ProductAttributeId")]
        public virtual DBProductAttribute ProductAttribute { get; set; }
        public virtual ICollection<DBAttributeItemCategoryMap> AttributeItemCategories { get; set; }
        [NotMapped]
        public string Categories { get; set; }

    }
}
