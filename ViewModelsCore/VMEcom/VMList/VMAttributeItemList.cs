using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.VMEcom.VMList
{
    public class VMAttributeItemList
    {
        public VMAttributeItemList()
        {

        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string AttributeName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public VMAttributeItemList(DataRow objectRow)
        {
            this.Id = objectRow["Id"].ToString();
            this.Name = objectRow["Name"].ToString();
            this.AttributeName = objectRow["AttributeName"].ToString();
            this.CreatedDate = objectRow["CreatedDate"].ToString();
            this.CreatedBy = objectRow["CreatedBy"].ToString();
        }
    }
}
