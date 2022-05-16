using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Catalog
{
    public class DBCategoryAttributeMaper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual DBProductCategory Category { get; set; }
        public long AttributeId { get; set; }
        [ForeignKey("AttributeId")]
        public virtual DBProductAttribute Attribute { get; set; }
    }
}
