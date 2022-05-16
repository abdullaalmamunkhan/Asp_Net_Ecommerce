using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Catalog
{
    public class DBProductTag:BaseClassInfo
    {
        [StringLength(200)]
        public string Name { get; set; }
    }
}
