using DbModelsCore.Models.Ecommerce.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Catalog
{
    public class DBProductCategory:BaseClassInfo
    {
        [StringLength(200)]
        public string Name { get; set; }
        public long? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual DBProductCategory Category { get; set; }
        public string Description { get; set; }
        public bool IsShowOnHome { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public virtual IFormFile CategoryImage { get; set; }
        public virtual ICollection<DBProduct> Products { get; set; }
        public virtual ICollection<DBProductCategory> Categories { get; set; }
    }
}
