using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Product
{
   public  class DBProductFeedback : BaseClassInfo
    {
        public int UserID { get; set; }
        public double Rating { get; set; }
        public string Message { get; set; }
        public int ReplyID { get; set; }
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual DBProduct Product { get; set; }
    }
}
