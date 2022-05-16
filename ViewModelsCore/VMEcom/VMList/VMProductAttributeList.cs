using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.VMEcom.VMList
{
    public class VMProductAttributeList
    {
        public VMProductAttributeList()
        {

        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public VMProductAttributeList(DataRow objectRow)
        {
            this.Id = objectRow["Id"].ToString();
            this.Name = objectRow["Name"].ToString();
            this.CreatedDate = objectRow["CreatedDate"].ToString();
            this.CreatedBy = objectRow["CreatedBy"].ToString();
        }
    }
}
