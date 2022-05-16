using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.VMEcom.VMList
{
    public class VMCategoryList
    {
        public VMCategoryList()
        {

        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public VMCategoryList(DataRow objectRow)
        {
            this.Id = (objectRow["Id"] != DBNull.Value) ? long.Parse(objectRow["Id"].ToString()) : 0;
            this.Name = objectRow["Name"].ToString();
            this.FullName = objectRow["FullName"].ToString();
            this.CreatedDate = objectRow["CreatedDate"].ToString();
            this.CreatedBy = objectRow["CreatedBy"].ToString();
        }
    }
}
