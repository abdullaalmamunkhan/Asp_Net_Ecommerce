using DbModelsCore.Models.Ecommerce.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelsCore.Models.Ecommerce.Product
{
    public class DBProduct:BaseClassInfo
    {
        
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string FeatureImage { get; set; }
        public string SKU { get; set; }
        public string GTIN { get; set; }
        public string AdminComment { get; set; }
        public bool IsShowOnHome { get; set; }
        public bool IsOpenReview { get; set; }
        public bool IsDraft { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public double DiscountInPercent { get; set; }
        public double DiscountAmount { get; set; }
        public bool IsEnableShop { get; set; }
        public bool IsTrackStoke { get; set; }
        public double StokeAmount { get; set; }
        public double MinimumStokeLimit { get; set; }
        public bool IsMultipleWareHouse { get; set; }
        public bool IsDisplayAvaiable { get; set; }
        public bool IsReturnAble { get; set; }
        public bool IsNew { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual DBProductCategory Category { get; set; }
        //public virtual ICollection<DBProductCategoryMap> ProductCategoryMaps { get; set; }
        public virtual ICollection<DBProductAttributeItemMap> ProductAttributeItemMaps { get; set; }
        //public virtual ICollection<DBProductAttributeMap> ProductAttributeMaps { get; set; }
        public virtual ICollection<DBProductTagMap> ProductTagMaps { get; set; }
        public virtual ICollection<DBProdutImageMap> DBProdutImageMaps { get; set; }
        public virtual ICollection<DBProductFeedback> DBProductFeedbacks { get; set; }
    }
}
