using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.VMEcom.VMList
{
    public class VMSliderImageInfo
    {
        public VMSliderImageInfo()
        {

        }

        public string Id { get; set; }
        public string ImageName { get; set; }
        public string ImageTitle { get; set; }
        public string ImageUrl { get; set; }
        public bool ImageViewer { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public VMSliderImageInfo(DataRow objectRow)
        {
            this.Id = objectRow["Id"].ToString();
            this.ImageName = objectRow["ImageName"].ToString();
            this.ImageTitle = objectRow["ImageTitle"].ToString();
            this.ImageUrl = objectRow["ImageUrl"].ToString();
            this.ImageViewer = (objectRow["ImageViewer"] != null && Convert.ToBoolean(objectRow["ImageViewer"]) == true) ? true : false;
            this.CreatedDate = objectRow["CreatedDate"].ToString();
            this.CreatedBy = objectRow["CreatedBy"].ToString();
        }
    }
}
