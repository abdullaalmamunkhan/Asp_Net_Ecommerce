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
    public class DBSliderImage : BaseClassInfo
    {
        [StringLength(200)]
        [Required]
        public string ImageName { get; set; }
        [StringLength(200)]
        [Required]
        public string ImageTitle { get; set; }
        [StringLength(500)]
        [Required]
        public string ImageUrl { get; set; }
        [StringLength(100)]
        public string ColorCode { get; set; }
        [NotMapped]
        public virtual IFormFile UploadImage { get; set; }
        public bool ImageViewer { get; set; }
    }
}
