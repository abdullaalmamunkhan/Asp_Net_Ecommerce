using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.VMEcom.VMList
{
    public class VMProductFilterList
    {
        public VMProductFilterList()
        {

        }
        public long CatID { get; set; }
        public long ProductID { get; set; }
        public string ProductNAME { get; set; }
        public string CatName { get; set; }
        public string FeatureImage { get; set; }
        public double NewPrice { get; set; }
        public double? OldPrice { get; set; }
        public double? DiscountAmount { get; set; }
        public double? DiscountInPercent { get; set; }

        public VMProductFilterList(DataRow objectRow)
        {
            this.CatID = (objectRow["C_ID"] != DBNull.Value) ? long.Parse(objectRow["C_ID"].ToString()) : 0;
            this.ProductID = (objectRow["P_ID"] != DBNull.Value) ? long.Parse(objectRow["P_ID"].ToString()) : 0;
            this.ProductNAME = objectRow["P_NAME"].ToString();
            this.CatName = objectRow["Cat_Name"].ToString();
            this.FeatureImage = objectRow["FeatureImage"].ToString();
            this.NewPrice = (objectRow["NewPrice"] != DBNull.Value) ? double.Parse(objectRow["NewPrice"].ToString()) : 0;
            this.OldPrice = (objectRow["OldPrice"] != DBNull.Value) ? double.Parse(objectRow["OldPrice"].ToString()) : 0;
            this.DiscountAmount = (objectRow["DiscountAmount"] != DBNull.Value) ? double.Parse(objectRow["DiscountAmount"].ToString()) : 0;
            this.DiscountInPercent = (objectRow["DiscountInPercent"] != DBNull.Value) ? double.Parse(objectRow["DiscountInPercent"].ToString()) : 0;
        }
    }

    public class ProductFilterParam
    {
        public long catId { get; set; }
        public string proId { get; set; }
        public string attId { get; set; }
        public string itemId { get; set; }
        public string searchstring { get; set; }
        public string minValue { get; set; }
        public string maxValue { get; set; }

    }

}
