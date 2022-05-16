using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.VMEcom.VMList
{
    public class VMProductList
    {
        public VMProductList()
        {

        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string FeatureImage { get; set; }
        public string Category { get; set; }
        public string SKU { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public double NewPrice { get; set; }

        public VMProductList(DataRow objectRow)
        {
            this.Id = (objectRow["Id"] != DBNull.Value) ? long.Parse(objectRow["Id"].ToString()) : 0;
            this.Name = objectRow["Name"].ToString();
            this.FeatureImage = objectRow["FeatureImage"].ToString();
            this.Category = objectRow["Category"].ToString();
            this.SKU = objectRow["SKU"].ToString();
            this.CreatedBy = objectRow["CreatedBy"].ToString();
            this.CreatedDate = objectRow["CreatedDate"].ToString();
            this.NewPrice = (objectRow["NewPrice"] != DBNull.Value) ? double.Parse(objectRow["NewPrice"].ToString()) : 0;
        }
    }
}
