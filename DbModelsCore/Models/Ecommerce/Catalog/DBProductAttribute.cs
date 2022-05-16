using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Catalog
{
    public class DBProductAttribute:BaseClassInfo
    {
        [StringLength(200)]
        public string Name { get; set; }
        public virtual ICollection<DBCategoryAttributeMaper> CategoryAttributeMapers { get; set; }
        public virtual ICollection<DBProductAttributeItems> AttributeItems { get; set; }
    }
}
