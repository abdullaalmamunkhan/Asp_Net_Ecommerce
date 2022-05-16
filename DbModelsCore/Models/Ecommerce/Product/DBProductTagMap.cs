using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Product
{
    public class DBProductTagMap
    {
        public long ID { get; set; }
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual DBProduct Product { get; set; }
        public long TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual DBProductTag ProductTag { get; set; }
    }
}
